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


        public DateTime StartTime
        {
            get; set;
        } = DateTime.UtcNow;

        public DateTime EndTime
        {
            get; set;
        } 

        public TimeSpan TargetInSeconds
        {
            get; set;
        } = TimeSpan.FromSeconds(new Random().Next(_minTargetSeconds, _maxTargetSeconds + 1));

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
    }
}