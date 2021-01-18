using System;
using System.Collections.Generic;
using XQ.SDK.Enum.Event;
using XQ.SDK.Model;
using XQ.SDK.XQ;
using XQ.SDK.XQ.Json;

namespace XQ.SDK.EventArgs
{
    public class XqMessageEventArgs : XqEventArgs
    {
        public XqMessageEventArgs(XqApi xqApi, XqRawEvent rawEvent) : base(xqApi, rawEvent)
        {
        }

        public Qq FromQq => string.IsNullOrWhiteSpace(RawEvent.FromQq) ? null : new Qq(XqApi, Robot, RawEvent.FromQq);

        public string Text => RawEvent.Content;

        public XqMessageEventType Type =>
            System.Enum.IsDefined(typeof(XqMessageEventType), RawEvent.EventType)
                ? (XqMessageEventType) RawEvent.EventType
                : throw new ArgumentException("type不正确");

        /// <summary>
        ///     若不是语音消息，将返回null
        /// </summary>
        public VoiceMessageObject GetVoiceList()
        {
            return VoiceMessageObject.GetFromMessage(Text);
        }

        /// <summary>
        ///     语音信息的下载链接(silk格式)
        ///     若不是语音消息，将报错
        /// </summary>
        public string GetVoiceMessageDownloadLink(VoiceMessageObject obj)
        {
            return XqApi.TencentApi.GetVoiLink(Robot, obj);
        }

        /// <summary>
        ///     若不是语音消息，将报错
        /// </summary>
        public string VoiceMessageText(VoiceMessageObject obj)
        {
            return XqApi.TencentApi.VoiToText(Robot, RawEvent.From, (int) Type, obj);
        }

        /// <summary>
        ///     若不含图片消息，将返回null
        /// </summary>
        public List<ImageMessageObject> GetImageList()
        {
            return ImageMessageObject.GetFromMessage(Text);
        }

        /// <returns>图片的下载链接</returns>
        public string GetImageDownloadLink(ImageMessageObject obj)
        {
            return XqApi.TencentApi.GetPicLink(Robot, Type == XqMessageEventType.Group ? 2 : 1, RawEvent.From, obj);
        }

        /// <returns>图片Ocr后的信息</returns>
        public List<OcrItem> GetImageOcrResult(ImageMessageObject obj)
        {
            return XqApi.TencentApi.OcrPic(Robot, obj);
        }
    }
}