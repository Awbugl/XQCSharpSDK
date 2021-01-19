using XQ.SDK.Enum;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    public class XqPrivateMessageEventArgs : XqMessageEventArgs
    {
        public XqPrivateMessageEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

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