using System.Diagnostics;

namespace SecondsClient.Models
{
    public class Round : IRound
    {
        public enum LevelsOfAccuracy
        {
            VeryClose,
            Close,
            Distant
        }

        private const int _minTargetSeconds = 1;
        private const int _maxTargetSeconds = 5;
       
  
        public TimeSpan TargetInSeconds
        {
            get; set;
        } = TimeSpan.FromSeconds(new Random().Next(_minTargetSeconds, _maxTargetSeconds + 1));

        public TimeSpan Accuracy
        {
            get =>_stopwatch.Elapsed - TargetInSeconds;
        }

        Stopwatch _stopwatch =  new();


        public LevelsOfAccuracy AccuracyLevel
        {
            get
            {
                return Math.Abs(Accuracy.TotalMilliseconds) switch
                {
                    < 250 => LevelsOfAccuracy.VeryClose,
                    < 500 => LevelsOfAccuracy.Close,
                    _ => LevelsOfAccuracy.Distant,
                };
            }
        }

        public void StartStopwatch()
        {
            _stopwatch.Start();

        }

        public void StopStopwatch()
        {
            _stopwatch.Stop();

        }


    }
}