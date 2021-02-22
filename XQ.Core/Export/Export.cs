using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

using XQ.SDK.Enum;
using XQ.SDK.Enum.Event;
using XQ.SDK.EventArgs;
using XQ.SDK.Interface;
using XQ.SDK.Model;
using static XQ.Global.Global;

namespace XQ.Core.Export
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class Export
    {
        [DllExport(ExportName = "XQ_Create", CallingConvention = CallingConvention.StdCall)]
        public static string XQ_Create(string frameworkVersion)
        {
            try
            {
                return Create();
            }
            catch (Exception e)
            {
                ExceptionReport(e, false);
                return "";
            }
        }


        [DllExport(ExportName = "XQ_AuthId", CallingConvention = CallingConvention.StdCall)]
        public static void XQ_AuthId(int id, int addr)
        {
            try
            {
                AuthId(id, addr);
            }
            catch (Exception e)
            {
                ExceptionReport(e, false);
            }
        }

        [DllExport(ExportName = "XQ_DestroyPlugin", CallingConvention = CallingConvention.StdCall)]
        public static int XQ_DestroyPlugin()
        {
            try
            {
                Destroy();
                AppDomain.CreateDomain("Unload").DoCallBack(() => AppDomain.Unload(AppDomain.CurrentDomain));
                return 1;
            }
            catch (Exception e)
            {
                ExceptionReport(e, false);
                return 1;
            }
        }

        [DllExport(ExportName = "XQ_SetUp", CallingConvention = CallingConvention.StdCall)]
        public static int XQ_SetUp()
        {
            return SetUp();
        }

        [DllExport(ExportName = "XQ_Event", CallingConvention = CallingConvention.StdCall)]
        public static int XQ_Event(string robotQq, int eventType, int extraType, string fromGroup, string fromQq,
            string targetQq, string content, string index, string msgid, string udpmsg, string unix, int p)
        {
            return Event(robotQq, eventType, extraType, fromGroup, fromQq, targetQq, content, index, msgid,
                    udpmsg, unix, p);
        }

        public static string Create()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => ExceptionReport(e.ExceptionObject as Exception);

            var info = ExportMain.AddRegister() ?? new PluginInfo("XQCSharpSDKTest", "littlenine12", "1.0.0", "未找到插件");

            SetInfo(info);

            if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);

            return info.GetJson();
        }

        public static void AuthId(int id, int addr)
        {
            SetApi(BitConverter.GetBytes(id).Concat(BitConverter.GetBytes(addr)).ToArray());
        }

        public static void Destroy()
        {
            try
            {
                if (Init.Container.CanResolve<IPluginDestroy>())
                    Init.Container.Resolve<IPluginDestroy>().PluginDestroy(Global.Global.XqApi);
            }
            catch (Exception e)
            {
                ExceptionReport(e, false);
            }
        }

        public static int SetUp()
        {
            try
            {
                if (Init.Container.CanResolve<ICallMenu>())
                    Init.Container.Resolve<ICallMenu>().CallMenu(Global.Global.XqApi);
                return 0;
            }
            catch (Exception e)
            {
                ExceptionReport(e);
                return 1;
            }
        }

        /// <summary>
        ///     XQ插件事件,此子程序会分发先驱机器人QQ接收到的所有：事件，消息；您可在此函数中自行调用所有参数
        /// </summary>
        /// <param name="robotQq">机器人QQ,多Q版用于判定哪个QQ接收到该消息</param>
        /// <param name="eventType">消息类型,接收到消息类型，该类型可在常量表中查询具体定义</param>
        /// <param name="extraType">消息子类型,此参数在不同消息类型下，有不同的定义，暂定：接收财付通转账时 1为好友 4为群临时会话 5为讨论组临时会话 有人请求入群时，不良成员这里为1</param>
        /// <param name="from">XQ_消息来源,此消息的来源，如：群号、讨论组ID、临时会话QQ、好友QQ等</param>
        /// <param name="fromQq">XQ_触发对象_主动,主动发送这条消息的QQ，踢人时为踢人管理员QQ</param>
        /// <param name="targetQq">XQ_触发对象_被动,被动触发的QQ，如某人被踢出群，则此参数为被踢出人QQ</param>
        /// <param name="content">XQ_消息内容,此参数有多重含义，常见为：对方发送的消息内容，但当XQ_消息类型为 某人申请入群，则为入群申请理由</param>
        /// <param name="index">XQ_消息序号,此参数暂定用于消息回复，消息撤回</param>
        /// <param name="msgid">XQ_消息ID,此参数暂定用于消息回复，消息撤回</param>
        /// <param name="udpmsg">XQ_原始信息,UDP收到的原始信息，特殊情况下会返回JSON结构（入群事件时，这里为该事件seq）</param>
        /// <param name="unix">消息时间戳,接受到消息的时间戳</param>
        /// <param name="p">回传文本指针,此参数用于插件加载拒绝理由  用法：写到内存（“拒绝理由”，XQ_回传文本指针，255） ‘最大写入字节数量为255超过此长度可能导致插件异常崩溃</param>
        /// <returns></returns>
        public static int Event(string robotQq, int eventType, int extraType, string from, string fromQq,
            string targetQq, string content, string index, string msgid, string udpmsg, string unix, int p)
        {
            var args = new XqRawEvent(robotQq, eventType, extraType, from, fromQq, targetQq, content, index, msgid,
                udpmsg, unix, p);

            XqEventArgs eventargs = null;

            try
            {
                switch (eventType)
                {
                    case 0:
                    case 1:
                    case 4:
                    case 5:
                    case 7: //Private
                        if (!Init.Container.CanResolve<IPrivateMessage>())
                            return (int)XqEventReturnType.Ignore;
                        eventargs = new PrivateMessageEventArgs(Global.Global.XqApi, args);
                        Init.Container.Resolve<IPrivateMessage>()
                            .PrivateMessage((PrivateMessageEventArgs)eventargs);
                        break;

                    case 2: // Group:
                        if (!Init.Container.CanResolve<IGroupMessage>())
                            return (int)XqEventReturnType.Ignore;
                        eventargs = new GroupMessageEventArgs(Global.Global.XqApi, args);
                        Init.Container.Resolve<IGroupMessage>()
                            .GroupMessage((GroupMessageEventArgs)eventargs);
                        break;

                    case 6: //Transfer
                        if (!Init.Container.CanResolve<ITransfer>())
                            return (int)XqEventReturnType.Ignore;
                        eventargs = new TransferEventArgs(Global.Global.XqApi, args);
                        Init.Container.Resolve<ITransfer>()
                            .Transfer((TransferEventArgs)eventargs);
                        break;

                    case 9: // WithDraw
                        if (!Init.Container.CanResolve<IWithDrawMessage>())
                            return (int)XqEventReturnType.Ignore;
                        eventargs = new WithDrawMessageEventArgs(Global.Global.XqApi, args);
                        Init.Container.Resolve<IWithDrawMessage>()
                            .WithDrawMessage((WithDrawMessageEventArgs)eventargs);
                        break;

                    case 10: // GroupEcho
                        if (!Init.Container.CanResolve<IGroupEchoMessage>())
                            return (int)XqEventReturnType.Ignore;
                        eventargs = new GroupEchoMessageEventArgs(Global.Global.XqApi, args);
                        Init.Container.Resolve<IGroupEchoMessage>()
                            .GroupEchoMessage((GroupEchoMessageEventArgs)eventargs);
                        break;

                    case 101: // AddFriendRequest
                        if (!Init.Container.CanResolve<IFriendAddRequest>())
                            return (int)XqEventReturnType.Ignore;
                        eventargs = new FriendAddRequestEventArgs(Global.Global.XqApi, args);
                        Init.Container.Resolve<IFriendAddRequest>()
                            .FriendAddRequest((FriendAddRequestEventArgs)eventargs);
                        break;

                    case 213: // JoinGroupRequest
                    case 215: // SomeoneBeInvitedToGroup
                        if (!Init.Container.CanResolve<IJoinGroupRequest>())
                            return (int)XqEventReturnType.Ignore;
                        eventargs = new JoinGroupRequestEventArgs(Global.Global.XqApi, args);
                        Init.Container.Resolve<IJoinGroupRequest>()
                            .JoinGroupRequest((JoinGroupRequestEventArgs)eventargs);
                        break;

                    case 214: // BeInvitedToGroup
                        if (!Init.Container.CanResolve<IBeInvitedToGroup>())
                            return (int)XqEventReturnType.Ignore;
                        eventargs = new BeInvitedToGroupEventArgs(Global.Global.XqApi, args);
                        Init.Container.Resolve<IBeInvitedToGroup>()
                            .BeInvitedToGroupRequest((BeInvitedToGroupEventArgs)eventargs);
                        break;

                    case 12001: // PluginEnabled
                        if (!Init.Container.CanResolve<IAppEnable>())
                            return (int)XqEventReturnType.Ignore;
                        new Thread(() =>
                        {
                            Thread.Sleep(1000);
                            Init.Container.Resolve<IAppEnable>().AppEnable(Global.Global.XqApi);
                        }).Start();
                        break;

                    case 12002: // PluginDisabled
                        if (!Init.Container.CanResolve<IAppDisable>())
                            return (int)XqEventReturnType.Ignore;
                        Init.Container.Resolve<IAppDisable>().AppDisable(Global.Global.XqApi);
                        break;

                    default:

                        if (Enum.IsDefined(typeof(FriendEventEventType), eventType))
                        {
                            if (!Init.Container.CanResolve<IFriendEvent>())
                                return (int)XqEventReturnType.Ignore;
                            eventargs = new FriendEventArgs(Global.Global.XqApi, args);
                            Init.Container.Resolve<IFriendEvent>().FriendEvent((FriendEventArgs)eventargs);
                        }

                        if (Enum.IsDefined(typeof(GroupEventEventType), eventType))
                        {
                            if (!Init.Container.CanResolve<IGroupEvent>())
                                return (int)XqEventReturnType.Ignore;
                            eventargs = new GroupEventEventArgs(Global.Global.XqApi, args);
                            Init.Container.Resolve<IGroupEvent>().GroupEvent((GroupEventEventArgs)eventargs);
                        }

                        if (Enum.IsDefined(typeof(RobotEventEventType), eventType))
                        {
                            if (!Init.Container.CanResolve<IRobotEvent>())
                                return (int)XqEventReturnType.Ignore;
                            eventargs = new RobotEventEventArgs(Global.Global.XqApi, args);
                            Init.Container.Resolve<IRobotEvent>().RobotEvent((RobotEventEventArgs)eventargs);
                        }

                        if (Enum.IsDefined(typeof(FrameEventEventType), eventType))
                        {
                            if (!Init.Container.CanResolve<IFrameEvent>())
                                return (int)XqEventReturnType.Ignore;
                            eventargs = new FrameEventArgs(Global.Global.XqApi, args);
                            Init.Container.Resolve<IFrameEvent>().FrameEvent((FrameEventArgs)eventargs);
                        }

                        break;
                }

                return (int)(eventargs?.Handler == true
                    ? XqEventReturnType.Intercept
                    : XqEventReturnType.Ignore);
            }
            catch (Exception ex)
            {
                ExceptionReport(ex);
                return (int)XqEventReturnType.Ignore;
            }
        }
    }
}