using XQ.SDK.EventArgs;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     申请入群事件接口
    /// </summary>
    public interface IJoinGroupRequest : IXqEvent
    {
        /// <summary>
        ///     处理申请入群事件
        /// </summary>
        void JoinGroupRequest(JoinGroupRequestEventArgs e);
    }
}