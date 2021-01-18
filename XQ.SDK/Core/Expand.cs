using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace XQ.SDK.Core
{
    [SuppressMessage("ReSharper", "CommentTypo")]
    public static class Expand
    {
        public static List<string> ToList(string str)
        {
            return str.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries).Select(i => i.Trim()).ToList();
        }

        /// <summary>
        ///     将IntPtrToString中未转换的[em]exxxxxx[/em]转换为Emoji码
        ///     代码逻辑来自w4123/CQXQ
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static string NickToSendString(string msg)
        {
            var ret = new StringBuilder();
            var reg = new Regex("(&nbsp;|\\[em\\](e[0-9]{1,6})\\[\\/em\\])",
                RegexOptions.IgnoreCase | RegexOptions.ECMAScript);
            var last = 0;
            Match rslt;
            while ((rslt = reg.Match(msg.Substring(last))).Success)
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
        /// <param name="intPtr"></param>
        /// <returns></returns>
        public static string IntPtrToString(IntPtr intPtr)
        {
            try
            {
                var gb18030 = Encoding.GetEncoding("gb18030");
                var length = Marshal.ReadInt32(intPtr);
                if (length <= 0) return "";

                var bin = new byte[length];
                Marshal.Copy(intPtr + 4, bin, 0, length);

                var sb = new StringBuilder();

                for (var i = 0; i < length - 1;)
                    sb.Append(EncodingGetString(gb18030, bin, ref i, bin[i] < 0x80 ? 1 : bin[i + 1] > 0x3F ? 2 : 4));

                if (length > 1 && bin[length - 2] > 0x80) return sb.ToString();

                sb.Append(gb18030.GetString(bin, length - 1, 1));

                return NickToSendString(sb.ToString());
            }
            finally
            {
                if (Kernel32.HeapFree(Kernel32.GetProcessHeap(), 0, intPtr) == 0)
                {
                    var err = Marshal.GetLastWin32Error();
                    if (err != 0) throw new Win32Exception(err);
                }
            }
        }

        private static string EncodingGetString(Encoding encoding, byte[] bin, ref int index, int count)
        {
            index += count;

            return count < 4
                ? encoding.GetString(bin, index - count, count)
                : Encoding.Convert(encoding, Encoding.UTF8,
                    bin.Skip(index - count).Take(4).ToArray()).Aggregate("[emoji=", (current, bi)
                    => current + bi.ToString("X2")) + "]";
        }
    }
}