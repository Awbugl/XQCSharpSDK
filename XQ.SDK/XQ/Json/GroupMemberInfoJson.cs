using Newtonsoft.Json;

namespace XQ.SDK.XQ.Json
{
    public class GroupMemberInfoJson
    {
        /// <summary>
        ///     上次发言时间时间戳
        /// </summary>
        [JsonProperty(PropertyName = "lst")]
        public int LastTime { get; set; }

        /// <summary>
        ///     加群时间时间戳
        /// </summary>
        [JsonProperty(PropertyName = "jt")]
        public int JoinTime { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "cd")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "nk")]
        public string Nick { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "fr")]
        public int? IsFriend { get; set; }
    }
}