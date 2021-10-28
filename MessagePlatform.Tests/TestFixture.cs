using MessagePlatform.Domains;
using MessagePlatform.Helpers;
using MessagePlatform.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MessagePlatform.Tests
{
    public class TestFixture
    {
        protected ServiceProvider provider;
        public TestFixture()
        {
            provider = new ServiceCollection()
                .AddSingleton<CacheHelper>()
                .AddTransient<Following>()
                .AddTransient<Messages>()
                .AddTransient<TimelineService>()
                .BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return provider.GetService<T>();
        }
    }
}