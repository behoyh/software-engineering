using System;

namespace MessagePlatform.Models
{
    public class Message: Dated
    {
        public string User { get; set; }
        public string Content { get; set; }
    }
}
