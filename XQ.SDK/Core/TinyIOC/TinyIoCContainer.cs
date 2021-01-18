#define EXPRESSIONS

// Platform supports System.Linq.Expressions
#define COMPILED_EXPRESSIONS // Platform supports compiling expressions
#define APPDOMAIN_GETASSEMBLIES // Platform supports getting all assemblies from the AppDomain object
#define UNBOUND_GENERICS_GETCONSTRUCTORS // Platform supports GetConstructors on unbound generic types
#define GETPARAMETERS_OPEN_GENERICS // Platform supports GetParameters on open generics
#define RESOLVE_OPEN_GENERICS // Platform supports resolving open generics
#define READER_WRITER_LOCK_SLIM // Platform supports ReaderWriterLockSlim
#define SERIALIZABLE // Platform supports SerializableAttribute/SerializationInfo/StreamingContext

#if PORTABLE
#undef APPDOMAIN_GETASSEMBLIES
#undef COMPILED_EXPRESSIONS
#undef READER_WRITER_LOCK_SLIM
#undef SERIALIZABLE
#endif

#if NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2
#undef COMPILED_EXPRESSIONS
#undef READER_WRITER_LOCK_SLIM
#endif

#if NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2
#undef APPDOMAIN_GETASSEMBLIES
#endif

#if NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6
#undef SERIALIZABLE
#endif

#if PocketPC || WINDOWS_PHONE
#undef EXPRESSIONS
#undef COMPILED_EXPRESSIONS
#undef APPDOMAIN_GETASSEMBLIES
#undef UNBOUND_GENERICS_GETCONSTRUCTORS
#endif

#if PocketPC
#undef GETPARAMETERS_OPEN_GENERICS
#undef RESOLVE_OPEN_GENERICS
#undef READER_WRITER_LOCK_SLIM
#endif

#if SILVERLIGHT
#undef APPDOMAIN_GETASSEMBLIES
#endif

#if NETFX_CORE
#undef APPDOMAIN_GETASSEMBLIES
#undef RESOLVE_OPEN_GENERICS
#endif

#if COMPILED_EXPRESSIONS
#define USE_OBJECT_CONSTRUCTOR
#endif




using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace XQ.SDK.Core
{
    public sealed class TinyIoCContainer : IDisposable
    {
        /// <summary>
        ///     Registration options for "fluent" API
        /// </summary>
        public sealed class RegisterOptions
        {
        }

        #region Public API

        #region Registration

        /// <summary>
        ///     Creates/replaces a container class registration with a given implementation and default options.
        /// </summary>
        /// <param name="registerType">Type to register</param>
        /// <param name="registerImplementation">Type to instantiate that implements RegisterType</param>
        /// <returns>RegisterOptions for fluent API</returns>
        public RegisterOptions Register(Type registerType, Type registerImplementation)
        {
            return RegisterInternal(registerType, string.Empty,
                GetDefaultObjectFactory(registerType, registerImplementation));
        }


        /// <summary>
        ///     Creates/replaces a container class registration with a specific, strong referenced, instance.
        /// </summary>
        /// <param name="registerType">Type to register</param>
        /// <param name="instance">Instance of RegisterType to register</param>
        /// <returns>RegisterOptions for fluent API</returns>
        public RegisterOptions Register(Type registerType, object instance)
        {
            return RegisterInternal(registerType, string.Empty,
                new InstanceFactory(registerType, registerType, instance));
        }


        /// <summary>
        ///     Creates/replaces a container class registration with a given implementation and default options.
        /// </summary>
        /// <typeparam name="TRegisterType">Type to register</typeparam>
        /// <typeparam name="TRegisterImplementation">Type to instantiate that implements RegisterType</typeparam>
        /// <returns>RegisterOptions for fluent API</returns>
        public RegisterOptions Register<TRegisterType, TRegisterImplementation>()
            where TRegisterType : class
            where TRegisterImplementation : class, TRegisterType
        {
            return Register(typeof(TRegisterType), typeof(TRegisterImplementation));
        }

        /// <summary>
        ///     Creates/replaces a container class registration with a specific, strong referenced, instance.
        /// </summary>
        /// <typeparam name="TRegisterType">Type to register</typeparam>
        /// <param name="instance">Instance of RegisterType to register</param>
        /// <returns>RegisterOptions for fluent API</returns>
        public RegisterOptions Register<TRegisterType>(TRegisterType instance)
            where TRegisterType : class
        {
            return Register(typeof(TRegisterType), instance);
        }

        #endregion

        #region Resolution

        /// <summary>
        ///     Attempts to resolve a type using default options.
        /// </summary>
        /// <param name="resolveType">Type to resolve</param>
        /// <returns>Instance of type</returns>
        /// <exception cref="TinyIoCResolutionException">Unable to resolve the type.</exception>
        public object Resolve(Type resolveType)
        {
            return ResolveInternal(new TypeRegistration(resolveType), NamedParameterOverloads.Default,
                ResolveOptions.Default);
        }

        /// <summary>
        ///     Attempts to resolve a type using default options.
        /// </summary>
        /// <typeparam name="TResolveType">Type to resolve</typeparam>
        /// <returns>Instance of type</returns>
        /// <exception cref="TinyIoCResolutionException">Unable to resolve the type.</exception>
        public TResolveType Resolve<TResolveType>()
            where TResolveType : class
        {
            return (TResolveType)Resolve(typeof(TResolveType));
        }


        /// <summary>
        ///     Attempts to predict whether a given type can be resolved with default options.
        ///     Note: Resolution may still fail if user defined factory registrations fail to construct objects when called.
        /// </summary>
        /// <param name="resolveType">Type to resolve</param>
        /// <returns>Bool indicating whether the type can be resolved</returns>
        public bool CanResolve(Type resolveType)
        {
            return CanResolveInternal(new TypeRegistration(resolveType), NamedParameterOverloads.Default,
                ResolveOptions.Default);
        }


        /// <summary>
        ///     Attempts to predict whether a given type can be resolved with default options.
        ///     Note: Resolution may still fail if user defined factory registrations fail to construct objects when called.
        /// </summary>
        /// <typeparam name="TResolveType">Type to resolve</typeparam>
        /// <returns>Bool indicating whether the type can be resolved</returns>
        public bool CanResolve<TResolveType>()
            where TResolveType : class
        {
            return CanResolve(typeof(TResolveType));
        }

        #endregion

        #endregion

        #region Object Factories

        private abstract class ObjectFactoryBase
        {
            /// <summary>
            ///     Whether to assume this factory successfully constructs its objects
            ///     Generally set to true for delegate style factories as CanResolve cannot delve
            ///     into the delegates they contain.
            /// </summary>
            public virtual bool AssumeConstruction => false;

            /// <summary>
            ///     The type the factory instantiates
            /// </summary>
            public abstract Type CreatesType { get; }

            /// <summary>
            ///     Constructor to use, if specified
            /// </summary>
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public ConstructorInfo Constructor { get; private set; }

            /// <summary>
            ///     Create the type
            /// </summary>
            /// <param name="requestedType">Type user requested to be resolved</param>
            /// <param name="container">Container that requested the creation</param>
            /// <param name="parameters">Any user parameters passed</param>
            /// <param name="options"></param>
            /// <returns></returns>
            public abstract object GetObject(Type requestedType, TinyIoCContainer container,
                NamedParameterOverloads parameters, ResolveOptions options);

            public virtual ObjectFactoryBase GetFactoryForChildContainer(Type type, TinyIoCContainer parent,
                TinyIoCContainer child)
            {
                return this;
            }
        }

        /// <summary>
        ///     IObjectFactory that creates new instances of types for each resolution
        /// </summary>
        private class MultiInstanceFactory : ObjectFactoryBase
        {
            private readonly Type _registerImplementation;
            private readonly Type _registerType;

            public MultiInstanceFactory(Type registerType, Type registerImplementation)
            {
                if (registerImplementation.IsAbstract() || registerImplementation.IsInterface())
                    throw new TinyIoCRegistrationTypeException(registerImplementation, "MultiInstanceFactory");

                if (!IsValidAssignment(registerType, registerImplementation))
                    throw new TinyIoCRegistrationTypeException(registerImplementation, "MultiInstanceFactory");

                _registerType = registerType;
                _registerImplementation = registerImplementation;
            }

            public override Type CreatesType => _registerImplementation;

            public override object GetObject(Type requestedType, TinyIoCContainer container,
                NamedParameterOverloads parameters, ResolveOptions options)
            {
                try
                {
                    return container.ConstructType(requestedType, _registerImplementation, Constructor, parameters,
                        options);
                }
                catch (TinyIoCResolutionException ex)
                {
                    throw new TinyIoCResolutionException(_registerType, ex);
                }
            }
        }

        /// <summary>
        ///     Stores an particular instance to return for a type
        /// </summary>
        private sealed class InstanceFactory : ObjectFactoryBase, IDisposable
        {
            private readonly object _instance;

            public InstanceFactory(Type registerType, Type registerImplementation, object instance)
            {
                if (!IsValidAssignment(registerType, registerImplementation))
                    throw new TinyIoCRegistrationTypeException(registerImplementation, "InstanceFactory");

                CreatesType = registerImplementation;
                _instance = instance;
            }

            public override bool AssumeConstruction => true;

            public override Type CreatesType { get; }

            public void Dispose()
            {
                if (_instance is IDisposable disposable)
                    disposable.Dispose();
            }

            public override object GetObject(Type requestedType, TinyIoCContainer container,
                NamedParameterOverloads parameters, ResolveOptions options)
            {
                return _instance;
            }
        }


        /// <summary>
        ///     A factory that lazy instantiates a type and always returns the same instance
        /// </summary>
        private sealed class SingletonFactory : ObjectFactoryBase, IDisposable
        {
            private readonly object _singletonLock = new object();
            private object _current;

            public SingletonFactory(Type registerType, Type registerImplementation)
            {

                if (registerImplementation.IsAbstract() || registerImplementation.IsInterface())
                    throw new TinyIoCRegistrationTypeException(registerImplementation, "SingletonFactory");

                if (!IsValidAssignment(registerType, registerImplementation))
                    throw new TinyIoCRegistrationTypeException(registerImplementation, "SingletonFactory");

                CreatesType = registerImplementation;
            }

            public override Type CreatesType { get; }

            public void Dispose()
            {
                switch (_current)
                {
                    case null:
                        return;
                    case IDisposable disposable:
                        disposable.Dispose();
                        break;
                }
            }

            public override object GetObject(Type requestedType, TinyIoCContainer container,
                NamedParameterOverloads parameters, ResolveOptions options)
            {
                if (parameters.Count != 0)
                    throw new ArgumentException("Cannot specify parameters for singleton types");

                lock (_singletonLock)
                {
                    if (_current == null)
                        _current = container.ConstructType(requestedType, CreatesType, Constructor, options);
                }

                return _current;
            }

            public override ObjectFactoryBase GetFactoryForChildContainer(Type type, TinyIoCContainer parent,
                TinyIoCContainer child)
            {
                // We make sure that the singleton is constructed before the child container takes the factory.
                // Otherwise the results would vary depending on whether or not the parent container had resolved
                // the type before the child container does.
                GetObject(type, parent, NamedParameterOverloads.Default, ResolveOptions.Default);
                return this;
            }
        }

        #endregion

        #region Type Registrations

        public sealed class TypeRegistration
        {
            private readonly int _hashCode;

            public TypeRegistration(Type type)
                : this(type, string.Empty)
            {
            }

            public TypeRegistration(Type type, string name)
            {
                Type = type;
                Name = name;

                _hashCode = string.Concat(Type.FullName, "|", Name).GetHashCode();
            }

            public Type Type { get; }
            public string Name { get; }

            public override bool Equals(object obj)
            {
                if (!(obj is TypeRegistration typeRegistration))
                    return false;

                if (Type != typeRegistration.Type)
                    return false;

                return string.Compare(Name, typeRegistration.Name, StringComparison.Ordinal) == 0;
            }

            public override int GetHashCode()
            {
                return _hashCode;
            }
        }

        private readonly SafeDictionary<TypeRegistration, ObjectFactoryBase> _registeredTypes;
#if USE_OBJECT_CONSTRUCTOR
        private delegate object ObjectConstructor(params object[] parameters);

        private static readonly SafeDictionary<ConstructorInfo, ObjectConstructor> ObjectConstructorCache =
            new SafeDictionary<ConstructorInfo, ObjectConstructor>();
#endif

        #endregion

        #region Constructors

        public TinyIoCContainer()
        {
            _registeredTypes = new SafeDictionary<TypeRegistration, ObjectFactoryBase>();

            RegisterDefaultTypes();
        }

#pragma warning disable 649
        private readonly TinyIoCContainer _parent;
#pragma warning restore 649

        #endregion

        #region Internal Methods

        private void RegisterDefaultTypes()
        {
            Register(this);
        }

        private RegisterOptions RegisterInternal(Type registerType, string name, ObjectFactoryBase factory)
        {
            var typeRegistration = new TypeRegistration(registerType, name);

            return AddUpdateRegistration(typeRegistration, factory);
        }

        private RegisterOptions AddUpdateRegistration(TypeRegistration typeRegistration, ObjectFactoryBase factory)
        {
            _registeredTypes[typeRegistration] = factory;

            return new RegisterOptions();
        }

        private static ObjectFactoryBase GetDefaultObjectFactory(Type registerType, Type registerImplementation)
        {
            return registerType.IsInterface() || registerType.IsAbstract()
                ? (ObjectFactoryBase) new SingletonFactory(registerType, registerImplementation)
                : new MultiInstanceFactory(registerType, registerImplementation);
        }

        private bool CanResolveInternal(TypeRegistration registration, NamedParameterOverloads parameters,
            ResolveOptions options)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var checkType = registration.Type;
            var name = registration.Name;

            if (_registeredTypes.TryGetValue(new TypeRegistration(checkType, name), out var factory))
            {
                if (factory.AssumeConstruction)
                    return true;

                if (factory.Constructor == null)
                    return GetBestConstructor(factory.CreatesType, parameters, options) != null;
                return CanConstruct(factory.Constructor, parameters, options);
            }

#if RESOLVE_OPEN_GENERICS
            if (checkType.IsInterface() && checkType.IsGenericType())
                // if the type is registered as an open generic, then see if the open generic is registered
                if (_registeredTypes.TryGetValue(new TypeRegistration(checkType.GetGenericTypeDefinition(), name),
                    out factory))
                {
                    if (factory.AssumeConstruction)
                        return true;

                    if (factory.Constructor == null)
                        return GetBestConstructor(factory.CreatesType, parameters, options) != null;
                    return CanConstruct(factory.Constructor, parameters, options);
                }
#endif

            // Fail if requesting named resolution and settings set to fail if unresolved
            // Or bubble up if we have a parent
            if (!string.IsNullOrEmpty(name) &&
                options.NamedResolutionFailureAction == NamedResolutionFailureActions.Fail)
                return _parent?.CanResolveInternal(registration, parameters, options) == true;

            // Attempted unnamed fallback container resolution if relevant and requested
            if (!string.IsNullOrEmpty(name) && options.NamedResolutionFailureAction ==
                NamedResolutionFailureActions.AttemptUnnamedResolution)
                if (_registeredTypes.TryGetValue(new TypeRegistration(checkType), out factory))
                {
                    if (factory.AssumeConstruction)
                        return true;

                    return GetBestConstructor(factory.CreatesType, parameters, options) != null;
                }

            // Check if type is an automatic lazy factory request
            if (IsAutomaticLazyFactoryRequest(checkType))
                return true;

            // Check if type is an IEnumerable<ResolveType>
            if (IsIEnumerableRequest(registration.Type))
                return true;

            // Attempt unregistered construction if possible and requested
            // If we cant', bubble if we have a parent
            if (options.UnregisteredResolutionAction == UnregisteredResolutionActions.AttemptResolve ||
                checkType.IsGenericType() &&
                options.UnregisteredResolutionAction == UnregisteredResolutionActions.GenericsOnly)
                return GetBestConstructor(checkType, parameters, options) != null ||
                       (_parent?.CanResolveInternal(registration, parameters, options) ?? false);

            // Bubble resolution up the container tree if we have a parent
            if (_parent != null)
                return _parent.CanResolveInternal(registration, parameters, options);

            return false;
        }

        private bool IsIEnumerableRequest(Type type)
        {
            if (!type.IsGenericType())
                return false;

            var genericType = type.GetGenericTypeDefinition();

            if (genericType == typeof(IEnumerable<>))
                return true;

            return false;
        }

        private bool IsAutomaticLazyFactoryRequest(Type type)
        {
            if (!type.IsGenericType())
                return false;

            var genericType = type.GetGenericTypeDefinition();

            if (genericType == typeof(Func<>))
                return true;

            if (genericType == typeof(Func<,>) && type.GetGenericArguments()[0] == typeof(string))
                return true;

            return genericType == typeof(Func<,,>) && type.GetGenericArguments()[0] == typeof(string) &&
                   type.GetGenericArguments()[1] == typeof(IDictionary<string, object>);
        }

        private ObjectFactoryBase GetParentObjectFactory(TypeRegistration registration)
        {
            if (_parent == null)
                return null;

            ObjectFactoryBase factory;

            if (!registration.Type.IsGenericType())
                return _parent._registeredTypes.TryGetValue(registration, out factory)
                    ? factory.GetFactoryForChildContainer(registration.Type, _parent, this)
                    : _parent.GetParentObjectFactory(registration);
            var openTypeRegistration = new TypeRegistration(registration.Type,
                registration.Name);

            return _parent._registeredTypes.TryGetValue(openTypeRegistration, out factory) ? factory.GetFactoryForChildContainer(openTypeRegistration.Type, _parent, this) : _parent.GetParentObjectFactory(registration);

        }

        private object ResolveInternal(TypeRegistration registration, NamedParameterOverloads parameters,
            ResolveOptions options)
        {
            // Attempt container resolution
            if (_registeredTypes.TryGetValue(registration, out var factory))
                try
                {
                    return factory.GetObject(registration.Type, this, parameters, options);
                }
                catch (TinyIoCResolutionException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new TinyIoCResolutionException(registration.Type, ex);
                }

#if RESOLVE_OPEN_GENERICS
            // Attempt container resolution of open generic
            if (registration.Type.IsGenericType())
            {
                var openTypeRegistration = new TypeRegistration(registration.Type.GetGenericTypeDefinition(),
                    registration.Name);

                if (_registeredTypes.TryGetValue(openTypeRegistration, out factory))
                    try
                    {
                        return factory.GetObject(registration.Type, this, parameters, options);
                    }
                    catch (TinyIoCResolutionException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        throw new TinyIoCResolutionException(registration.Type, ex);
                    }
            }
#endif

            // Attempt to get a factory from parent if we can
            var bubbledObjectFactory = GetParentObjectFactory(registration);
            if (bubbledObjectFactory != null)
                try
                {
                    return bubbledObjectFactory.GetObject(registration.Type, this, parameters, options);
                }
                catch (TinyIoCResolutionException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new TinyIoCResolutionException(registration.Type, ex);
                }

            // Fail if requesting named resolution and settings set to fail if unresolved
            if (!string.IsNullOrEmpty(registration.Name) &&
                options.NamedResolutionFailureAction == NamedResolutionFailureActions.Fail)
                throw new TinyIoCResolutionException(registration.Type);

            // Attempted unnamed fallback container resolution if relevant and requested
            if (!string.IsNullOrEmpty(registration.Name) && options.NamedResolutionFailureAction ==
                NamedResolutionFailureActions.AttemptUnnamedResolution)
                if (_registeredTypes.TryGetValue(new TypeRegistration(registration.Type, string.Empty), out factory))
                    try
                    {
                        return factory.GetObject(registration.Type, this, parameters, options);
                    }
                    catch (TinyIoCResolutionException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        throw new TinyIoCResolutionException(registration.Type, ex);
                    }

#if EXPRESSIONS
            // Attempt to construct an automatic lazy factory if possible
            if (IsAutomaticLazyFactoryRequest(registration.Type))
                return GetLazyAutomaticFactoryRequest(registration.Type);
#endif
            if (IsIEnumerableRequest(registration.Type))
                return GetIEnumerableRequest(registration.Type);

            // Attempt unregistered construction if possible and requested
            if (options.UnregisteredResolutionAction != UnregisteredResolutionActions.AttemptResolve &&
                (!registration.Type.IsGenericType() || options.UnregisteredResolutionAction !=
                    UnregisteredResolutionActions.GenericsOnly))
                throw new TinyIoCResolutionException(registration.Type);
            if (!registration.Type.IsAbstract() && !registration.Type.IsInterface())
                return ConstructType(null, registration.Type, parameters, options);

            // Unable to resolve - throw
            throw new TinyIoCResolutionException(registration.Type);
        }

#if EXPRESSIONS
        private object GetLazyAutomaticFactoryRequest(Type type)
        {
            if (!type.IsGenericType())
                return null;

            var genericType = type.GetGenericTypeDefinition();
            
            var genericArguments = type.GetGenericArguments();
            
            // Just a func
            if (genericType == typeof(Func<>))
            {
                var returnType = genericArguments[0];

              
                var resolveMethod = typeof(TinyIoCContainer).GetMethod("Resolve", new Type[] { });

                if (!(resolveMethod is null))
                {
                    resolveMethod = resolveMethod.MakeGenericMethod(returnType);

                    var resolveCall = Expression.Call(Expression.Constant(this), resolveMethod);

                    var resolveLambda = Expression.Lambda(resolveCall).Compile();

                    return resolveLambda;
                }
            }

            // 2 parameter func with string as first parameter (name)
            if (genericType == typeof(Func<,>) && genericArguments[0] == typeof(string))
            {
                var returnType = genericArguments[1];
                
                var resolveMethod = typeof(TinyIoCContainer).GetMethod("Resolve", new Type[] { typeof(string) });

                if (!(resolveMethod is null))
                {
                    resolveMethod = resolveMethod.MakeGenericMethod(returnType);

                    var resolveCall = Expression.Call(Expression.Constant(this), resolveMethod,
                        Expression.Parameter(typeof(string), "name"));

                    return Expression.Lambda(resolveCall, Expression.Parameter(typeof(string), "name")).Compile();
                }
            }

            // 3 parameter func with string as first parameter (name) and IDictionary<string, object> as second (parameters)

            if (genericType != typeof(Func<,,>) || type.GetGenericArguments()[0] != typeof(string) ||
                type.GetGenericArguments()[1] != typeof(IDictionary<string, object>))
                throw new TinyIoCResolutionException(type);
            {
                var returnType = genericArguments[2];

                var name = Expression.Parameter(typeof(string), "name");
                var parameters = Expression.Parameter(typeof(IDictionary<string, object>), "parameters");

               
                var resolveMethod = typeof(TinyIoCContainer).GetMethod("Resolve",
                    new Type[] { typeof(string), typeof(NamedParameterOverloads) });
                if (resolveMethod is null) throw new TinyIoCResolutionException(type);
                resolveMethod = resolveMethod.MakeGenericMethod(returnType);

                var resolveCall = Expression.Call(Expression.Constant(this), resolveMethod, name,
                    Expression.Call(typeof(NamedParameterOverloads), "FromIDictionary", null, parameters));

                var resolveLambda = Expression.Lambda(resolveCall, name, parameters).Compile();

                return resolveLambda;
            }
        }
#endif
        private object GetIEnumerableRequest(Type type)
        {
            var genericResolveAllMethod = GetType().GetGenericMethod(BindingFlags.Public | BindingFlags.Instance,
                "ResolveAll", type.GetGenericArguments(), new[] { typeof(bool) });
         
            return genericResolveAllMethod.Invoke(this, new object[] { false });
        }

        private bool CanConstruct(ConstructorInfo ctor, NamedParameterOverloads parameters, ResolveOptions options)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            foreach (var parameter in ctor.GetParameters())
            {
                if (string.IsNullOrEmpty(parameter.Name))
                    return false;

                var isParameterOverload = parameters.ContainsKey(parameter.Name);

              
                if (parameter.ParameterType.IsPrimitive() && !isParameterOverload)
                    
                    return false;

                if (!isParameterOverload && !CanResolveInternal(new TypeRegistration(parameter.ParameterType),
                    NamedParameterOverloads.Default, options))
                    return false;
            }

            return true;
        }

        private ConstructorInfo GetBestConstructor(Type type, NamedParameterOverloads parameters,
            ResolveOptions options)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            if (type.IsValueType())
                return null;

            var ctors = GetTypeConstructors(type);

            return ctors.FirstOrDefault(ctor => CanConstruct(ctor, parameters, options));
        }

        private static IEnumerable<ConstructorInfo> GetTypeConstructors(Type type)
        {
            var candidateCtors = type
                .GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(x => !x.IsPrivate) // Includes internal constructors but not private constructors
                .ToList();

            var attributeCtors = candidateCtors
                .Where(x => x.GetCustomAttributes(typeof(TinyIoCConstructorAttribute), false).Any())
                .ToList();

            if (attributeCtors.Any())
                candidateCtors = attributeCtors;

            return candidateCtors.OrderByDescending(ctor => ctor.GetParameters().Length);
          }

        private object ConstructType(Type requestedType, Type implementationType, ConstructorInfo constructor,
            ResolveOptions options)
        {
            return ConstructType(requestedType, implementationType, constructor, NamedParameterOverloads.Default,
                options);
        }

        private object ConstructType(Type requestedType, Type implementationType, NamedParameterOverloads parameters,
            ResolveOptions options)
        {
            return ConstructType(requestedType, implementationType, null, parameters, options);
        }

        private object ConstructType(Type requestedType, Type implementationType, ConstructorInfo constructor,
            NamedParameterOverloads parameters, ResolveOptions options)
        {
            var typeToConstruct = implementationType;

#if RESOLVE_OPEN_GENERICS
            if (implementationType.IsGenericTypeDefinition())
            {
                if (requestedType == null || !requestedType.IsGenericType() ||
                    !requestedType.GetGenericArguments().Any())
                    throw new TinyIoCResolutionException(typeToConstruct);

                typeToConstruct = typeToConstruct.MakeGenericType(requestedType.GetGenericArguments());
            }
#endif
            if (constructor == null)
                // Try and get the best constructor that we can construct
                // if we can't construct any then get the constructor
                // with the least number of parameters so we can throw a meaningful
                // resolve exception
                constructor = GetBestConstructor(typeToConstruct, parameters, options) ??
                              GetTypeConstructors(typeToConstruct).LastOrDefault();

            if (constructor == null)
                throw new TinyIoCResolutionException(typeToConstruct);

            var ctorParams = constructor.GetParameters();
            var args = new object[ctorParams.Length];

            for (var parameterIndex = 0; parameterIndex < ctorParams.Length; parameterIndex++)
            {
                var currentParam = ctorParams[parameterIndex];

                try
                {
                    args[parameterIndex] = parameters.ContainsKey(currentParam.Name)
                        ? parameters[currentParam.Name]
                        : ResolveInternal(
                            new TypeRegistration(currentParam.ParameterType),
                            NamedParameterOverloads.Default,
                            options);
                }
                catch (TinyIoCResolutionException ex)
                {
                    // If a constructor parameter can't be resolved
                    // it will throw, so wrap it and throw that this can't
                    // be resolved.
                    throw new TinyIoCResolutionException(typeToConstruct, ex);
                }
                catch (Exception ex)
                {
                    throw new TinyIoCResolutionException(typeToConstruct, ex);
                }
            }

            try
            {
#if USE_OBJECT_CONSTRUCTOR
                var constructionDelegate = CreateObjectConstructionDelegateWithCache(constructor);
                return constructionDelegate.Invoke(args);
#else
                return constructor.Invoke(args);
#endif
            }
            catch (Exception ex)
            {
                throw new TinyIoCResolutionException(typeToConstruct, ex);
            }
        }

#if USE_OBJECT_CONSTRUCTOR
        private static ObjectConstructor CreateObjectConstructionDelegateWithCache(ConstructorInfo constructor)
        {
            if (ObjectConstructorCache.TryGetValue(constructor, out var objectConstructor))
                return objectConstructor;

            // We could lock the cache here, but there's no real side
            // effect to two threads creating the same ObjectConstructor
            // at the same time, compared to the cost of a lock for 
            // every creation.
            var constructorParams = constructor.GetParameters();
            var lambdaParams = Expression.Parameter(typeof(object[]), "parameters");
            var newParams = new Expression[constructorParams.Length];

            for (var i = 0; i < constructorParams.Length; i++)
            {
                var paramsParameter = Expression.ArrayIndex(lambdaParams, Expression.Constant(i));

                newParams[i] = Expression.Convert(paramsParameter, constructorParams[i].ParameterType);
            }

            var newExpression = Expression.New(constructor, newParams);

            var constructionLambda = Expression.Lambda(typeof(ObjectConstructor), newExpression, lambdaParams);

            objectConstructor = (ObjectConstructor)constructionLambda.Compile();

            ObjectConstructorCache[constructor] = objectConstructor;
            return objectConstructor;
        }
#endif


        private static bool IsValidAssignment(Type registerType, Type registerImplementation)
        {
            if (!registerType.IsGenericTypeDefinition())
            {
                if (!registerType.IsAssignableFrom(registerImplementation))
                    return false;
            }
            else
            {
                if (registerType.IsInterface())
                {
#if (PORTABLE || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
                    if (!registerImplementation.GetInterfaces().Any(t => t.Name == registerType.Name))
                        return false;
#else
                    if (!registerImplementation.FindInterfaces((t, o) => t.Name == registerType.Name, null).Any())
                        return false;
#endif
                }
                else if (registerType.IsAbstract() && registerImplementation.BaseType() != registerType)
                {
                    return false;
                }
            }

            //#endif
            return true;
        }

        #endregion

        #region IDisposable Members

        private bool _disposed;

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            _registeredTypes.Dispose();

            // ReSharper disable once GCSuppressFinalizeForTypeWithoutDestructor
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}