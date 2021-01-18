namespace XQ.SDK.Enum
{
    /// <summary>
    ///     好友请求的QQ处理方式
    /// </summary>
    public enum XqFriendAddRequestType
    {
        /// <summary>
        ///     允许任何人
        /// </summary>
        Anyone = 0,

        /// <summary>
        ///     需要验证消息
        /// </summary>
        NeedValidate = 1,

        /// <summary>
        ///     不允许任何人
        /// </summary>
        Nobody = 2,

        /// <summary>
        ///     需要正确回答问题
        /// </summary>
        NeedAnswerQuestionCorrectly = 3,

        /// <summary>
        ///     需要回答问题并由我确认
        /// </summary>
        NeedAnswerQuestionAndValidate = 4
    }
}