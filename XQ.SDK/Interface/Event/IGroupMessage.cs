using XQ.SDK.EventArgs;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     群聊消息事件接口
    /// </summary>
    public interface IGroupMessage : IXqEvent
    {
        /// <summary>
        ///     处理群聊消息事件
        /// </summary>
        void GroupMessage(GroupMessageEventArgs e);
    }
}