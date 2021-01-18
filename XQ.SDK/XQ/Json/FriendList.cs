using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace XQ.SDK.XQ.Json
{
    public class FriendList
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
        ///     好友列表
        /// </summary>
        [JsonProperty(PropertyName = "result")]
        public Dictionary<string, FriendListItem> List { get; set; }

        public List<FriendInfoJson> GetList()
        {
            var infos = new List<FriendInfoJson>();
            foreach (var i in List.Values.Select(i => i.Members)) infos.AddRange(i);
            return infos;
        }
    }

    public class FriendListItem
    {
        [JsonProperty(PropertyName = "mems")] public List<FriendInfoJson> Members { get; set; }
    }
}