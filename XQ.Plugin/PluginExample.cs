using XQ.SDK.EventArgs;
using XQ.SDK.Interface;

namespace XQ.Plugin
{
    /// <summary>
    ///     这里是插件逻辑部分，需要处理什么事件就继承接口
    ///     需要在XQ.Main.ExportMain.cs 中的AddRegister函数中注册插件
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
            e.SendGroupMessage(e.Text);
        }

        public void PrivateMessage(XqPrivateMessageEventArgs e)
        {
        }
    }
}