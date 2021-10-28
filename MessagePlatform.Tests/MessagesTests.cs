using System;
using FluentAssertions;
using MessagePlatform.Domains;
using Xunit;

namespace MessagePlatform.Tests
{
    public class MessagesTests : TestFixture
    {
        private Messages _messages;
        public MessagesTests()
        {
            _messages = GetService<Messages>();
        }

        [Fact]
        public void AddMessage_Success()
        {
            var result = _messages.AddMessage("myUser", "Hello!");
            result.Should().BeTrue();
        }

        [Fact]
        public void GetMessage_Success()
        {
            AddMessage_Success();
            var result = _messages.GetMessages("myUser");
            result.Should().NotBeEmpty();
        }

        [Fact]
        public void GetMultipleFollowing_Success()
        {
            AddMessage_Success();
            AddMessage_Success();
            var result = _messages.GetMessages("myUser");
            result.Count.Should().Be(2);
        }
    }
}
