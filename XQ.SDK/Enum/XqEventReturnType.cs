namespace XQ.SDK.Enum
{
    /// <summary>
    ///     在处理XQEvent时的返回值类型
    /// </summary>
    public enum XqEventReturnType
    {
        /// <summary>
        ///     忽略,允许优先级较低的其他插件继续处理
        /// </summary>
        Ignore = 1,

        /// <summary>
        ///     拦截,不允许优先级较低的其他插件处理
        /// </summary>
        Intercept = 2
    }
}