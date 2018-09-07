using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UI.RunnerManagement.Common
{
    internal static class Extensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> @this)
            => new ObservableCollection<T>(@this);
    }
}
