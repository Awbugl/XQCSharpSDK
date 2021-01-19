using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using XQ.SDK.Core;
using XQ.SDK.XQ;

namespace XQ.Core
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal static class Global
    {
        internal static TinyIoCContainer Container = new TinyIoCContainer();

        internal static XqApi XqApi { get; set; }

        internal static XqPluginInfo PluginInfo { get; set; }

        internal static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ExceptionReport(e.ExceptionObject as Exception);
        }

        internal static void ExceptionReport(Exception ex)
        {
            var dir = $"Log/{DateTime.Now:yyyy-M-d}/";
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            try
            {
                File.AppendAllText($"{dir}XQCSharp_Err.log", $"{DateTime.Now:T}\n{ex}\n\n");
                XqApi.OutPutLogToFrame($"[{PluginInfo.Name}] 发生异常：" + ex.Message);
            }
            catch
            {
                Thread.Sleep(2000);
                ExceptionReport(ex);
            }
        }
    }
}