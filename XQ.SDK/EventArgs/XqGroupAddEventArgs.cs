using XQ.SDK.Enum;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    public class XqGroupAddEventArgs : XqGroupEventArgs
    {
        public XqGroupAddEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        public void Handle(XqGroupRequestHandlerType type, string refuseMessage = "")
        {
            XqApi.HandleGroupEvent(Robot, 214, FromQq, FromGroup, RawEvent.Udpmsg, (int) type,
                refuseMessage);
        }
    }
}