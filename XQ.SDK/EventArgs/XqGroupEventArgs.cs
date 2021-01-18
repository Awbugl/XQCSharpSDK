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

        public string FromQq => RawEvent.FromQq;

        public string TargetQq => RawEvent.TargetQq;

        public string FromGroup => RawEvent.From;

        public XqGroupEventType Type =>
            System.Enum.IsDefined(typeof(XqGroupEventType), RawEvent.EventType)
                ? (XqGroupEventType) RawEvent.EventType
                : throw new ArgumentException("type不正确");
    }
}