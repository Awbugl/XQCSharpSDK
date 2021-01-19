using XQ.SDK.Enum;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    /// <summary>
    ///     群回音消息事件
    ///     对应IGroupEchoMessage接口
    /// </summary>
    public class GroupEchoMessageEventArgs : XqEventArgs
    {
        /// <summary>
        ///     事件构造函数
        /// </summary>
        /// <param name="xqApi">XQApi</param>
        /// <param name="rawEvent">XQEvent的原始参数</param>
        public GroupEchoMessageEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        /// <summary>
        ///     事件来源群聊
        /// </summary>
        public Group FromGroup =>
            string.IsNullOrWhiteSpace(RawEvent.From) ? null : new Group(XqApi, Robot, RawEvent.From);

        /// <summary>
        ///     撤回消息
        /// </summary>
        public void WithdrawMessage()
        {
            XqApi.WithdrawMsg(Robot, MessageType.Group, FromGroup, Robot, RawEvent.Index, RawEvent.Msgid,
                RawEvent.Unix);
        }
    }
}