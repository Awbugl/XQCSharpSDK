using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    public class XqTransferEventArgs : XqFriendEventArgs
    {
        public XqTransferEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }
    }
}