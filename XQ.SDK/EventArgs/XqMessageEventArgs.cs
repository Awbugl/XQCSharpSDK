using System;
using XQ.SDK.Enum.Event;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    public class XqMessageEventArgs : XqEventArgs
    {
        public XqMessageEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        public string FromQq => RawEvent.FromQq;

        public string Text => RawEvent.Content;

        public XqMessageEventType Type =>
            System.Enum.IsDefined(typeof(XqMessageEventType), RawEvent.EventType)
                ? (XqMessageEventType) RawEvent.EventType
                : throw new ArgumentException("type不正确");
    }
}