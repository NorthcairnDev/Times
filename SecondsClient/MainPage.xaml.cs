using SecondsClient.Models;
using SecondsClient.ViewModels;

namespace SecondsClient
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            GameHistory gameHistory = new();

            MainViewModelDelays delays = new() { InstructionVisisbleDurationMs=1000, GetReadyVisisbleDurationMs=750, GoVisisbleDurationMs  =  750, PauseBetweenRoundsDurationMs=1500};

            BindingContext = new MainViewModel(gameHistory, delays);

        }
    }
}
