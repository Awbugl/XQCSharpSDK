namespace XQ.SDK.Enum
{
    /// <summary>
    ///     在处理加好友请求时的messageType类型
    /// </summary>
    public enum XqFriendRequestHandlerType
    {
        /// <summary>
        ///     同意
        /// </summary>
        Agree = 10,

        /// <summary>
        ///     拒绝
        /// </summary>
        Refuse = 20,

        /// <summary>
        ///     忽略
        /// </summary>
        Ingore = 30,

        /// <summary>
        ///     同意被单项加为好友
        /// </summary>
        AgreeAsOneWayFriend = 40
    }
}