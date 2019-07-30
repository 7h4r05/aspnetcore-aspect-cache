using System;
using Microsoft.AspNetCore.Http;
using AspectCache.Interfaces;

namespace AspectCache
{
    public class KeyFactoryGenerator
    {
        public static string Create(string cacheKey, Type keyFactoryType, HttpContext context)
        {
            var result = cacheKey;
            if (keyFactoryType != null)
            {
                var baseType = typeof(ICacheKeyFactory<>);
                var constructedType = baseType.MakeGenericType(keyFactoryType);
                dynamic cacheKeyFactory = context.RequestServices.GetService(constructedType);
                if (cacheKeyFactory == null)
                {
                    throw new ArgumentNullException("KeyFactoryType", $"Implementation for ICacheKeyFactory<{keyFactoryType.Name}> not found");
                }
                result = cacheKeyFactory.Create(context);
            }
            return result;

        }
    }
}
