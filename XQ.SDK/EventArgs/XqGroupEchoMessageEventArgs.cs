using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    public class XqGroupEchoMessageEventArgs : XqMessageEventArgs
    {
        public XqGroupEchoMessageEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        public string FromGroup => RawEvent.From;

        public void WithdrawMessage()
        {
            XqApi.TencentApi.WithdrawMsg(RobotQq, 2, FromGroup, FromQq, RawEvent.Index, RawEvent.Msgid, RawEvent.Unix);
        }
    }
}