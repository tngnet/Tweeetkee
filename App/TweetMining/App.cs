namespace TweetMining
{
    using System.Threading.Tasks;
    using TweetMining.Enums;
    using TweetMining.Models;

    /// <summary>
    /// This is where the application starts
    /// </summary>
    public class App
    {
        /// <summary>
        /// Defines the instance of <see cref="IStreamService"/> implementation
        /// </summary>
        private readonly IStreamService _streamService;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        /// <param name="streamService">The <see cref="SampleService"/></param>
        public App(IStreamService streamService)
        {
            this._streamService = streamService;
        }

        /// <summary>
        /// This method will be invoked by <see cref="Program"/> class to start the application
        /// </summary>
        /// <returns>A task</returns>
        public async Task<Result> RunAsync()
        {
            return await this._streamService.RunAsync(Interval.Second, 1);
        }
    }
}