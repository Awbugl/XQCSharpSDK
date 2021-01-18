using XQ.Core;
using XQ.SDK.Interface;

namespace XQ.SDK.Core
{
    public static class Init
    {
        public static void Register<T, TU>() where TU : class, T where T : class, IProcess
        {
            Global.Container.Register<T, TU>();
        }
    }
}