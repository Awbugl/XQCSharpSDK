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

        public Qq FromQq => RawEvent.FromQq == null ? null : new Qq(XqApi, Robot, RawEvent.FromQq);

        public Qq TargetQq => RawEvent.TargetQq == null ? null : new Qq(XqApi, Robot, RawEvent.TargetQq);

        public Group FromGroup => RawEvent.From == null ? null : new Group(XqApi, Robot, RawEvent.From);

        public XqGroupEventType Type =>
            System.Enum.IsDefined(typeof(XqGroupEventType), RawEvent.EventType)
                ? (XqGroupEventType) RawEvent.EventType
                : throw new ArgumentException("type不正确");
    }
}