using System;
using System.Collections.Generic;
using MessagePlatform.Helpers;
using MessagePlatform.Models;

namespace MessagePlatform.Domains
{
    public class Messages
    {
        private CacheHelper _cacheHelper;
        public Messages(CacheHelper cacheHelper)
        {
            _cacheHelper = cacheHelper;
        }

        public bool AddMessage(string user, string content)
        {
            var messages = GetMessages(user);
            var message = new Message()
            {
                User = user,
                Content = content,
                Date = DateTime.Now
            };
            if (messages == default)
            {
                messages = new List<Message>();
                messages.Add(message);
                _cacheHelper.Set(CacheKeys.Messages(user), messages);
                return true;
            }
            messages.Add(message);
            return true;
        }

        public List<Message> GetMessages(string user)
        {
            return _cacheHelper.Get<List<Message>>(CacheKeys.Messages(user));
        }
    }
}
