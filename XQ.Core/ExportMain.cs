using XQ.Plugin;
using XQ.SDK.Core;
using XQ.SDK.Interface;

namespace XQ.Core
{
    public class ExportMain
    {
        /// <summary>
        ///     将需要处理的事件接口和插件注册
        ///     并设置插件信息
        /// </summary>
        public static XqPluginInfo AddRegister()
        {
            Init.Register<IGroupEvent, PluginExample>();
            Init.Register<IPrivateMessage, PluginExample>();
            Init.Register<IGroupAddRequest, PluginExample>();
            Init.Register<IGroupMessage, PluginExample>();

            return new XqPluginInfo(
                "TestPlugin", //此参数应与 XQ.Core => 属性 => 程序集名称 保持一致
                "littlenine12",
                "1.0.0",
                "测试用插件"
            );
        }
    }
}