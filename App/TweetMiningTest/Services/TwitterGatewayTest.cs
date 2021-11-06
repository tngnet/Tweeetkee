namespace TweetMining.Test.Services
{
    using Moq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TweetMining.Services;
    using TweetMiningTest.Default;
    using Xunit;

    public class TwitterGatewayTest
    {
        [Fact]
        public void Constructor_Works()
        {
            var _sampleStream = new TwitterGateway(default);
            Assert.NotNull(_sampleStream);
        }

        [Fact]
        public async Task ReadStreamAsync_CreateHttpClient_ReturnNewStreamReader()
        {
            var clientHandlerStub = new MockHandler();
            var client = new HttpClient(clientHandlerStub);

            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var _sampleStream = new TwitterGateway(mockFactory.Object);
            var _result = await _sampleStream.ReadV2SampleStreamAsync();

            Assert.NotNull(_result);
        }
    }
}