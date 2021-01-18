using System.Diagnostics.CodeAnalysis;

namespace XQ.SDK.Core
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public enum UnregisteredResolutionActions
    {
        /// <summary>
        ///     Attempt to resolve type, even if the type isn't registered.
        ///     Registered types/options will always take precedence.
        /// </summary>
        AttemptResolve,

        /// <summary>
        ///     Fail resolution if type not explicitly registered
        /// </summary>
        Fail,

        /// <summary>
        ///     Attempt to resolve unregistered type if requested type is generic
        ///     and no registration exists for the specific generic parameters used.
        ///     Registered types/options will always take precedence.
        /// </summary>
        GenericsOnly
    }
}