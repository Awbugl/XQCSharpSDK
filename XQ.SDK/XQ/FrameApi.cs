using System.Collections.Generic;
using Newtonsoft.Json;
using XQ.SDK.XQ.Json;
using static XQ.SDK.Core.Expand;

namespace XQ.SDK.XQ
{
    public class FrameApi
    {
        private readonly byte[] _authid;

        public FrameApi(byte[] authid)
        {
            _authid = authid;
        }

        /// <summary>
        ///     标记函数执行流程 debug时使用 每个函数内只需要调用一次
        ///     未被测试
        /// </summary>
        /// <param name="message"></param>
        public void DbgName(string message)
        {
            Xqdll.DbgName(_authid, message);
        }

        /// <summary>
        ///     函数内标记附加信息 函数内可多次调用
        ///     未被测试
        /// </summary>
        /// <param name="message"></param>
        public void Mark(string message)
        {
            Xqdll.Mark(_authid, message);
        }

        /// <summary>
        ///     输出日志 (在框架中显示)
        /// </summary>
        /// <param name="message"></param>
        public void OutPutLogToFrame(string message)
        {
            Xqdll.OutPutLog(_authid, message);
        }

        /// <summary>
        ///     检查指定RobotQQ是否在线
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="qq"></param>
        /// <returns></returns>
        public bool IsOnline(string robotQq, string qq)
        {
            return Xqdll.IsOnline(_authid, robotQq, qq);
        }

        /// <summary>
        ///     取机器人账号在线信息
        /// </summary>
        /// <param name="robotQq"></param>
        /// <returns></returns>
        public RobotInfo GetRInf(string robotQq)
        {
            return JsonConvert.DeserializeObject<RobotInfo>(IntPtrToString(Xqdll.GetRInf(_authid, robotQq)));
        }

        /// <summary>
        ///     修改QQ在线状态
        /// </summary>
        /// <param name="robotQq"></param>
        /// <param name="onLineType"></param>
        /// <param name="message"></param>
        public void SetRInf(string robotQq, string onLineType, string message)
        {
            Xqdll.SetRInf(_authid, robotQq, onLineType, message);
        }

        /// <summary>
        ///     主动卸载插件自身
        /// </summary>
        public bool Uninstall()
        {
            return Xqdll.Uninstall(_authid);
        }

        /// <summary>
        ///     重新从Plugin目录下载入本插件(一般用做自动更新)
        /// </summary>
        public bool Reload()
        {
            return Xqdll.Reload(_authid);
        }

        /// <summary>
        ///     登录指定QQ
        /// </summary>
        /// <param name="qq"></param>
        public void LoginQq(string qq)
        {
            Xqdll.LoginQQ(_authid, qq);
        }

        /// <summary>
        ///     离线指定QQ
        /// </summary>
        /// <param name="qq"></param>
        public void OffLineQq(string qq)
        {
            Xqdll.OffLineQQ(_authid, qq);
        }

        /// <summary>
        ///     获取机器人在线账号列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetOnlineList()
        {
            return ToList(IntPtrToString(Xqdll.GetOnLineList(_authid)));
        }

        /// <summary>
        ///     获取机器人账号是否在线
        /// </summary>
        /// <param name="qq"></param>
        /// <returns></returns>
        public bool GetBotsOnline(string qq)
        {
            return Xqdll.Getbotisonline(_authid, qq);
        }

        /// <summary>
        ///     取插件是否启用
        /// </summary>
        /// <returns></returns>
        public bool IsEnable()
        {
            return Xqdll.IsEnable(_authid);
        }

        /// <summary>
        ///     取所有RobotQQ列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetRobotQqList()
        {
            return ToList(IntPtrToString(Xqdll.GetQQList(_authid)));
        }
    }
}