using XQ.SDK.Enum;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    public class XqWithDrawMessageEventArgs : XqMessageEventArgs
    {
        public XqWithDrawMessageEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        public new WithdrawMessageType Type => (WithdrawMessageType) RawEvent.ExtraType;
    }
}