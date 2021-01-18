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

        public void SendPrivateMessage(string msg)
        {
            XqApi.TencentApi.SendPrivateMessage(RobotQq, FromQq, Type, msg,
                Type == PrivateMessageType.TempGroupMessage ? RawEvent.From : "");
        }
    }
}