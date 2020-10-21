using System;
using System.Collections;
using System.Collections.Generic;

namespace BusinessDaysChallenge.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool IsNullOrEmpty(this IEnumerable value)
        {
            return value == null || !value.GetEnumerator().MoveNext();
        }

        /// <summary>
        /// Creates a dictionary from the given enumerable. Safely handles a null enumerable without throwing exceptions.
        /// </summary>
        public static Dictionary<TKey, TSource> SafeToDictionary<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> comparer = null)
        {
            if (source.IsNullOrEmpty())
                return null;

            var dictionary = comparer == null ? new Dictionary<TKey, TSource>() : new Dictionary<TKey, TSource>(comparer);
            foreach (var current in source)
                dictionary[keySelector(current)] = current;

            return dictionary;
        }
    }
}