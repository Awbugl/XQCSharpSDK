using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.SDK.EventArgs
{
    /// <summary>
    ///     XQ事件的基类
    /// </summary>
    public class XqEventArgs
    {
        public XqEventArgs(XqApi xqApi, XqRawEvent rawEvent)
        {
            XqApi = xqApi;
            RawEvent = rawEvent;
        }

        /// <summary>
        ///     获取QQApi
        /// </summary>
        public XqApi XqApi { get; }

        /// <summary>
        ///     获取XQEvent的全部参数
        /// </summary>
        public XqRawEvent RawEvent { get; }

        /// <summary>
        ///     获取或设置一个值, 指示该事件是否允许其他插件处理
        /// </summary>
        public bool Handler { get; set; }

        public Robot Robot => RawEvent.RobotQq == null ? null : new Robot(XqApi, RawEvent.RobotQq);
    }
}