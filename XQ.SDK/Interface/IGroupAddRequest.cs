using XQ.SDK.EventArgs;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     被邀请入群事件接口
    /// </summary>
    public interface IGroupAddRequest : IProcess
    {
        /// <summary>
        ///     当在派生类中重写时, 处理 被邀请入群事件 回调
        /// </summary>
        /// <param name="e">附加的事件参数</param>
        void GroupAddRequest(XqGroupAddEventArgs e);
    }
}