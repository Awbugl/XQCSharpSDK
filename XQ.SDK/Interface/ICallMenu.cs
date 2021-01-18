using XQ.SDK.XQ;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     调用菜单事件接口
    /// </summary>
    public interface ICallMenu : IProcess
    {
        /// <summary>
        ///     当在派生类中重写时, 处理 调用菜单事件 回调
        /// </summary>
        void CallMenu(XqApi xqApi);
    }
}