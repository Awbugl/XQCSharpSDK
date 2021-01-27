using System.Collections.Generic;
using XQ.SDK.Interface;
using XQ.SDK.Model.MessageObject;
using XQ.SDK.XQ;

namespace XQ.SDK.Model
{
    /// <summary>
    ///     事件中的消息
    /// </summary>
    public class QqMessage : IToSendString
    {
        private readonly string _robotqq;

        private readonly XqApi _xqapi;

        public QqMessage(XqApi xqapi, string robotqq, string message)
        {
            RawContent = message;
            _xqapi = xqapi;
            _robotqq = robotqq;
        }

        /// <summary>
        ///     消息原始文本
        /// </summary>
        public string RawContent { get; }

        public string ToSendString()
        {
            return RawContent;
        }

        public override string ToString()
        {
            return ToSendString();
        }

        /// <summary>
        ///     获取语音消息
        ///     若不是语音消息将返回null
        /// </summary>
        /// <returns></returns>
        public VoiceMessage GetVoiceMessage()
        {
            return VoiceMessage.GetFromMessage(_xqapi, _robotqq, RawContent);
        }

        /// <summary>
        ///     获取图片消息
        ///     若不含图片消息将返回null
        /// </summary>
        /// <returns></returns>
        public List<ImageMessage> GetImageMessages()
        {
            return ImageMessage.GetFromMessage(_xqapi, _robotqq, RawContent);
        }
    }
}