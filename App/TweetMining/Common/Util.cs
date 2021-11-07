namespace TweetMining.Common
{
    using System;
    using TweetMining.Enums;

    public class Util
    {
        /// <summary>
        /// Display custom message to console
        /// </summary>
        /// <param name="startTime">Time when start to count number tweet </param>
        /// <param name="currTime">Time when each tweet is tweeted</param>
        /// <param name="count">Number of tweet</param>
        /// <param name="totalCount">Total of tweet since start</param>
        public static void ConsoleWrite(DateTime currTime, int count, int totalCount)
        {
            Console.WriteLine($"{currTime.ToLongTimeString()} - {count}");
            Console.WriteLine($"Total tweets recieved - {totalCount}");
        }

        /// <summary>
        /// Calculate time span per given time interval
        /// </summary>
        /// <param name="interval">Type of time interval</param>
        /// <param name="currTime">Current DateTime</param>
        /// <param name="startTime">Initial DateTime</param>
        /// <returns>Total time span in <see cref="double"></see></returns>
        public static double GetTimeSpan(IntervalType interval, DateTime currTime, DateTime startTime)
        {
            return interval switch
            {
                IntervalType.Second => (currTime - startTime).TotalSeconds,
                IntervalType.Minute => (currTime - startTime).TotalMinutes,
                _ => -1,
            };
        }

        /// <summary>
        /// Calculate the time to stop a given task
        /// </summary>
        /// <param name="interval">Type of time interval</param>
        /// <param name="currTime">Current DateTime</param>
        /// <param name="startTime">Initial DateTime</param>
        /// <param name="capTime">A time to stop</param>
        /// <returns>A boolean value</returns>
        public static bool CapTime(IntervalType interval, DateTime currTime, DateTime initTime, int capTime = 10)
        {
            return GetTimeSpan(interval, currTime, initTime) >= capTime;
        }
    }
}