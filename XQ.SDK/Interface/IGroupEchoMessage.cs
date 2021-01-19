using XQ.SDK.EventArgs;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     群回音消息事件接口
    /// </summary>
    public interface IGroupEchoMessage : IXqEvent
    {
        /// <summary>
        ///     处理群回音消息事件
        /// </summary>
        void GroupEchoMessage(GroupEchoMessageEventArgs e);
    }
}