using System.Collections.Generic;
using System.Linq;
using MessagePlatform.Domains;
using MessagePlatform.Models;

namespace MessagePlatform.Services
{
    public class TimelineService
    {
        private Following _following;
        private Messages _messages;
        public TimelineService(Following following, Messages messages)
        {
            _following = following;
            _messages = messages;
        }

        public List<Message> GetTimeline(string user)
        {
            var following = _following.GetFollowing(user);

            if (following == default)
            {
                return new List<Message>();
            }

            // Add current user to timeline
            following.Add(user);

            var timeline = new List<Message>();
            foreach(var person in following)
            {
                var messages = _messages.GetMessages(person);
                if (messages == default)
                {
                    continue;
                }
                timeline.AddRange(messages);
            }

            return timeline.OrderByDescending(x=>x.Date).ToList();
        }
    }
}
