using System.Collections.Generic;

namespace XQ.SDK.Core.TinyIOC
{
    public sealed class NamedParameterOverloads : Dictionary<string, object>
    {
        public static NamedParameterOverloads Default { get; } = new NamedParameterOverloads();
    }
}