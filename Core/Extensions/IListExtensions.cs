using System.Collections;
using System.Collections.Generic;

namespace System
{
    public static class IListExtensions
    {
        public static void AddRange(this IList @this, IEnumerable<object> values)
            => values
            .ForEach(v => @this.Add(v));
    }
}
