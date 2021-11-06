namespace TweetMining.Models
{
    /// <summary>
    /// A class to represent the returning result
    /// </summary>
    public class Result
    {
        /// <summary>
        /// An indicator whether the result is passed or failed
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// An error message
        /// </summary>
        public string Error { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="success"></param>
        /// <param name="error"></param>
        protected Result(bool success, string error)
        {
            this.Success = success;
            this.Error = error;
        }

        /// <summary>
        /// A fail result
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        /// <summary>
        /// A pass result
        /// </summary>
        /// <returns></returns>
        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }
    }
}