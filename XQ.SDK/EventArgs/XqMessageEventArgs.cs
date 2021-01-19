using System;
using XQ.SDK.Enum;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    public abstract class XqMessageEventArgs : XqEventArgs
    {
        /// <summary>
        ///     事件构造函数
        /// </summary>
        /// <param name="xqApi">XQApi</param>
        /// <param name="rawEvent">XQEvent的原始参数</param>
        protected XqMessageEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        /// <summary>
        ///     事件来源QQ
        /// </summary>
        public Qq FromQq => string.IsNullOrWhiteSpace(RawEvent.FromQq)
            ? null
            : new Qq(XqApi, Robot, RawEvent.FromQq, Type, RawEvent.From, RawEvent.ExtraType);

        /// <summary>
        ///     事件消息
        /// </summary>
        public QqMessage Message => new QqMessage(XqApi, Robot, RawEvent.Content);

        /// <summary>
        ///     事件类型
        /// </summary>
        public MessageType Type =>
            System.Enum.IsDefined(typeof(MessageType), RawEvent.EventType)
                ? (MessageType) RawEvent.EventType
                : throw new ArgumentException("type不正确");
    }
}