using System;
using System.Collections.Generic;

namespace System
{
    public static class ICollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> @this, IEnumerable<T> values)
            => values
            .ForEach(v => @this.Add(v));
    }
}
