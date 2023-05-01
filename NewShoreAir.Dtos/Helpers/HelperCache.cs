using Microsoft.Extensions.Caching.Memory;


namespace NewShoreAir.Domain.Helpers
{
    public static class HelperCache
    {
        public static IMemoryCache _cache;

        public static void Initialize(IMemoryCache cache)
        {
            _cache = cache;
        }





        public static T GetItem<T>(string key, Func<T> loadItemFromDb)
        {
            if (_cache == null)
            {
                T obj = loadItemFromDb();
                return obj;
            }

            object cachedItem = _cache.Get(key);
            if (cachedItem is T)
                return (T)cachedItem;
            T item = loadItemFromDb();
            _cache.Set(key, item, DateTimeOffset.UtcNow.AddHours(8));
            return item;
        }
    }
}
