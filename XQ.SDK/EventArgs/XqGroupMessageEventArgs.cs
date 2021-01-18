using XQ.SDK.Enum;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    public class XqGroupMessageEventArgs : XqMessageEventArgs
    {
        public XqGroupMessageEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        public string FromGroup => RawEvent.From;

        public void SendPrivateMessage(string msg)
        {
            XqApi.TencentApi.SendPrivateMessage(RawEvent.RobotQq, FromQq, PrivateMessageType.TempGroupMessage, msg,
                FromGroup);
        }

        public void SendGroupMessage(string msg)
        {
            XqApi.TencentApi.SendGroupMessage(RawEvent.RobotQq, FromGroup, msg);
        }
    }
}