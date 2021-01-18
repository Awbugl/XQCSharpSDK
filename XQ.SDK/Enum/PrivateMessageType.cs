using System;

namespace XQ.SDK.Enum
{
    public enum PrivateMessageType
    {
        /// <summary>
        ///     在线状态临时会话
        /// </summary>
        TempOnlineSession = 0,

        /// <summary>
        ///     好友私聊消息
        /// </summary>
        Friend = 1,

        /// <summary>
        ///     来自群的临时消息
        /// </summary>
        TempGroupMessage = 4,

        /// <summary>
        ///     来自讨论组的临时消息
        /// </summary>
        [Obsolete] TempDiscussMessage = 5,

        /// <summary>
        ///     来自好友验证的对话消息
        /// </summary>
        AddFriendReply = 7
    }
}