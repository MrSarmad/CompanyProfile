using ASI.Sugar.Collections;
using System.Collections.Generic;

namespace CompanyProfile.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static string StringJoin<T>(this IEnumerable<T>? list, string separator = ", ")
        {
            return string.Join(separator, list.OrEmptyIfNull());
        }
    }
}
