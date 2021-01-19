using System.Collections.Generic;
using Newtonsoft.Json;

namespace XQ.SDK.Model.Json
{
    public class OcrInfo
    {
        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "list")]
        public List<OcrItem> List { get; set; }
    }
}