using System;

namespace XQ.SDK.Model
{
    public class XqRawEvent
    {
        /// <summary>
        ///     机器人QQ
        ///     用于判定哪个QQ接收到该消息
        /// </summary>
        public string RobotQq { get; }

        /// <summary>
        ///     消息类型
        ///     可在对应枚举中查询具体定义
        /// </summary>
        public int EventType { get; }

        /// <summary>
        ///     消息子类型
        ///     在不同消息类型下有不同的定义
        ///     eg.：接收财付通转账时 1为好友 4为群临时会话 5为讨论组临时会话
        ///     有人请求入群时，不良成员为1
        /// </summary>
        public int ExtraType { get; }

        /// <summary>
        ///     此消息的来源
        ///     如：群号、讨论组ID、临时会话QQ、好友QQ等
        /// </summary>
        public string From { get; }

        /// <summary>
        ///     触发对象_主动
        ///     主动发送这条消息的QQ，踢人时为踢人管理员QQ
        /// </summary>
        public string FromQq { get; }

        /// <summary>
        ///     触发对象_被动
        ///     被动触发的QQ，如某人被踢出群，则此参数为被踢出人QQ
        /// </summary>
        public string TargetQq { get; }

        /// <summary>
        ///     消息内容
        ///     此参数有多重含义，一般为对方发送的消息内容
        ///     eg.：当某人申请入群时，为入群申请理由
        /// </summary>
        public string Content { get; }

        /// <summary>
        ///     消息序号
        ///     用于消息回复，消息撤回
        /// </summary>
        public string Index { get; }

        /// <summary>
        ///     消息ID
        ///     用于消息回复，消息撤回
        /// </summary>
        public string Msgid { get; }

        /// <summary>
        ///     UDP收到的原始信息
        ///     特殊情况下会返回JSON结构
        ///     （入群事件时，这里为该事件seq）
        /// </summary>
        public string Udpmsg { get; }

        /// <summary>
        ///     接受到消息的时间戳
        /// </summary>
        public string Unix { get; }

        /// <summary>
        ///     回传文本指针,此参数用于插件加载拒绝理由
        ///     易语言用法：写到内存（“拒绝理由”，XQ_回传文本指针，255）
        ///     最大写入字节数量为255,超过此长度可能导致插件异常崩溃
        /// </summary>
        public IntPtr Intptr { get; }

        public XqRawEvent(string robotQq, int eventType, int extraType, string from, string fromQq,
            string targetQq, string content, string index, string msgid, string udpmsg, string unix, int p)
        {
            RobotQq = robotQq?.Trim();
            EventType = eventType;
            ExtraType = extraType;
            From = from?.Trim();
            FromQq = fromQq?.Trim();
            TargetQq = targetQq?.Trim();
            Content = content?.Trim();
            Index = index?.Trim();
            Msgid = msgid?.Trim();
            Udpmsg = udpmsg?.Trim();
            Unix = unix?.Trim();
            Intptr = new IntPtr(p);
        }

        public override string ToString()
        {
            return
                $"RobotQq : {RobotQq}\n" +
                $"EventType : {EventType}\n" +
                $"ExtraType : {ExtraType}\n" +  
                $"From : {From}\n" +
                $"FromQq : {FromQq}\n" +
                $"TargetQq : {TargetQq}\n" +
                $"Content : {Content}\n" +
                $"Index : {Index}\n" +
                $"Udpmsg : {Udpmsg}\n" +
                $"Udpmsg : {Udpmsg}\n" +
                $"Unix : {Unix}\n" +
                $"Intptr : {Intptr}";
        }
    }
}