using XQ.SDK.EventArgs;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     好友、群临时事件接口
    /// </summary>
    public interface IFriendEvent : IXqEvent
    {
        /// <summary>
        ///     处理好友、群临时事件
        /// </summary>
        void FriendEvent(FriendEventArgs e);
    }
}