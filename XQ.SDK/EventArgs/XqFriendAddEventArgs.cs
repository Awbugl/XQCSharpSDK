using XQ.SDK.Enum;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    public class XqFriendAddEventArgs : XqFriendEventArgs
    {
        public XqFriendAddEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        public void Handle(XqFriendRequestHandlerType type, string refuseMessage = "")
        {
            XqApi.HandleFriendEvent(Robot, FromQq, 101, refuseMessage);
        }
    }
}