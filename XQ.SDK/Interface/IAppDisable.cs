using XQ.SDK.XQ;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     应用关闭事件接口
    /// </summary>
    public interface IAppDisable : IProcess
    {
        /// <summary>
        ///     当在派生类中重写时, 处理 应用关闭事件 回调
        /// </summary>
        void AppDisable(XqApi xqApi);
    }
}