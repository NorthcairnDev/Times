using CommunityToolkit.Maui;
using Plugin.MauiMTAdmob;

namespace SecondsClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            
            builder
                .UseMauiApp<App>()
                .UseMauiMTAdmob()
                .ConfigureFonts(fonts =>
                {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseMauiCommunityToolkit();


            return builder.Build();
        }
    }
}