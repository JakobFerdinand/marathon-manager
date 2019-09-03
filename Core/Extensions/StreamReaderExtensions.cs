using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;

namespace System
{
    public static class StreamReaderExtensions
    {
        public static ImmutableArray<string> ReadAllLines(this StreamReader @this)
        {
            var lines = new List<string>();
            while (!@this.EndOfStream)
                lines.Add(@this.ReadLine());
            return lines.ToImmutableArray();
        }
    }
}
