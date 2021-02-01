using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;

using XQ.SDK.Core.TinyIOC;
using XQ.SDK.Model;
using XQ.SDK.XQ;

namespace XQ.Core.Export
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal static class Global
    {
        internal static TinyIoCContainer Container = new TinyIoCContainer();

        internal static XqApi XqApi { get; set; }

        internal static PluginInfo PluginInfo { get; set; }

        internal static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ExceptionReport(e.ExceptionObject as Exception);
        }

        internal static void ExceptionReport(Exception ex, bool output = true)
        {
            var dir = $"Log/{DateTime.Now:yyyy-M-d}/";
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