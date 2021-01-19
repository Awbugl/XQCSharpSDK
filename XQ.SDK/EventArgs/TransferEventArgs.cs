using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    /// <summary>
    ///     收到财付通转账事件
    ///     对应ITransfer接口
    /// </summary>
    public class TransferEventArgs : FriendEventArgs
    {
        /// <summary>
        ///     事件构造函数
        /// </summary>
        /// <param name="xqApi">XQApi</param>
        /// <param name="rawEvent">XQEvent的原始参数</param>
        public TransferEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }
    }
}