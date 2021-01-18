using System;
using XQ.SDK.XQ;

namespace XQ.SDK.Model
{
    /// <summary>
    ///     描述XQ数据模型的基础抽象类
    /// </summary>
    public abstract class BasisModel
    {
        /// <exception cref="ArgumentNullException">参数: api 为 null</exception>
        protected BasisModel(XqApi api, Robot robot)
        {
            XqApi = api ?? throw new ArgumentNullException(nameof(api));
            Robot = robot;
        }

        public XqApi XqApi { get; }

        public Robot Robot { get; }
    }
}