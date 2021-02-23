using XQ.SDK.EventArgs;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     群聊事件事件接口
    /// </summary>
    public interface IGroupEvent : IXqEvent
    {
        /// <summary>
        ///     处理群聊事件事件
        /// </summary>
        void GroupEvent(GroupEventEventArgs e);
    }
}