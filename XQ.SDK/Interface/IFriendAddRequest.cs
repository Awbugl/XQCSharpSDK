using XQ.SDK.EventArgs;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     好友添加请求事件接口
    /// </summary>
    public interface IFriendAddRequest : IProcess
    {
        /// <summary>
        ///     当在派生类中重写时, 处理 好友添加请求事件 回调
        /// </summary>
        /// <param name="e">附加的事件参数</param>
        void FriendAddRequest(XqFriendAddEventArgs e);
    }
}