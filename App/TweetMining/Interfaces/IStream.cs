namespace TweetMining
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// A contract for implementing of <see cref="IStream"></see>
    /// </summary>
    public interface IStream
    {
        /// <summary>
        /// Consuming a Twitter V2 sample stream endpoint and taking live stream data into memory stream
        /// </summary>
        /// <returns>A Task of <see cref="StreamReader"></see></returns>
        Task<StreamReader> ReadV2SampleStreamAsync();
    }
}