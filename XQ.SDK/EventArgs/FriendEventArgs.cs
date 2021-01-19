using System;
using XQ.SDK.Enum;
using XQ.SDK.Enum.Event;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    /// <summary>
    ///     好友、群临时事件接口
    /// </summary>
    public class FriendEventArgs : XqEventArgs
    {
        /// <summary>
        ///     事件构造函数
        /// </summary>
        /// <param name="xqApi">XQApi</param>
        /// <param name="rawEvent">XQEvent的原始参数</param>
        public FriendEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        /// <summary>
        ///     事件来源QQ
        /// </summary>
        public Qq FromQq => string.IsNullOrWhiteSpace(RawEvent.FromQq)
            ? null
            : new Qq(XqApi, Robot, RawEvent.FromQq, MessageType.Friend, null);

        /// <summary>
        ///     事件类型
        /// </summary>
        public FriendEventEventType Type =>
            System.Enum.IsDefined(typeof(FriendEventEventType), RawEvent.EventType)
                ? (FriendEventEventType) RawEvent.EventType
                : throw new ArgumentException("type不正确");
    }
}