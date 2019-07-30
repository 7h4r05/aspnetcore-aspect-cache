namespace AspectCache.Interfaces
{
    public interface ICache
    {
        T Get<T>(string key);
        void Set<T>(string key, object value);
        void Remove(string key);
    }
}