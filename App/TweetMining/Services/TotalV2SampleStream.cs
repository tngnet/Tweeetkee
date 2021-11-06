namespace TweetMining.Services
{
    using TweetMining.Enums;
    using TweetMining.Models;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Read live sample stream data from Twitter and display counts result on console
    /// </summary>
    public class TotalV2SampleStream : IStreamService
    {
        /// <summary>
        /// Defines a contract implemented by <see cref="Logger"/>
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Defines a contract implemented by <see cref="TwitterGateway"/>
        /// </summary>
        private readonly IStream _twitterGateway;

        /// <summary>
        /// For testing purpose only - to control rate limit
        /// </summary>
        private readonly int _capTime = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="TotalV2SampleStream"/> class.
        /// </summary>
        /// <param name="twitterGateway">An API service</param>
        /// <param name="logger">A Microsoft logger for logging</param>
        public TotalV2SampleStream(IStream twitterGateway, ILogger<TotalV2SampleStream> logger)
        {
            this._logger = logger;
            this._twitterGateway = twitterGateway;
        }

        /// <summary>
        /// Start mining streaming tweet data then display calculation total result on console as configured
        /// </summary>
        /// <param name="interval">Date time interval for displaying number of tweets on console</param>
        /// <param name="frequency">how often for displaying number of tweets on console</param>
        /// <returns>A <see cref="Task"></see></returns>
        public async Task<Result> RunAsync(Interval interval, int frequency)
        {
            if (frequency <= 0)
            {
                this._logger.LogWarning($"Invalid {nameof(frequency)}");
                return Result.Fail($"Invalid {nameof(frequency)}");
            }
            else
            {
                try
                {
                    using StreamReader _reader = await this._twitterGateway.ReadV2SampleStreamAsync();

                    int _totalCount = 0;
                    Object _lock = new();
                    List<string> _collection = new();
                    DateTime _startTime = DateTime.Now;
                    DateTime _initTime = DateTime.Now;
                    bool _isInit = true;

                    while (_reader != null && !_reader.EndOfStream)
                    {
                        DateTime _currTime = DateTime.Now;

                        // First time - start time is current time
                        if (_isInit)
                        {
                            _startTime = _currTime;
                            _isInit = false;
                        }

                        lock (_lock)
                        {
                            _collection.Add(_reader.ReadLine());
                            var _timeSpan = GetTimeSpan(interval, _currTime, _startTime);

                            if (_timeSpan == -1)
                            {
                                this._logger.LogWarning($"Invalid {nameof(interval)}");
                                return Result.Fail($"Invalid {nameof(interval)}");
                            }
                            else if (_timeSpan >= frequency)
                            {
                                int _itemCount = _collection.Count;
                                _totalCount += _itemCount;

                                ConsoleWrite(_currTime, _itemCount, _totalCount);
                                _collection = new List<string>();
                                _startTime = DateTime.Now;
                            }

                            //// This to control rate limit - for testing purpose
                            //if ((_currTime - _initTime).TotalMinutes >= _capTime)
                            //{
                            //    Console.WriteLine("===============Stop==============");
                            //    return Result.Ok();
                            //}
                        }
                    }
                    return Result.Ok();
                }
                catch (Exception e)
                {
                    this._logger.LogError(e.ToString());
                    return Result.Fail("Exception thrown.");
                }
            }
        }

        /// <summary>
        /// Display custom message to console
        /// </summary>
        /// <param name="startTime">Time when start to count number tweet </param>
        /// <param name="currTime">Time when each tweet is tweeted</param>
        /// <param name="count">Number of tweet</param>
        /// <param name="totalCount">Total of tweet since start</param>
        private static void ConsoleWrite(DateTime currTime, int count, int totalCount)
        {
            Console.WriteLine($"{currTime.ToLongTimeString()} - {count}");
            Console.WriteLine($"Total - {totalCount}");
        }

        /// <summary>
        /// Calculate time span per given time interval
        /// </summary>
        /// <param name="interval">Type of time interval</param>
        /// <param name="currTime">Current DateTime</param>
        /// <param name="startTime">Initial DateTime</param>
        /// <returns>Total time span in <see cref="double"></see></returns>
        private static double GetTimeSpan(Interval interval, DateTime currTime, DateTime startTime)
        {
            return interval switch
            {
                Interval.Second => (currTime - startTime).TotalSeconds,
                Interval.Minute => (currTime - startTime).TotalMinutes,
                _ => -1,
            };
        }
    }
}