using Google.Android.Material.Color.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondsClient.Models
{
    class Game
    {
        public int HighScore {
            get => Preferences.Default.Get<int>("HighScore", 0);
            set => Preferences.Default.Set<int>("HighScore", HighScore);
        }

        public int Score
        {
            get; set;
        }

        public DateTime StartTime
        {
            get; set;
        }

        public DateTime EndTime
        {
            get; set;
        }


        public int TargetSeconds
        {
            get; set;
        }

        public double Accuracy
        {
            get => ((EndTime - StartTime).TotalSeconds) - TargetSeconds;
        }



        public double Reserve
        {
            get; set;
        }

        public Game() 
        { Score = 0; }


        public readonly double InitalReserve = 5;

        public void ResetGame() 
        {
            Reserve = 5;
            Score = 0;
            NewTargetSeconds();
        }
        public void Stopped() 
        {
            EndTime = DateTime.UtcNow;
            Reserve -= Math.Abs(Accuracy);

            if (Reserve < 0)
            {
                GameOver();
                return;
            }

            Score++;

        }

        private bool IsGameOver()
        {
            return Reserve - Math.Abs(Accuracy) < 0;

        }

        private void GameOver()
        {
            HighScoreUpdate();
                       
        }


        private void HighScoreUpdate()
        {
            if (Score > HighScore) HighScore = Score;
        }


    public void NewTargetSeconds()
        {
            TargetSeconds = new Random().Next(1, 6);
        }
    }
}
