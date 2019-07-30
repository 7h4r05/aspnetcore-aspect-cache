using Microsoft.AspNetCore.Http;

namespace AspectCache.Interfaces
{
    public interface ICacheKeyFactory<T>
    {
        string Create(HttpContext context);
    }
}
