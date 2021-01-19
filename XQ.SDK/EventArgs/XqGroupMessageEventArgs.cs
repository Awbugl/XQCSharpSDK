using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    public class XqGroupMessageEventArgs : XqMessageEventArgs
    {
        public XqGroupMessageEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        public Group FromGroup =>
            string.IsNullOrWhiteSpace(RawEvent.From) ? null : new Group(XqApi, Robot, RawEvent.From);

        /// <summary>
        ///     群聊回复消息
        /// </summary>
        /// <param name="anonymous">选择是否匿名发送,在群聊不允许发送匿名消息时无效</param>
        /// <param name="at">选择是否at</param>
        /// <param name="msg"></param>
        public void ReplyAsGroupMessage(bool anonymous, bool at, params object[] msg)
        {
            FromGroup.SendGroupMessage(anonymous, at ? XqMessageObject.At(FromQq) : null, msg);
        }

        /// <summary>
        ///     私聊回复消息
        /// </summary>
        /// <param name="msg"></param>
        public void ReplyAsPrivateMessage(params object[] msg)
        {
            FromQq.SendPrivateMessage(msg);
        }
    }
}