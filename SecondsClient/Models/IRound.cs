
namespace SecondsClient.Models
{
    public interface IRound
    {
        TimeSpan Accuracy { get; }
        Round.LevelsOfAccuracy AccuracyLevel { get; }
        DateTime EndTime { get; set; }
        DateTime StartTime { get; set; }
        TimeSpan TargetInSeconds { get; set; }
    }
}