using FluentAssertions;
using MessagePlatform.Domains;
using MessagePlatform.Services;
using Xunit;

namespace MessagePlatform.Tests
{
    public class TimelineServiceTests : TestFixture
    {
        private Messages _messages;
        private Following _following;
        private TimelineService _timelineService;

        public TimelineServiceTests()
        {
            _messages = GetService<Messages>();
            _following = GetService<Following>();
            _timelineService = GetService<TimelineService>();
        }

        [Fact]
        public void GetTimeline_Empty_Success()
        {
           var timeline = _timelineService.GetTimeline("myUser");
           timeline.Should().BeNullOrEmpty();
        }

        [Fact]
        public void GetTimeline_FollowingOne_NoMessages_Success()
        {
            _following.AddFollowing("myUser", "yourUser");
            var timeline = _timelineService.GetTimeline("myUser");
            timeline.Should().BeNullOrEmpty();
        }

        [Fact]
        public void GetTimeline_FollowingOne_OneMessage_Success()
        {
            _messages.AddMessage("yourUser", "hello!");
            _following.AddFollowing("myUser", "yourUser");
            var timeline = _timelineService.GetTimeline("myUser");
            timeline.Count.Should().Be(1);
        }

        [Fact]
        public void GetTimeline_FollowingOne_OneMessage_FromCurrentUser_Success()
        {
            _messages.AddMessage("myUser", "hello!");
            _messages.AddMessage("yourUser", "hello!");
            _following.AddFollowing("myUser", "yourUser");
            var timeline = _timelineService.GetTimeline("myUser");
            timeline.Count.Should().Be(2);
        }

        [Fact]
        public void GetTimeline_FollowingMultiple_MultipleMessagesFromCurrentUser_InOrder_Success()
        {
            _messages.AddMessage("myUser", "hello!");
            _messages.AddMessage("yourUser2", "hello!");
            _messages.AddMessage("myUser", "hello!");
            _messages.AddMessage("yourUser3", "hello!");
            _messages.AddMessage("myUser", "hello!");
            _messages.AddMessage("yourUser4", "hello!");
            _messages.AddMessage("myUser", "hello!");
            _messages.AddMessage("yourUser5", "hello!");
            _messages.AddMessage("yourUser5", "hello!");
            _messages.AddMessage("yourUser5", "hello!");
            _messages.AddMessage("yourUser5", "hello!");
            _following.AddFollowing("myUser", "yourUser");
            _following.AddFollowing("myUser", "yourUser2");
            _following.AddFollowing("myUser", "yourUser3");
            _following.AddFollowing("myUser", "yourUser4");
            _following.AddFollowing("myUser", "yourUser5");
            _following.AddFollowing("myUser", "yourUser");
            var timeline = _timelineService.GetTimeline("myUser");
            timeline.Count.Should().Be(11);

            timeline[0].Date.Should().BeAfter(timeline[timeline.Count - 1].Date);
        }
    }
}
