using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    /// <summary>
    ///     XQ事件的抽象基类
    ///     对应IXqEvent接口
    /// </summary>
    public abstract class XqEventArgs
    {
        /// <summary>
        ///     事件构造函数
        /// </summary>
        /// <param name="xqApi">XQApi</param>
        /// <param name="rawEvent">XQEvent的原始参数</param>
        protected XqEventArgs(XqApi xqApi, XqRawEvent rawEvent)
        {
            XqApi = xqApi;
            RawEvent = rawEvent;
        }

        /// <summary>
        ///     获取XQApi
        /// </summary>
        public XqApi XqApi { get; }

        /// <summary>
        ///     获取XQEvent的原始参数
        /// </summary>
        public XqRawEvent RawEvent { get; }

        /// <summary>
        ///     获取或设置一个值, 指示该事件是否允许其他插件处理
        /// </summary>
        public bool Handler { get; set; }

        /// <summary>
        ///     获取接收到当前事件的Robot实例
        ///     部分事件中为null
        /// </summary>
        public Robot Robot => string.IsNullOrWhiteSpace(RawEvent.RobotQq) ? null : new Robot(XqApi, RawEvent.RobotQq);
    }
}