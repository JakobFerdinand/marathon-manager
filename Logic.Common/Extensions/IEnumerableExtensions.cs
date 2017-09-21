using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Common.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool ConaintsEqual<T>(this IEnumerable<T> source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source), $"{nameof(source)} must not be null.");

            return source.Count() != source.Distinct().Count();
        }
    }
}
