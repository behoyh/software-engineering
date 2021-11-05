using System;
namespace MessagePlatform.Models
{
    public abstract class Dated
    {
      public DateTime Date {get; set;}

      public string getElapsed() {
        var timeDifference = DateTime.Now.Second - Date.Second;
        if (timeDifference > 1) {
          return timeDifference + " seconds ago";
        }
        return "1 second ago";
      }
    }
}
