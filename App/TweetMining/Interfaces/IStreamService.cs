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
        /// Define a type of an operation
        /// </summary>
        public OperationType OperationType { get; }

        /// <summary>
        /// Define a contract
        /// </summary>
        /// <param name="interval">Type of time interval</param>
        /// <param name="frequency">Number of interval</param>
        /// <returns>A task</returns>
        Task<Result> RunAsync(IntervalType interval, int frequency);
    }
}