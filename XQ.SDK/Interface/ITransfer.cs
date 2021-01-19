using XQ.SDK.EventArgs;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     收到财付通转账事件接口
    /// </summary>
    public interface ITransfer : IXqEvent
    {
        /// <summary>
        ///     处理收到财付通转账事件
        /// </summary>
        void Transfer(TransferEventArgs e);
    }
}