using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using XQ.SDK.Interface;

namespace XQ.SDK.Model
{
    public class VoiceMessageObject : IToSendString
    {
        private readonly string _sendString;

        public VoiceMessageObject(string sendString)
        {
            _sendString = sendString;
        }

        public static implicit operator VoiceMessageObject(string str)
        {
            return new VoiceMessageObject(str);
        }

        /// <summary>
        ///     获取语音消息的Guid
        /// </summary>
        /// <param name="message"></param>
        public static VoiceMessageObject GetFromMessage(string message)
        {
            try
            {
                return !message.Contains("[Voi=")
                    ? null
                    : new VoiceMessageObject($"[{Regex.Match(message, @"([Voi])(.)+?(?=\])").Value}]");
            }
            catch
            {
                return null;
            }
        }

        public string ToSendString()
        {
            return _sendString;
        }
    }
}