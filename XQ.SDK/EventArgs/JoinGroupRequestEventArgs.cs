using XQ.SDK.Enum;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    /// <summary>
    ///     申请入群事件
    ///     对应IJoinGroupRequest接口
    /// </summary>
    public class JoinGroupRequestEventArgs : GroupEventEventArgs
    {
        /// <summary>
        ///     事件构造函数
        /// </summary>
        /// <param name="xqApi">XQApi</param>
        /// <param name="rawEvent">XQEvent的原始参数</param>
        public JoinGroupRequestEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        /// <summary>
        ///     处理申请入群请求
        /// </summary>
        /// <param name="type">处理类型</param>
        /// <param name="refuseMessage">拒绝时可填写拒绝理由</param>
        public void Handle(GroupRequestHandlerType type, string refuseMessage = "")
        {
            XqApi.HandleGroupEvent(Robot, (int)Type, FromQq, FromGroup, RawEvent.Udpmsg, type,
                refuseMessage);
        }
    }
}