using Newtonsoft.Json;

namespace XQ.SDK.Model.Json
{
    public class FriendInfoJson
    {
        /// <summary>
        ///     昵称
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        ///     QQ号
        /// </summary>
        [JsonProperty(PropertyName = "uin")]
        public string Id { get; set; }
    }
}