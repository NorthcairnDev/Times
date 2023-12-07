
namespace SecondsClient.Models
{
    class GameHistory
    {
        public static int HighScore
        {
            get => Preferences.Default.Get<int>("HighScore", 0);
            set => Preferences.Default.Set<int>("HighScore", value);
        }

    }
}
