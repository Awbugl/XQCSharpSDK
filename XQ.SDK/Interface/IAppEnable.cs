using XQ.SDK.XQ;

namespace XQ.SDK.Interface
{
    /// <summary>
    ///     应用初始化事件接口
    /// </summary>
    public interface IAppEnable : IProcess
    {
        /// <summary>
        ///     当在派生类中重写时, 处理 应用初始化事件 回调
        /// </summary>
        void AppEnable(XqApi xqApi);
    }
}