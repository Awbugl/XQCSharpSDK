namespace XQ.SDK.Core
{
    public sealed class ResolveOptions
    {
        public UnregisteredResolutionActions UnregisteredResolutionAction { get; set; } =
            UnregisteredResolutionActions.AttemptResolve;

        public NamedResolutionFailureActions NamedResolutionFailureAction { get; set; } =
            NamedResolutionFailureActions.Fail;

        /// <summary>
        ///     Gets the default options (attempt resolution of unregistered types, fail on named resolution if name not found)
        /// </summary>
        public static ResolveOptions Default { get; } = new ResolveOptions();
    }
}