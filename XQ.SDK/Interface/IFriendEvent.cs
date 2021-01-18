using XQ.SDK.EventArgs;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     好友、群临时事件接口
    /// </summary>
    public interface IFriendEvent : IProcess
    {
        /// <summary>
        ///     当在派生类中重写时, 处理 好友、群临时事件 回调
        /// </summary>
        /// <param name="e">附加的事件参数</param>
        void FriendEvent(XqFriendEventArgs e);
    }
}