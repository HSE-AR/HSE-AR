#nullable enable

using System;
using System.Threading.Tasks;

namespace HseAr.Core.Objects
{
    public static class ObjectExtensions
    {
        public static T SelfOrDefaultIf<T>(
            this T value,
            Func<T, bool> predicate,
            T defaultValue = default)
        => predicate(value)
            ? defaultValue
            : value;

        public static TResult? IfNotNull<TParam, TResult>(this TParam? value, Func<TParam, TResult?> action)
            where TParam : class
            where TResult : class
            => value == null
                ? default
                : action(value);

        public static Task<TResult?> IfNotNull<TParam, TResult>(this TParam? value, Func<TParam, Task<TResult?>> action)
            where TParam : class
            where TResult : class
            => value == null
                ? Task.FromResult<TResult?>(default)
                : action(value);

        public static TResult? IfNotNull<TParam, TResult>(this TParam? value, Func<TParam, TResult?> action)
            where TParam : struct
            where TResult : struct
            => value.HasValue
                ? action(value.Value)
                : default;

        public static TResult? IfNotNull<TParam, TResult>(this TParam? value, Func<TParam, TResult?> action)
            where TParam : struct
            where TResult : class
            => value.HasValue
                ? action(value.Value)
                : default;

        public static TResult? IfNotNull<TParam, TResult>(this TParam? value, Func<TParam, TResult?> action)
            where TParam : class
            where TResult : struct
            => value == null
                ? default
                : action(value);

        public static Task<TResult?> IfNotNull<TParam, TResult>(this TParam? value, Func<TParam, Task<TResult?>> action)
            where TParam : struct
            where TResult : struct
            => value.HasValue
                ? action(value.Value)
                : Task.FromResult<TResult?>(default);

        public static Task<TResult?> IfNotNull<TParam, TResult>(this TParam? value, Func<TParam, Task<TResult?>> action)
            where TParam : struct
            where TResult : class
            => value.HasValue
                ? action(value.Value)
                : Task.FromResult<TResult?>(default);

        public static T? TryInvoke<T>(Func<T?> action, Func<Exception, T?>? onFailAction = null)
            where T : class
        {
            try
            {
                return action.Invoke();
            }
            catch (Exception ex)
            {
                return onFailAction?.Invoke(ex) ?? default;
            }
        }

        public static T? TryInvoke<T>(Func<T?> action, Action<Exception> onFailAction)
            where T : class
        {
            try
            {
                return action.Invoke();
            }
            catch (Exception ex)
            {
                onFailAction.Invoke(ex);

                return default;
            }
        }
    }
}
