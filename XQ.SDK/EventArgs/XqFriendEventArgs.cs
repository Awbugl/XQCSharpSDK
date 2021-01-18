using System;
using XQ.SDK.Enum.Event;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    public class XqFriendEventArgs : XqEventArgs
    {
        public XqFriendEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        public string FromQq => RawEvent.FromQq;

        public XqFriendEventType Type =>
            System.Enum.IsDefined(typeof(XqFriendEventType), RawEvent.EventType)
                ? (XqFriendEventType) RawEvent.EventType
                : throw new ArgumentException("type不正确");
    }
}