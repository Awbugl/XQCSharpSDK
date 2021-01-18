using Newtonsoft.Json;

namespace XQ.SDK.Core
{
    public class XqPluginInfo
    {
        /// <param name="name">插件名</param>
        /// <param name="author">插件作者</param>
        /// <param name="version">插件版本</param>
        /// <param name="desc">插件描述</param>
        public XqPluginInfo(string name, string author, string version, string desc)
        {
            Name = name;
            Author = author;
            Version = version;
            Desc = desc;
            Sver = 3;
        }

        [JsonProperty(PropertyName = "name")] public string Name { get; set; }

        [JsonProperty(PropertyName = "pver")] public string Version { get; set; }

        [JsonProperty(PropertyName = "sver")] public int Sver { get; set; }

        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "desc")] public string Desc { get; set; }

        public string GetJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}