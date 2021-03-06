using System;
using System.Collections.Generic;
using MessagePlatform.Helpers;
using MessagePlatform.Models;

namespace MessagePlatform.Domains
{
    public class Following
    {
        private CacheHelper _cacheHelper;
        public Following(CacheHelper cacheHelper)
        {
            _cacheHelper = cacheHelper;
        }

        public bool AddFollowing(string user, string following)
        {
            var followers = GetFollowing(user);

            if (followers == default)
            {
                followers = new HashSet<string>();
                followers.Add(following);
                _cacheHelper.Set(CacheKeys.Following(user), followers);
                return true;
            }
            followers.Add(following);
            return true;
        }

        public HashSet<string> GetFollowing(string user)
        {
            return _cacheHelper.Get<HashSet<string>>(CacheKeys.Following(user));
        }
    }
}
