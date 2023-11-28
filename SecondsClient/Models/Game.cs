namespace SecondsClient.Models
{
    class Game
    {
        public int HighScore
        {
            get => Preferences.Default.Get<int>("HighScore", 0);
            set => Preferences.Default.Set<int>("HighScore", value);
        }

        public int Score
        {
            get; set;
        }

        public double Reserve
        {
            get; set;
        }

        public readonly double InitalReserve = 5;

        public List<Round> Rounds
        {
            get;
        }

        public Round? Round
        {
            get;
            set;
        }



        public Game()
        {
            Rounds = [];
            Round = new Round();
            ResetGame();
        }

        public void ResetGame()
        {
            Reserve = 5;
            Score = 0;
            Rounds.Clear();
            Reserve = InitalReserve;
        }
        public void RoundOver()
        {
                       
            Round.EndTime = DateTime.UtcNow;
            Rounds.Add(Round);
            Reserve -= Math.Abs(Round.Accuracy.Value.TotalSeconds);

            if (Reserve < 0)
            {
                GameOver();
                return;
            }

            Score++;

        }


        private void GameOver()
        {
            HighScoreUpdate();

        }

        private void HighScoreUpdate()
        {
            if (Score > HighScore) HighScore = Score;
        }


        public void NewRound()
        {
            Round = new Round();
            
        }
    }
}
