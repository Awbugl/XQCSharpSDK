using XQ.SDK.XQ;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     应用关闭事件接口
    /// </summary>
    public interface IAppDisable : IXqEvent
    {
        /// <summary>
        ///     处理应用关闭事件
        /// </summary>
        void AppDisable(XqApi xqApi);
    }
}