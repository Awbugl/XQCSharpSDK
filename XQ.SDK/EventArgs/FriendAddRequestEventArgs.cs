using XQ.SDK.Enum;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    /// <summary>
    ///     好友添加请求事件
    ///     对应IFriendAddRequest接口
    /// </summary>
    public class FriendAddRequestEventArgs : FriendEventArgs
    {
        /// <summary>
        ///     事件构造函数
        /// </summary>
        /// <param name="xqApi">XQApi</param>
        /// <param name="rawEvent">XQEvent的原始参数</param>
        public FriendAddRequestEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        /// <summary>
        ///     处理好友添加请求
        /// </summary>
        /// <param name="type">处理类型</param>
        /// <param name="refuseMessage">拒绝时可填写拒绝理由</param>
        public void Handle(FriendRequestHandlerType type, string refuseMessage = "")
        {
            XqApi.HandleFriendEvent(Robot, FromQq, type, refuseMessage);
        }
    }
}