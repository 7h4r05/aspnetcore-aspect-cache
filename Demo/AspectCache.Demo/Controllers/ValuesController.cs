using System;
using AspectCache.Demo.Cache;
using AspectCache.Demo.Models;
using AspectCache.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspectCache.Demo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ICache _distributedCache;

        public ValuesController(ICache cache)
        {
            _distributedCache = cache;
        }

        [HttpGet]
        [CachedAction(CacheKey ="Single", EntityType = typeof(Item))]
        [Route("single")]
        public ActionResult Get()
        {
            var random = new Random();
            var item = new Item
            {
                Value = random.Next(),
                Id = random.Next().ToString()
            };

            return Ok(item);
        }

        [HttpGet]
        [Route("by")]
        [CachedAction(EntityType = typeof(Item))]
        public ActionResult GetById()
        {
            var random = new Random();
            var item = new Item
            {
                Id = Request.Query["id"].ToString(),
                Value = random.Next()
              };

            return Ok(item);
        }

        [HttpPost]
        [Route("clearBy")]
        [InvalidateCachedAction(EntityType =typeof(Item))]
        public ActionResult Invalidate()
        {
            return Ok();
        }


        [HttpDelete]
        public ActionResult Clear()
        {
            _distributedCache.Remove("Single");
            return Ok(1);
        }

        }
}
