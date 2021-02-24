#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HseAr.Core.Threading
{
    public static class EnumerableTaskExtensions
    {
        private const int DefaultMaxDegreeOfParallelism = 4;

        public static async Task<TResult[]> WhenAll<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, Task<TResult>> asyncAction,
            int? maxDegreeOfParallelism = null,
            CancellationToken cancellationToken = default)
        {
            using (var throttler = new SemaphoreSlim(
                maxDegreeOfParallelism ?? DefaultMaxDegreeOfParallelism,
                maxDegreeOfParallelism ?? DefaultMaxDegreeOfParallelism))
            {

                var tasks = source.Select(async input =>
                {
                    await throttler.WaitAsync(cancellationToken).ConfigureAwait(false);
                    try
                    {
                        return await asyncAction(input).ConfigureAwait(false);
                    }
                    finally
                    {
                        throttler.Release();
                    }
                });

                return await Task.WhenAll(tasks).ConfigureAwait(false);
            }
        }

        public static async Task WhenAll<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, Task> asyncAction,
            int? maxDegreeOfParallelism = null,
            CancellationToken cancellationToken = default)
        {
            using (var throttler = new SemaphoreSlim(
                maxDegreeOfParallelism ?? DefaultMaxDegreeOfParallelism,
                maxDegreeOfParallelism ?? DefaultMaxDegreeOfParallelism))
            {
                var tasks = source.Select(async input =>
                {
                    await throttler.WaitAsync(cancellationToken).ConfigureAwait(false);
                    try
                    {
                        await asyncAction(input).ConfigureAwait(false);
                    }
                    finally
                    {
                        throttler.Release();
                    }
                });

                await Task.WhenAll(tasks).ConfigureAwait(false);
            }
        }

        public static Task<IEnumerable<TSource>> Where<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, Task<bool>> asyncPredicate,
            int? maxDegreeOfParallelism = null,
            CancellationToken cancellationToken = default)
            => source.WhenAll(
                    async item => (Item: item, IsSatisfied: await asyncPredicate(item).ConfigureAwait(false)),
                    maxDegreeOfParallelism,
                    cancellationToken)
               .Bind(result => result.Where(x => x.IsSatisfied).Select(x => x.Item));

        public static async Task<TSource> FirstOrDefault<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, Task<bool>> asyncPredicate,
            CancellationToken cancellationToken = default)
        {
            foreach (var item in source)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var isSatisfied = await asyncPredicate(item).ConfigureAwait(false);

                if (isSatisfied)
                {
                    return item;
                }
            }

            return default;
        }
    }
}
