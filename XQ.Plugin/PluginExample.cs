using XQ.SDK.Enum;
using XQ.SDK.Enum.Event;
using XQ.SDK.EventArgs;
using XQ.SDK.Interface;
using XQ.SDK.XQ;

namespace XQ.Plugin
{
    
    internal static class Global
    {
        public static bool LoadCompleted => Api != null;

        public static XqApi Api { get; set; }
    }


    public class MyPlugin : IPrivateMessage //举例 ： 此处 MyPlugin 未在 XQ.Core.ExportMain.cs 中的AddRegister函数中注册，故不会被调用
    {
        public void PrivateMessage(PrivateMessageEventArgs e)
        {
        }
    }

    /// <summary>
    ///     此处为插件例子
    ///     这里是插件逻辑部分，需要处理什么事件就继承接口
    ///     也可自行新建项目，并在XQ.Core中添加您的新项目的引用
    ///     需要在XQ.Core.ExportMain.cs 中的AddRegister函数中注册插件
    ///     由于托管环境限制和XQ插件缓存机制，更新插件后(无论之前是否加载成功)请重启XQ框架
    /// </summary>
    public class PluginExample : IPrivateMessage, IGroupMessage, IGroupEvent, IBeInvitedToGroup, IJoinGroupRequest, IAppEnable
    //举例 ： 此处 PluginExample 的  IBeInvitedToGroup, IJoinGroupRequest 未在 XQ.Core.ExportMain.cs 中的AddRegister函数中注册，故不会被调用
    {
        public void GroupEvent(GroupEventEventArgs e)
        {
            switch (e.Type) //GroupEvent处理方式举例
            {
                case GroupEventEventType.SomeoneLeaveGroup:
                    e.FromGroup.SendGroupMessage(false, "某人离开群聊。");
                    return;

                case GroupEventEventType.SomeoneBeRemovedFromGroup:
                    e.FromGroup.SendGroupMessage(false, "某人被移除群聊。");
                    return;

                default:
                    return;
            }
        }

        public void BeInvitedToGroupRequest(BeInvitedToGroupEventArgs e)
        {
            e.Handle(GroupRequestHandlerType.Refuse); //举例： 机器人被邀请入群时，自动拒绝
        }

        public void JoinGroupRequest(JoinGroupRequestEventArgs e)
        {
            e.Handle(GroupRequestHandlerType.Agree); //举例： 收到入群申请时，自动同意
        }

        public void GroupMessage(GroupMessageEventArgs e) //群聊消息处理方式举例
        {
            e.ReplyAsGroupMessage(false, false, e.Message); //复读消息

            if (Global.LoadCompleted)
                Global.Api.SendPrivateMessage(
                      e.Robot, "123456789", PrivateMessageType.Friend, "",
                      $"收到群{e.FromGroup}消息\n{e.Message}");//向某人发送消息
            e.Handler = false; //不允许优先级插件处理
        }

        public void PrivateMessage(PrivateMessageEventArgs e) //私聊消息处理方式举例
        {
            e.ReplyPrivateMessage("收到消息。");
        }

        public void AppEnable(XqApi xqApi) //插件初始化
        {
            Global.Api = xqApi;
            //一般进行初始化操作
            //注意 此时QQ可能尚未登录
        }
    }
}