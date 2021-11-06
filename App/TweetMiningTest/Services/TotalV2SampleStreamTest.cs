namespace TweetMining.Test.Services
{
    using FakeItEasy;
    using Microsoft.Extensions.Logging;
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using TweetMining.Enums;
    using TweetMining.Services;
    using Xunit;

    public class TotalV2SampleStreamTest
    {
        [Fact]
        public void Constructor_Works()
        {
            var _totalV2SampleStream = new TotalV2SampleStream(default, default);
            Assert.NotNull(_totalV2SampleStream);
        }

        [Fact]
        public async Task RunAsync_WhenStreamDataNull_NothingHappen()
        {
            var _sampleStream = A.Fake<IStream>();
            var _logger = A.Fake<ILogger<TotalV2SampleStream>>();

            var _totalV2SampleStream = new TotalV2SampleStream(_sampleStream, _logger);

            var _result = await _totalV2SampleStream.RunAsync(Enums.Interval.Second, 1);

            Assert.True(_result.Success);
            Assert.Empty(_result.Error);
        }

        [Fact]
        public async Task RunAsync_WhenIntervalInvalid_LogWarning()
        {
            var _sampleStream = A.Fake<IStream>();
            var _logger = A.Fake<ILogger<TotalV2SampleStream>>();

            string _path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            var _fileName = Path.Combine(_path, "Services\\TestFile.txt");

            A.CallTo(() => _sampleStream.ReadV2SampleStreamAsync()).Returns(new StreamReader(_fileName));

            var _totalV2SampleStream = new TotalV2SampleStream(_sampleStream, _logger);

            var _result = await _totalV2SampleStream.RunAsync(Enums.Interval.None, 1);

            Assert.False(_result.Success);
            Assert.Equal("Invalid interval", _result.Error);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task RunAsync_WhenFrequencyInvalid_LogWarning(int frequency)
        {
            var _sampleStream = A.Fake<IStream>();
            var _logger = A.Fake<ILogger<TotalV2SampleStream>>();

            var _totalV2SampleStream = new TotalV2SampleStream(_sampleStream, _logger);

            var _result = await _totalV2SampleStream.RunAsync(Enums.Interval.Second, frequency);

            Assert.False(_result.Success);
            Assert.Equal("Invalid frequency", _result.Error);
        }

        [Theory]
        [InlineData(Interval.Second)]
        [InlineData(Interval.Minute)]
        public async Task RunAsync_WhenIntervalValid_WriteToConsole(Interval interval)
        {
            var _sampleStream = A.Fake<IStream>();
            var _logger = A.Fake<ILogger<TotalV2SampleStream>>();

            string _path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            var _fileName = Path.Combine(_path, "Services\\TestFile.txt");

            A.CallTo(() => _sampleStream.ReadV2SampleStreamAsync()).Returns(new StreamReader(_fileName));

            var _totalV2SampleStream = new TotalV2SampleStream(_sampleStream, _logger);

            var _result = await _totalV2SampleStream.RunAsync(interval, 1);

            Assert.True(_result.Success);
            Assert.Empty(_result.Error);
        }
    }
}