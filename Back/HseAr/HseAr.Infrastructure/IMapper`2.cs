namespace HseAr.Infrastructure
{
    public interface IMapper<in TSource, out TDest>
    {
        TDest Map(TSource source);
    }
}