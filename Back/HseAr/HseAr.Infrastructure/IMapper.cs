namespace HseAr.Infrastructure
{
    public interface IMapper
    {
        TDest Map<TSource, TDest>(TSource source);
    }
}