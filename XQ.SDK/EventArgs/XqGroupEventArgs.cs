using System;
using XQ.SDK.Enum.Event;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    public class XqGroupEventArgs : XqEventArgs
    {
        public XqGroupEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        public Qq FromQq => string.IsNullOrWhiteSpace(RawEvent.FromQq)
            ? null
            : new Qq(XqApi, Robot, RawEvent.FromQq, XqMessageEventType.Group, RawEvent.From);

        public Qq TargetQq => string.IsNullOrWhiteSpace(RawEvent.TargetQq)
            ? null
            : new Qq(XqApi, Robot, RawEvent.TargetQq, XqMessageEventType.Group, RawEvent.From);

        public Group FromGroup =>
            string.IsNullOrWhiteSpace(RawEvent.From) ? null : new Group(XqApi, Robot, RawEvent.From);

        public XqGroupEventType Type =>
            System.Enum.IsDefined(typeof(XqGroupEventType), RawEvent.EventType)
                ? (XqGroupEventType) RawEvent.EventType
                : throw new ArgumentException("type不正确");
    }
}