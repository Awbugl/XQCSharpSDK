using XQ.SDK.EventArgs;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     撤回消息事件接口
    /// </summary>
    public interface IWithDrawMessage : IXqEvent
    {
        /// <summary>
        ///     处理撤回消息事件
        /// </summary>
        void WithDrawMessage(WithDrawMessageEventArgs e);
    }
}