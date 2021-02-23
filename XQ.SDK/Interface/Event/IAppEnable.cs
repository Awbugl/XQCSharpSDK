using XQ.SDK.XQ;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     应用初始化事件接口
    /// </summary>
    public interface IAppEnable : IXqEvent
    {
        /// <summary>
        ///     处理应用初始化事件
        /// </summary>
        void AppEnable(XqApi xqApi);
    }
}