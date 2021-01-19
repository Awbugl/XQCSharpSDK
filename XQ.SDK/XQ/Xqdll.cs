using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace XQ.SDK.XQ
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public static class XqDll
    {
        private const string DllName = "xqapi.dll";

        #region 获取消息 OR 设置

        /// <summary>
        ///     获取好友列表-http模式
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="qq">机器人QQ</param>
        /// <returns>原始JSON格式信息</returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetFriendList")]
        public static extern IntPtr GetFriendList(byte[] autoid, string qq);

        /// <summary>
        ///     获取群列表
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="qq">机器人QQ</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetGroupList")]
        public static extern IntPtr GetGroupList(byte[] autoid, string qq);

        /// <summary>
        ///     获取机器人在线账号列表
        /// </summary>
        /// <param name="autoid"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetOnLineList")]
        public static extern IntPtr GetOnLineList(byte[] autoid);

        /// <summary>
        ///     获取机器人账号是否在线
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="qq">机器人QQ</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_Getbotisonline")]
        public static extern bool Getbotisonline(byte[] autoid, string qq);

        /// <summary>
        ///     获取群员列表
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="qq">机器人QQ</param>
        /// <param name="group">群号</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetGroupMemberList")]
        public static extern IntPtr GetGroupMemberList(byte[] autoid, string qq, string group);

        /// <summary>
        ///     获取群成员名片
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <param name="group">群号</param>
        /// <param name="qq">群成员QQ</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetGroupCard")]
        public static extern IntPtr GetGroupCard(byte[] autoid, string robotQq, string group, string qq);

        /// <summary>
        ///     获取群管理员列表
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <param name="group">QQ群号</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetGroupAdmin")]
        public static extern IntPtr GetGroupAdmin(byte[] autoid, string robotQq, string group);

        /// <summary>
        ///     获取群通知
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <param name="group">群号</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetNotice")]
        public static extern IntPtr GetNotice(byte[] autoid, string robotQq, string group);

        /// <summary>
        ///     获取群成员禁言状态
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <param name="group">群号</param>
        /// <param name="qq">QQ</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_IsShutUp")]
        public static extern bool IsShutUp(byte[] autoid, string robotQq, string group, string qq);

        /// <summary>
        ///     是否是好友
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <param name="qq">QQ</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_IfFriend")]
        public static extern bool IfFriend(byte[] autoid, string robotQq, string qq);

        /// <summary>
        ///     取得QQ群页面操作用参数P_skey
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetGroupPsKey")]
        public static extern IntPtr GetGroupPsKey(byte[] autoid, string robotQq);

        /// <summary>
        ///     取得QQ空间页面操作用参数P_skey
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetZonePsKey")]
        public static extern IntPtr GetZonePsKey(byte[] autoid, string robotQq);

        /// <summary>
        ///     取得机器人网页操作用的Cookies
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetCookies")]
        public static extern IntPtr GetCookies(byte[] autoid, string robotQq);

        /// <summary>
        ///     获取赞数量
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <param name="qq">QQ</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetObjVote")]
        public static extern int GetObjVote(byte[] autoid, string robotQq, string qq);

        /// <summary>
        ///     取插件是否启用
        /// </summary>
        /// <param name="autoid"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_IsEnable")]
        public static extern bool IsEnable(byte[] autoid);

        /// <summary>
        ///     取所有QQ列表
        /// </summary>
        /// <param name="autoid"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetQQList")]
        public static extern IntPtr GetQQList(byte[] autoid);

        /// <summary>
        ///     取QQ昵称
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetNick")]
        public static extern IntPtr GetNick(byte[] autoid, string robotQq, string qq);

        /// <summary>
        ///     取好友备注姓名
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetFriendsRemark")]
        public static extern IntPtr GetFriendsRemark(byte[] autoid, string robotQq, string qq);

        /// <summary>
        ///     取短Clientkey
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetClientkey")]
        public static extern IntPtr GetClientkey(byte[] autoid, string robotQq);

        /// <summary>
        ///     取得机器人网页操作用的长Clientkey
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetLongClientkey")]
        public static extern IntPtr GetLongClientkey(byte[] autoid, string robotQq);

        /// <summary>
        ///     取得腾讯课堂页面操作用参数P_skey
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetClassRoomPsKey")]
        public static extern IntPtr GetClassRoomPsKey(byte[] autoid, string robotQq);

        /// <summary>
        ///     取得QQ举报页面操作用参数P_skey
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetRepPsKey")]
        public static extern IntPtr GetRepPsKey(byte[] autoid, string robotQq);

        /// <summary>
        ///     取得财付通页面操作用参数P_skey
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetTenPayPsKey")]
        public static extern IntPtr GetTenPayPsKey(byte[] autoid, string robotQq);

        /// <summary>
        ///     取bkn
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetBkn")]
        public static extern IntPtr GetBkn(byte[] autoid, string robotQq);

        /// <summary>
        ///     封包模式获取群号列表(最多可以取得999)
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetGroupList_B")]
        public static extern IntPtr GetGroupList_B(byte[] autoid, string robotQq);

        /// <summary>
        ///     封包模式取好友列表(与封包模式取群列表同源)
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetFriendList_B")]
        public static extern IntPtr GetFriendList_B(byte[] autoid, string robotQq);

        /// <summary>
        ///     取登录二维码base64
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetQrcode")]
        public static extern IntPtr GetQrcode(byte[] autoid, string key);

        /// <summary>
        ///     检查登录二维码状态
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_CheckQrcode")]
        public static extern int CheckQrcode(byte[] autoid, string key);

        /// <summary>
        ///     取指定的群名称
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetGroupName")]
        public static extern IntPtr GetGroupName(byte[] autoid, string robotQq, string group);

        /// <summary>
        ///     取群人数上线与当前人数 换行符分隔
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetGroupMemberNum")]
        public static extern IntPtr GetGroupMemberNum(byte[] autoid, string robotQq, string group);

        /// <summary>
        ///     取群等级
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetGroupLv")]
        public static extern int GetGroupLv(byte[] autoid, string robotQq, string group);

        /// <summary>
        ///     取群成员列表
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetGroupMemberList_B")]
        public static extern IntPtr GetGroupMemberList_B(byte[] autoid, string robotQq, string group);

        /// <summary>
        ///     封包模式取群成员列表返回重组后的json文本
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetGroupMemberList_C")]
        public static extern IntPtr GetGroupMemberList_C(byte[] autoid, string robotQq, string group);

        /// <summary>
        ///     检查指定QQ是否在线
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_IsOnline")]
        public static extern bool IsOnline(byte[] autoid, string robotQq, string qq);

        /// <summary>
        ///     取机器人账号在线信息
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetRInf")]
        public static extern IntPtr GetRInf(byte[] autoid, string robotQq);

        /// <summary>
        ///     查询指定群是否允许匿名消息
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetAnon")]
        public static extern bool GetAnon(byte[] autoid, string robotQq, string group);

        /// <summary>
        ///     通过图片GUID获取图片链接
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="imageType">(图片类型)2群 讨论组  1临时会话和好友</param>
        /// <param name="group">图片所属对应的群号（可随意乱填写，只有群图片需要填写）</param>
        /// <param name="imageGuid">(图片GUID)[pic={xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}.jpg]</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetPicLink")]
        public static extern IntPtr GetPicLink(byte[] autoid, string robotQq, int imageType, string group,
            string imageGuid);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "Api_GetVer")]
        public static extern IntPtr GetVer();

        /// <summary>
        ///     获取指定QQ个人资料的年龄
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetAge")]
        public static extern int GetAge(byte[] autoid, string robotQq, string qq);

        /// <summary>
        ///     获取QQ个人资料的性别
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetGender")]
        public static extern int GetGender(byte[] autoid, string robotQq, string qq);

        #endregion 获取消息 OR 设置

        #region 发送 OR 设置

        /// <summary>
        ///     发送消息
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <param name="messageType">信息类型 : 0在线临时会话/1好友/2群/3讨论群/4群临时会话/5讨论组临时会话/7好友验证回复会话</param>
        /// <param name="group">QQ群号(发送群信息、讨论组等时候填写，其他为0或者为空)</param>
        /// <param name="qq">收信人QQ</param>
        /// <param name="message">消息内容</param>
        /// <param name="bubbleId">气泡ID</param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_SendMsg")]
        public static extern void SendMsg(byte[] autoid, string robotQq, int messageType, string group, string qq,
            string message, int bubbleId);

        /// <summary>
        ///     上传图片
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <param name="messageType">上传类型（1好友、临时会话  2群、讨论组 Ps：好友临时会话用类型 1，群讨论组用类型 2；当填写错误时，图片GUID发送不会成功）</param>
        /// <param name="groupOrQq">上传到QQ群或者QQ（填写群号或者QQ）</param>
        /// <param name="message">图片字节集数据</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_UpLoadPic")]
        public static extern IntPtr UpLoadPic(byte[] autoid, string robotQq, int messageType, string groupOrQq,
            byte[] message);

        /// <summary>
        ///     群禁言
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <param name="group">QQ群号</param>
        /// <param name="qq">禁言对象QQ(为空则为全员禁言)(QQ机器人需要管理员权限)</param>
        /// <param name="time">禁言时间(0:解除禁言)(单位:秒)</param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_ShutUP")]
        public static extern void ShutUP(byte[] autoid, string robotQq, string group, string qq, int time);

        /// <summary>
        ///     修改群成员昵称
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <param name="group">QQ群号</param>
        /// <param name="qq">被修改人QQ</param>
        /// <param name="card">修改的名片</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_SetGroupCard")]
        public static extern bool SetGroupCard(byte[] autoid, string robotQq, string group, string qq, string card);

        /// <summary>
        ///     群删除成员
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <param name="group">群号</param>
        /// <param name="qq">被移除人QQ</param>
        /// <param name="allow">是否不在允许接受申请入群(true/false)</param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_KickGroupMBR")]
        public static extern void KickGroupMBR(byte[] autoid, string robotQq, string group, string qq, bool allow);

        /// <summary>
        ///     修改QQ在线状态
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <param name="onLineType">类型(1、我在线上 2、Q我吧 3、离开 4、忙碌 5、请勿打扰 6、隐身 7、修改昵称 8、修改个性签名 9、修改性别)</param>
        /// <param name="message">修改内容(类型为7和8时填写修改内容  类型9时“1”为男 “2”为女      其他填“”)</param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_SetRInf")]
        public static extern void SetRInf(byte[] autoid, string robotQq, string onLineType, string message);

        /// <summary>
        ///     发布群公告
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <param name="group">群号</param>
        /// <param name="messageTitle">公告标题</param>
        /// <param name="message">公告内容</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_PBGroupNotic")]
        public static extern bool PBGroupNotic(byte[] autoid, string robotQq, string group, string messageTitle,
            string message);

        /// <summary>
        ///     主动卸载插件自身
        /// </summary>
        /// <param name="autoid"></param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_Uninstall")]
        public static extern bool Uninstall(byte[] autoid);

        /// <summary>
        ///     重新从Plugin目录下载入本插件(一般用做自动更新)
        /// </summary>
        /// <param name="autoid"></param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_Uninstall")]
        public static extern bool Reload(byte[] autoid);


        /// <summary>
        ///     输出日志 (在框架中显示)
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="message">内容</param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_OutPutLog")]
        public static extern void OutPutLog(byte[] autoid, string message);

        /// <summary>
        ///     提取图片文字
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <param name="imageMessage">图片数据</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_OcrPic")]
        public static extern IntPtr OcrPic(byte[] autoid, string robotQq, byte[] imageMessage);

        /// <summary>
        ///     主动加群
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <param name="group">群号</param>
        /// <param name="message">附加理由，可留空（需回答正确问题时，请填写问题答案</param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_JoinGroup")]
        public static extern void JoinGroup(byte[] autoid, string robotQq, string group, string message);

        /// <summary>
        ///     点赞
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <param name="qq">QQ</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_UpVote")]
        public static extern IntPtr UpVote(byte[] autoid, string robotQq, string qq);

        /// <summary>
        ///     通过列表或群临时通道点赞
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <param name="qq">QQ</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_UpVote_temp")]
        public static extern IntPtr UpVote_temp(byte[] autoid, string robotQq, string qq);

        /// <summary>
        ///     置好友添加请求
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <param name="qq">申请入群 被邀请人 请求添加好友人的QQ （当请求类型为214时这里为邀请人QQ</param>
        /// <param name="messageType">10同意 20拒绝 30忽略 40同意单项好友的请求</param>
        /// <param name="message">拒绝入群，拒绝添加好友 附加信息</param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_HandleFriendEvent")]
        public static extern void HandleFriendEvent(byte[] autoid, string robotQq, string qq, int messageType,
            string message);

        /// <summary>
        ///     置群请求
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <param name="requestType">213请求入群  214我被邀请加入某群  215某人被邀请加入群  101某人请求添加好友</param>
        /// <param name="qq">申请入群 被邀请人 请求添加好友人的QQ （当请求类型为214时这里为邀请人QQ）</param>
        /// <param name="group">收到请求群号（好友添加时这里请为空）</param>
        /// <param name="seq">需要处理事件的seq</param>
        /// <param name="messageType">10同意 20拒绝 30忽略</param>
        /// <param name="message">拒绝入群，拒绝添加好友 附加信息</param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_HandleGroupEvent")]
        public static extern void HandleGroupEvent(byte[] autoid, string robotQq, int requestType, string qq,
            string group, string seq, int messageType, string message);

        /// <summary>
        ///     向框架添加一个QQ
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">帐号</param>
        /// <param name="password">密码</param>
        /// <param name="automatic">自动登录</param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_AddQQ")]
        public static extern IntPtr AddQQ(byte[] autoid, string robotQq, string password, bool automatic);

        /// <summary>
        ///     登录指定QQ
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_LoginQQ")]
        public static extern void LoginQQ(byte[] autoid, string robotQq);

        /// <summary>
        ///     离线指定QQ
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_OffLineQQ")]
        public static extern void OffLineQQ(byte[] autoid, string robotQq);

        /// <summary>
        ///     删除指定QQ
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_DelQQ")]
        public static extern IntPtr DelQQ(byte[] autoid, string robotQq);

        /// <summary>
        ///     删除指定好友
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <param name="qq">被删除对象</param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_DelFriend")]
        public static extern bool DelFriend(byte[] autoid, string robotQq, string qq);

        /// <summary>
        ///     修改好友备注名称
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <param name="message">需要修改的备注姓名</param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_SetFriendsRemark")]
        public static extern void SetFriendsRemark(byte[] autoid, string robotQq, string qq, string message);

        /// <summary>
        ///     邀请好友加入群
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="group">被邀请加入的群号</param>
        /// <param name="qq"></param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_InviteGroup")]
        public static extern void InviteGroup(byte[] autoid, string robotQq, string group, string qq);

        /// <summary>
        ///     邀请群成员加入群
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="group">邀请到哪个群</param>
        /// <param name="groupY">被邀请成员所在群</param>
        /// <param name="qq">被邀请人的QQ</param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_InviteGroupMember")]
        public static extern bool InviteGroupMember(byte[] autoid, string robotQq, string group, string groupY,
            string qq);

        /// <summary>
        ///     创建群 组包模式
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_CreateDisGroup")]
        public static extern IntPtr CreateDisGroup(byte[] autoid, string robotQq);

        /// <summary>
        ///     创建群 群官网Http模式
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_CreateGroup")]
        public static extern IntPtr CreateGroup(byte[] autoid, string robotQq);

        /// <summary>
        ///     退出群
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="group">欲退出的群号</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_QuitGroup")]
        public static extern void QuitGroup(byte[] autoid, string robotQq, string group);

        /// <summary>
        ///     屏蔽或接收某群消息
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <param name="messageType">真 为屏蔽接收  假为接收并提醒</param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_SetShieldedGroup")]
        public static extern void SetShieldedGroup(byte[] autoid, string robotQq, string group, bool messageType);

        /// <summary>
        ///     多功能删除好友 可删除陌生人或者删除为单项好友
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <param name="messageType">1为在对方的列表删除我 2为在我的列表删除对方</param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_DelFriend_A")]
        public static extern void DelFriend_A(byte[] autoid, string robotQq, string qq, int messageType);

        /// <summary>
        ///     设置机器人被添加好友时的验证方式
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="messageType">0 允许任何人 1 需要验证消息 2不允许任何人 3需要回答问题 4需要回答问题并由我确认</param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_Setcation")]
        public static extern void Setcation(byte[] autoid, string robotQq, int messageType);

        /// <summary>
        ///     设置机器人被添加好友时的问题与答案
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="problem">设置的问题</param>
        /// <param name="answer">设置的问题答案 </param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_Setcation_problem_A")]
        public static extern void Setcation_problem_A(byte[] autoid, string robotQq, string problem, string answer);

        /// <summary>
        ///     设置机器人被添加好友时的三个可选问题
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="problem1">设置问题一</param>
        /// <param name="problem2">设置问题二</param>
        /// <param name="problem3">设置问题三</param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_Setcation_problem_B")]
        public static extern void Setcation_problem_B(byte[] autoid, string robotQq, string problem1, string problem2,
            string problem3);

        /// <summary>
        ///     主动添加好友
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <param name="message">验证消息</param>
        /// <param name="xxlay">(来源信息)1QQ号码查找 2昵称查找 3条件查找 5临时会话 6QQ群 10QQ空间 11拍拍网 12最近联系人 14企业查找 其他的自己测试吧 1-255</param>
        /// <returns>请求成功返回真否则返回假</returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_AddFriend")]
        public static extern bool AddFriend(byte[] autoid, string robotQq, string qq, string message, int xxlay);

        /// <summary>
        ///     发送json结构消息
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="sendType">(发送方式)1普通 2匿名（匿名需要群开启）</param>
        /// <param name="messageType">(信息类型)0在线临时会话 1好友 2群 3讨论组 4群临时会话 5讨论组临时会话 7好友验证回复会话</param>
        /// <param name="group">发送群信息、讨论组、群或讨论组临时会话信息时填，如发送对象为好友或信息类型是0时可空</param>
        /// <param name="qq"></param>
        /// <param name="jsonMessage">Json结构内容</param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_SendJSON")]
        public static extern void SendJSON(byte[] autoid, string robotQq, int sendType, int messageType, string group,
            string qq, string jsonMessage);

        /// <summary>
        ///     发送xml结构消息
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="sendType">(发送方式)1普通 2匿名（匿名需要群开启）</param>
        /// <param name="messageType">(信息类型)0在线临时会话 1好友 2群 3讨论组 4群临时会话 5讨论组临时会话 7好友验证回复会话</param>
        /// <param name="group">发送群信息、讨论组、群或讨论组临时会话信息时填，如发送对象为好友或信息类型是0时可空</param>
        /// <param name="qq"></param>
        /// <param name="xmlMessage">XML结构内容</param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_SendXML")]
        public static extern void SendXML(byte[] autoid, string robotQq, int sendType, int messageType, string group,
            string qq, string xmlMessage);

        /// <summary>
        ///     上传silk语音文件
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="sendType">(上传类型)2、QQ群 讨论组</param>
        /// <param name="group">需上传的群号</param>
        /// <param name="message">语音字节集数据（AMR Silk编码）</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_UpLoadVoice")]
        public static extern IntPtr UpLoadVoice(byte[] autoid, string robotQq, int sendType, string group,
            byte[] message);

        /// <summary>
        ///     发送普通消息支持群匿名方式
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="messageType"></param>
        /// <param name="group"></param>
        /// <param name="qq"></param>
        /// <param name="message"></param>
        /// <param name="bubbleId"></param>
        /// <param name="anonymous">不需要匿名请填写假 可调用Api_GetAnon函数 查看群是否开启匿名如果群没有开启匿名发送消息会失败</param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_SendMsgEX")]
        public static extern IntPtr SendMsgEX(byte[] autoid, string robotQq, int messageType, string group, string qq,
            string message, int bubbleId, bool anonymous);

        /// <summary>
        ///     通过语音GUID获取语音文件下载连接
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="message">(语音GUID)[IR:Voi={xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxx}.amr]</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_GetVoiLink")]
        public static extern IntPtr GetVoiLink(byte[] autoid, string robotQq, string message);

        /// <summary>
        ///     开关群匿名功能
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <param name="kg">真开    假关</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_SetAnon")]
        public static extern bool SetAnon(byte[] autoid, string robotQq, string group, bool kg);

        /// <summary>
        ///     修改机器人自身头像
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="message">(图像数据)</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_SetHeadPic")]
        public static extern bool SetHeadPic(byte[] autoid, string robotQq, byte[] message);

        /// <summary>
        ///     语音GUID转换为文本内容
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="ckdx">参考对象 群消息为群号，其他为qq号</param>
        /// <param name="cklx">参考类型 群、好友、临时会话等</param>
        /// <param name="yyGuid">语音GUID</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_VoiToText")]
        public static extern IntPtr VoiToText(byte[] autoid, string robotQq, string ckdx, int cklx, string yyGuid);

        /// <summary>
        ///     群签到
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="group"></param>
        /// <param name="address">签到地名</param>
        /// <param name="message">想发表的内容</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_SignIn")]
        public static extern bool SignIn(byte[] autoid, string robotQq, string group, string address, string message);

        /// <summary>
        ///     向好友发送窗口抖动消息
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_ShakeWindow")]
        public static extern bool ShakeWindow(byte[] autoid, string robotQq, string qq);

        /// <summary>
        ///     同步发送消息 有返回值可以用来撤回机器人自己发送的消息
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="messageType"></param>
        /// <param name="group"></param>
        /// <param name="qq"></param>
        /// <param name="message"></param>
        /// <param name="bubbleId"></param>
        /// <param name="anonymous"></param>
        /// <param name="jsonMessage">附加JSON参数</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_SendMsgEX_V2")]
        public static extern IntPtr SendMsgEX_V2(byte[] autoid, string robotQq, int messageType, string group,
            string qq, string message, int bubbleId, bool anonymous, string jsonMessage);

        /// <summary>
        ///     撤回群消息
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq">机器人QQ</param>
        /// <param name="group">群号</param>
        /// <param name="messageNumber">消息序号</param>
        /// <param name="messageId">消息ID</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_WithdrawMsg")]
        public static extern IntPtr WithdrawMsg(byte[] autoid, string robotQq, string group, string messageNumber,
            string messageId);

        /// <summary>
        ///     撤回群消息或者私聊消息
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="robotQq"></param>
        /// <param name="withdrawType">1好友 2群聊 4群临时会话 </param>
        /// <param name="group">非临时会话时请留空 临时会话请填群号</param>
        /// <param name="qq">非私聊消息请留空 私聊消息请填写对方QQ号码</param>
        /// <param name="messageNumber">需撤回消息序号</param>
        /// <param name="messageId">需撤回消息ID</param>
        /// <param name="messageTime">私聊消息需要群聊时3可留空</param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_WithdrawMsgEX")]
        public static extern IntPtr WithdrawMsgEX(byte[] autoid, string robotQq, int withdrawType, string group,
            string qq, string messageNumber, string messageId, string messageTime);

        [DllImport(DllName, CharSet = CharSet.Ansi)]
        public static extern IntPtr GetGroupList(string robotQq);

        #endregion 发送 OR 设置

        #region Debug 函数

        /// <summary>
        ///     标记函数执行流程 debug时使用 每个函数内只需要调用一次
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="message">标记内容</param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_DbgName")]
        public static extern void DbgName(byte[] autoid, string message);

        /// <summary>
        ///     函数内标记附加信息 函数内可多次调用
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="message"></param>
        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "S3_Api_Mark")]
        public static extern void Mark(byte[] autoid, string message);

        #endregion Debug 函数
    }
}