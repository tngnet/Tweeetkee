namespace TweetMining.Test
{
    using FakeItEasy;
    using System.Threading.Tasks;
    using TweetMining;
    using TweetMining.Models;
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
            var _streamService = A.Fake<IStreamService>();
            A.CallTo(() => _streamService.RunAsync(default, default)).WithAnyArguments().Returns(Result.Ok());
            var _app = new App(_streamService);

            var _result = await _app.RunAsync();

            Assert.True(_result.Success);
            Assert.Empty(_result.Error);
        }
    }
}