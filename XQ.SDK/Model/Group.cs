using System.Collections.Generic;
using XQ.SDK.Enum;
using XQ.SDK.Model.Json;
using XQ.SDK.XQ;

namespace XQ.SDK.Model
{
    /// <summary>
    ///     事件中的来源群
    /// </summary>
    public class Group : BasisModel
    {
        private List<Qq> _adminList;

        private List<Qq> _groupmemberlist;

        private string _name;

        private Qq _owner;

        public Group(XqApi api, string robotqq, string id) : base(api, robotqq)
        {
            Id = id;
        }

        public Group(XqApi api, string robotqq, GroupInfoJson info) : base(api, robotqq)
        {
            Id = info.Id;
            _name = info.Name;
            _owner = new Qq(api, robotqq, info.Owner, MessageType.Group, info.Id);
        }

        /// <summary>
        ///     QQ群号
        /// </summary>
        public string Id { get; }

        /// <summary>
        ///     QQ群名称
        /// </summary>
        public string Name => _name ??= XqApi.GetGroupName(Robot, Id);

        public Qq Owner => _owner ??= GetAdminList()[0];

        /// <summary>
        ///     获取群管理员列表
        /// </summary>
        public List<Qq> GetAdminList()
        {
            return _adminList ??= XqApi.GetAdminList(Robot, Id);
        }

        /// <summary>
        ///     获取群成员名片
        /// </summary>
        public string GetGroupCard(Qq qq)
        {
            return XqApi.GetGroupCard(Robot, Id, qq.Id);
        }

        /// <summary>
        ///     取群成员列表
        /// </summary>
        public List<Qq> GetGroupMemberList()
        {
            return _groupmemberlist ??= XqApi.GetGroupMemberList(Robot, Id);
        }

        /// <summary>
        ///     取当前群人数和群人数上限
        /// </summary>
        /// <returns>item1 :当前群人数; item2 :群人数上限</returns>
        public (string, string) GetGroupMemberNum()
        {
            return XqApi.GetGroupMemberNum(Robot, Id);
        }

        /// <summary>
        ///     查询是否允许发送匿名消息
        /// </summary>
        public bool GetAnon()
        {
            return XqApi.GetAnon(Robot, Id);
        }

        /// <summary>
        ///     退出群
        /// </summary>
        public void QuitGroup()
        {
            XqApi.QuitGroup(Robot, Id);
        }

        /// <summary>
        ///     删除群成员
        ///     当Bot不为管理员时报错
        /// </summary>
        /// <param name="qq">群成员QQ</param>
        /// <param name="notallowagain">指定是否不再允许接受申请入群</param>
        public void KickGroupMember(Qq qq, bool notallowagain)
        {
            XqApi.KickGroupMember(Robot, Id, qq.Id, notallowagain);
        }

        /// <summary>
        ///     修改群成员昵称
        /// </summary>
        public bool SetGroupCard(Qq qq, string card)
        {
            return XqApi.SetGroupCard(Robot, Id, qq.Id, card);
        }

        /// <summary>
        ///     群禁言
        /// </summary>
        /// <param name="qq">对象QQ(为空则为全员禁言)</param>
        /// <param name="time">禁言时间(单位:秒)(0:解除禁言)</param>
        public void BanSpeak(Qq qq, int time)
        {
            XqApi.BanSpeak(Robot, Id, qq?.Id ?? "", time);
        }

        /// <summary>
        ///     开关群匿名功能
        /// </summary>
        /// <param name="openOrClose">True 则开启群匿名 ，False 则关闭群匿名</param>
        public bool SetAnon(bool openOrClose)
        {
            return XqApi.SetAnon(Robot, Id, openOrClose);
        }

        /// <summary>
        ///     屏蔽本群消息
        /// </summary>
        /// <param name="openOrClose">True 则屏蔽 ，False 则取消屏蔽</param>
        public void ShieldThisGroup(bool openOrClose)
        {
            XqApi.SetShieldedGroup(Robot, Id, openOrClose);
        }

        /// <summary>
        ///     发送群聊消息
        /// </summary>
        /// <param name="anonymous">选择是否匿名发送,在群聊不允许发送匿名消息时无效</param>
        /// <param name="msg"></param>
        public void SendGroupMessage(bool anonymous, params object[] msg)
        {
            XqApi.SendGroupMessage(Robot, Id, anonymous && GetAnon(), msg);
        }

        public static implicit operator string(Group group)
        {
            return group.Id;
        }
    }
}