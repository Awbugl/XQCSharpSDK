using System.Collections.Generic;
using System.Linq;
using XQ.SDK.XQ.Json;

namespace XQ.SDK.Model
{
    public class OcrItemCollection
    {
        public OcrItemCollection(List<OcrItem> list)
        {
            List = list;
        }

        public List<OcrItem> List { get; }

        public List<string> AllText => List.Select(i => i.Text).ToList();
    }
}