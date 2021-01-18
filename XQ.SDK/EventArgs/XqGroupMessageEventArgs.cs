using XQ.SDK.Core;
using XQ.SDK.Enum;
using XQ.SDK.Enum.Event;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    public class XqGroupMessageEventArgs : XqMessageEventArgs
    {
        public XqGroupMessageEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        public Group FromGroup => RawEvent.From == null ? null : new Group(XqApi, Robot, RawEvent.From);

        public void SendGroupMessage(params object[] msg)
        {
            XqApi.TencentApi.SendGroupMessage(RawEvent.RobotQq, FromGroup, msg.ToSend());
        }

        public void SendPrivateMessage(params object[] msg)
        {
            XqApi.TencentApi.SendPrivateMessage(Robot, FromQq, PrivateMessageType.TempGroupMessage, msg.ToSend(),
                Type == XqMessageEventType.TempGroupMessage ? RawEvent.From : "");
        }
    }
}