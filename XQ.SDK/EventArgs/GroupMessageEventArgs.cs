using XQ.SDK.Enum;
using XQ.SDK.Model;
using XQ.SDK.Model.MessageObject;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    /// <summary>
    ///     群消息事件
    ///     对应IGroupMessage接口
    /// </summary>
    public class GroupMessageEventArgs : XqMessageEventArgs
    {
        /// <summary>
        ///     事件构造函数
        /// </summary>
        /// <param name="xqApi">XQApi</param>
        /// <param name="rawEvent">XQEvent的原始参数</param>
        public GroupMessageEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        /// <summary>
        ///     事件来源群聊
        /// </summary>
        public Group FromGroup =>
            string.IsNullOrWhiteSpace(RawEvent.From) ? null : new Group(XqApi, Robot, RawEvent.From);

        /// <summary>
        ///     撤回此条消息
        /// </summary>
        public void WithdrawMessage() =>
            XqApi.WithdrawMsg(Robot, MessageType.Group, FromGroup, FromQq, RawEvent.Index,
            RawEvent.Msgid, RawEvent.Unix);

        /// <summary>
        ///     群聊回复消息
        /// </summary>
        /// <param name="at">选择是否at</param>
        /// <param name="msg">要发送的消息</param>
        public void ReplyAsGroupMessage(bool at, params object[] msg)
        {
            FromGroup.SendGroupMessage(at ? PlainMessage.At(FromQq) + "\n" : null, msg);
        }

        /// <summary>
        ///     私聊回复消息
        /// </summary>
        /// <param name="msg">要发送的消息</param>
        public void ReplyAsPrivateMessage(params object[] msg)
        {
            FromQq.SendPrivateMessage(msg);
        }
    }
}