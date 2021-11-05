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

        private void AddMessage(string user, string content)
        {
            var result = _messages.AddMessage("myUser", "Hello!");
            result.Should().BeTrue();
        }

        [Fact]
        public void AddMessage_Success()
        {
            AddMessage("myUser", "Hello!");
        }

        [Fact]
        public void GetMessage_Success()
        {
            AddMessage("myUser", "Hello!");
            var result = _messages.GetMessages("myUser");
            result.Should().NotBeEmpty();
        }

        [Fact]
        public void AddMultipleMessages_Success()
        {
            AddMessage("myUser", "Hello!");
            AddMessage("myUser", "Hello!");
            var result = _messages.GetMessages("myUser");
            result.Count.Should().Be(2);
        }
    }
}
