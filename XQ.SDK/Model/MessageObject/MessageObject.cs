using System;
using XQ.SDK.Interface;

namespace XQ.SDK.Model
{
    public class XqMessageObject : IToSendString
    {
        public static readonly XqMessageObject
            TargetName = new XqMessageObject("[ObjName]"),
            TargetId = new XqMessageObject("[ObjQQ]"),
            FromHeadPic = new XqMessageObject("[DisPic]"),
            Time = new XqMessageObject("[Time]"),
            NumTime = new XqMessageObject("[NumTime]"),
            GroupName = new XqMessageObject("[GName]"),
            GroupId = new XqMessageObject("[GNum]"),
            RobotId = new XqMessageObject("[RQQ]"),
            RandomFace = new XqMessageObject("[RFace]"),
            SegmentMark = new XqMessageObject("[Next]"),
            AtAll = new XqMessageObject("[@all]");

        private readonly string _sendString;

        public XqMessageObject(string sendString)
        {
            _sendString = sendString;
        }

        public string ToSendString()
        {
            return _sendString;
        }

        public static explicit operator XqMessageObject(string str)
        {
            return new XqMessageObject(str);
        }

        public static XqMessageObject RandomNumber(int begin, int end)
        {
            if (begin < end) throw new ArgumentException();
            return new XqMessageObject($"[{begin},{end}]");
        }

        public static XqMessageObject At(string qq)
        {
            return new XqMessageObject($"[@{qq}]");
        }

        /// <summary>
        ///     对照表请参考 https://github.com/kyubotics/coolq-http-api/wiki/%E8%A1%A8%E6%83%85-CQ-%E7%A0%81-ID-%E8%A1%A8
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static XqMessageObject Face(int number)
        {
            return new XqMessageObject($"[Face{number}.gif]");
        }
    }
}