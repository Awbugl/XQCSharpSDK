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