namespace XQ.SDK.Enum.Event
{
    /// <summary>
    ///     好友、群临时事件对应的eventType
    /// </summary>
    public enum XqFriendEventType
    {
        /// <summary>
        ///     被单项添加为好友
        /// </summary>
        BeAddedAsOneWayFriend = 100,

        /// <summary>
        ///     某人请求加为好友
        /// </summary>
        AddFriendRequest = 101,

        /// <summary>
        ///     被同意加为好友
        /// </summary>
        FriendAddRequestPermitted = 102,

        /// <summary>
        ///     被拒绝加为好友
        /// </summary>
        FriendAddRequestDenied = 103,

        /// <summary>
        ///     被删除好友
        /// </summary>
        BeDeletedByFriend = 104,

        /// <summary>
        ///     好友离线文件接收
        /// </summary>
        FriendOfflineFileReceive = 105,

        /// <summary>
        ///     好友签名变更
        /// </summary>
        FriendSignatureChanged = 106,

        /// <summary>
        ///     说说被某人评论
        /// </summary>
        QZoneCommented = 107,

        /// <summary>
        ///     好友视频接收
        /// </summary>
        ReceiveFriendVideo = 108,

        /// <summary>
        ///     被好友抖动
        /// </summary>
        ShakedByFriend = 109,

        /// <summary>
        ///     群临时文件接收
        /// </summary>
        ReceiveTempGroupFile = 110,

        /// <summary>
        ///     群临时视频接收
        /// </summary>
        ReceiveTempGroupVideo = 111
    }
}