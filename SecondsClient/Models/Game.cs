using System.ComponentModel;

namespace SecondsClient.Models
{
    class Game
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

      
        public List<Round> Rounds
        {
            get;
        }

        public Round? Round
        {
            get;
            set;
        }

        public bool GameOver
        {
            get;
            set;
        } = false;

        public bool NewHighScore
        {
            get;
            set;
        } = false;


        public Game()
        {
            Rounds = [];
            Round = new Round();
        }

        public void RoundOver()
        {
                       
            Round.EndTime = DateTime.UtcNow;
            Rounds.Add(Round);
            Reserve -= Math.Abs(Round.Accuracy.Value.TotalSeconds);

            if (Reserve < 0)
            {
                GameOver = true;
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
