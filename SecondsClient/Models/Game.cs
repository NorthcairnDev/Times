
using System.Diagnostics;

namespace SecondsClient.Models
{
    public class Game
    {
        public const double InitalReserve = 5;
        private const int InitalScore = 0;
        

        public int Score
        {
            get; set;
        } = InitalScore;

        public double Reserve
        {
            get; set;
        } = InitalReserve;

        public IRound? Round
        {
            get;
            set;
        }

        public bool IsGameOver
        {
            get;
            set;
        } = false;

        public bool NewHighScore
        {
            get;
            set;
        } = false;

       
        public void RoundOver()
        {
                       
            //Round!.EndTime = DateTime.UtcNow;
            Reserve -= Math.Abs(Round.Accuracy.TotalSeconds);

            if (Reserve < 0)
            {
                IsGameOver = true;
                return;
            }
            Score++;
        }

        public void NewRound()
        {
            Round = new Round();
        }

      
    }
}
