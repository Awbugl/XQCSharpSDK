using XQ.SDK.Enum;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    /// <summary>
    ///     撤回消息事件
    ///     对应IWithDrawMessage接口
    /// </summary>
    public class WithDrawMessageEventArgs : XqMessageEventArgs
    {
        /// <summary>
        ///     事件构造函数
        /// </summary>
        /// <param name="xqApi">XQApi</param>
        /// <param name="rawEvent">XQEvent的原始参数</param>
        public WithDrawMessageEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        /// <summary>
        ///     事件类型
        /// </summary>
        public new WithdrawMessageType Type => (WithdrawMessageType) RawEvent.ExtraType;
    }
}