using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondsClient.Models
{
    internal class Round
    {
        public enum LevelsOfAccuracy
        {
            VeryClose,
            Close,
            Distant
        }


        public DateTime StartTime
        {
            get; set;
        }

        public DateTime EndTime
        {
            get; set;
        }

        public TimeSpan TargetInSeconds
        {
            get; set;
        }

        public TimeSpan Accuracy
        {
            get => EndTime - StartTime - TargetInSeconds;
        }

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

        
        private readonly int _minTargetSeconds = 1;
        private readonly int _maxTargetSeconds = 5;

        public Round()
        {
            TargetInSeconds = TimeSpan.FromSeconds(new Random().Next(_minTargetSeconds, _maxTargetSeconds + 1));
            StartTime = DateTime.UtcNow;
        }
    }
}
