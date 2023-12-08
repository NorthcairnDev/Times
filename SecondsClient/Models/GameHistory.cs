
namespace SecondsClient.Models
{
    public class GameHistory : IGameHistory
    {
        public int HighScore
        {
            get => Preferences.Default.Get<int>("HighScore", 0);
            set => Preferences.Default.Set<int>("HighScore", value);
        }

    }
}
