using XQ.SDK.EventArgs;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     私聊消息事件接口
    /// </summary>
    public interface IPrivateMessage : IXqEvent
    {
        /// <summary>
        ///     处理私聊消息事件
        /// </summary>
        void PrivateMessage(PrivateMessageEventArgs e);
    }
}