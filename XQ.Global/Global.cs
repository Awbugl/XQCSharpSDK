using System;
using System.IO;
using System.Threading;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.Global
{
    public static class Global
    {
        /// <summary>
        ///     获取Api是否已经加载完成
        /// </summary>
        public static bool LoadCompleted => XqApi != null;

        /// <summary>
        ///     XqApi单例
        /// </summary>
        public static XqApi XqApi { get; private set; }

        /// <summary>
        ///     插件信息单例
        /// </summary>
        public static PluginInfo PluginInfo { get; private set; }

        /// <summary>
        ///     配置文件路径
        /// </summary>
        public static string ConfigPath => AppContext.BaseDirectory + $@"\Config\{PluginInfo.Name}\";

        /// <summary>
        ///     设置XqApi单例
        ///     请不要在插件运行时更改
        /// </summary>
        /// <param name="authId">插件AuthId</param>
        public static void SetApi(byte[] authId) => XqApi = new XqApi(authId);

        /// <summary>
        ///     设置插件信息单例
        ///     请不要在插件运行时更改
        /// </summary>
        /// <param name="info">插件信息</param>
        public static void SetInfo(PluginInfo info) => PluginInfo = info;

        /// <summary>
        ///     向Log中写入异常信息
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="output">是否在框架UI输出</param>
        public static void ExceptionReport(Exception ex, bool output = true)
        {
            var dir = $"{AppContext.BaseDirectory}/Log/{DateTime.Now:yyyy-M-d}/";
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            try
            {
                File.AppendAllText($"{dir}XQCSharp_Err.log", $"{DateTime.Now:T}\n{ex}\n\n");
                if (output) XqApi.OutPutLogToFrame($"[{PluginInfo.Name}] 发生异常：" + ex.Message);
            }
            catch
            {
                Thread.Sleep(2000);
                ExceptionReport(ex);
            }
        }
    }
}
