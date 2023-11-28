using SecondsClient.ViewModels;

namespace SecondsClient
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainViewModel();



        }

       
    }

}
