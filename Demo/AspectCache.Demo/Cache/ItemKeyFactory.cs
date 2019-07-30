using AspectCache.Demo.Models;
using AspectCache.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AspectCache.Demo.Cache
{
    public class ItemKeyFactory : ICacheKeyFactory<Item>
    {
        public string Create(HttpContext context)
        {
            return context.Request.Query["id"];
        }
    }
}
