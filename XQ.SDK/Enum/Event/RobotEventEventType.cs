namespace XQ.SDK.Enum.Event
{
    /// <summary>
    ///     Robot事件事件的EventType
    /// </summary>
    public enum RobotEventEventType
    {
        /// <summary>
        ///     列表添加了新帐号
        /// </summary>
        RobotNewAccount = 1100,

        /// <summary>
        ///     QQ登录完成
        /// </summary>
        RobotLoggedIn = 1101,

        /// <summary>
        ///     QQ被手动离线
        /// </summary>
        RobotManualOffline = 1102,

        /// <summary>
        ///     QQ被强制离线
        /// </summary>
        RobotForcedOffline = 1103,

        /// <summary>
        ///     QQ掉线
        /// </summary>
        RobotOffline = 1104,

        /// <summary>
        ///     QQ二次数据缓存完成
        /// </summary>
        RobotSecondaryDataBuffered = 1105,

        /// <summary>
        ///     QQ登录需要令牌
        /// </summary>
        RobotNeedTokenVerify = 1106,

        /// <summary>
        ///     QQ登录需要短信
        /// </summary>
        RobotNeedSmsVerify = 1107,

        /// <summary>
        ///     QQ登录失败
        /// </summary>
        RobotLoginFailed = 1108
    }
}