using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using XQ.SDK.Core;
using XQ.SDK.Enum;
using XQ.SDK.Enum.Event;
using XQ.SDK.Model;
using XQ.SDK.XQ.Json;

namespace XQ.SDK.XQ
{
    public class XqApi
    {
        private readonly byte[] _authid;

        public XqApi(byte[] authid)
        {
            _authid = authid;
        }

        #region QQApi

        /// <summary>
        /// 发送群消息
        /// </summary>
        /// <param name="robot">robotQq号</param>
        /// <param name="group">群号</param>
        /// <param name="messages"></param>
        /// <param name="anonymous"></param>
        public void SendGroupMessage(string robot, string group, bool anonymous = false, params object[] messages)
        {
            Xqdll.SendMsgEX_V2(_authid, robot, 2, group, "", messages.ToSend(), 0, anonymous, "");
        }

        /// <summary>
        /// 发送私聊消息
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="qqid"></param>
        /// <param name="messagetype"></param>
        /// <param name="messages"></param>
        /// <param name="group"></param>
        public void SendPrivateMessage(string robot, string qqid, PrivateMessageType messagetype, string group = "", params object[] messages)
        {
            Xqdll.SendMsgEX_V2(_authid, robot, (int)messagetype, group, qqid, messages.ToSend(), 0, false, "");
        }

        /// <summary>
        /// 发送群聊json结构消息
        /// </summary>
        /// <param name="robotqq"></param>
        /// <param name="group"></param>
        /// <param name="jsonMessage">Json结构内容</param>
        /// <param name="anonymous"></param>
        public void SendGroupJsonMessage(string robotqq, string group, string jsonMessage, bool anonymous = false)
        {
            Xqdll.SendJSON(_authid, robotqq, anonymous ? 2 : 1, 2, group, "", jsonMessage);
        }

        /// <summary>
        /// 发送私聊json结构消息
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="qq"></param>
        /// <param name="messagetype"></param>
        /// <param name="jsonMessage">Json结构内容</param>
        public void SendPrivateJsonMessage(string robot, string qq, PrivateMessageType messagetype, string jsonMessage)
        {
            Xqdll.SendJSON(_authid, robot, 1, (int)messagetype, "", qq, jsonMessage);
        }

        /// <summary>
        /// 发送群聊xml结构消息
        /// </summary>
        /// <param name="robotqq"></param>
        /// <param name="group"></param>
        /// <param name="xmlMessage"></param>
        /// <param name="anonymous"></param>
        public void SendGroupXmlMessage(string robotqq, string group, string xmlMessage, bool anonymous = false)
        {
            Xqdll.SendXML(_authid, robotqq, anonymous ? 2 : 1, 2, group, "", xmlMessage);
        }

        /// <summary>
        /// 发送私聊xml结构消息
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="qq"></param>
        /// <param name="messagetype"></param>
        /// <param name="xmlMessage"></param>
        public void SendPrivateXmlMessage(string robot, string qq, PrivateMessageType messagetype, string xmlMessage)
        {
            Xqdll.SendXML(_authid, robot, 1, (int)messagetype, "", qq, xmlMessage);
        }

        /// <summary>
        ///     获取好友列表
        /// </summary>
        /// <param name="robotqq"></param>
        /// <returns></returns>
        public List<Qq> GetFriendList(string robotqq)
        {
            try
            {
                return JsonConvert.DeserializeObject<FriendList>(Xqdll.GetFriendList(_authid, robotqq).IntPtrToString()).GetList()
                    .Select(i => new Qq(this, robotqq, i.Id, i.Name, XqMessageEventType.Friend, null)).ToList();
            }
            catch
            {
                return Xqdll.GetFriendList_B(_authid, robotqq).IntPtrToString().SplitToList()
                    .Select(i => new Qq(this, robotqq, i, XqMessageEventType.Friend, null)).ToList();
            }
        }


        /// <summary>
        ///     获取群列表
        /// </summary>
        /// <param name="robotqq"></param>
        /// <returns></returns>
        public List<Group> GetGroupList(string robotqq)
        {
            try
            {
                return JsonConvert.DeserializeObject<GroupList>(Xqdll.GetGroupList(_authid, robotqq).IntPtrToString()).List.Select(i => new Group(this, robotqq, i))
                    .ToList();
            }
            catch
            {
                return Xqdll.GetGroupList_B(_authid, robotqq).IntPtrToString().SplitToList()
                    .Select(i => new Group(this, robotqq, i)).ToList();
            }
        }

        /// <summary>
        /// 获取群成员列表
        /// </summary>
        /// <param name="robotqq"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public List<Qq> GetGroupMemberList(string robotqq, string group)
        {
            try
            {
                return JsonConvert.DeserializeObject<GroupMemberList>(Xqdll.GetGroupMemberList_B(_authid, robotqq, group).IntPtrToString()).Members
                    .Select(i => new Qq(this, robotqq, i.Key, i.Value.Name, XqMessageEventType.Group, group)).ToList();

            }
            catch
            {
                return JsonConvert
                    .DeserializeObject<GroupMemberListQqonly>(Xqdll.GetGroupMemberList_C(_authid, robotqq, group)
                        .IntPtrToString())
                    .List.Select(i => new Qq(this, robotqq, i.Qq, XqMessageEventType.Group, group)).ToList();
            }
        }

        /// <summary>
        ///     获取群成员名片
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <param name="qq"></param>
        /// <returns>群成员名片</returns>
        public string GetGroupCard(string robotQq, string group, string qq)
        {
            return Xqdll.GetGroupCard(_authid, robotQq, group, qq).IntPtrToString();
        }

        /// <summary>
        ///     获取群管理员列表
        /// </summary>
        /// <param name="robotqq"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public List<Qq> GetAdminList(string robotqq, string group)
        {
            return Xqdll.GetGroupAdmin(_authid, robotqq, group).IntPtrToString().SplitToList().Select(i => new Qq(this, robotqq, i, XqMessageEventType.Group, group)).ToList();
        }

        /// <summary>
        ///     获取群成员禁言状态
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <param name="qq"></param>
        /// <returns></returns>
        public bool IsShutUp(string robotQq, string group, string qq)
        {
            return Xqdll.IsShutUp(_authid, robotQq, group, qq);
        }

        /// <summary>
        ///     是否是好友
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <returns></returns>
        public bool IfFriend(string robotQq, string qq)
        {
            return Xqdll.IfFriend(_authid, robotQq, qq);
        }

        /// <summary>
        ///     获取赞数量
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <returns></returns>
        public int GetObjVote(string robotQq, string qq)
        {
            return Xqdll.GetObjVote(_authid, robotQq, qq);
        }

        /// <summary>
        ///     通过图片GUID获取图片下载链接
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="imageType"></param>
        /// <param name="group"></param>
        /// <param name="imageGuid"></param>
        /// <returns></returns>
        public string GetPicLink(string robotQq, int imageType, string group, ImageMessageObject imageGuid)
        {
            return Xqdll.GetPicLink(_authid, robotQq, imageType, group, imageGuid.ToSendString()).IntPtrToString();
        }

        /// <summary>
        ///     取QQ昵称
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <returns></returns>
        public string GetNick(string robotQq, string qq)
        {
            return Xqdll.GetNick(_authid, robotQq, qq).IntPtrToString();
        }

        /// <summary>
        ///     取好友备注姓名
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <exception cref="AccessViolationException">当无备注时XQ会报错</exception>
        /// <returns></returns>
        public string GetFriendsRemark(string robotQq, string qq)
        {
            return Xqdll.GetFriendsRemark(_authid, robotQq, qq).IntPtrToString();
        }

        /// <summary>
        ///     取指定的群名称
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public string GetGroupName(string robotQq, string group)
        {
            return Xqdll.GetGroupName(_authid, robotQq, group).IntPtrToString();
        }

        /// <summary>
        ///     取当前群人数和群人数上限
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <returns>item1 :当前群人数; item2 :群人数上限</returns>
        /// 
        public (string, string) GetGroupMemberNum(string robotQq, string group)
        {
            var list = Xqdll.GetGroupMemberNum(_authid, robotQq, group).IntPtrToString().SplitToList();
            return (list[0], list[1]);
        }

        /// <summary>
        ///     查询指定群是否允许匿名消息
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public bool GetAnon(string robotQq, string group)
        {
            return Xqdll.GetAnon(_authid, robotQq, group);
        }

        /// <summary>
        ///     获取指定QQ个人资料的年龄
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <returns>未知255</returns>
        public int GetAge(string robotQq, string qq)
        {
            return Xqdll.GetAge(_authid, robotQq, qq);
        }

        /// <summary>
        ///     获取QQ个人资料的性别
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <returns></returns>
        public XqSexType GetGender(string robotQq, string qq)
        {
            return (XqSexType)Xqdll.GetGender(_authid, robotQq, qq);
        }

        /// <summary>
        ///     上传图片
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="messageType"></param>
        /// <param name="groupOrQq"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ImageMessageObject UpLoadPic(string robotQq, int messageType, string groupOrQq, byte[] message)
        {
            return Xqdll.UpLoadPic(_authid, robotQq, messageType, groupOrQq, message).IntPtrToString();
        }

        /// <summary>
        ///     群禁言
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <param name="qq"></param>
        /// <param name="time"></param>
        public void ShutUp(string robotQq, string group, string qq, int time)
        {
            Xqdll.ShutUP(_authid, robotQq, group, qq, time);
        }

        /// <summary>
        ///     修改群成员昵称
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <param name="qq"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool SetGroupCard(string robotQq, string group, string qq, string card)
        {
            return Xqdll.SetGroupCard(_authid, robotQq, group, qq, card);
        }

        /// <summary>
        ///     群删除成员
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <param name="qq"></param>
        /// <param name="allow"></param>
        public void KickGroupMember(string robotQq, string group, string qq, bool allow)
        {
            Xqdll.KickGroupMBR(_authid, robotQq, group, qq, allow);
        }

        /// <summary>
        ///     发布群公告
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <param name="messageTitle"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool PublishGroupNotice(string robotQq, string group, string messageTitle, string message)
        {
            return Xqdll.PBGroupNotic(_authid, robotQq, group, messageTitle, message);
        }


        /// <summary>
        ///     提取图片文字
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="imageMessage"></param>
        /// <returns></returns>
        public OcrItemCollection OcrPic(string robotQq, ImageMessageObject imageMessage)
        {
            return new OcrItemCollection(JsonConvert.DeserializeObject<OcrInfo>(Xqdll.OcrPic(_authid, robotQq, imageMessage.ToBytes()).IntPtrToString()).List);
        }

        /// <summary>
        ///     主动加群
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <param name="message"></param>
        public void JoinGroup(string robotQq, string group, string message)
        {
            Xqdll.JoinGroup(_authid, robotQq, group, message);
        }

        /// <summary>
        ///     置好友添加请求
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <param name="messageType"></param>
        /// <param name="message"></param>
        public void HandleFriendEvent(string robotQq, string qq, int messageType, string message)
        {
            Xqdll.HandleFriendEvent(_authid, robotQq, qq, messageType, message);
        }

        /// <summary>
        ///     置群请求
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="requestType"></param>
        /// <param name="qq"></param>
        /// <param name="group"></param>
        /// <param name="seq"></param>
        /// <param name="messageType"></param>
        /// <param name="message"></param>
        public void HandleGroupEvent(string robotQq, int requestType, string qq, string group, string seq,
            int messageType, string message)
        {
            Xqdll.HandleGroupEvent(_authid, robotQq, requestType, qq, group, seq, messageType, message);
        }

        /// <summary>
        ///     删除指定好友
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <returns></returns>
        public bool DeleteFriend(string robotQq, string qq)
        {
            return Xqdll.DelFriend(_authid, robotQq, qq);
        }

        /// <summary>
        ///     修改好友备注名称
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <param name="message"></param>
        public void SetFriendsRemark(string robotQq, string qq, string message)
        {
            Xqdll.SetFriendsRemark(_authid, robotQq, qq, message);
        }

        /// <summary>
        ///     邀请好友加入群
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <param name="qq"></param>
        public void InviteFriendInfoGroup(string robotQq, string group, string qq)
        {
            Xqdll.InviteGroup(_authid, robotQq, group, qq);
        }

        /// <summary>
        ///     邀请群成员加入群
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <param name="groupY"></param>
        /// <param name="qq"></param>
        /// <returns></returns>
        public bool InviteGroupMemberInfoGroup(string robotQq, string group, string groupY, string qq)
        {
            return Xqdll.InviteGroupMember(_authid, robotQq, group, groupY, qq);
        }

        /// <summary>
        ///     创建群 组包模式
        /// </summary>
        /// <param name="robotQq"></param>
        /// <returns>返回群号</returns>
        public string CreateGroup(string robotQq)
        {
            return Xqdll.CreateDisGroup(_authid, robotQq).IntPtrToString();
        }

        /// <summary>
        ///     退出群
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        public void QuitGroup(string robotQq, string group)
        {
            Xqdll.QuitGroup(_authid, robotQq, group);
        }

        /// <summary>
        ///     屏蔽或接收某群消息
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <param name="messageType"></param>
        public void SetShieldedGroup(string robotQq, string group, bool messageType)
        {
            Xqdll.SetShieldedGroup(_authid, robotQq, group, messageType);
        }

        /// <summary>
        ///     多功能删除好友 可删除陌生人或者删除为单项好友
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <param name="messageType"></param>
        public void DelFriend_A(string robotQq, string qq, int messageType)
        {
            Xqdll.DelFriend_A(_authid, robotQq, qq, messageType);
        }

        /// <summary>
        ///     设置机器人被添加好友时的验证方式
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="messageType"></param>
        public void SetCation(string robotQq, XqFriendAddRequestType messageType)
        {
            Xqdll.Setcation(_authid, robotQq, (int)messageType);
        }

        /// <summary>
        ///     设置机器人被添加好友时的问题与答案
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="problem"></param>
        /// <param name="answer"></param>
        public void SetCationWithQuestion(string robotQq, string problem, string answer)
        {
            Xqdll.Setcation_problem_A(_authid, robotQq, problem, answer);
        }

        /// <summary>
        ///     设置机器人被添加好友时的三个可选问题
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="problem1"></param>
        /// <param name="problem2"></param>
        /// <param name="problem3"></param>
        public void SetCationWithThreeQuestion(string robotQq, string problem1, string problem2, string problem3)
        {
            Xqdll.Setcation_problem_B(_authid, robotQq, problem1, problem2, problem3);
        }

        /// <summary>
        ///     主动添加好友
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <param name="message"></param>
        /// <param name="xxlay"></param>
        /// <returns></returns>
        public bool AddFriend(string robotQq, string qq, string message, int xxlay)
        {
            return Xqdll.AddFriend(_authid, robotQq, qq, message, xxlay);
        }

        /// <summary>
        ///     上传silk语音文件
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="sendType"></param>
        /// <param name="group"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public VoiceMessageObject UpLoadVoice(string robotQq, int sendType, string group, byte[] message)
        {
            return Xqdll.UpLoadVoice(_authid, robotQq, sendType, group, message).IntPtrToString();
        }

        /// <summary>
        ///     通过语音GUID获取语音文件下载连接
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="message"></param>
        /// <returns>silk格式的文件链接</returns>
        public string GetVoiLink(string robotQq, VoiceMessageObject message)
        {
            return Xqdll.GetVoiLink(_authid, robotQq, message.ToSendString()).IntPtrToString();
        }

        /// <summary>
        ///     语音GUID转换为文本内容
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="ckdx"></param>
        /// <param name="cklx"></param>
        /// <param name="yyGuid"></param>
        /// <returns></returns>
        public string VoiToText(string robotQq, string ckdx, int cklx, VoiceMessageObject yyGuid)
        {
            return Xqdll.VoiToText(_authid, robotQq, ckdx, cklx, yyGuid.ToSendString()).IntPtrToString();
        }

        /// <summary>
        ///     开关群匿名功能
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <param name="kg"></param>
        /// <returns></returns>
        public bool SetAnon(string robotQq, string group, bool kg)
        {
            return Xqdll.SetAnon(_authid, robotQq, group, kg);
        }

        /// <summary>
        ///     修改机器人自身头像
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool SetHeadPic(string robotQq, byte[] message)
        {
            return Xqdll.SetHeadPic(_authid, robotQq, message);
        }

        /// <summary>
        ///     向好友发送窗口抖动消息
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <returns></returns>
        public bool ShakeWindow(string robotQq, string qq)
        {
            return Xqdll.ShakeWindow(_authid, robotQq, qq);
        }

        /// <summary>
        ///     撤回群消息或者私聊消息
        ///     未被测试
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="withdrawType"></param>
        /// <param name="group"></param>
        /// <param name="qq"></param>
        /// <param name="messageNumber"></param>
        /// <param name="messageId"></param>
        /// <param name="messageTime"></param>
        /// <returns></returns>
        public string WithdrawMsg(string robotQq, int withdrawType, string group, string qq, string messageNumber,
            string messageId, string messageTime)
        {
            return Xqdll.WithdrawMsgEX(_authid, robotQq, withdrawType, group, qq, messageNumber,
                messageId, messageTime).IntPtrToString();
        }

        #endregion

        #region FrameApi

        /// <summary>
        ///     标记函数执行流程 debug时使用 每个函数内只需要调用一次
        ///     未被测试
        /// </summary>
        /// <param name="message"></param>
        public void DbgName(string message)
        {
            Xqdll.DbgName(_authid, message);
        }

        /// <summary>
        ///     函数内标记附加信息 函数内可多次调用
        ///     未被测试
        /// </summary>
        /// <param name="message"></param>
        public void Mark(string message)
        {
            Xqdll.Mark(_authid, message);
        }

        /// <summary>
        ///     输出日志 (在框架中显示)
        /// </summary>
        /// <param name="message"></param>
        public void OutPutLogToFrame(string message)
        {
            Xqdll.OutPutLog(_authid, message);
        }

        /// <summary>
        ///     检查指定RobotQQ是否在线
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <returns></returns>
        public bool IsOnline(string robotQq, string qq)
        {
            return Xqdll.IsOnline(_authid, robotQq, qq);
        }

        /// <summary>
        ///     取机器人账号在线信息
        /// </summary>
        /// <param name="robotQq"></param>
        /// <returns></returns>
        public RobotInfo GetRobotInfo(string robotQq)
        {
            return JsonConvert.DeserializeObject<RobotInfo>(Xqdll.GetRInf(_authid, robotQq).IntPtrToString());
        }

        /// <summary>
        ///     修改机器人账号在线状态
        /// </summary>
        /// <param name="robotqq">机器人QQ</param>
        /// <param name="onLineType">在线状态类型</param>
        public void SetOnlineStatus(string robotqq, XqOnlineStatusType onLineType)
        {
            Xqdll.SetRInf(_authid, robotqq, $"{(int)onLineType}", "");
        }

        /// <summary>
        ///     修改机器人账号个性签名
        /// </summary>
        /// <param name="robotqq">机器人QQ</param>
        /// <param name="signature">个性签名</param>
        public void SetSignature(string robotqq, string signature)
        {
            Xqdll.SetRInf(_authid, robotqq, "8", signature);
        }

        /// <summary>
        ///     修改机器人账号性别
        /// </summary>
        /// <param name="robotqq">机器人QQ</param>
        /// <param name="type">性别</param>
        public void SetSex(string robotqq, XqSexType type)
        {
            Xqdll.SetRInf(_authid, robotqq, "9", $"{(int)type}");
        }

        /// <summary>
        ///     主动卸载插件自身
        /// </summary>
        public bool Uninstall()
        {
            return Xqdll.Uninstall(_authid);
        }

        /// <summary>
        ///     重新从Plugin目录下载入本插件(一般用做自动更新)
        /// </summary>
        public bool Reload()
        {
            return Xqdll.Reload(_authid);
        }

        /// <summary>
        ///     登录指定QQ
        /// </summary>
        /// <param name="qq"></param>
        public void LoginQq(string qq)
        {
            Xqdll.LoginQQ(_authid, qq);
        }

        /// <summary>
        ///     离线指定QQ
        /// </summary>
        /// <param name="qq"></param>
        public void OffLineQq(string qq)
        {
            Xqdll.OffLineQQ(_authid, qq);
        }

        /// <summary>
        ///     获取机器人在线账号列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetOnlineList()
        {
            return Xqdll.GetOnLineList(_authid).IntPtrToString().SplitToList();
        }

        /// <summary>
        ///     获取机器人账号是否在线
        /// </summary>
        /// <param name="qq"></param>
        /// <returns></returns>
        public bool GetBotsOnline(string qq)
        {
            return Xqdll.Getbotisonline(_authid, qq);
        }

        /// <summary>
        ///     取插件是否启用
        /// </summary>
        /// <returns></returns>
        public bool IsEnable()
        {
            return Xqdll.IsEnable(_authid);
        }

        /// <summary>
        ///     取所有RobotQQ列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetRobotQqList()
        {
            return Xqdll.GetQQList(_authid).IntPtrToString().SplitToList();
        }

        #endregion

        #region PsKey Cookies Clientkey Api

        /// <summary>
        ///     取得QQ群页面操作用参数P_skey
        /// </summary>
        /// <param name="robotQq"></param>
        /// <example> "; p_uin=o{robotQq}; p_skey={p_skeyvalue}" </example>
        /// <returns></returns>
        public string GetGroupPsKey(string robotQq)
        {
            return Xqdll.GetGroupPsKey(_authid, robotQq).IntPtrToString();
        }

        /// <summary>
        ///     取得QQ空间页面操作用参数P_skey
        /// </summary>
        /// <param name="robotQq"></param>
        /// <example> "; p_uin=o{robotQq}; p_skey={p_skeyvalue}" </example>
        /// <returns></returns>
        public string GetZonePsKey(string robotQq)
        {
            return Xqdll.GetZonePsKey(_authid, robotQq).IntPtrToString();
        }

        /// <summary>
        ///     取得机器人网页操作用的Cookies
        /// </summary>
        /// <param name="robotQq"></param>
        /// <example> "uin=o{robotQq}; skey={skeyvalue}" </example>
        /// <returns></returns>
        public string GetCookies(string robotQq)
        {
            return Xqdll.GetCookies(_authid, robotQq).IntPtrToString();
        }

        /// <summary>
        ///     取短Clientkey
        /// </summary>
        /// <param name="robotQq"></param>
        /// <returns>16进制字符串</returns>
        public string GetClientkey(string robotQq)
        {
            return Xqdll.GetClientkey(_authid, robotQq).IntPtrToString();
        }

        /// <summary>
        ///     取得机器人网页操作用的长Clientkey
        /// </summary>
        /// <param name="robotQq"></param>
        /// <returns>16进制字符串</returns>
        public string GetLongClientkey(string robotQq)
        {
            return Xqdll.GetLongClientkey(_authid, robotQq).IntPtrToString();
        }

        /// <summary>
        ///     取得腾讯课堂页面操作用参数P_skey
        /// </summary>
        /// <param name="robotQq"></param>
        /// <example> "; p_uin=o{robotQq}; p_skey={p_skeyvalue}" </example>
        /// <returns></returns>
        public string GetClassRoomPsKey(string robotQq)
        {
            return Xqdll.GetClassRoomPsKey(_authid, robotQq).IntPtrToString();
        }

        /// <summary>
        ///     取得QQ举报页面操作用参数P_skey
        /// </summary>
        /// <param name="robotQq"></param>
        /// <example> "; p_uin=o{robotQq}; p_skey={p_skeyvalue}" </example>
        /// <returns></returns>
        public string GetRepPsKey(string robotQq)
        {
            return Xqdll.GetRepPsKey(_authid, robotQq).IntPtrToString();
        }

        /// <summary>
        ///     取得财付通页面操作用参数P_skey
        /// </summary>
        /// <param name="robotQq"></param>
        /// <example> "; p_uin=o{robotQq}; p_skey={p_skeyvalue}" </example>
        /// <returns></returns>
        public string GetTenPayPsKey(string robotQq)
        {
            return Xqdll.GetTenPayPsKey(_authid, robotQq).IntPtrToString();
        }

        /// <summary>
        ///     取bkn
        /// </summary>
        /// <param name="robotQq"></param>
        /// <returns>bkn （一串数字）</returns>
        public string GetBkn(string robotQq)
        {
            return Xqdll.GetBkn(_authid, robotQq).IntPtrToString();
        }

        #endregion
    }
}
