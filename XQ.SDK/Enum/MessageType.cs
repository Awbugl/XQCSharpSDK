using System;

namespace XQ.SDK.Enum
{
    /// <summary>
    ///     消息类型
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        ///     未定义
        /// </summary>
        Undefined = -1,

        /// <summary>
        ///     在线状态临时会话
        /// </summary>
        TempOnlineSession = 0,

        /// <summary>
        ///     好友私聊消息
        /// </summary>
        Friend = 1,

        /// <summary>
        ///     群消息
        /// </summary>
        Group = 2,

        /// <summary>
        ///     讨论组
        /// </summary>
        [Obsolete] Discuss = 3,

        /// <summary>
        ///     来自群的临时消息
        /// </summary>
        TempGroupMessage = 4,

        /// <summary>
        ///     来自讨论组的临时消息
        /// </summary>
        [Obsolete] TempDiscussMessage = 5,

        /// <summary>
        ///     收到财付通转账
        /// </summary>
        Transfer = 6,

        /// <summary>
        ///     来自好友验证的对话消息
        /// </summary>
        AddFriendReply = 7,

        /// <summary>
        ///     其他客户端发来消息
        /// </summary>
        MessageFromOtherClient = 8,

        /// <summary>
        ///     消息被撤回
        ///     子类型 1:好友,2:群
        /// </summary>
        MsgWithdrawn = 9,

        /// <summary>
        ///     群回音消息
        /// </summary>
        GroupEchoMsg = 10
    }
}