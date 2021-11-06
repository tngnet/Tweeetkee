namespace TweetMining
{
    using TweetMining.Enums;
    using TweetMining.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// A contract for <see cref="SampleService"/> class
    /// </summary>
    public interface IStreamService
    {
        /// <summary>
        /// Start mining streaming tweet data then display calculation total result on console as configured
        /// </summary>
        /// <param name="interval">Date time interval for displaying number of tweets on console</param>
        /// <param name="frequency">how often for displaying number of tweets on console</param>
        /// <returns>A task</returns>
        Task<Result> RunAsync(Interval interval, int frequency);
    }
}