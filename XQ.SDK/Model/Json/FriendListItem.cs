using System.Collections.Generic;
using Newtonsoft.Json;

namespace XQ.SDK.Model.Json
{
    public class FriendListItem
    {
        [JsonProperty(PropertyName = "mems")] public List<FriendInfoJson> Members { get; set; }
    }
}