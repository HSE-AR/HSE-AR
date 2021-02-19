namespace HseAr.Core.Objects
{
    public static class ValueTypeExtensions
    {
        public static TValue? GetNullIfDefault<TValue>(this TValue value)
            where TValue : struct
            => value.Equals(default(TValue))
                ? null
                : (TValue?)value;
    }
}
