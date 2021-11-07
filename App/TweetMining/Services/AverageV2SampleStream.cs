namespace TweetMining.Services
{
    using TweetMining.Enums;
    using TweetMining.Models;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using TweetMining.Common;

    public class AverageV2SampleStream : IStreamService
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
        /// Define a type of an operation
        /// </summary>
        public OperationType OperationType => OperationType.Average;

        /// <summary>
        /// Initializes a new instance of the <see cref="TotalV2SampleStream"/> class.
        /// </summary>
        /// <param name="twitterGateway">An API service</param>
        /// <param name="logger">A Microsoft logger for logging</param>
        public AverageV2SampleStream(IStream twitterGateway, ILogger<AverageV2SampleStream> logger)
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
        public async Task<Result> RunAsync(IntervalType interval, int frequency)
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
                    int _timeIncrement = frequency;
                    Object _lock = new();
                    List<string> _collection = new();
                    DateTime _startTime = DateTime.Now;
                    DateTime _initTime = DateTime.Now;
                    bool _isInit = true;

                    Console.WriteLine($"Start calculating average tweets per every {frequency} {interval}");

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
                            var _timeSpan = Util.GetTimeSpan(interval, _currTime, _startTime);

                            if (_timeSpan == -1)
                            {
                                this._logger.LogWarning($"Invalid {nameof(interval)}");
                                return Result.Fail($"Invalid {nameof(interval)}");
                            }
                            else if (_timeSpan >= frequency)
                            {
                                int _itemCount = _collection.Count;
                                _totalCount += _itemCount;
                                int _averageCount = _totalCount / _timeIncrement;

                                Util.ConsoleWrite(_currTime, _averageCount, _totalCount);

                                // reset
                                _collection = new List<string>();
                                _startTime = DateTime.Now;

                                // calculate average number
                                _timeIncrement += frequency;
                            }

                            // This to control rate limit - for testing purpose only
                            if (Util.CapTime(interval, _currTime, _initTime))
                            {
                                Console.WriteLine("===============Stop==============");
                                return Result.Ok();
                            }
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
    }
}