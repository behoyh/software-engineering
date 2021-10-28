using System;
namespace MessagePlatform.Models
{
    public static class CacheKeys
    {
        public static string Following(string user)
        {
            return user + "_following";
        }

        public static string Messages(string user)
        {
            return user + "_messages";
        }
    }
}
