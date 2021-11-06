namespace TweetMining.Services
{
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TweetMining.Common;

    /// <summary>
    /// A service gateway to Twitter's endpoints
    /// </summary>
    public class TwitterGateway : IStream
    {
        /// <summary>
        /// Defines a contract of default HttpClientFactory to create HttpClient instance
        /// </summary>
        private readonly IHttpClientFactory _clientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterGateway"/> class.
        /// </summary>
        /// <param name="clientFactory">A default HttpClientFactory</param>
        public TwitterGateway(IHttpClientFactory clientFactory)
        {
            this._clientFactory = clientFactory;
        }

        /// <summary>
        /// Consuming a Twitter V2 sample stream endpoint and taking live stream data into memory stream
        /// </summary>
        /// <returns>A Task of <see cref="StreamReader"></see></returns>
        public async Task<StreamReader> ReadV2SampleStreamAsync()
        {
            HttpClient _client = this._clientFactory.CreateClient(Client.V2Sample);
            Stream stream = await _client.GetStreamAsync(TwitterUrl.V2SampleStream).ConfigureAwait(false);
            return new StreamReader(stream);
        }
    }
}