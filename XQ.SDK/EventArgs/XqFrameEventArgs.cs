using System;
using XQ.SDK.Enum.Event;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    public class XqFrameEventArgs : XqEventArgs
    {
        public XqFrameEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        public XqFrameEventType Type =>
            System.Enum.IsDefined(typeof(XqFrameEventType), RawEvent.EventType)
                ? (XqFrameEventType) RawEvent.EventType
                : throw new ArgumentException("type不正确");
    }
}