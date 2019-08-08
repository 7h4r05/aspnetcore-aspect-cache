
using System;
using System.Net;
using AspectCache.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace AspectCache
{
    public class InvalidateCachedActionAttribute: Attribute, IActionFilter
    {
        public string CacheKey { get; set; }
        public Type EntityType { get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.OK)
            {
                var cache = context.HttpContext.RequestServices.GetService<ICache>();
                var cacheKey = string.IsNullOrWhiteSpace(CacheKey) ?
                KeyFactoryGenerator.Create(CacheKey, EntityType, context.HttpContext)
                : CacheKey;
                typeof(ICache)
                            .GetMethod("Remove")
                            .Invoke(cache, new object[] { cacheKey });
             }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {


        }
    }

}
