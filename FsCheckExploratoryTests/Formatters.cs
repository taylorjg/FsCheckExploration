using System;
using System.Collections.Generic;
using System.Linq;

namespace FsCheckExploratoryTests
{
    internal static class Formatters
    {
        public static string FormatCollection<T>(IEnumerable<T> xs, Func<T, string> itemFormatterOverride = null)
        {
            var itemFormatter = itemFormatterOverride ?? DefaultItemFormatter<T>();
            return string.Format("[{0}]", string.Join(", ", xs.Select(itemFormatter)));
        }

        public static string Format2DArray<T>(T[,] arr, Func<T, string> itemFormatterOverride = null)
        {
            var rows = arr.GetLength(0);
            var cols = arr.GetLength(1);
            var formattedRows = new List<string>();

            // ReSharper disable LoopCanBeConvertedToQuery
            foreach (var row in Enumerable.Range(0, rows))
            {
                var items = new List<T>();
                foreach (var col in Enumerable.Range(0, cols)) items.Add(arr[row, col]);
                var formattedRow = FormatCollection(items, itemFormatterOverride);
                formattedRows.Add(formattedRow);
            }
            // ReSharper restore LoopCanBeConvertedToQuery

            return string.Join(", ", formattedRows);
        }

        public static Func<T, string> DefaultItemFormatter<T>()
        {
            return t => t as object == null ? "(null)" : t.ToString();
        }
    }
}
