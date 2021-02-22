using Newtonsoft.Json;

namespace XQ.SDK.Model.Json
{
    public class GroupMemberInfoJson
    {
        /// <summary>
        ///     上次发言时间时间戳
        /// </summary>
        [JsonProperty(PropertyName = "lst")]
        public long LastTime { get; set; }

        /// <summary>
        ///     加群时间时间戳
        /// </summary>
        [JsonProperty(PropertyName = "jt")]
        public long JoinTime { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        [JsonProperty(PropertyName = "cd")]
        public string Name { get; set; }

        /// <summary>
        ///     群昵称
        /// </summary>
        [JsonProperty(PropertyName = "nk")]
        public string Nick { get; set; }

        /// <summary>
        ///     禁言剩余时间（秒）
        /// </summary>
        [JsonProperty(PropertyName = "sut")]
        public long? BanSpeckTime { get; set; } = 0;

        /// <summary>
        ///     是否为好友，0为否
        /// </summary>
        [JsonProperty(PropertyName = "fr")]
        public int? IsFriend { get; set; } = 0;
    }
}