using System;

namespace XQ.SDK.Core.TinyIOC
{
    [Serializable]
    public class TinyIoCResolutionException : Exception
    {
        private const string ErrorText = "Unable to resolve type: {0}";

        public TinyIoCResolutionException(Type type)
            : base(string.Format(ErrorText, type.FullName))
        {
        }

        public TinyIoCResolutionException(Type type, Exception innerException)
            : base(string.Format(ErrorText, type.FullName), innerException)
        {
        }
    }
}