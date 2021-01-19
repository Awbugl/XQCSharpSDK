using System;
using XQ.SDK.Enum;
using XQ.SDK.Enum.Event;
using XQ.SDK.XQ;

namespace XQ.SDK.Model
{
    public class Qq : BasisModel
    {
        private readonly int _extratype;

        private readonly string _fromgroup;

        private readonly XqMessageEventType _type;
        private string _name;

        public Qq(XqApi api, string robot, string id, XqMessageEventType type, string fromgroup) : base(api, robot)
        {
            Id = id;
            _type = type;
            _fromgroup = fromgroup;
        }

        public Qq(XqApi api, string robot, string id, XqMessageEventType type, string fromgroup, int extratype) : this(
            api, robot, id, type, fromgroup)
        {
            _extratype = extratype;
        }


        public Qq(XqApi api, string robot, string id, string name, XqMessageEventType type, string fromgroup) : this(
            api, robot, id, type, fromgroup)
        {
            _name = name;
        }

        /// <summary>
        ///     QQ号
        /// </summary>
        public string Id { get; }

        /// <summary>
        ///     昵称
        /// </summary>
        public string Name => _name ??= XqApi.GetNick(Robot, Id);

        /// <summary>
        ///     年龄，未知为255
        /// </summary>
        public int Age => XqApi.GetAge(Robot, Id);

        /// <summary>
        ///     性别
        /// </summary>
        public XqSexType Gender => XqApi.GetGender(Robot, Id);

        /// <summary>
        ///     赞数量
        /// </summary>
        public int ObjVote => XqApi.GetObjVote(Robot, Id);

        /// <summary>
        ///     查询是否是好友
        /// </summary>
        public bool IfFriend()
        {
            return XqApi.IfFriend(Robot, Id);
        }

        /// <summary>
        ///     删除好友
        /// </summary>
        public void DeleteFriend()
        {
            XqApi.DeleteFriend(Robot, Id);
        }

        /// <summary>
        ///     添加好友
        /// </summary>
        /// <param name="msg">验证消息</param>
        public bool AddFriend(string msg)
        {
            return XqApi.AddFriend(Robot, Id, msg, 1);
        }

        /// <summary>
        ///     修改好友备注名称
        /// </summary>
        /// <param name="remark">备注</param>
        public void SetRemark(string remark)
        {
            XqApi.SetFriendsRemark(Robot, Id, remark);
        }

        /// <summary>
        ///     向好友发送窗口抖动消息
        /// </summary>
        public void ShakeWindow()
        {
            XqApi.ShakeWindow(Robot, Id);
        }


        /// <summary>
        ///     发送私聊消息
        /// </summary>
        /// <param name="msg"></param>
        public void SendPrivateMessage(params object[] msg)
        {
            XqApi.SendPrivateMessage(Robot, Id, GetPrivateMessageType(_type),
                _type == XqMessageEventType.TempGroupMessage ? _fromgroup : "", msg);
        }

        public PrivateMessageType GetPrivateMessageType(XqMessageEventType type)
        {
            if (System.Enum.IsDefined(typeof(PrivateMessageType), type)) return (PrivateMessageType) type;

            switch (type)
            {
                case XqMessageEventType.Group:
                    return IfFriend() ? PrivateMessageType.Friend : PrivateMessageType.TempGroupMessage;
                case XqMessageEventType.MsgWithdrawn:
                case XqMessageEventType.Transfer:
                    return GetPrivateMessageType((XqMessageEventType) _extratype);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }


        public static implicit operator string(Qq qq)
        {
            return qq.Id;
        }
    }
}