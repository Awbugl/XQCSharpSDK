using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace XQ.SDK.Model.Json
{
    public class GroupInfoJson
    {
        private string _name;

        /// <summary>
        ///     群号
        /// </summary>
        [JsonProperty(PropertyName = "gc")]
        public string Id { get; set; }

        /// <summary>
        ///     群名称
        /// </summary>
        [JsonProperty(PropertyName = "gn")]
        public string Name
        {
            get => _name;
            set => _name = Regex.Unescape(value);
        }

        /// <summary>
        ///     群主
        /// </summary>
        [JsonProperty(PropertyName = "owner")]
        public string Owner { get; set; }
    }
}