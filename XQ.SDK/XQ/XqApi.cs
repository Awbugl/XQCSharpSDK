using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Newtonsoft.Json;

using XQ.SDK.Core;
using XQ.SDK.Enum;
using XQ.SDK.Model;
using XQ.SDK.Model.Json;
using XQ.SDK.Model.MessageObject;

namespace XQ.SDK.XQ
{
    /// <summary>
    /// XQApi
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class XqApi
    {
        private readonly byte[] _authid;

        public PluginInfo PluginInfo { get; private set; }

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="authid"></param>
        public XqApi(byte[] authid)
        {
            _authid = authid;
        }

        public void SetPluginInfo(PluginInfo info) => PluginInfo = info;

        #region QQApi

        /// <summary>
        /// 发送群消息
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="group">群号</param>
        /// <param name="anonymous">是否以匿名形式发送</param>
        /// <param name="messages">要发送的消息</param>
        public void SendGroupMessage(string robot, string group, bool anonymous = false, params object[] messages)
        {
            XqDll.SendMsgEX_V2(_authid, robot, 2, group, "", messages.ToSend(), 0, anonymous, "");
        }

        /// <summary>
        /// 发送私聊消息
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="qq">要发送的对象QQ</param>
        /// <param name="messagetype">私聊消息类型</param>
        /// <param name="messages">要发送的消息</param>
        /// <param name="group">群号，在群临时消息时填写</param>
        public void SendPrivateMessage(string robot, string qq, PrivateMessageType messagetype, string group = "", params object[] messages)
        {
            XqDll.SendMsgEX_V2(_authid, robot, (int)messagetype, group, qq, messages.ToSend(), 0, false, "");
        }

        /// <summary>
        /// 发送群聊json结构消息
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="group">群号</param>
        /// <param name="anonymous">是否以匿名形式发送</param>
        /// <param name="jsonMessage">Json结构内容</param>
        public void SendGroupJsonMessage(string robot, string group, string jsonMessage, bool anonymous = false)
        {
            XqDll.SendJSON(_authid, robot, anonymous ? 2 : 1, 2, group, "", jsonMessage);
        }

        /// <summary>
        /// 发送私聊json结构消息
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="qq">要发送的对象QQ</param>
        /// <param name="messagetype">私聊消息类型</param>
        /// <param name="jsonMessage">Json结构内容</param>
        public void SendPrivateJsonMessage(string robot, string qq, PrivateMessageType messagetype, string jsonMessage)
        {
            XqDll.SendJSON(_authid, robot, 1, (int)messagetype, "", qq, jsonMessage);
        }

        /// <summary>
        /// 发送群聊xml结构消息
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="group">群号</param>
        /// <param name="anonymous">是否以匿名形式发送</param>
        /// <param name="xmlMessage">Xml结构内容</param>
        public void SendGroupXmlMessage(string robot, string group, string xmlMessage, bool anonymous = false)
        {
            XqDll.SendXML(_authid, robot, anonymous ? 2 : 1, 2, group, "", xmlMessage);
        }

        /// <summary>
        /// 发送私聊xml结构消息
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="qq">要发送的对象QQ</param>
        /// <param name="messagetype">私聊消息类型</param>
        /// <param name="xmlMessage">Xml结构内容</param>
        public void SendPrivateXmlMessage(string robot, string qq, PrivateMessageType messagetype, string xmlMessage)
        {
            XqDll.SendXML(_authid, robot, 1, (int)messagetype, "", qq, xmlMessage);
        }

        /// <summary>
        ///     获取好友列表
        /// </summary>
        /// <param name="robot">botQQ</param>
        public List<Qq> GetFriendList(string robot)
        {
            try
            {
                return JsonConvert.DeserializeObject<FriendList>(XqDll.GetFriendList(_authid, robot).IntPtrToString()).GetList()
                    .Select(i => new Qq(this, robot, i.Id, i.Name, MessageType.Friend, null)).ToList();
            }
            catch
            {
                return XqDll.GetFriendList_B(_authid, robot).IntPtrToString().SplitToList()
                    .Select(i => new Qq(this, robot, i, MessageType.Friend, null)).ToList();
            }
        }


        /// <summary>
        ///     获取群列表
        /// </summary>
        /// <param name="robot">botQQ</param>
        public List<Group> GetGroupList(string robot)
        {
            try
            {
                var groupList =
                    JsonConvert.DeserializeObject<GroupList>(XqDll.GetGroupList(_authid, robot).IntPtrToString());

                var totalList = new List<GroupInfoJson>();

                if (groupList.JoinList != null && groupList.JoinList.Count > 0)
                {
                    totalList.AddRange(groupList.JoinList);
                }

                if (groupList.ManageList != null && groupList.ManageList.Count > 0)
                {
                    totalList.AddRange(groupList.ManageList);
                }

                if (groupList.CreateList != null && groupList.CreateList.Count > 0)
                {
                    totalList.AddRange(groupList.CreateList);
                }


                return totalList.Select(i => new Group(this, robot, i)).ToList();
            }
            catch
            {
                return XqDll.GetGroupList_B(_authid, robot).IntPtrToString().SplitToList()
                    .Select(i => new Group(this, robot, i)).ToList();
            }
        }


        /// <summary>
        ///     获取群列表B
        /// </summary>
        /// <param name="robot">botQQ</param>
        public List<Group> GetGroupList_B(string robot)
        {
            return XqDll.GetGroupList_B(_authid, robot).IntPtrToString().SplitToList()
                .Select(i => new Group(this, robot, i)).ToList();
        }


        /// <summary>
        /// 获取群成员信息列表
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="group">群号</param>
        public Dictionary<string, GroupMemberInfoJson> GetGroupMemberInfoList(string robot, string group)
        {
            return JsonConvert
                .DeserializeObject<GroupMemberList>(XqDll.GetGroupMemberList_B(_authid, robot, group).IntPtrToString())
                .Members;
        }

        /// <summary>
        /// 获取群成员列表
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="group">群号</param>
        public List<Qq> GetGroupMemberList(string robot, string group)
        {
            try
            {
                return GetGroupMemberInfoList(robot, group)
                    .Select(i => new Qq(this, robot, i.Key, i.Value.Name, MessageType.Group, group)).ToList();
            }
            catch
            {
                return JsonConvert
                    .DeserializeObject<GroupMemberListQqonly>(XqDll.GetGroupMemberList_C(_authid, robot, group)
                        .IntPtrToString())
                    .List.Select(i => new Qq(this, robot, i.Qq, MessageType.Group, group)).ToList();
            }
        }

        /// <summary>
        ///     获取群成员名片
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="group">群号</param>
        /// <param name="qq">群成员QQ</param>
        public string GetGroupCard(string robot, string group, string qq)
        {
            return XqDll.GetGroupCard(_authid, robot, group, qq).IntPtrToString();
        }

        /// <summary>
        ///     获取群管理员列表
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="group">群号</param>
        public List<Qq> GetAdminList(string robot, string group)
        {
            return XqDll.GetGroupAdmin(_authid, robot, group).IntPtrToString().SplitToList().Select(i => new Qq(this, robot, i, MessageType.Group, group)).ToList();
        }

        /// <summary>
        ///     获取群成员禁言状态
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="group">群号</param>
        /// <param name="qq">对象QQ</param>
        public bool IsShutUp(string robot, string group, string qq)
        {
            return XqDll.IsShutUp(_authid, robot, group, qq);
        }

        /// <summary>
        ///     是否是好友
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="qq">对象QQ</param>
        public bool IfFriend(string robot, string qq)
        {
            return XqDll.IfFriend(_authid, robot, qq);
        }

        /// <summary>
        ///     获取赞数量
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="qq">对象QQ</param>
        public int GetObjVote(string robot, string qq)
        {
            return XqDll.GetObjVote(_authid, robot, qq);
        }

        /// <summary>
        ///     获取图片下载链接
        ///     只能获取消息中得到的图片
        ///     从文件路径或url构造ImageMessage的无效
        /// </summary>
        /// <param name="imageType">消息类型</param>
        /// <param name="group">群号，在图片为群聊图片时填写</param>
        /// <param name="imageGuid">图片</param>
        public string GetPicLink(MessageType imageType, string group, ImageMessage imageGuid)
        {
            return XqDll.GetPicLink(_authid, imageGuid.RobotQq, System.Enum.IsDefined(typeof(PrivateMessageType), (int)imageType) ? 1 : 2, group, imageGuid.ToSendString()).IntPtrToString();
        }

        /// <summary>
        ///     取QQ昵称,可能为空字符串
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="qq">对象QQ</param>
        public string GetNick(string robot, string qq)
        {
            return XqDll.GetNick(_authid, robot, qq).IntPtrToString();
        }

        /// <summary>
        ///     取好友备注姓名,可能为空字符串
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="qq">对象QQ</param>
        public string GetFriendsRemark(string robot, string qq)
        {
            return XqDll.GetFriendsRemark(_authid, robot, qq).IntPtrToString();
        }

        /// <summary>
        ///     取指定的群名称
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="group">群号</param>
        public string GetGroupName(string robot, string group)
        {
            return XqDll.GetGroupName(_authid, robot, group).IntPtrToString();
        }

        /// <summary>
        ///     取当前群人数和群人数上限
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="group">群号</param>
        /// <returns>item1 : 当前群人数; item2 : 群人数上限</returns>
        /// 
        public (string, string) GetGroupMemberNum(string robot, string group)
        {
            var list = XqDll.GetGroupMemberNum(_authid, robot, group).IntPtrToString().SplitToList();
            return (list[0], list[1]);
        }

        /// <summary>
        ///     查询指定群是否允许匿名消息
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="group">群号</param>
        public bool GetAnon(string robot, string group)
        {
            return XqDll.GetAnon(_authid, robot, group);
        }

        /// <summary>
        ///     获取指定QQ个人资料的年龄
        ///     若未知返回255
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="qq">对象QQ</param>
        public int GetAge(string robot, string qq)
        {
            return XqDll.GetAge(_authid, robot, qq);
        }

        /// <summary>
        ///     获取指定QQ个人资料的性别
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="qq">对象QQ</param>
        public QqSex GetGender(string robot, string qq)
        {
            return (QqSex)XqDll.GetGender(_authid, robot, qq);
        }

        /// <summary>
        ///     上传图片
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="imageType">消息类型</param>
        /// <param name="groupOrQq">发送对象的群号或QQ</param>
        /// <param name="message">要上传的图片</param>
        public ImageMessage UpLoadPic(string robot, MessageType imageType, string groupOrQq, byte[] message)
        {
            return ImageMessage.GetFromMessage(this, robot, XqDll.UpLoadPic(_authid, robot, System.Enum.IsDefined(typeof(PrivateMessageType), (int)imageType) ? 1 : 2, groupOrQq, message).IntPtrToString()).FirstOrDefault();
        }

        /// <summary>
        ///     群禁言
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="group">群号</param>
        /// <param name="qq">对象QQ(为空则为全员禁言)</param>
        /// <param name="time">禁言时间(单位:秒)(0:解除禁言)</param>
        public void BanSpeak(string robot, string group, string qq, int time)
        {
            XqDll.ShutUP(_authid, robot, group, qq, time);
        }

        /// <summary>
        ///     修改群成员昵称
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="group">群号</param>
        /// <param name="qq">对象QQ</param>
        /// <param name="card"></param>
        public bool SetGroupCard(string robot, string group, string qq, string card)
        {
            return XqDll.SetGroupCard(_authid, robot, group, qq, card);
        }

        /// <summary>
        ///     群删除成员
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="group">群号</param>
        /// <param name="qq">对象QQ</param>
        /// <param name="notallowagain">指定是否不再允许接受申请入群</param>
        public void KickGroupMember(string robot, string group, string qq, bool notallowagain)
        {
            XqDll.KickGroupMBR(_authid, robot, group, qq, notallowagain);
        }

        /// <summary>
        ///     提取图片文字
        /// </summary>
        /// <param name="imageMessage">图片</param>
        public OcrItemCollection GetImageOcrResult(ImageMessage imageMessage)
        {
            return new OcrItemCollection(JsonConvert.DeserializeObject<OcrInfo>(XqDll.OcrPic(_authid, imageMessage.RobotQq, imageMessage.ToBytes()).IntPtrToString()).List);
        }

        /// <summary>
        ///     主动加群
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="group">群号</param>
        /// <param name="message">附加理由，可留空（需回答正确问题时，请填写问题答案</param>
        public void JoinGroup(string robot, string group, string message)
        {
            XqDll.JoinGroup(_authid, robot, group, message);
        }

        /// <summary>
        ///     处理好友添加请求
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="qq">对象QQ</param>
        /// <param name="type">处理类型</param>
        /// <param name="refuseMessage">拒绝时可填写拒绝理由</param>
        public void HandleFriendEvent(string robot, string qq, FriendRequestHandlerType type, string refuseMessage)
        {
            XqDll.HandleFriendEvent(_authid, robot, qq, (int)type, refuseMessage);
        }

        /// <summary>
        ///     处理群请求
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="requestType">213某人请求入群  214我被邀请加入某群  215某人被邀请加入群</param>
        /// <param name="qq">213申请入群QQ 214邀请人QQ 215被邀请人QQ</param>
        /// <param name="group">群号</param>
        /// <param name="seq">需要处理事件的seq</param>
        /// <param name="type">处理类型</param>
        /// <param name="refuseMessage">拒绝时可填写拒绝理由</param>
        public void HandleGroupEvent(string robot, int requestType, string qq, string group, string seq,
            GroupRequestHandlerType type, string refuseMessage)
        {
            XqDll.HandleGroupEvent(_authid, robot, requestType, qq, group, seq, (int)type, refuseMessage);
        }

        /// <summary>
        ///     删除指定好友
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="qq">对象QQ</param>
        public bool DeleteFriend(string robot, string qq)
        {
            return XqDll.DelFriend(_authid, robot, qq);
        }

        /// <summary>
        ///     修改好友备注名称
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="qq">对象QQ</param>
        /// <param name="remark">备注</param>
        public void SetFriendsRemark(string robot, string qq, string remark)
        {
            XqDll.SetFriendsRemark(_authid, robot, qq, remark);
        }

        /// <summary>
        ///     邀请好友加入群
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="group">群号</param>
        /// <param name="qq">对象QQ</param>
        public void InviteFriendInfoGroup(string robot, string group, string qq)
        {
            XqDll.InviteGroup(_authid, robot, group, qq);
        }

        /// <summary>
        ///     邀请群成员加入群
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="group">邀请到哪个群群号</param>
        /// <param name="groupY">被邀请成员所在群</param>
        /// <param name="qq">对象QQ</param>
        public bool InviteGroupMemberInfoGroup(string robot, string group, string groupY, string qq)
        {
            return XqDll.InviteGroupMember(_authid, robot, group, groupY, qq);
        }

        /// <summary>
        ///     创建群
        /// </summary>
        /// <param name="robot">botQQ</param>
        public string CreateGroup(string robot)
        {
            return XqDll.CreateDisGroup(_authid, robot).IntPtrToString();
        }

        /// <summary>
        ///     退出群
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="group">群号</param>
        public void QuitGroup(string robot, string group)
        {
            XqDll.QuitGroup(_authid, robot, group);
        }

        /// <summary>
        ///     屏蔽或接收某群消息
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="group">群号</param>
        /// <param name="openOrClose">True 则屏蔽 ，False 则取消屏蔽</param>
        public void SetShieldedGroup(string robot, string group, bool openOrClose)
        {
            XqDll.SetShieldedGroup(_authid, robot, group, openOrClose);
        }

        /// <summary>
        ///     设置机器人被添加好友时的验证方式
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="type">验证方式</param>
        public void SetCation(string robot, FriendAddRequestType type)
        {
            XqDll.Setcation(_authid, robot, (int)type);
        }

        /// <summary>
        ///     设置机器人被添加好友时的问题与答案
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="problem">设置的问题</param>
        /// <param name="answer">设置的问题答案 </param>
        public void SetCationWithQuestion(string robot, string problem, string answer)
        {
            XqDll.Setcation_problem_A(_authid, robot, problem, answer);
        }

        /// <summary>
        ///     设置机器人被添加好友时的三个可选问题
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="problem1">设置问题一</param>
        /// <param name="problem2">设置问题二</param>
        /// <param name="problem3">设置问题三</param>
        public void SetCationWithThreeQuestion(string robot, string problem1, string problem2, string problem3)
        {
            XqDll.Setcation_problem_B(_authid, robot, problem1, problem2, problem3);
        }

        /// <summary>
        ///     主动添加好友
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="qq">对象QQ</param>
        /// <param name="message">验证消息</param>
        public bool AddFriend(string robot, string qq, string message)
        {
            return XqDll.AddFriend(_authid, robot, qq, message, 1);
        }

        /// <summary>
        ///     上传silk语音文件
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="type">消息类型</param>
        /// <param name="message">要上传的语音</param>
        /// <param name="groupOrQq">发送对象的群号或QQ</param>
        public VoiceMessage UpLoadVoice(string robot, MessageType type, string groupOrQq, byte[] message)
        {
            return VoiceMessage.GetFromMessage(this, robot,
                XqDll.UpLoadVoice(_authid, robot, System.Enum.IsDefined(typeof(PrivateMessageType), (int)type) ? 1 : 2, groupOrQq, message).IntPtrToString());
        }

        /// <summary>
        ///     语音信息的下载链接(silk格式)
        /// </summary>
        /// <param name="message">语音消息</param>
        public string GetVoiLink(VoiceMessage message)
        {
            return XqDll.GetVoiLink(_authid, message.RobotQq, message.ToSendString()).IntPtrToString();
        }

        /// <summary>
        ///   语音信息转换为文本内容
        /// </summary>
        /// <param name="groupOrQq">发送对象的群号或QQ</param>
        /// <param name="type">消息类型</param>
        /// <param name="obj">语音消息</param>
        /// <returns></returns>
        public string VoiToText(string groupOrQq, MessageType type, VoiceMessage obj)
        {
            return XqDll.VoiToText(_authid, obj.RobotQq, groupOrQq,
                System.Enum.IsDefined(typeof(PrivateMessageType), (int)type) ? 1 : 2, obj.ToSendString()).IntPtrToString();
        }

        /// <summary>
        ///     开关群匿名功能
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="group">群号</param>
        /// <param name="openOrClose">True 则开启群匿名 ，False 则关闭群匿名</param>
        public bool SetAnon(string robot, string group, bool openOrClose)
        {
            return XqDll.SetAnon(_authid, robot, group, openOrClose);
        }

        /// <summary>
        ///     修改机器人自身头像
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="pic">图片文件</param>
        public bool SetHeadPic(string robot, byte[] pic)
        {
            return XqDll.SetHeadPic(_authid, robot, pic);
        }

        /// <summary>
        ///     向好友发送窗口抖动消息
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="qq">对象QQ</param>
        public bool ShakeWindow(string robot, string qq)
        {
            return XqDll.ShakeWindow(_authid, robot, qq);
        }

        /// <summary>
        ///     撤回群消息或者私聊消息
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="type">消息类型</param>
        /// <param name="group">群号</param>
        /// <param name="qq">对象QQ</param>
        /// <param name="messageNumber">需撤回消息序号</param>
        /// <param name="messageId">需撤回消息ID</param>
        /// <param name="messageTime">需撤回消息unix时间戳</param>
        /// <returns></returns>
        public string WithdrawMsg(string robot, MessageType type, string group, string qq, string messageNumber,
            string messageId, string messageTime)
        {
            return XqDll.WithdrawMsgEX(_authid, robot, (int)type, group, qq, messageNumber,
                messageId, messageTime).IntPtrToString();
        }

        #endregion

        #region FrameApi

        /// <summary>
        ///     获取插件配置文件路径
        /// </summary>
        public string GetConfigPath() => AppContext.BaseDirectory + $@"\Config\{PluginInfo.Name}\";

        /// <summary>
        ///     标记函数执行流程 debug时使用 每个函数内只需要调用一次
        /// </summary>
        /// <param name="message">标记内容</param>
        public void DebugName(string message)
        {
            XqDll.DbgName(_authid, message);
        }

        /// <summary>
        ///     函数内标记附加信息 函数内可多次调用
        /// </summary>
        /// <param name="message">标记内容</param>
        public void Mark(string message)
        {
            XqDll.Mark(_authid, message);
        }

        /// <summary>
        ///     输出日志 (在框架中显示)
        /// </summary>
        /// <param name="message">日志内容</param>
        public void OutPutLogToFrame(string message)
        {
            XqDll.OutPutLog(_authid, message);
        }

        /// <summary>
        ///     取机器人账号在线信息
        /// </summary>
        /// <param name="robot">botQQ</param>
        public RobotInfo GetRobotInfo(string robot)
        {
            return JsonConvert.DeserializeObject<RobotInfo>(XqDll.GetRInf(_authid, robot).IntPtrToString());
        }

        /// <summary>
        ///     修改机器人账号在线状态
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="onLineType">在线状态类型</param>
        public void SetOnlineStatus(string robot, OnlineStatusType onLineType)
        {
            XqDll.SetRInf(_authid, robot, $"{(int)onLineType}", "");
        }

        /// <summary>
        ///     修改机器人账号个性签名
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="signature">个性签名</param>
        public void SetSignature(string robot, string signature)
        {
            XqDll.SetRInf(_authid, robot, "8", signature);
        }

        /// <summary>
        ///     修改机器人账号性别
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <param name="type">性别</param>
        public void SetSex(string robot, QqSex type)
        {
            XqDll.SetRInf(_authid, robot, "9", $"{(int)type}");
        }

        /// <summary>
        ///     主动卸载插件自身
        /// </summary>
        public bool Uninstall()
        {
            return XqDll.Uninstall(_authid);
        }

        /// <summary>
        ///     重新从Plugin目录下载入本插件(一般用做自动更新)
        /// </summary>
        public bool Reload()
        {
            return XqDll.Reload(_authid);
        }

        /// <summary>
        ///     登录指定QQ
        /// </summary>
        /// <param name="robot">botQQ</param>
        public void LoginQq(string robot)
        {
            XqDll.LoginQQ(_authid, robot);
        }

        /// <summary>
        ///     离线指定QQ
        /// </summary>
        /// <param name="robot">botQQ</param>
        public void OffLineQq(string robot)
        {
            XqDll.OffLineQQ(_authid, robot);
        }

        /// <summary>
        ///     获取机器人在线账号列表
        /// </summary>
        public List<string> GetOnlineList()
        {
            return XqDll.GetOnLineList(_authid).IntPtrToString().SplitToList();
        }

        /// <summary>
        ///     获取机器人账号是否在线
        /// </summary>
        /// <param name="robot">botQQ</param>
        public bool GetBotIsOnline(string robot)
        {
            return XqDll.GetBotIsOnline(_authid, robot);
        }

        /// <summary>
        ///     取插件是否启用
        /// </summary>
        /// <returns></returns>
        public bool IsEnable()
        {
            return XqDll.IsEnable(_authid);
        }

        /// <summary>
        ///     取所有robot列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetrobotList()
        {
            return XqDll.GetQQList(_authid).IntPtrToString().SplitToList();
        }

        /// <summary>
        /// 取登录二维码base64
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetQrCode(byte[] key)
        {
            return XqDll.GetQrCode(_authid, key).IntPtrToString();
        }

        /// <summary>
        /// 检查登录二维码状态
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int CheckQrCode(byte[] key)
        {
            return XqDll.CheckQrCode(_authid, key);
        }

        #endregion

        #region PsKey Cookies Clientkey Api

        /// <summary>
        ///     取得QQ群页面操作用参数P_skey
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <example> "; p_uin=o{robot}; p_skey={p_skeyvalue}" </example>
        /// <returns></returns>
        public string GetGroupPsKey(string robot)
        {
            return XqDll.GetGroupPsKey(_authid, robot).IntPtrToString();
        }

        /// <summary>
        ///     取得QQ空间页面操作用参数P_skey
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <example> "; p_uin=o{robot}; p_skey={p_skeyvalue}" </example>
        /// <returns></returns>
        public string GetZonePsKey(string robot)
        {
            return XqDll.GetZonePsKey(_authid, robot).IntPtrToString();
        }

        /// <summary>
        ///     取得机器人网页操作用的Cookies
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <example> "uin=o{robot}; skey={skeyvalue}" </example>
        /// <returns></returns>
        public string GetCookies(string robot)
        {
            return XqDll.GetCookies(_authid, robot).IntPtrToString();
        }

        /// <summary>
        ///     取短Clientkey
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <returns>16进制字符串</returns>
        public string GetClientkey(string robot)
        {
            return XqDll.GetClientkey(_authid, robot).IntPtrToString();
        }

        /// <summary>
        ///     取得机器人网页操作用的长Clientkey
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <returns>16进制字符串</returns>
        public string GetLongClientkey(string robot)
        {
            return XqDll.GetLongClientkey(_authid, robot).IntPtrToString();
        }

        /// <summary>
        ///     取得腾讯课堂页面操作用参数P_skey
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <example> "; p_uin=o{robot}; p_skey={p_skeyvalue}" </example>
        /// <returns></returns>
        public string GetClassRoomPsKey(string robot)
        {
            return XqDll.GetClassRoomPsKey(_authid, robot).IntPtrToString();
        }

        /// <summary>
        ///     取得QQ举报页面操作用参数P_skey
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <example> "; p_uin=o{robot}; p_skey={p_skeyvalue}" </example>
        /// <returns></returns>
        public string GetRepPsKey(string robot)
        {
            return XqDll.GetRepPsKey(_authid, robot).IntPtrToString();
        }

        /// <summary>
        ///     取得财付通页面操作用参数P_skey
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <example> "; p_uin=o{robot}; p_skey={p_skeyvalue}" </example>
        /// <returns></returns>
        public string GetTenPayPsKey(string robot)
        {
            return XqDll.GetTenPayPsKey(_authid, robot).IntPtrToString();
        }

        /// <summary>
        ///     取bkn
        /// </summary>
        /// <param name="robot">botQQ</param>
        /// <returns>bkn （一串数字）</returns>
        public string GetBkn(string robot)
        {
            return XqDll.GetBkn(_authid, robot).IntPtrToString();
        }

        #endregion
    }
}
