using System;
using XQ.SDK.Enum.Event;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    /// <summary>
    ///     框架Robot事件事件
    ///     对应IRobotEvent接口
    /// </summary>
    public class RobotEventEventArgs : XqEventArgs
    {
        /// <summary>
        ///     事件构造函数
        /// </summary>
        /// <param name="xqApi">XQApi</param>
        /// <param name="rawEvent">XQEvent的原始参数</param>
        public RobotEventEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        /// <summary>
        ///     事件类型
        /// </summary>
        public RobotEventEventType Type =>
            System.Enum.IsDefined(typeof(RobotEventEventType), RawEvent.EventType)
                ? (RobotEventEventType) RawEvent.EventType
                : throw new ArgumentException("type不正确");
    }
}