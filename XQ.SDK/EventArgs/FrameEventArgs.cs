using System;
using XQ.SDK.Enum.Event;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    /// <summary>
    ///     框架事件
    ///     对应IFrameEvent接口
    /// </summary>
    public class FrameEventArgs : XqEventArgs
    {
        /// <summary>
        ///     事件构造函数
        /// </summary>
        /// <param name="xqApi">XQApi</param>
        /// <param name="rawEvent">XQEvent的原始参数</param>
        public FrameEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        /// <summary>
        ///     框架事件类型
        /// </summary>
        public FrameEventEventType Type =>
            System.Enum.IsDefined(typeof(FrameEventEventType), RawEvent.EventType)
                ? (FrameEventEventType) RawEvent.EventType
                : throw new ArgumentException("type不正确");
    }
}