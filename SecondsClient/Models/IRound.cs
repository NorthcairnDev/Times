
using System.Diagnostics;

namespace SecondsClient.Models
{
    public interface IRound
    {
        TimeSpan Accuracy { get; }
        Round.LevelsOfAccuracy AccuracyLevel { get; }
        TimeSpan TargetInSeconds { get; set; }

        public void StartStopwatch();

        public void StopStopwatch();
    }

}