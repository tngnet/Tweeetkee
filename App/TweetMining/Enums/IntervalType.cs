namespace TweetMining.Enums
{
    using System.ComponentModel;

    public enum IntervalType
    {
        [Description("none")]
        None,

        [Description("in second")]
        Second,

        [Description("in minute")]
        Minute,
    }
}