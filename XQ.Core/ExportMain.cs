using XQ.Core.Export;
using XQ.Plugin;
using XQ.SDK.Interface;
using XQ.SDK.Model;

namespace XQ.Core
{
    public class ExportMain
    {
        /// <summary>
        ///     将需要处理的事件接口和插件注册
        ///     并设置插件信息
        ///     (在Release x86模式下)生成的dll文件在Output文件夹下 (注：可在 {XQ.Core => (右键菜单最底)属性 => 生成 => 输出路径} 进行修改)
        ///     dll名称为 {XQ.Core => (右键菜单最底)属性 => 应用程序 => 程序集名称}.XQ.dll
        /// </summary>
        public static PluginInfo AddRegister()
        {
            Init.Register<IGroupEvent, PluginExample>();
            Init.Register<IPrivateMessage, PluginExample>();
            Init.Register<IGroupMessage, PluginExample>();

            return new PluginInfo(
                "TestPlugin", //此参数应与 XQ.Core => 属性 => 应用程序 => 程序集名称 保持一致，否则XQ将报错
                "littlenine12",
                "1.0.0",
                "测试用插件"
            );
        }
    }
}
