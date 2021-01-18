using XQ.SDK.EventArgs;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     框架Robot相关事件接口
    /// </summary>
    public interface IRobotEvent : IProcess
    {
        /// <summary>
        ///     当在派生类中重写时, 框架Robot相关事件 回调
        /// </summary>
        /// <param name="e">附加的事件参数</param>
        void RobotEvent(XqFrameRobotQqEventArgs e);
    }
}