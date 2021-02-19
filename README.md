## XQCSharpSDK
<img width="100" src="XQCsharpSDK.png" alt="icon"> 
使用C#8.0编写的先驱机器人插件开发框架
  
----
  
## 特点

* 封装了XQ提供的接口，提供基于抽象数据类型的XQApi
* 在事件中保留并封装了XQ的Event原参数，提供用户扩展空间
* 提供了托管异常处理
* 支持导出函数
* 支持整合Dll依赖项

## 感谢

* 整体SDK架构参考了<a href="https://github.com/Jie2GG/Native.Framework">Jie2GG/Native.Framework</a>
* 部分消息解析逻辑参考了<a href="https://github.com/w4123/CQXQ">w4123/CQXQ</a>
* XqDll.cs的使用授权由<a href="https://gitee.com/heerkaisair">赫尔heer</a>提供

## 开发文档
+ 修改 <a href="https://github.com/littlenine12/XQCSharpSDK/blob/main/XQ.Plugin/PluginExample.cs">XQ.Plugin/PluginExample.cs</a> 
  + 继承希望处理的事件的对应接口，并实现
+ 修改 <a href="https://github.com/littlenine12/XQCSharpSDK/blob/main/XQ.Core/ExportMain.cs">XQ.Core/ExportMain.cs</a> 
  + 注册需要处理的事件接口和插件 并设置插件信息
+ 打开 XQ.Core => 属性
  + 将 程序集名称 修改为插件名称
+ Release x86模式进行编译
  + 打开XQCSharpSDK/Output文件夹
  + 插件名称.XQ.dll 即为 XQ可调用的插件
