using XQ.SDK.EventArgs;

namespace XQ.SDK.Interface
{
    public interface ITransferEvent : IProcess
    {
        /// <summary>
        ///     当在派生类中重写时, 处理 收到财付通转账事件 回调
        ///     应在此函数内释放资源及线程
        /// </summary>
        void Transfer(XqTransferEventArgs e);
    }
}