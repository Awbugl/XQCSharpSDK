using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using XQ.SDK.Enum;
using XQ.SDK.Interface;
using XQ.SDK.XQ;

namespace XQ.SDK.Model.MessageObject
{
    /// <summary>
    ///     语音消息
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class VoiceMessage : IToSendString
    {
        private readonly string _sendString;
        private readonly XqApi _xqapi;

        public readonly string RobotQq;

        private VoiceMessage(XqApi xqapi, string robotQq, string sendString)
        {
            _sendString = sendString;
            RobotQq = robotQq;
            _xqapi = xqapi;
        }

        public string ToSendString()
        {
            return _sendString;
        }

        /// <summary>
        ///     语音信息的下载链接(silk格式)
        /// </summary>
        public string GetVoiceMessageDownloadLink()
        {
            return _xqapi.GetVoiLink(this);
        }

        /// <summary>
        ///     语音转文字
        /// </summary>
        /// <param name="groupOrQq">发送对象的群号或QQ</param>
        /// <param name="type"></param>
        public string VoiceMessageText(string groupOrQq, MessageType type)
        {
            return _xqapi.VoiToText(groupOrQq, type, this);
        }


        /// <summary>
        ///     获取语音消息的Guid
        /// </summary>
        /// <param name="message"></param>
        /// <param name="api"></param>
        /// <param name="robotqq"></param>
        public static VoiceMessage GetFromMessage(XqApi api, string robotqq, string message)
        {
            try
            {
                return !message.Contains("[Voi=")
                    ? null
                    : new VoiceMessage(api, robotqq, $"[{Regex.Match(message, @"([Voi])(.)+?(?=\])").Value}]");
            }
            catch
            {
                return null;
            }
        }
    }
}