using XQ.SDK.Core.TinyIOC;
using XQ.SDK.Interface;

namespace XQ.Core.Export
{
    internal static class Init
    {
        internal static TinyIoCContainer Container { get; } = new TinyIoCContainer();

        internal static void Register<T, TU>() where TU : class, T where T : class, IXqEvent
        {
            Container.Register<T, TU>();
        }
    }
}