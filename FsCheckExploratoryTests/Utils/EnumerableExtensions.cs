using System;
using System.Collections.Generic;
using System.Linq;

namespace FsCheckExploratoryTests.Utils
{
    internal static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            // ReSharper disable ReturnValueOfPureMethodIsNotUsed
            source.Select((item, index) =>
                {
                    action(item, index);
                    return 0;
                }).ToList();
            // ReSharper restore ReturnValueOfPureMethodIsNotUsed
        }

        // https://github.com/fsharp/FsCheck/blob/master/docs/csharp/Properties.cs
        public static bool IsOrdered<T>(this IEnumerable<T> source)
        {
            var comparer = Comparer<T>.Default;
            var previous = default(T);
            var first = true;

            foreach (var element in source)
            {
                if (!first && comparer.Compare(previous, element) > 0)
                {
                    return false;
                }
                first = false;
                previous = element;
            }
            return true;
        }
    }
}
