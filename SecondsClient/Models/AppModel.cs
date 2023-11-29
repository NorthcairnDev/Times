using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondsClient.Models
{
    class AppModel
    {
        public static int HighScore
        {
            get => Preferences.Default.Get<int>("HighScore", 0);
            set => Preferences.Default.Set<int>("HighScore", value);
        }

    }
}
