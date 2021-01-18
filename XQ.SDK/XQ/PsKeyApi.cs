using static XQ.SDK.Core.Expand;

namespace XQ.SDK.XQ
{
    public class PsKeyApi
    {
        private readonly byte[] _authid;

        public PsKeyApi(byte[] authid)
        {
            _authid = authid;
        }

        /// <summary>
        ///     取得QQ群页面操作用参数P_skey
        /// </summary>
        /// <param name="robotQq"></param>
        /// <example> "; p_uin=o{robotQq}; p_skey={p_skeyvalue}" </example>
        /// <returns></returns>
        public string GetGroupPsKey(string robotQq)
        {
            return Xqdll.GetGroupPsKey(_authid, robotQq).IntPtrToString();
        }

        /// <summary>
        ///     取得QQ空间页面操作用参数P_skey
        /// </summary>
        /// <param name="robotQq"></param>
        /// <example> "; p_uin=o{robotQq}; p_skey={p_skeyvalue}" </example>
        /// <returns></returns>
        public string GetZonePsKey(string robotQq)
        {
            return Xqdll.GetZonePsKey(_authid, robotQq).IntPtrToString();
        }

        /// <summary>
        ///     取得机器人网页操作用的Cookies
        /// </summary>
        /// <param name="robotQq"></param>
        /// <example> "uin=o{robotQq}; skey={skeyvalue}" </example>
        /// <returns></returns>
        public string GetCookies(string robotQq)
        {
            return Xqdll.GetCookies(_authid, robotQq).IntPtrToString();
        }

        /// <summary>
        ///     取短Clientkey
        /// </summary>
        /// <param name="robotQq"></param>
        /// <returns>16进制字符串</returns>
        public string GetClientkey(string robotQq)
        {
            return Xqdll.GetClientkey(_authid, robotQq).IntPtrToString();
        }

        /// <summary>
        ///     取得机器人网页操作用的长Clientkey
        /// </summary>
        /// <param name="robotQq"></param>
        /// <returns>16进制字符串</returns>
        public string GetLongClientkey(string robotQq)
        {
            return Xqdll.GetLongClientkey(_authid, robotQq).IntPtrToString();
        }

        /// <summary>
        ///     取得腾讯课堂页面操作用参数P_skey
        /// </summary>
        /// <param name="robotQq"></param>
        /// <example> "; p_uin=o{robotQq}; p_skey={p_skeyvalue}" </example>
        /// <returns></returns>
        public string GetClassRoomPsKey(string robotQq)
        {
            return Xqdll.GetClassRoomPsKey(_authid, robotQq).IntPtrToString();
        }

        /// <summary>
        ///     取得QQ举报页面操作用参数P_skey
        /// </summary>
        /// <param name="robotQq"></param>
        /// <example> "; p_uin=o{robotQq}; p_skey={p_skeyvalue}" </example>
        /// <returns></returns>
        public string GetRepPsKey(string robotQq)
        {
            return Xqdll.GetRepPsKey(_authid, robotQq).IntPtrToString();
        }

        /// <summary>
        ///     取得财付通页面操作用参数P_skey
        /// </summary>
        /// <param name="robotQq"></param>
        /// <example> "; p_uin=o{robotQq}; p_skey={p_skeyvalue}" </example>
        /// <returns></returns>
        public string GetTenPayPsKey(string robotQq)
        {
            return Xqdll.GetTenPayPsKey(_authid, robotQq).IntPtrToString();
        }

        /// <summary>
        ///     取bkn
        /// </summary>
        /// <param name="robotQq"></param>
        /// <returns>bkn （一串数字）</returns>
        public string GetBkn(string robotQq)
        {
            return Xqdll.GetBkn(_authid, robotQq).IntPtrToString();
        }
    }
}