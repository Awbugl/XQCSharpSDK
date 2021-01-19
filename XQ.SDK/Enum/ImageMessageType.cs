namespace XQ.SDK.Enum
{
    /// <summary>
    ///     图片消息的来源
    /// </summary>
    public enum ImageMessageType
    {
        /// <summary>
        ///     来自文件
        /// </summary>
        FromFile = 0,

        /// <summary>
        ///     来自Url
        /// </summary>
        FromWebUrl = 1,

        /// <summary>
        ///     来自收到的消息
        /// </summary>
        FromMessage = 2
    }
}