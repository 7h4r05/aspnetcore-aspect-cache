# aspnetcore-aspect-cache
ASP.NET Core Reusable cache implementation


You can cache any serializable type of entity with explicit key
`[CachedAction(CacheKey ="Single", EntityType = typeof(Item))]`
or use runtime key factory
`[CachedAction(CacheKey ="Single")]`


Provides:

- use cached value before executing controller's action
- auto-cache response after action execution

## Usage

### Cache implementation

You have to implement `AspectCache.ICache` in your project. That allows to use any cache implementation for example Memory Cache
```csharp
  public class InMemoryCache : ICache
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void Set<T>(string key, object value)
        {
            _memoryCache.Set<T>(key, (T)value);
        }
    }
```

### Key Factory implementation

In real-life solutions sometimes you base the cache key on the property of time, or you build a complex map that needs to be evaluated. 
In that scenarios you would need implementation of `ICacheKeyFactory<T>`

Assuming a hash key of entity Item equals "id" from Query String
```csharp
    public class ItemKeyFactory : ICacheKeyFactory<Item>
    {
        public string Create(HttpContext context)
        {
            return context.Request.Query["id"];
        }
    }
```
### Init

Register your implementations in service collection

```csharp
    services.AddSingleton<ICache, InMemoryCache>();
    services.AddSingleton<ICacheKeyFactory<Item>, ItemKeyFactory>();
```

### CachedAction

Finally decorate your actions with attributes to either use a const cache key or to create a cache key during runtime based on key factory implementation



```csharp
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
```
