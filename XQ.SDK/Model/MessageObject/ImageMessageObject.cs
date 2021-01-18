using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using XQ.SDK.Enum;
using XQ.SDK.Interface;

namespace XQ.SDK.Model
{
    public class ImageMessageObject : IToSendString
    {
        private readonly bool _fromFile;

        private readonly string _sendString;

        private ImageMessageObject(string toSendString, bool fromFile = false)
        {
            _sendString = toSendString;
            _fromFile = fromFile;
        }

        private string Rawmsg => _sendString.Substring(5, _sendString.Length - 6);

        public static implicit operator ImageMessageObject(string str)
        {
            return new ImageMessageObject(str);
        }

        /// <summary>
        /// 请使用绝对路径
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static ImageMessageObject FromFile(string filename)
        {
            return new ImageMessageObject($"[pic={filename}]", true);
        }

        public static ImageMessageObject FromUrl(string url)
        {
            return new ImageMessageObject($"[pic={url}]");
        }

        public XqMessageObject ToShowPic(XqShowPicType type)
        {
            return new XqMessageObject(_sendString.Replace("[pic=", "[ShowPic=") + $",type={(int)type}]");
        }

        public XqMessageObject ToFlashPic()
        {
            return new XqMessageObject(_sendString.Replace("[pic=", "[FlashPic="));
        }

        /// <summary>
        ///     获取消息中所有图片的Guid
        /// </summary>
        /// <param name="message"></param>
        /// <param name="guidList"></param>
        /// <returns>是否获取成功</returns>
        public static List<ImageMessageObject> GetFromMessage(string message)
        {
            try
            {
                return !message.Contains("[pic=")
                    ? null
                    : (from Match item in Regex.Matches(message,
                            @"([pic])(.)+?(?=\])")
                        select $"[{item.Value}]").Select(i => new ImageMessageObject(i))
                    .ToList();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///     图片转byte[]
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes()
        {
            Stream fs = null;
            BinaryReader binaryWriter = null;
            try
            {
                fs = _fromFile
                    ? new FileStream(Rawmsg, FileMode.Open, FileAccess.Read)
                    : GetFromWeb();
                binaryWriter = new BinaryReader(fs);
                return binaryWriter.ReadBytes((int)fs.Length);
            }
            finally
            {
                binaryWriter?.Close();
                fs?.Close();
                binaryWriter?.Dispose();
                fs?.Dispose();
            }
        }

        private Stream GetFromWeb()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(Rawmsg),
                Method = HttpMethod.Get
            };
            return httpClient.SendAsync(request).Result.Content.ReadAsStreamAsync().Result;
        }

        public bool Download(string filename)
        {
            if (_fromFile) return false;

            Image i = null;
            try
            {
                i = Image.FromStream(GetFromWeb());
                i.Save(filename);
                return true;
            }
            finally
            {
                i?.Dispose();
            }
        }

        public string ToSendString()
        {
            return _sendString;
        }
    }
}