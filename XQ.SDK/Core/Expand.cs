using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

using XQ.SDK.Interface;

namespace XQ.SDK.Core
{
    /// <summary>
    ///     扩展函数
    /// </summary>
    [SuppressMessage("ReSharper", "CommentTypo")]
    public static class Expand
    {
        private static readonly Lazy<Regex> Reg = new Lazy<Regex>(() =>
            new Regex("(&nbsp;|\\[em\\](e[0-9]{1,6})\\[\\/em\\])", RegexOptions.IgnoreCase | RegexOptions.ECMAScript));

        private static readonly Encoding Gb18030 = Encoding.GetEncoding("gb18030");

        /// <summary>
        ///     将以换行符分割的string转换为list
        /// </summary>
        public static List<string> SplitToList(this string str)
        {
            return str.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(i => i.Trim()).ToList();
        }

        /// <summary>
        ///     将IntPtrToString中未转换的[em]exxxxxx[/em]转换为Emoji码
        ///     代码逻辑来自w4123/CQXQ
        /// </summary>
        private static string EmojiToSendString(this string msg)
        {
            var ret = new StringBuilder();
            var last = 0;
            Match rslt;
            while ((rslt = Reg.Value.Match(msg.Substring(last))).Success)
            {
                var m = rslt.Groups;
                ret.Append(msg.Substring(last, m[0].Index));
                if (m[0].Value[0] == '&')
                {
                    ret.Append(" ");
                }
                else
                {
                    var codePoint = int.Parse(m[2].Value.Substring(1));
                    if (codePoint > 200000)
                        ret.Append(
                            $"[emoji={Encoding.Convert(Encoding.UTF32, Encoding.UTF8, BitConverter.GetBytes(codePoint - 200000)).Aggregate("", (current, i) => current + i.ToString("X2"))}]");
                    else if (codePoint >= 100000)
                        ret.Append($"[Face{codePoint - 100000}.gif]");
                    else
                        ret.Append($"[pic=http://qzonestyle.gtimg.cn/qzone/em/{m[2].Value}.gif]");
                }

                last += rslt.Index + rslt.Length;
            }

            ret.Append(msg.Substring(last));
            return ret.ToString();
        }

        /// <summary>
        ///     将Api返回的IntPtr转换为string和Emoji码
        /// </summary>
        public static string IntPtrToString(this IntPtr intPtr)
        {
            if (intPtr == IntPtr.Zero) return "";
            try
            {
                var length = Marshal.ReadInt32(intPtr);
                if (length <= 0) return "";
                var bin = new byte[length];
                Marshal.Copy(IntPtr.Add(intPtr, 4), bin, 0, length);
                return EmojiToSendString(BytesToString(bin));
            }
            finally
            {
                Kernel32.HeapFree(Kernel32.GetProcessHeap(), 0, intPtr);
            }
        }

        /// <summary>
        ///     将文本中的编码错位部分转换成Emoji码
        /// </summary>
        /// <param name="utf8String">要发送的消息</param>
        /// <returns></returns>
        private static string Utf8ToSendString(this string utf8String)
        {
            return BytesToString(Encoding.Convert(Encoding.UTF8, Gb18030, Encoding.UTF8.GetBytes(utf8String)));
        }

        private static string BytesToString(byte[] bin)
        {
            var length = bin.Length;
            if (length == 1) return Gb18030.GetString(bin);

            var sb = new StringBuilder();
            for (var i = 0; i < length;)
                sb.Append(EncodingGetString(Gb18030, bin, ref i, bin[i] < 0x80 ? 1 : bin[i + 1] > 0x3F ? 2 : 4));

            return sb.ToString();
        }

        /// <summary>
        ///     使用Gb18030编码
        /// </summary>
        private static string EncodingGetString(Encoding encoding, byte[] bin, ref int index, int count)
        {
            index += count;

            return count < 4
                ? encoding.GetString(bin, index - count, count)
                : Encoding.Convert(encoding, Encoding.UTF8,
                    bin.Skip(index - count).Take(4).ToArray()).Aggregate("[emoji=", (current, bi)
                    => current + bi.ToString("X2")) + "]";
        }

        /// <summary>
        ///     将对象转换为可发送的字符串, 如果待转换的对象继承自 <see cref="IToSendString" /> 将使用该接口的方法获取字符串
        /// </summary>
        /// <param name="objects">消息参数</param>
        /// <returns>可发送的字符串</returns>
        public static string ToSend(this object[] objects)
        {
            var builder = new StringBuilder();
            foreach (var t in objects)
                switch (t)
                {
                    case null:
                        continue;
                    case object[] objs:
                        builder.Append(objs.ToSend());
                        break;
                    case string str:
                        builder.Append(str.Utf8ToSendString());
                        break;
                    case IToSendString toSend:
                        builder.Append(toSend.ToSendString().Utf8ToSendString());
                        break;
                    default:
                        builder.Append(t.ToString().Utf8ToSendString());
                        break;
                }

            return builder.ToString();
        }
    }
}