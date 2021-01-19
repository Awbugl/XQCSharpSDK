using System.Collections.Generic;
using System.Linq;

namespace XQ.SDK.Model.Json
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