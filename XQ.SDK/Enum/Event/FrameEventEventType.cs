namespace XQ.SDK.Enum.Event
{
    /// <summary>
    ///     XQ框架事件的EventType
    /// </summary>
    public enum FrameEventEventType
    {
        /// <summary>
        ///     框架启动完成
        /// </summary>
        FrameStarted = 10000,

        /// <summary>
        ///     框架即将关闭
        /// </summary>
        FrameClosing = 10001,

        /// <summary>
        ///     插件被点击
        ///     点击方式: 子类型: 1:左键单击 2:右键单击 3:左键双击
        /// </summary>
        PluginClicked = 12003
    }
}