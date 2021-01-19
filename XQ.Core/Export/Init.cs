using XQ.SDK.Interface;

namespace XQ.Core.Export
{
    public static class Init
    {
        public static void Register<T, TU>() where TU : class, T where T : class, IXqEvent
        {
            Global.Container.Register<T, TU>();
        }
    }
}