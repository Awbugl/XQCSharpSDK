using System.Collections.Generic;

namespace XQ.SDK.Core
{
    public sealed class NamedParameterOverloads : Dictionary<string, object>
    {
        public static NamedParameterOverloads Default { get; } = new NamedParameterOverloads();
    }
}