using XQ.SDK.Enum;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    /// <summary>
    ///     私聊消息事件
    ///     对应IPrivateMessage接口
    /// </summary>
    public class PrivateMessageEventArgs : XqMessageEventArgs
    {
        /// <summary>
        ///     事件构造函数
        /// </summary>
        /// <param name="xqApi">XQApi</param>
        /// <param name="rawEvent">XQEvent的原始参数</param>
        public PrivateMessageEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        /// <summary>
        ///     事件类型
        /// </summary>
        public new PrivateMessageType Type => (PrivateMessageType) RawEvent.EventType;

        /// <summary>
        ///     回复私聊消息
        /// </summary>
        /// <param name="msg"></param>
        public void ReplyPrivateMessage(params object[] msg)
        {
            FromQq.SendPrivateMessage(msg);
        }
    }
}