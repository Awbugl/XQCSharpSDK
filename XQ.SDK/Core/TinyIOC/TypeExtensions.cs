using System;
using System.Linq;
using System.Reflection;

namespace XQ.SDK.Core
{
    public static class TypeExtensions
    {
        private static readonly SafeDictionary<GenericMethodCacheKey, MethodInfo> GenericMethodCache;

        static TypeExtensions()
        {
            GenericMethodCache = new SafeDictionary<GenericMethodCacheKey, MethodInfo>();
        }

        /// <summary>
        ///     Gets a generic method from a type given the method name, binding flags, generic types and parameter types
        /// </summary>
        /// <param name="sourceType">Source type</param>
        /// <param name="bindingFlags">Binding flags</param>
        /// <param name="methodName">Name of the method</param>
        /// <param name="genericTypes">Generic types to use to make the method generic</param>
        /// <param name="parameterTypes">Method parameters</param>
        /// <returns>MethodInfo or null if no matches found</returns>
        /// <exception cref="System.Reflection.AmbiguousMatchException" />
        /// <exception cref="System.ArgumentException" />
        public static MethodInfo GetGenericMethod(this Type sourceType, BindingFlags bindingFlags, string methodName,
            Type[] genericTypes, Type[] parameterTypes)
        {
            var cacheKey = new GenericMethodCacheKey(sourceType, methodName, genericTypes, parameterTypes);

            // Shouldn't need any additional locking
            // we don't care if we do the method info generation
            // more than once before it gets cached.
            if (GenericMethodCache.TryGetValue(cacheKey, out var method)) return method;
            method = GetMethod(sourceType, bindingFlags, methodName, genericTypes, parameterTypes);
            GenericMethodCache[cacheKey] = method;

            return method;
        }

        private static MethodInfo GetMethod(Type sourceType, BindingFlags bindingFlags, string methodName,
            Type[] genericTypes, Type[] parameterTypes)
        {
            var validMethods = from method in sourceType.GetMethods(bindingFlags)
                                where method.Name == methodName
                                where method.IsGenericMethod
                                where method.GetGenericArguments().Length == genericTypes.Length
                                let genericMethod = method.MakeGenericMethod(genericTypes)
                                where genericMethod.GetParameters().Count() == parameterTypes.Length
                                where genericMethod.GetParameters().Select(pi => pi.ParameterType).SequenceEqual(parameterTypes)
                                select genericMethod;

            var methods = validMethods.ToList();

            if (methods.Count > 1) throw new AmbiguousMatchException();

            return methods.FirstOrDefault();
        }

        private sealed class GenericMethodCacheKey
        {
            private readonly Type[] _genericTypes;

            private readonly int _hashCode;

            private readonly string _methodName;

            private readonly Type[] _parameterTypes;
            private readonly Type _sourceType;

            public GenericMethodCacheKey(Type sourceType, string methodName, Type[] genericTypes, Type[] parameterTypes)
            {
                _sourceType = sourceType;
                _methodName = methodName;
                _genericTypes = genericTypes;
                _parameterTypes = parameterTypes;
                _hashCode = GenerateHashCode();
            }

            public override bool Equals(object obj)
            {
                if (!(obj is GenericMethodCacheKey cacheKey))
                    return false;

                if (_sourceType != cacheKey._sourceType)
                    return false;

                if (!string.Equals(_methodName, cacheKey._methodName, StringComparison.Ordinal))
                    return false;

                if (_genericTypes.Length != cacheKey._genericTypes.Length)
                    return false;

                if (_parameterTypes.Length != cacheKey._parameterTypes.Length)
                    return false;

                if (_genericTypes.Where((t, i) => t != cacheKey._genericTypes[i]).Any())
                {
                    return false;
                }

                return !_parameterTypes.Where((t, i) => t != cacheKey._parameterTypes[i]).Any();
            }

            public override int GetHashCode()
            {
                return _hashCode;
            }

            private int GenerateHashCode()
            {
                unchecked
                {
                    var result = _sourceType.GetHashCode();

                    result = (result * 397) ^ _methodName.GetHashCode();

                    result = _genericTypes.Aggregate(result, (current, t) => (current * 397) ^ t.GetHashCode());

                    return _parameterTypes.Aggregate(result, (current, t) => (current * 397) ^ t.GetHashCode());
                }
            }
        }
    }
}