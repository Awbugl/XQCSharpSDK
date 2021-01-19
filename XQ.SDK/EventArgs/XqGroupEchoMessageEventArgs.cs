using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    public class XqGroupEchoMessageEventArgs : XqEventArgs
    {
        public XqGroupEchoMessageEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        public Group FromGroup =>
            string.IsNullOrWhiteSpace(RawEvent.From) ? null : new Group(XqApi, Robot, RawEvent.From);

        public void WithdrawMessage()
        {
            XqApi.WithdrawMsg(Robot, 2, FromGroup, Robot, RawEvent.Index, RawEvent.Msgid, RawEvent.Unix);
        }
    }
}