using FluentAssertions;
using MessagePlatform.Domains;
using Xunit;

namespace MessagePlatform.Tests
{
    public class FollowingTests : TestFixture
    {
        private Following _following;
        public FollowingTests()
        {
            _following = GetService<Following>();
        }

        [Fact]
        public void AddFollowing_Success()
        {
            var result = _following.AddFollowing("myUser", "yourUser");
            result.Should().BeTrue();
        }

        [Fact]
        public void GetFollowing_Success()
        {
            AddFollowing_Success();
            var result = _following.GetFollowing("myUser");
            result.Should().NotBeEmpty();
        }

        [Fact]
        public void GetMultipleFollowing_Success()
        {
            AddFollowing_Success();
            AddFollowing_Success();
            var result = _following.GetFollowing("myUser");
            result.Count.Should().Be(2);
        }
    }
}
