using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SecondsClient.Models;
using System.Text;


namespace SecondsClient.ViewModels
{
    partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? _scoreLabelText;
        
        [ObservableProperty]
        private string? _highScoreLabelText;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(UnitsLabelText))]
        private bool _easyModeSwitchIsToggled;


        [ObservableProperty]
        private double _reserveProgressProgressBar;
        [ObservableProperty]
        private bool _reserveProgressBarIsVisible;

        [ObservableProperty]
        private string? _finalScoreLabelText;
        [ObservableProperty]
        private bool _finalScoreLabelIsVisible;


        [ObservableProperty]
        private bool _pauseActivityIndicatorIsVisible;

        [ObservableProperty]
        private Color? _pauseActivityIndicatorColor;

        //[ObservableProperty]
        //private string? _roundTargetInSecondsLabelText;
        //[ObservableProperty]
        //private bool _roundTargetInSecondsLabelIsVisible;

        [ObservableProperty]
        private string? _targetInSecondsImageSource;
        [ObservableProperty]
        private bool _targetInSecondsImageIsVisible;


        [ObservableProperty]
        private bool _unitsLabelIsVisible;

        [ObservableProperty]
        private string? _accuracyLabelText;

        [ObservableProperty]
        private bool _accuracyLabelIsVisible;


        [ObservableProperty]
        private bool _playButtonIsEnabled;
        [ObservableProperty]
        private bool _playButtonIsVisible = true;

        [ObservableProperty]
        private string? _playButtonText;

        [ObservableProperty]
        private bool _stopButtonIsEnabled;
        [ObservableProperty]
        private bool _stopButtonIsVisible;
        [ObservableProperty]
        private string? _stopButtonText;
        [ObservableProperty]
        private string? _stopButtonImageSource;
        [ObservableProperty]
        private FormattedString? _reportLabelFormattedText;
        [ObservableProperty]
        private bool _reportLabelVisible;



        private Game? _game;
   
        public string UnitsLabelText => EasyModeSwitchIsToggled ? "Mississippis": "Seconds";

        private enum GameState
        {
            AppStarted,
            RoundStarting,
            RoundActive,
            RoundEnded,
            GameOver,
        }

        public MainViewModel()
        {

#if DEBUG
            AppModel.HighScore = 0;
#endif
            TransitionTo(GameState.AppStarted);


        }


        [RelayCommand]
        private async Task StartGame() 
        {
            _game = new Game();
            AccuracyLabelText = "Get" +"\n" + "Ready!";
            await StartRoundAsync();

        }

        private async Task StartRoundAsync()
        {
            TransitionTo(GameState.RoundStarting);
            await Task.Delay(1500);
            _game.NewRound();
            TransitionTo(GameState.RoundActive);
        }

        [RelayCommand]
        private async Task Stop()
        {
            _game.RoundOver();
                                    
            if (_game.GameOver)
            {
            
                if (_game.Score > AppModel.HighScore) AppModel.HighScore = _game.Score;
                HighScoreLabelText = AppModel.HighScore.ToString();
                TransitionTo(GameState.GameOver);
                return;
            }

            TransitionTo(GameState.RoundEnded);

            await StartRoundAsync();


        }


        private void TransitionTo(GameState gameState)
        {

            //private string? _ScoreLabelText;
            //private string? _highScoreLabelText;
            //[NotifyPropertyChangedFor(nameof(UnitsLabelText))]
            //private bool _easyModeSwitchIsToggled;
            //private double _reserveProgressProgressBar;
            //private bool _reserveProgressBarIsVisible;
            //private string? _finalScoreLabelText;
            //private bool _finalScoreLabelIsVisible;
            //private bool _pauseActivityIndicatorIsVisible;
            //private Color? _pauseActivityIndicatorColor;
            //private string? _roundTargetInSecondsLabelText;
            //private bool _roundTargetInSecondsLabelIsVisible;
            //private bool _unitsLabelIsVisible;
            //private string? _accuracyLabelText;
            //private bool _accuracyLabelIsVisible;
            //private bool _playButtonIsEnabled;
            //private bool _playButtonIsVisible = true;
            //private string? _playButtonText;
            //private bool _stopButtonIsEnabled;
            //private bool _stopButtonIsVisible;
            //private string? _stopButtonText;
            //private string? _stopButtonImageSource;
            //private FormattedString? _reportLabelFormattedText;
            //private bool _reportLabelVisible;



            switch (gameState)
            {
                case GameState.AppStarted:
                    ScoreLabelText = "0";
                    HighScoreLabelText = AppModel.HighScore.ToString();
                    ReserveProgressProgressBar = 0;
                    ReserveProgressBarIsVisible = false;
                    FinalScoreLabelText = String.Empty;
                    FinalScoreLabelIsVisible = false;
                    PauseActivityIndicatorIsVisible = false;
                    TargetInSecondsImageSource = String.Empty;
                    TargetInSecondsImageIsVisible = false;
                    UnitsLabelIsVisible = false;
                    PlayButtonIsEnabled = true;
                    PlayButtonIsVisible = true;
                    PlayButtonText = string.Empty;
                    StopButtonIsEnabled = false;
                    StopButtonIsVisible = false;
                    StopButtonText = String.Empty;
                    ReportLabelFormattedText = String.Empty;
                    ReportLabelVisible = false;
                    break;
 
                case GameState.RoundStarting:
                    ScoreLabelText = _game.Score.ToString();
                    PlayButtonIsEnabled = false;
                    PlayButtonIsVisible = false;
                    FinalScoreLabelIsVisible = false;
                    TargetInSecondsImageIsVisible = false;
                    ReserveProgressProgressBar =_game.Reserve / Game.InitalReserve;
                    ReserveProgressBarIsVisible = true;
                    StopButtonIsEnabled = true;
                    UnitsLabelIsVisible = false;
                    AccuracyLabelIsVisible = true;
                    StopButtonImageSource = "pausebutton.svg";
                    HighScoreLabelText = AppModel.HighScore.ToString();
                    PauseActivityIndicatorColor = _game.Rounds.Count > 0 ? AccuracyColor() :Colors.White;
                    PauseActivityIndicatorIsVisible = true;
                    ReportLabelVisible = false;
                    ReportLabelFormattedText = String.Empty;
                    break;
                case GameState.RoundActive:
                    ScoreLabelText = _game.Score.ToString();
                    TargetInSecondsImageIsVisible = true;
                    PauseActivityIndicatorIsVisible = false;
                    AccuracyLabelIsVisible = false;
                    UnitsLabelIsVisible = true;
                    StopButtonImageSource = "stopbutton.svg";
                    StopButtonIsVisible = true;
                    TargetInSecondsImageSource = TargetSecondsImage();
     //               RoundTargetInSecondsLabelText = _game.Round.TargetInSeconds.Value.Seconds.ToString();
                    break;
                case GameState.RoundEnded:
                    ScoreLabelText = _game.Score.ToString();
                    ReserveProgressProgressBar = _game.Reserve / Game.InitalReserve;
                    ScoreLabelText = _game.Score.ToString();

                    StopButtonIsEnabled = false;

                    var accuracy = (decimal)_game.Round.Accuracy.Value.TotalSeconds;



                    if (accuracy >= 0)
                    {
                        AccuracyLabelText = "+" + Math.Round(accuracy, 2).ToString();
                    }
                    else
                    {
                        AccuracyLabelText = "-" + Math.Abs(Math.Round(accuracy, 2)).ToString();
                    }


                    TargetInSecondsImageIsVisible = false;
                    UnitsLabelIsVisible = false;
                    AccuracyLabelIsVisible = true;
                    PauseActivityIndicatorIsVisible = true;
                    StopButtonImageSource = "pausebutton.svg";
                    PauseActivityIndicatorColor = AccuracyColor();

                    break;
                case GameState.GameOver:

                    ReserveProgressProgressBar = 0;
                    StopButtonIsEnabled = false;
                    StopButtonIsVisible = false;
                    ReserveProgressBarIsVisible = false;
                    PlayButtonIsVisible = true;
                    PlayButtonIsEnabled = true;
                    TargetInSecondsImageIsVisible = false;
                    UnitsLabelIsVisible = false;
                    AccuracyLabelIsVisible = false;
                    FinalScoreLabelIsVisible = true;
                    FinalScoreLabelText = "Game Over";
                    PlayButtonText = String.Empty;
                    break;
                default:
                    break;
            }


        }

        private Color AccuracyColor() 
        {

            switch (_game.Round.AccuracyLevel)
            {
                case Round.LevelsOfAccuracy.VeryClose
                : return Color.FromArgb("05C405");
                case Round.LevelsOfAccuracy.Close
                : return Color.FromArgb("FF9900");
                default
                : return Color.FromArgb("FE0000");
            }


        }


        private string TargetSecondsImage()
        {

            if (!EasyModeSwitchIsToggled)
            {

                switch (_game.Round.TargetInSeconds.Value.Seconds)
                {
                    case 1: return "onesecond.svg";
                    case 2: return "twosecond.svg";
                    case 3: return "threesecond.svg";
                    case 4: return "foursecond.svg";
                    case 5: return "fivesecond.svg";
                    default: return string.Empty;
                }
            }

            switch (_game.Round.TargetInSeconds.Value.Seconds)
            {
                case 1: return "onemississippi.svg";
                case 2: return "twomississippi.svg";
                case 3: return "threemississippi.svg";
                case 4: return "fourmississippi.svg";
                case 5: return "fivemississippi.svg";
                default: return string.Empty;
            }


        }

        //StringBuilder stringBuilder = new();
        //stringBuilder.Append("Play Again?");


        //ReportLabelFormattedText = String.Empty;

        //var report =new StringBuilder();
        //var formattedReport = new FormattedString();

        //foreach (Round round in _game.Rounds)
        //{
        //    report.Append((_game.Rounds.IndexOf(round)+1).ToString());
        //    report.Append(" -> ");
        //    report.Append(round.TargetInSeconds.Value.TotalSeconds.ToString());
        //    report.Append(" -> ");
        //    var reportaccuracy = (decimal)round.Accuracy.Value.TotalSeconds;
        //    report.Append(Math.Round(reportaccuracy, 2).ToString());
        //    report.AppendLine();

        //    switch (round.AccuracyLevel)
        //    {
        //        case Round.LevelsOfAccuracy.VeryClose
        //:
        //            formattedReport.Spans.Add(new Span { Text = report.ToString(), TextColor = Colors.Green });

        //            break;
        //        case Round.LevelsOfAccuracy.Close
        //:
        //            formattedReport.Spans.Add(new Span { Text = report.ToString(), TextColor = Colors.Green });

        //            break;
        //        default
        //:
        //            formattedReport.Spans.Add(new Span { Text = report.ToString(), TextColor = Colors.Red });

        //            break;


        //    }

        //    report.Clear();


        //    //FormattedString formattedString = new FormattedString();
        //    //formattedString.Spans.Add(new Span { Text = "Red bold, ", TextColor = Colors.Red, FontAttributes = FontAttributes.Bold });

        //    //Span span = new Span { Text = "default, " };
        //    //span.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(async () => await DisplayAlert("Tapped", "This is a tapped Span.", "OK")) });
        //    //formattedString.Spans.Add(span);
        //    //formattedString.Spans.Add(new Span { Text = "italic small.", FontAttributes = FontAttributes.Italic, FontSize = 14 });

        //    //Label label = new Label { FormattedText = formattedString };



        //}
        //ReportLabelFormattedText = formattedReport;
        //ReportLabelVisible = true;

    }


}

