namespace TweetMining
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TweetMining.Enums;
    using TweetMining.Models;

    /// <summary>
    /// This is where the application starts
    /// </summary>
    public class App
    {
        /// <summary>
        /// Defines a collection of <see cref="IStreamService"/> implementation
        /// </summary>
        private readonly IEnumerable<IStreamService> _streamServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        /// <param name="streamServices">A collection of implementation of <see cref="IStreamService"/></param>
        public App(IEnumerable<IStreamService> streamServices)
        {
            this._streamServices = streamServices;
        }

        /// <summary>
        /// This method will find the match of operation services and execute to perform the task
        /// </summary>
        /// <returns>A task</returns>
        public async Task<Result> RunAsync()
        {
            return await this._streamServices
                .Where(i => i.OperationType == OperationType.Average)
                .SingleOrDefault()
                .RunAsync(IntervalType.Minute, 1);
        }
    }
}