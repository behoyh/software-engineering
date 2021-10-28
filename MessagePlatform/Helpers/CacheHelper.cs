using System.Collections.Generic;

namespace MessagePlatform.Helpers
{
    public class CacheHelper
    {
        private Dictionary<string,object> _cache;
        public CacheHelper()
        {
            _cache = new Dictionary<string, object>();
        }

        public T Get<T>(string key)
        {
            if(!_cache.ContainsKey(key))
            {
                return default;
            }
            return (T)_cache[key];
        }

        public void Set<T>(string key, T value)
        {
            _cache[key] = value;
        }
    }
}
