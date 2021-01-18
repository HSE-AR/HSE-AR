namespace HseAr.Core.Infrastructure
{
    public interface IMapper
    {
        TRusult Map<TSource, TRusult>(TSource source);
    }
}