using XQ.SDK.EventArgs;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     群聊消息事件接口
    /// </summary>
    public interface IGroupMessage : IProcess
    {
        /// <summary>
        ///     当在派生类中重写时, 处理 群聊消息事件 回调
        /// </summary>
        /// <param name="e">附加的事件参数</param>
        void GroupMessage(XqGroupMessageEventArgs e);
    }
}