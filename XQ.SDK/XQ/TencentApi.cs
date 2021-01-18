using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Newtonsoft.Json;

using XQ.SDK.Enum;
using XQ.SDK.Enum.Event;
using XQ.SDK.Model;
using XQ.SDK.XQ.Json;

using static XQ.SDK.Core.Expand;

namespace XQ.SDK.XQ
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class TencentApi
    {
        private readonly byte[] _authid;

        public TencentApi(byte[] authid)
        {
            _authid = authid;
        }

        public void SendGroupMessage(string robot, string group, string messages)
        {
            Xqdll.SendMsgEX_V2(_authid, robot, 2, group, "", messages, 0, false, "");
        }

        public void SendPrivateMessage(string robot, string qqid, PrivateMessageType messagetype, string messages, string group = "")
        {
            Xqdll.SendMsgEX_V2(_authid, robot, (int)messagetype, group, qqid, messages, 0, false, "");
        }

        /// <summary>
        ///     发送json结构消息
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="anon"></param>
        /// <param name="messageType"></param>
        /// <param name="group"></param>
        /// <param name="qq"></param>
        /// <param name="jsonMessage"></param>
        public void SendJson(string robotQq, bool anon, XqMessageEventType messageType, string group, string qq, string jsonMessage)
        {
            Xqdll.SendJSON(_authid, robotQq, anon ? 1 : 2, (int)messageType, group, qq, jsonMessage);
        }

        /// <summary>
        ///     发送xml结构消息
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="anon"></param>
        /// <param name="messageType"></param>
        /// <param name="group"></param>
        /// <param name="qq"></param>
        /// <param name="xmlMessage"></param>
        public void SendXml(string robotQq, bool anon, XqMessageEventType messageType, string group, string qq, string xmlMessage)
        {
            Xqdll.SendXML(_authid, robotQq, anon ? 1 : 2, (int)messageType, group, qq, xmlMessage);
        }


        /// <summary>
        ///     获取好友列表-http模式
        /// </summary>
        /// <param name="qq"></param>
        /// <returns></returns>
        public List<FriendInfoJson> GetFriendList(string qq)
        {
            return JsonConvert.DeserializeObject<FriendList>(Xqdll.GetFriendList(_authid, qq).IntPtrToString()).GetList();
        }

        /// <summary>
        ///     获取群列表
        /// </summary>
        /// <param name="qq"></param>
        /// <returns></returns>
        public List<GroupInfoJson> GetGroupList(string qq)
        {
            return JsonConvert.DeserializeObject<GroupList>(Xqdll.GetGroupList(_authid, qq).IntPtrToString()).List;
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
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public List<string> GetGroupAdmin(string robotQq, string group)
        {
            return Xqdll.GetGroupAdmin(_authid, robotQq, group).IntPtrToString().SplitToList();
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
        ///     封包模式获取群号列表(最多可以取得999)
        /// </summary>
        /// <param name="robotQq"></param>
        /// <returns>用换行符分割的群号</returns>
        public List<string> GetGroupList_NumberOnly(string robotQq)
        {
            return Xqdll.GetGroupList_B(_authid, robotQq).IntPtrToString().SplitToList();
        }

        /// <summary>
        ///     封包模式取好友列表(与封包模式取群列表同源)
        /// </summary>
        /// <param name="robotQq"></param>
        /// <returns>用换行符分割的好友qq</returns>
        public List<string> GetFriendList_NumberOnly(string robotQq)
        {
            return Xqdll.GetFriendList_B(_authid, robotQq).IntPtrToString().SplitToList();
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
        ///     取群成员列表
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public Dictionary<string,GroupMemberInfoJson> GetGroupMemberList(string robotQq, string group)
        {
            return JsonConvert.DeserializeObject<GroupMemberList>(Xqdll.GetGroupMemberList_B(_authid, robotQq, group).IntPtrToString()).Members;
        }

        /// <summary>
        ///     取群成员列表
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public List<string> GetGroupMemberList_NumberOnly(string robotQq, string group)
        {
            return JsonConvert.DeserializeObject<GroupMemberListQqonly>(Xqdll.GetGroupMemberList_C(_authid, robotQq, group).IntPtrToString())
                .List.Select(i => i.Qq).ToList();
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
        /// <returns>未知255</returns>
        public int GetGender(string robotQq, string qq)
        {
            return Xqdll.GetGender(_authid, robotQq, qq);
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
        public List<OcrItem> OcrPic(string robotQq, ImageMessageObject imageMessage)
        {
            return JsonConvert.DeserializeObject<OcrInfo>(Xqdll.OcrPic(_authid, robotQq, imageMessage.ToBytes()).IntPtrToString()).List;
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
        public bool DelFriend(string robotQq, string qq)
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
        public void InviteGroup(string robotQq, string group, string qq)
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
        public bool InviteGroupMember(string robotQq, string group, string groupY, string qq)
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
        public void Setcation_problem_A(string robotQq, string problem, string answer)
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
        public void Setcation_problem_B(string robotQq, string problem1, string problem2, string problem3)
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
    }
}