namespace XQ.SDK.Enum
{
    /// <summary>
    ///     检查登录二维码状态时的返回值类型
    /// </summary>
    public enum XqQrCodeType
    {
        /// <summary>
        ///     失效
        /// </summary>
        Failure = -1,

        /// <summary>
        ///     获取失败
        /// </summary>
        GetCodeFailure = 0,

        /// <summary>
        ///     等待扫码
        /// </summary>
        WaitForScan = 1,

        /// <summary>
        ///     等待确认
        /// </summary>
        WaitForConfirm = 2,

        /// <summary>
        ///     正在登陆
        /// </summary>
        Logining = 3
    }
}