using System;
using XQ.SDK.Enum;
using XQ.SDK.Enum.Event;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    /// <summary>
    ///     群聊事件事件
    ///     对应IGroupEvent接口
    /// </summary>
    public class GroupEventEventArgs : XqEventArgs
    {
        /// <summary>
        ///     事件构造函数
        /// </summary>
        /// <param name="xqApi">XQApi</param>
        /// <param name="rawEvent">XQEvent的原始参数</param>
        public GroupEventEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        /// <summary>
        ///     事件来源QQ
        /// </summary>
        public Qq FromQq => string.IsNullOrWhiteSpace(RawEvent.FromQq)
            ? null
            : new Qq(XqApi, Robot, RawEvent.FromQq, MessageType.Group, RawEvent.From);

        /// <summary>
        ///     事件目标QQ
        /// </summary>
        public Qq TargetQq => string.IsNullOrWhiteSpace(RawEvent.TargetQq)
            ? null
            : new Qq(XqApi, Robot, RawEvent.TargetQq, MessageType.Group, RawEvent.From);

        /// <summary>
        ///     事件来源群聊
        /// </summary>
        public Group FromGroup =>
            string.IsNullOrWhiteSpace(RawEvent.From) ? null : new Group(XqApi, Robot, RawEvent.From);

        /// <summary>
        ///     事件类型
        /// </summary>
        public GroupEventEventType Type =>
            System.Enum.IsDefined(typeof(GroupEventEventType), RawEvent.EventType)
                ? (GroupEventEventType) RawEvent.EventType
                : throw new ArgumentException("type不正确");
    }
}