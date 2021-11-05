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

        private void AddFollowing(string user, string following)
        {
            var result = _following.AddFollowing(user, following);
            result.Should().BeTrue();
        }

        [Fact]
        public void AddFollowing_Success()
        {
            AddFollowing("myUser", "yourUser");
        }

        [Fact]
        public void GetFollowing_Success()
        {
            AddFollowing("myUser", "yourUser");
            var result = _following.GetFollowing("myUser");
            result.Should().NotBeEmpty();
        }

        [Fact]
        public void GetMultipleFollowing_Success()
        {
            AddFollowing("myUser", "yourUser");
            AddFollowing("myUser", "yourUser2");
            var result = _following.GetFollowing("myUser");
            result.Count.Should().Be(2);
        }

        [Fact]
        public void AddFollowing_Same_User_MultipleTimes_Should_Not_Duplicate_Following()
        {
            AddFollowing("myUser", "yourUser");
            AddFollowing("myUser", "yourUser");

            var result = _following.GetFollowing("myUser");

            result.Count.Should().Be(1);
        }
    }
}
