using XQ.SDK.XQ;

namespace XQ.SDK.Interface
{
    public interface IAppDestroy : IProcess
    {
        /// <summary>
        ///     当在派生类中重写时, 处理 插件实例销毁事件 回调
        ///     应在此函数内释放资源及线程
        /// </summary>
        void AppDestroy(XqApi xqApi);
    }
}