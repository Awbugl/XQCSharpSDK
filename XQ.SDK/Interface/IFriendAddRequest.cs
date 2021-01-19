using XQ.SDK.EventArgs;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     好友添加请求事件接口
    /// </summary>
    public interface IFriendAddRequest : IXqEvent
    {
        /// <summary>
        ///     处理好友添加请求事件
        /// </summary>
        void FriendAddRequest(FriendAddRequestEventArgs e);
    }
}