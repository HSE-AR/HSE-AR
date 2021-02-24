#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HseAr.Core.Objects;
using HseAr.Core.Threading;

namespace Afisha.Tickets.Core.Linq
{
    public static class EnumerableExtensions
    {
        public static string Join<TSource>(this IEnumerable<TSource>? source, string separator)
            => source != null
                ? string.Join(separator, source)
                : string.Empty;

        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int batchSize)
        {
            using var enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
                yield return YieldBatchElements(enumerator, batchSize - 1);
            }
        }

        
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source) => source ?? Enumerable.Empty<T>();
        
        
        private static IEnumerable<T> YieldBatchElements<T>(IEnumerator<T> source, int batchSize)
        {
            yield return source.Current;
            for (var i = 0; i < batchSize && source.MoveNext(); i++)
            {
                yield return source.Current;
            }
        }

        public static void ForEach<TItem>(this IEnumerable<TItem> sequence, Action<TItem> action)
        {
            foreach (var item in sequence)
            {
                action(item);
            }
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? sequence) => sequence?.Any() != true;

        public static bool HasAnyItem<TItem>(this IEnumerable<TItem>? sequence) => sequence?.Any() ?? false;

        public static bool HasAnyItem<TItem>(this IEnumerable<TItem>? sequence, Func<TItem, bool> condition)
            => sequence?.Any(condition) ?? false;

        public static string JoinStrings<TItem>(
            this IEnumerable<TItem> sequence,
            string separator,
            Func<TItem, string> converter)
        {
            var sb = new StringBuilder();
            sequence.Aggregate(sb, (builder, item) =>
            {
                if (builder.Length > 0)
                {
                    builder.Append(separator);
                }
                builder.Append(converter(item));
                return builder;
            });
            return sb.ToString();
        }

        public static bool EmptyOrContains<TItem>(this IEnumerable<TItem>? sequence, TItem item)
            => sequence.EmptyIfNull().DefaultIfEmpty(item).Contains(item);

        public static async Task<bool> Any<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, Task<bool>> predicate)
        {
            foreach (var item in source)
            {
                if (await predicate(item).ConfigureAwait(false))
                {
                    return true;
                }
            }

            return false;
        }

        public static async Task<(IEnumerable<T> Satisfied, IEnumerable<T> NotSatisfied)> SplitBy<T>(
            this IEnumerable<T> items,
            Func<T, Task<bool>> splitCondition)
        {
            var satisfied = await items.WhenAll(item => splitCondition(item).Bind(x => (Item: item, IsSatisfied: x)))
                .ConfigureAwait(false);

            var splitted = satisfied.ToLookup(x => x.IsSatisfied, x => x.Item);

            return (splitted[true], splitted[false]);
        }

        public static (IEnumerable<T> Satisfied, IEnumerable<T> NotSatisfied) SplitBy<T>(
            this IEnumerable<T> items,
            Func<T, bool> splitCondition)
        {
            var splitted = items.ToLookup(splitCondition);

            return (splitted[true], splitted[false]);
        }

        public static (IEnumerable<TResult> Satisfied, IEnumerable<TResult> NotSatisfied) SplitBy<T, TResult>(
            this IEnumerable<T> items,
            Func<T, bool> splitCondition,
            Func<T, TResult> elementSelector)
        {
            var splitted = items.ToLookup(splitCondition, elementSelector);

            return (splitted[true], splitted[false]);
        }

        public static decimal? NullableSum(this IEnumerable<decimal?> source)
            => source.Aggregate(
                default(decimal?),
                (sum, t) => sum.HasValue
                    ? sum + (t ?? 0)
                    : t);

        public static Dictionary<TKey, List<TItem>> GroupToDictionary<TKey, TItem>(
            this IEnumerable<TItem> source,
            Func<TItem, TKey> keySelector)
            => source.GroupBy(keySelector).ToDictionary(x => x.Key, x => x.ToList());

        public static Dictionary<TKey, TValue> ToDistinctDictionary<TItem, TKey, TValue>(
            this IEnumerable<TItem> source,
            Func<TItem, TKey> keySelector,
            Func<TItem, TValue> valueSelector)
            => source.GroupBy(keySelector).ToDictionary(x => x.Key, x => valueSelector(x.First()));

        public static bool ContainsIgnoreCase(this IEnumerable<string> strings, string valueString)
            => strings.Any(x => x.ContainsIgnoreCase(valueString));
    }
}
