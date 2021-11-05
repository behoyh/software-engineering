using FluentAssertions;
using MessagePlatform.Domains;
using System;
using MessagePlatform.Models;
using Xunit;

namespace MessagePlatform.Tests
{
    public class DatedTests : TestFixture
    {


        [Fact]
        public void postFromCurrentTimeToOneSecond_Equals_1_second_ago() {
          Message message = new Message(){ Date = DateTime.Now };
          message.getElapsed().Should().Be("1 second ago");
        }

        [Fact]
        public void postFrom5secondsAgo_equals_5_seconds_ago() {
          Message message = new Message(){ Date = DateTime.Now.AddSeconds(-5) };
          message.getElapsed().Should().Be("5 seconds ago");
        }
    }
}
