using XQ.SDK.Core;
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

        public void SendPrivateMessage(params object[] msg)
        {
            XqApi.TencentApi.SendPrivateMessage(Robot, FromQq, Type, msg.ToSend(),
                Type == PrivateMessageType.TempGroupMessage ? RawEvent.From : "");
        }
    }
}