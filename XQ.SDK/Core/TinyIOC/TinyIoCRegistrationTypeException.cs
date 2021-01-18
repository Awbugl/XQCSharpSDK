using System;

namespace XQ.SDK.Core
{
    [Serializable]
    public class TinyIoCRegistrationTypeException : Exception
    {
        private const string RegisterErrorText =
            "Cannot register type {0} - abstract classes or interfaces are not valid implementation types for {1}.";

        public TinyIoCRegistrationTypeException(Type type, string factory)
            : base(string.Format(RegisterErrorText, type.FullName, factory))
        {
        }
    }
}