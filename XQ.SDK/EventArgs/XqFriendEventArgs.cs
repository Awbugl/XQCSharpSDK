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

        public Qq FromQq => string.IsNullOrWhiteSpace(RawEvent.FromQq)
            ? null
            : new Qq(XqApi, Robot, RawEvent.FromQq, XqMessageEventType.Friend, null);

        public XqFriendEventType Type =>
            System.Enum.IsDefined(typeof(XqFriendEventType), RawEvent.EventType)
                ? (XqFriendEventType) RawEvent.EventType
                : throw new ArgumentException("type不正确");
    }
}