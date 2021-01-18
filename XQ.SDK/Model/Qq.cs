using XQ.SDK.Enum;
using XQ.SDK.XQ;

namespace XQ.SDK.Model
{
    public class Qq : BasisModel
    {
        /// <summary>
        ///     QQ号
        /// </summary>
        public string Id { get; }

        public Qq(XqApi api, Robot Robot, string id) : base(api, Robot)
        {
            Id = id;
        }

        public Qq(XqApi api, Robot Robot, string id, string name) : this(api, Robot, id)
        {
            _name = name;
        }

        private string _name;

        /// <summary>
        ///     昵称
        /// </summary>
        public string Name => _name ??= XqApi.TencentApi.GetNick(Robot, Id);

        /// <summary>
        ///     年龄，未知为255
        /// </summary>
        public int Age => XqApi.TencentApi.GetAge(Robot, Id);

        /// <summary>
        ///     性别
        /// </summary>
        public XqSexType Gender => (XqSexType)XqApi.TencentApi.GetGender(Robot, Id);

        /// <summary>
        ///     赞数量
        /// </summary>
        public int ObjVote => XqApi.TencentApi.GetObjVote(Robot, Id);

        /// <summary>
        ///     查询是否是好友
        /// </summary>
        public bool IfFriend() => XqApi.TencentApi.IfFriend(Robot, Id);

        /// <summary>
        ///     删除好友
        /// </summary>
        public void DeleteFriend() => XqApi.TencentApi.DelFriend(Robot, Id);

        /// <summary>
        ///     添加好友
        /// </summary>
        /// <param name="msg">验证消息</param>
        public bool AddFriend(string msg) => XqApi.TencentApi.AddFriend(Robot, Id, msg, 1);

        /// <summary>
        ///     修改好友备注名称
        /// </summary>
        /// <param name="remark">备注</param>
        public void SetRemark(string remark) => XqApi.TencentApi.SetFriendsRemark(Robot, Id, remark);

        /// <summary>
        ///     向好友发送窗口抖动消息
        /// </summary>
        public void ShakeWindow() => XqApi.TencentApi.ShakeWindow(Robot, Id);

        public static implicit operator string(Qq qq) => qq.Id;
    }
}