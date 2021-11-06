namespace TweetMining
{
    using TweetMining.Common;
    using TweetMining.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Configuration;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A generated initial class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Application entry point
        /// </summary>
        /// <param name="args">Any command line arguments</param>
        /// <returns>A Task</returns>
        public static async Task Main(string[] args)
        {
            var _services = ConfigureServices();

            var _provider = _services.BuildServiceProvider();

            await _provider.GetService<App>().RunAsync();
        }

        /// <summary>
        /// Create service collection and register all services
        /// </summary>
        /// <returns>A service collection</returns>
        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection _services = new ServiceCollection();

            _services.AddLogging(builder => builder.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Error)
                .AddTransient<TotalV2SampleStream>();

            _services.AddTransient<IStreamService, TotalV2SampleStream>();

            _services.AddTransient<IStream, TwitterGateway>();

            _services.AddHttpClient(Client.V2Sample, (HttpClient client) =>
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ConfigurationManager.AppSettings["BearerToken"]}");
            });

            _services.AddTransient<App>();

            return _services;
        }
    }
}