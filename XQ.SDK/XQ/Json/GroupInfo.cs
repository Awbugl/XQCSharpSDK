using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace XQ.SDK.XQ.Json
{
    public class GroupInfo
    {
        private string _name;

        /// <summary>
        ///     群号
        /// </summary>
        [JsonProperty(PropertyName = "gc")]
        public int Id { get; set; }

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

    public class GroupMemberItems
    {
        [JsonProperty(PropertyName = "QQ")] public string Qq { get; set; }
    }

    public class GroupMemberListQqonly
    {
        [JsonProperty(PropertyName = "list")] public List<GroupMemberItems> List { get; set; }
    }
}