using System.Collections.Generic;
using Newtonsoft.Json;

namespace XQ.SDK.Model.Json
{
    public class GroupMemberListQqonly
    {
        [JsonProperty(PropertyName = "list")] public List<GroupMemberItems> List { get; set; }
    }
}