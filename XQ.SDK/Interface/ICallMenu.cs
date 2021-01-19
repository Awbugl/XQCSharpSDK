using XQ.SDK.XQ;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     调用菜单事件接口
    /// </summary>
    public interface ICallMenu : IXqEvent
    {
        /// <summary>
        ///     处理调用菜单事件
        /// </summary>
        void CallMenu(XqApi xqApi);
    }
}