namespace TweetMining.Test
{
    using FakeItEasy;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TweetMining;
    using TweetMining.Enums;
    using TweetMining.Models;
    using TweetMining.Services;
    using Xunit;

    public class AppTest
    {
        [Fact]
        public void Constructor_Works()
        {
            var _app = new App(default);
            Assert.NotNull(_app);
        }

        [Fact]
        public async Task RunAsync_WhenStreamServiceInject_ReturnResult()
        {
            var _sampleStream = A.Fake<IStream>();
            var _loggerForTotal = A.Fake<ILogger<TotalV2SampleStream>>();
            var _loggerForAverage = A.Fake<ILogger<AverageV2SampleStream>>();

            var _totalV2SampleStream = new TotalV2SampleStream(_sampleStream, _loggerForTotal);
            var _averageV2SampleStream = new AverageV2SampleStream(_sampleStream, _loggerForAverage);

            List<IStreamService> _services = new() { _totalV2SampleStream, _averageV2SampleStream };

            var _app = new App(_services);
            var _result = await _app.RunAsync();

            Assert.True(_result.Success);
            Assert.Empty(_result.Error);
        }
    }
}