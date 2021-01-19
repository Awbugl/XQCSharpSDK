using Newtonsoft.Json;

namespace XQ.SDK.Model.Json
{
    public class RobotInfo
    {
        /// <summary>
        ///     RobotQQ
        /// </summary>
        [JsonProperty(PropertyName = "QQ")]
        public string Qq { get; set; }

        /// <summary>
        ///     Robot昵称
        /// </summary>
        [JsonProperty(PropertyName = "昵称")]
        public string NickName { get; set; }

        /// <summary>
        ///     Robot在线状态
        /// </summary>
        [JsonProperty(PropertyName = "在线状态")]
        public string Status { get; set; }

        /// <summary>
        ///     当前发送消息速度(每秒)
        /// </summary>
        [JsonProperty(PropertyName = "速度")]
        public int Speed { get; set; }

        /// <summary>
        ///     收到消息次数
        /// </summary>
        [JsonProperty(PropertyName = "收信")]
        public int RecieveNum { get; set; }

        /// <summary>
        ///     发送消息次数
        /// </summary>
        [JsonProperty(PropertyName = "发信")]
        public int SendNum { get; set; }

        /// <summary>
        ///     在线时长(秒)
        /// </summary>
        [JsonProperty(PropertyName = "在线时长")]
        public long OnlineTime { get; set; }

        /// <summary>
        ///     登录时间时间戳(秒)
        /// </summary>
        [JsonProperty(PropertyName = "登录时间")]
        public long LoginTime { get; set; }

        /// <summary>
        ///     当前时间时间戳(秒)
        /// </summary>
        [JsonProperty(PropertyName = "当前时间")]
        public long NowTime { get; set; }
    }
}