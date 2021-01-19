using XQ.SDK.EventArgs;
using XQ.SDK.Interface;

namespace XQ.Plugin
{
    /// <summary>
    ///     此处为插件例子
    ///     这里是插件逻辑部分，需要处理什么事件就继承接口
    ///     需要在XQ.Core.ExportMain.cs 中的AddRegister函数中注册插件
    ///     由于托管环境限制和XQ插件缓存机制，更新插件后(无论之前是否加载成功)请重启XQ框架
    /// </summary>
    public class PluginExample : IPrivateMessage, IGroupMessage, IGroupEvent, IGroupAddRequest
    {
        public void GroupAddRequest(XqGroupAddEventArgs e)
        {
        }

        public void GroupEvent(XqGroupEventArgs e)
        {
        }

        public void GroupMessage(XqGroupMessageEventArgs e)
        {
        }

        public void PrivateMessage(XqPrivateMessageEventArgs e)
        {
        }
    }
}