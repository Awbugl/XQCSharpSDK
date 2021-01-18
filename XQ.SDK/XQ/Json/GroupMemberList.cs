using System.Collections.Generic;
using Newtonsoft.Json;

namespace XQ.SDK.XQ.Json
{
    public class GroupMemberList
    {
        /// <summary>
        ///     指示是否获取成功
        /// </summary>
        [JsonProperty(PropertyName = "ec")]
        public int Ec { get; set; }

        /// <summary>
        ///     错误码
        /// </summary>
        [JsonProperty(PropertyName = "errcode")]
        public int Errcode { get; set; }

        /// <summary>
        ///     错误Msg
        /// </summary>
        [JsonProperty(PropertyName = "em")]
        public string ErrMessage { get; set; }

        /// <summary>
        ///     群等级
        /// </summary>
        [JsonProperty(PropertyName = "level")]
        public int Level { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "mem_num")]
        public int MemberNum { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "max_num")]
        public int MaxMemberNum { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "max_admin")]
        public int MaxAdminNum { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "owner")]
        public string Owner { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "adm")]
        public List<string> AdminList { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "members")]
        public Dictionary<string, GroupMemberInfoJson> Members { get; set; }
    }
}