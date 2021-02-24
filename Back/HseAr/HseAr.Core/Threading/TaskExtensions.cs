#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;

namespace HseAr.Core.Threading
{
    public static class TaskExtensions
    {
        public static T InvokeSync<T>(this Task<T> task)
        {
            return task
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        public static void InvokeSync(this Task task)
        {
            task
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        /// <summary>
        /// Если Task не поддерживает нормально отмену используем фокус с Task.Delay
        /// </summary>
        /// <exception cref="TimeoutException"></exception>
        public static async Task<T> WithForcedTimeout<T>(this Task<T> task, TimeSpan timeout, CancellationToken cancellationToken)
        {
            using (var timeoutSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                var delayTask = Task.Delay(timeout, timeoutSource.Token);
                var completedTask = await Task.WhenAny(task, delayTask).ConfigureAwait(false);

                timeoutSource.Cancel();
                if (completedTask == delayTask)
                {
                    throw new TimeoutException("Превышено время выполнения задачи");
                }

                return await task.ConfigureAwait(false);
            }
        }

        public static Task<T> AsTask<T>(this T value) => Task.FromResult(value);

        public static async Task<TOut> Bind<TIn, TOut>(this Task<TIn> task, Func<TIn, TOut> projection)
            => projection(await task.ConfigureAwait(false));

        public static async Task<TOut> Bind<TIn, TOut>(this Task<TIn> task, Func<TIn, Task<TOut>> projection)
            => await projection(await task.ConfigureAwait(false)).ConfigureAwait(false);

        /// <summary>
        /// Select many Linq Query Syntax
        /// </summary>
        public static async Task<TOut> SelectMany<TIn, TInterm, TOut>(
            this Task<TIn> task,
            Func<TIn, Task<TInterm>> function,
            Func<TIn, TInterm, TOut> projection)
        {
            var input = await task.ConfigureAwait(false);
            var interm = await function(input).ConfigureAwait(false);
            return projection(input, interm);
        }

        /// <summary>
        /// Select many Linq Query Syntax for task-returned projection
        /// </summary>
        public static async Task<TOut> SelectMany<TIn, TInterm, TOut>(
            this Task<TIn> task,
            Func<TIn, Task<TInterm>> function,
            Func<TIn, TInterm, Task<TOut>> projection)
        {
            var input = await task.ConfigureAwait(false);
            var interm = await function(input).ConfigureAwait(false);
            return await projection(input, interm);
        }
    }
}
