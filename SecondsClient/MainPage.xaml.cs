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

            BindingContext = new MainViewModel(gameHistory);

        }
    }
}
