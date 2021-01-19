using XQ.SDK.XQ;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     插件实例销毁事件接口
    /// </summary>
    public interface IPluginDestroy : IXqEvent
    {
        /// <summary>
        ///     处理插件实例销毁事件
        ///     应在此函数内释放资源及线程
        /// </summary>
        void PluginDestroy(XqApi xqApi);
    }
}