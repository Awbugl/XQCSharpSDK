using XQ.SDK.EventArgs;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     框架Robot事件事件接口
    /// </summary>
    public interface IRobotEvent : IXqEvent
    {
        /// <summary>
        ///     处理框架Robot相关事件
        /// </summary>
        void RobotEvent(RobotEventEventArgs e);
    }
}