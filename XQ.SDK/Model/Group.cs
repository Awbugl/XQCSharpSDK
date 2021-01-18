using System.Collections.Generic;
using System.Linq;
using XQ.SDK.XQ;
using XQ.SDK.XQ.Json;

namespace XQ.SDK.Model
{
    public class Group : BasisModel
    {
        private List<Qq> _adminList;

        private List<Qq> _groupmemberlist;

        private string _name;

        private Qq _owner;

        public Group(XqApi api, Robot robotqq, string id) : base(api, robotqq)
        {
            Id = id;
        }

        public Group(XqApi api, Robot robotqq, GroupInfoJson info) : base(api, robotqq)
        {
            Id = info.Id;
            _name = info.Name;
            _owner = new Qq(api, robotqq, info.Owner);
        }

        /// <summary>
        ///     QQ群号
        /// </summary>
        public string Id { get; }

        /// <summary>
        ///     QQ群名称
        /// </summary>
        public string Name => _name ??= XqApi.TencentApi.GetGroupName(Robot, Id);

        public Qq Owner => _owner ??= GetAdminList()[0];

        /// <summary>
        ///     获取群管理员列表
        /// </summary>
        public List<Qq> GetAdminList()
        {
            return _adminList ??=
                XqApi.TencentApi.GetGroupAdmin(Robot, Id).Select(i => new Qq(XqApi, Robot, i)).ToList();
        }

        /// <summary>
        ///     获取群成员名片
        /// </summary>
        public string GetGroupCard(Qq qq)
        {
            return XqApi.TencentApi.GetGroupCard(Robot, Id, qq.Id);
        }

        /// <summary>
        ///     取群成员列表
        /// </summary>
        public List<Qq> GetGroupMemberList()
        {
            try
            {
                return _groupmemberlist ??= XqApi.TencentApi.GetGroupMemberList_NumberOnly(Robot, Id)
                    .Select(i => new Qq(XqApi, Robot, i)).ToList();
            }
            catch
            {
                return _groupmemberlist ??= XqApi.TencentApi.GetGroupMemberList(Robot, Id)
                    .Select(i => new Qq(XqApi, Robot, i.Key, i.Value.Name)).ToList();
            }
        }

        /// <summary>
        ///     取当前群人数和群人数上限
        /// </summary>
        /// <returns>item1 :当前群人数; item2 :群人数上限</returns>
        public (string, string) GetGroupMemberNum()
        {
            return XqApi.TencentApi.GetGroupMemberNum(Robot, Id);
        }

        /// <summary>
        ///     查询是否允许发送匿名消息
        /// </summary>
        /// <returns></returns>
        public bool GetAnon()
        {
            return XqApi.TencentApi.GetAnon(Robot, Id);
        }

        /// <summary>
        ///     退出群
        /// </summary>
        public void QuitGroup()
        {
            XqApi.TencentApi.QuitGroup(Robot, Id);
        }

        /// <summary>
        ///     删除群成员
        ///     当Bot不为管理员时报错
        /// </summary>
        /// <param name="qq">群成员QQ</param>
        /// <param name="allow">是否不在允许接受申请入群(true/false)</param>
        public void KickGroupMember(Qq qq, bool allow)
        {
            XqApi.TencentApi.KickGroupMember(Robot, Id, qq.Id, allow);
        }

        /// <summary>
        ///     发布群公告
        /// </summary>
        /// <param name="title">公告标题</param>
        /// <param name="message">公告内容</param>
        /// <returns></returns>
        public bool PublishGroupNotice(string title, string message)
        {
            return XqApi.TencentApi.PublishGroupNotice(Robot, Id, title, message);
        }

        /// <summary>
        ///     修改群成员昵称
        /// </summary>
        /// <returns></returns>
        public bool SetGroupCard(Qq qq, string card)
        {
            return XqApi.TencentApi.SetGroupCard(Robot, Id, qq.Id, card);
        }

        /// <summary>
        ///     屏蔽本群消息
        /// </summary>
        public void ShieldThisGroup()
        {
            XqApi.TencentApi.SetShieldedGroup(Robot, Id, true);
        }

        public static implicit operator string(Group group)
        {
            return group.Id;
        }
    }
}