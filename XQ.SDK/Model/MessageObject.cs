using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using XQ.SDK.Enum;

namespace XQ.SDK.Model
{
    public class XqMessageObject
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

        public string ToSendString;

        public XqMessageObject(string toSendString)
        {
            ToSendString = toSendString;
        }

        public XqMessageObject RandomNumber(int begin, int end)
        {
            if (begin < end) throw new ArgumentException();
            return new XqMessageObject($"[{begin},{end}]");
        }

        public XqMessageObject At(string qq)
        {
            return new XqMessageObject($"[@{qq}]");
        }

        /// <summary>
        ///     number范围为1-213
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public XqMessageObject Face(int number)
        {
            return new XqMessageObject($"[Face{number}.gif]");
        }

        public static implicit operator XqMessageObject(string str)
        {
            return new XqMessageObject(str);
        }

        public static implicit operator string(XqMessageObject obj)
        {
            return obj.ToSendString;
        }
    }

    public class VoiceMessageObject : XqMessageObject
    {
        public VoiceMessageObject(string toSendString) : base(toSendString)
        {
        }

        public static implicit operator VoiceMessageObject(string str)
        {
            return new VoiceMessageObject(str);
        }


        /// <summary>
        ///     获取语音ImageGuid
        /// </summary>
        /// <param name="message"></param>
        /// <param name="guidList"></param>
        /// <returns>是否获取成功</returns>
        public bool VoiceGuid(string message, out List<VoiceMessageObject> guidList)
        {
            guidList = null;
            try
            {
                guidList = !message.Contains("[Voi=")
                    ? null
                    : (from Match item in Regex.Matches(message,
                            @"([Voi])(.)+?(?=\])")
                        select $"[{item.Value}]").Select(i => new VoiceMessageObject(i))
                    .ToList();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class ImageMessageObject : XqMessageObject
    {
        private readonly bool _fromFile;

        private ImageMessageObject(string toSendString, bool fromFile = false) : base(toSendString)
        {
            _fromFile = fromFile;
        }

        private string Rawmsg => ToSendString.Substring(5, ToSendString.Length - 6);

        public static implicit operator ImageMessageObject(string str)
        {
            return new ImageMessageObject(str);
        }

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
            return new XqMessageObject(ToSendString.Replace("[pic=", "[ShowPic=") + $",type={(int) type}]");
        }

        public XqMessageObject ToFlashPic()
        {
            return new XqMessageObject(ToSendString.Replace("[pic=", "[FlashPic="));
        }

        /// <summary>
        ///     获取图片ImageGuid
        /// </summary>
        /// <param name="message"></param>
        /// <param name="guidList"></param>
        /// <returns>是否获取成功</returns>
        public bool GetFromMessage(string message, out List<ImageMessageObject> guidList)
        {
            guidList = null;
            try
            {
                guidList = !message.Contains("[pic=")
                    ? null
                    : (from Match item in Regex.Matches(message,
                            @"([pic])(.)+?(?=\])")
                        select $"[{item.Value}]").Select(i => new ImageMessageObject(i))
                    .ToList();
                return true;
            }
            catch
            {
                return false;
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
                return binaryWriter.ReadBytes((int) fs.Length);
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
    }
}