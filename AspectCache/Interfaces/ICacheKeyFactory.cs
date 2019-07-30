using Microsoft.AspNetCore.Http;

namespace AspectCache.Interfaces
{
    public interface ICacheKeyFactory<T>
    {
        string Create(T obj, params object[] param);
        string Create(HttpContext context);
    }
}
