using XQ.SDK.EventArgs;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     框架事件接口
    /// </summary>
    public interface IFrameEvent : IXqEvent
    {
        /// <summary>
        ///     处理框架事件
        /// </summary>
        void FrameEvent(FrameEventArgs e);
    }
}