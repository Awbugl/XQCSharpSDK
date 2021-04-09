using XQ.SDK.Interface;

namespace XQ.SDK.Model.MessageObject
{
    /// <summary>
    ///     文字消息、XQ自带的部分消息缩写
    /// </summary>
    public class PlainMessage : IToSendString
    {
        /// <summary>
        ///     表示当前时间，例：2017年1月1日18时00分00秒
        /// </summary>
        public static PlainMessage Time = new PlainMessage("[Time]");

        /// <summary>
        ///     表示当前时间，例：18:00
        /// </summary>
        public static PlainMessage NumTime = new PlainMessage("[NumTime]");

        /// <summary>
        ///     FromGroup的群名
        /// </summary>
        public static PlainMessage FromGroupName = new PlainMessage("[GName]");

        /// <summary>
        ///     FromQQ的昵称
        /// </summary>
        public static PlainMessage FromQqName => new PlainMessage("[ObjName]");
        
        /// <summary>
        ///     机器人昵称
        /// </summary>
        public static PlainMessage RobotName = new PlainMessage("[RName]");

        /// <summary>
        ///     机器人Id
        /// </summary>
        public static PlainMessage RobotId = new PlainMessage("[RQQ]");

        /// <summary>
        ///     随机Face表情
        /// </summary>
        public static PlainMessage RandomFace = new PlainMessage("[RFace]");

        /// <summary>
        ///     将一条消息分作两次发送
        ///     一条消息中只能有一个
        /// </summary>
        public static PlainMessage SegmentMark = new PlainMessage("[Next]");

        /// <summary>
        ///     At全群
        /// </summary>
        public static PlainMessage AtAll = new PlainMessage("[@all]");
        
        /// <summary>
        ///     At某人
        /// </summary>
        /// <param name="qq">对象QQ</param>
        /// <returns></returns>
        public static PlainMessage At(string qq)
        {
            return new PlainMessage($"[@{qq}]");
        }

        /// <summary>
        ///     表情
        ///     对照表请参考 https://github.com/kyubotics/coolq-http-api/wiki/%E8%A1%A8%E6%83%85-CQ-%E7%A0%81-ID-%E8%A1%A8
        /// </summary>
        /// <param name="number">表情代码</param>
        /// <returns></returns>
        public static PlainMessage Face(int number)
        {
            return new PlainMessage($"[Face{number}.gif]");
        }

        private readonly string _sendString;

        public PlainMessage(string sendString)
        {
            _sendString = sendString;
        }

        public string ToSendString()
        {
            return _sendString;
        }

        public override string ToString()
        {
            return ToSendString();
        }

        public static implicit operator PlainMessage(string str)
        {
            return new PlainMessage(str);
        }
    }
}