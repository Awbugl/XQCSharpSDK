namespace XQ.SDK.Enum.Event
{
    /// <summary>
    ///     群事件对应的eventType
    /// </summary>
    public enum XqGroupEventType
    {
        /// <summary>
        ///     某人离开了群聊
        /// </summary>
        SomeoneLeaveGroup = 201,

        /// <summary>
        ///     某人被管理移除群
        /// </summary>
        SomeoneBeRemovedFromGroup = 202,

        /// <summary>
        ///     某人被禁言
        /// </summary>
        SomeoneBeBannedSpeaking = 203,

        /// <summary>
        ///     某人被解除禁言
        /// </summary>
        SomeoneBeUnBannedSpeaking = 204,

        /// <summary>
        ///     开启全群禁言
        /// </summary>
        GroupBannedSpeaking = 205,

        /// <summary>
        ///     关闭全群禁言
        /// </summary>
        GroupUnBannedSpeaking = 206,

        /// <summary>
        ///     开启匿名聊天
        /// </summary>
        AnonymousModeTurnedOn = 207,

        /// <summary>
        ///     关闭匿名聊天
        /// </summary>
        AnonymousModeTurnedOff = 208,

        /// <summary>
        ///     群公告变动
        /// </summary>
        GroupAnnouncementChanged = 209,

        /// <summary>
        ///     某人成为管理
        /// </summary>
        AdminListNumIncrease = 210,

        /// <summary>
        ///     某人被取消管理
        /// </summary>
        AdminListNumDecrease = 211,

        /// <summary>
        ///     某人被批准加入群
        /// </summary>
        AllowedJoinGroup = 212,

        /// <summary>
        ///     某人申请入群
        /// </summary>
        JoinGroupRequest = 213,

        /// <summary>
        ///     某人被邀请入群
        /// </summary>
        SomeoneBeInvitedToGroup = 215,

        /// <summary>
        ///     某群被解散
        /// </summary>
        GroupDissolved = 216,

        /// <summary>
        ///     群名片变动
        /// </summary>
        GroupCardChanged = 217,

        /// <summary>
        ///     群文件接收
        /// </summary>
        RecieveGroupFile = 218,

        /// <summary>
        ///     某人被群主或管理员并且进入了群(或100以内免审核)
        /// </summary>
        SomeoneHasBeenInvitedIntoGroupByAdminOrOwner = 219,

        /// <summary>
        ///     群名称变动
        /// </summary>
        GroupNameChanged = 220,

        /// <summary>
        ///     被拒绝入群
        /// </summary>
        JoinGroupRequestRefused = 221,

        /// <summary>
        ///     群视频接收
        /// </summary>
        RecieveGroupVideo = 222
    }
}