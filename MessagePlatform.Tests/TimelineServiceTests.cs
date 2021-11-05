using FluentAssertions;
using MessagePlatform.Domains;
using MessagePlatform.Services;
using Xunit;

namespace MessagePlatform.Tests
{
    public class WallServiceTests : TestFixture
    {
        private Messages _messages;
        private Following _following;
        private TimelineService _timelineService;

        public WallServiceTests()
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
        public void GetTimeline_OneMessage_Success()
        {
            _messages.AddMessage("yourUser", "hello!");
            var timeline = _timelineService.GetTimeline("yourUser");
            timeline.Count.Should().Be(1);
        }

        [Fact]
        public void GetTimeline_MultipleMessage_Success()
        {
            _messages.AddMessage("yourUser", "hello!");
            _messages.AddMessage("yourUser", "hello!");
            var timeline = _timelineService.GetTimeline("yourUser");
            timeline.Count.Should().Be(2);
        }

        [Fact]
        public void GetWall_Empty_Success()
        {
           var Wall = _timelineService.GetWall("myUser");
           Wall.Should().BeNullOrEmpty();
        }

        [Fact]
        public void GetWall_FollowingOne_NoMessages_Success()
        {
            _following.AddFollowing("myUser", "yourUser");
            var Wall = _timelineService.GetWall("myUser");
            Wall.Should().BeNullOrEmpty();
        }

        [Fact]
        public void GetWall_FollowingOne_OneMessage_Success()
        {
            _messages.AddMessage("yourUser", "hello!");
            _following.AddFollowing("myUser", "yourUser");
            var Wall = _timelineService.GetWall("myUser");
            Wall.Count.Should().Be(1);
        }

        [Fact]
        public void GetWall_FollowingOne_OneMessage_FromCurrentUser_Success()
        {
            _messages.AddMessage("myUser", "hello!");
            _messages.AddMessage("yourUser", "hello!");
            _following.AddFollowing("myUser", "yourUser");
            var Wall = _timelineService.GetWall("myUser");
            Wall.Count.Should().Be(2);
        }

        [Fact]
        public void GetWall_FollowingMultiple_MultipleMessagesFromCurrentUser_InOrder_Success()
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
            var Wall = _timelineService.GetWall("myUser");
            Wall.Count.Should().Be(11);

            Wall[0].Date.Should().BeAfter(Wall[Wall.Count - 1].Date);
        }
    }
}
