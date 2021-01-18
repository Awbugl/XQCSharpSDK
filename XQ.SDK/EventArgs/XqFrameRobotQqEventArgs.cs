using System;
using XQ.SDK.Enum.Event;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    public class XqFrameRobotQqEventArgs : XqEventArgs
    {
        public XqFrameRobotQqEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        public XqFrameRobotQqEventType Type =>
            System.Enum.IsDefined(typeof(XqFrameRobotQqEventType), RawEvent.EventType)
                ? (XqFrameRobotQqEventType) RawEvent.EventType
                : throw new ArgumentException("type不正确");
    }
}