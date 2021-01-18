using System.Collections.Generic;
using System.Linq;
using XQ.SDK.Enum;
using XQ.SDK.XQ;
using XQ.SDK.XQ.Json;

namespace XQ.SDK.Model
{
    public class Robot
    {
        private readonly XqApi _api;

        private readonly string _robotQq;


        private List<Qq> _friendlist;

        private List<Group> _grouplist;

        public Robot(XqApi api, string robotQq)
        {
            _api = api;
            _robotQq = robotQq;
            PsKey = new PsKeyApi(api, robotQq);
        }

        /// <summary>
        ///     PsKey相关的Api
        /// </summary>
        public PsKeyApi PsKey { get; }

        /// <summary>
        ///     获取好友列表
        /// </summary>
        public List<Qq> GetFriendList()
        {
            try
            {
                return _friendlist ??= _api.TencentApi.GetFriendList(_robotQq)
                    .Select(i => new Qq(_api, this, i.Id, i.Name)).ToList();
            }
            catch
            {
                return _friendlist ??= _api.TencentApi.GetFriendList_NumberOnly(_robotQq)
                    .Select(i => new Qq(_api, this, i)).ToList();
            }
        }

        /// <summary>
        ///     创建群
        /// </summary>
        /// <returns>返回创建群的群号</returns>
        public string CreateGroup()
        {
            return _api.TencentApi.CreateGroup(_robotQq);
        }

        /// <summary>
        ///     修改机器人自身头像
        /// </summary>
        public bool SetHeadPic(ImageMessageObject obj)
        {
            return _api.TencentApi.SetHeadPic(_robotQq, obj.ToBytes());
        }

        /// <summary>
        ///     获取群列表
        /// </summary>
        public List<Group> GetGroupList()
        {
            try
            {
                return _grouplist ??= _api.TencentApi.GetGroupList(_robotQq).Select(i => new Group(_api, this, i))
                    .ToList();
            }
            catch
            {
                return _grouplist ??= _api.TencentApi.GetGroupList_NumberOnly(_robotQq)
                    .Select(i => new Group(_api, this, i)).ToList();
            }
        }

        /// <summary>
        ///     取机器人账号在线信息
        /// </summary>
        public RobotInfo GetRobotInfo()
        {
            return _api.FrameApi.GetRInf(_robotQq);
        }

        /// <summary>
        ///     修改QQ在线状态
        /// </summary>
        /// <param name="onLineType">在线状态类型</param>
        public void SetOnlineStatus(XqOnlineStatusType onLineType)
        {
            _api.FrameApi.SetRInf(_robotQq, $"{(int) onLineType}", "");
        }

        /// <summary>
        ///     修改个性签名
        /// </summary>
        /// <param name="signature">个性签名</param>
        public void SetSignature(string signature)
        {
            _api.FrameApi.SetRInf(_robotQq, "8", signature);
        }

        /// <summary>
        ///     修改性别
        /// </summary>
        /// <param name="type">性别</param>
        public void SetSex(XqSexType type)
        {
            _api.FrameApi.SetRInf(_robotQq, "9", $"{(int) type}");
        }

        /// <summary>
        ///     邀请好友加入群
        /// </summary>
        /// <param name="group">被邀请加入的群号</param>
        /// <param name="qq"></param>
        public void InviteFriendInfoGroup(string group, string qq)
        {
            _api.TencentApi.InviteGroup(_robotQq, group, qq);
        }


        /// <summary>
        ///     邀请群成员加入群
        /// </summary>
        /// <param name="group">邀请到哪个群</param>
        /// <param name="groupY">被邀请成员所在群</param>
        /// <param name="qq">被邀请人的QQ</param>
        public bool InviteGroupMemberInfoGroup(string group, string groupY, string qq)
        {
            return _api.TencentApi.InviteGroupMember(_robotQq, group, groupY, qq);
        }


        /// <summary>
        ///     主动加群
        /// </summary>
        /// <param name="group">群号</param>
        /// <param name="message">附加理由，可留空（需回答正确问题时，请填写问题答案</param>
        public void JoinGroup(string group, string message)
        {
            _api.TencentApi.JoinGroup(_robotQq, group, message);
        }

        /// <summary>
        ///     设置机器人被添加好友时的验证方式
        /// </summary>
        public void SetCation(string group, XqFriendAddRequestType type)
        {
            _api.TencentApi.SetCation(_robotQq, type);
        }

        /// <summary>
        ///     设置机器人被添加好友时的问题与答案
        /// </summary>
        /// <param name="problem">设置的问题</param>
        /// <param name="answer">设置的问题答案 </param>
        public void SetCationWithQuestion(string problem, string answer)
        {
            _api.TencentApi.Setcation_problem_A(_robotQq, problem, answer);
        }

        /// <summary>
        ///     设置机器人被添加好友时的三个可选问题
        /// </summary>
        /// <param name="problem1">设置问题一</param>
        /// <param name="problem2">设置问题二</param>
        /// <param name="problem3">设置问题三</param>
        public void SetCationWithThreeQuestion(string problem1, string problem2, string problem3)
        {
            _api.TencentApi.Setcation_problem_B(_robotQq, problem1, problem2, problem3);
        }

        public static implicit operator string(Robot robot)
        {
            return robot._robotQq;
        }

        public class PsKeyApi
        {
            private readonly XqApi _api;
            private readonly string _robotQq;

            public PsKeyApi(XqApi api, string robotQq)
            {
                _api = api;
                _robotQq = robotQq;
            }

            /// <summary>
            ///     取得QQ群页面操作用参数P_skey
            /// </summary>
            /// <example> "; p_uin=o{robotQq}; p_skey={p_skeyvalue}" </example>
            /// <returns></returns>
            public string GetGroupPsKey()
            {
                return _api.PsKeyApi.GetGroupPsKey(_robotQq);
            }

            /// <summary>
            ///     取得QQ空间页面操作用参数P_skey
            /// </summary>
            /// <example> "; p_uin=o{robotQq}; p_skey={p_skeyvalue}" </example>
            /// <returns></returns>
            public string GetZonePsKey()
            {
                return _api.PsKeyApi.GetZonePsKey(_robotQq);
            }

            /// <summary>
            ///     取得机器人网页操作用的Cookies
            /// </summary>
            /// <example> "uin=o{robotQq}; skey={skeyvalue}" </example>
            /// <returns></returns>
            public string GetCookies()
            {
                return _api.PsKeyApi.GetCookies(_robotQq);
            }

            /// <summary>
            ///     取短Clientkey
            /// </summary>
            /// <returns>16进制字符串</returns>
            public string GetClientkey()
            {
                return _api.PsKeyApi.GetClientkey(_robotQq);
            }

            /// <summary>
            ///     取得机器人网页操作用的长Clientkey
            /// </summary>
            /// <returns>16进制字符串</returns>
            public string GetLongClientkey()
            {
                return _api.PsKeyApi.GetLongClientkey(_robotQq);
            }

            /// <summary>
            ///     取得腾讯课堂页面操作用参数P_skey
            /// </summary>
            /// <example> "; p_uin=o{robotQq}; p_skey={p_skeyvalue}" </example>
            /// <returns></returns>
            public string GetClassRoomPsKey()
            {
                return _api.PsKeyApi.GetClassRoomPsKey(_robotQq);
            }

            /// <summary>
            ///     取得QQ举报页面操作用参数P_skey
            /// </summary>
            /// <example> "; p_uin=o{robotQq}; p_skey={p_skeyvalue}" </example>
            /// <returns></returns>
            public string GetRepPsKey()
            {
                return _api.PsKeyApi.GetRepPsKey(_robotQq);
            }

            /// <summary>
            ///     取得财付通页面操作用参数P_skey
            /// </summary>
            /// <example> "; p_uin=o{robotQq}; p_skey={p_skeyvalue}" </example>
            /// <returns></returns>
            public string GetTenPayPsKey()
            {
                return _api.PsKeyApi.GetTenPayPsKey(_robotQq);
            }

            /// <summary>
            ///     取bkn
            /// </summary>
            /// <returns>bkn （一串数字）</returns>
            public string GetBkn()
            {
                return _api.PsKeyApi.GetBkn(_robotQq);
            }
        }
    }
}