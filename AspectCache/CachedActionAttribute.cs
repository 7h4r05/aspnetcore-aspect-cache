using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using AspectCache.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace AspectCache
{
    public class CachedActionAttribute: Attribute, IActionFilter
    {
        public string CacheKey { get; set; }
        public Type EntityType { get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.OK)
            {
                    if(context.Result is OkObjectResult)
                    {
                        var result = ((OkObjectResult)context.Result).Value;
                        var cache = context.HttpContext.RequestServices.GetService<ICache>();
                        var cacheKey = string.IsNullOrWhiteSpace(CacheKey) ?
                        KeyFactoryGenerator.Create(CacheKey, EntityType, context.HttpContext)
                        : CacheKey;
                        typeof(ICache)
                                    .GetMethod("Set")
                                    .MakeGenericMethod(EntityType)
                                    .Invoke(cache, new object[] { cacheKey, result });
                }
            }
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if(EntityType == null)
            {
                throw new ArgumentNullException("EntityType");
            }
            var cache = context.HttpContext.RequestServices.GetService<ICache>();

            var cacheKey = string.IsNullOrWhiteSpace(CacheKey) ?  KeyFactoryGenerator.Create(CacheKey, EntityType, context.HttpContext)
                :CacheKey;

            var result = typeof(ICache)
            .GetMethod("Get")
            .MakeGenericMethod(EntityType)
            .Invoke(cache, new object[] { cacheKey });

            if (result != null)
            {
                context.Result = new OkObjectResult(result);
            }
        }


    }

}
