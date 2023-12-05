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
        private Color? _scoreLabelBackgroundColor;

        [ObservableProperty]
        private Color? _scoreLabelTextColor;
    
        [ObservableProperty]
        private string? _highScoreLabelText;

        [ObservableProperty]
        private Color? _highScoreLabelBackgroundColor;
        
        [ObservableProperty]
        private Color? _highScoreLabelTextColor;


        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(UnitsLabelText))]
        private bool _easyModeSwitchIsToggled;


        [ObservableProperty]
        private double _reserveProgressProgressBar;
        [ObservableProperty]
        private bool _reserveProgressBarIsVisible;


        [ObservableProperty]
        private bool _startLabelIsVisible;
        [ObservableProperty]
        private FormattedString? _startLabelFormattedText;
   

        [ObservableProperty]
        private bool _pauseActivityIndicatorIsVisible;

        [ObservableProperty]
        private Color? _pauseActivityIndicatorColor;

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
        private FormattedString? _gameOverLabelFormattedText;
        [ObservableProperty]
        private bool _gameOverLabelIsVisible;


        [ObservableProperty]
        private bool _stopButtonIsEnabled;
        [ObservableProperty]
        private bool _stopButtonIsVisible;
        [ObservableProperty]
        private string? _stopButtonText;
        [ObservableProperty]
        private string? _stopButtonImageSource;
        [ObservableProperty]




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
        private async Task StartGameAsync() 
        {
            _game = new Game();
            AccuracyLabelText = "Get" +"\n" + "Ready";
            TransitionTo(GameState.RoundStarting);

            await Task.Delay(TimeSpan.FromSeconds(0.75));
            AccuracyLabelText = "GO!";

            await Task.Delay(TimeSpan.FromSeconds(0.75));
            StartRound();
         }

        private void StartRound()
        {
           
            _game.NewRound();
            TransitionTo(GameState.RoundActive);
        }

        [RelayCommand]
        private async Task StopAsync()
        {
            _game.RoundOver();
                                    
            if (_game.GameOver)
            {

                if (_game.Score > AppModel.HighScore)
                {
                    AppModel.HighScore = _game.Score;
                    _game.NewHighScore = true;
                    

                }
                ;

                TransitionTo(GameState.GameOver);
                return;
            }

            TransitionTo(GameState.RoundEnded);

            TransitionTo(GameState.RoundStarting);

            await Task.Delay(TimeSpan.FromSeconds(1.5));
            StartRound();

        }


        private void TransitionTo(GameState gameState)
        {

             switch (gameState)
            {
                case GameState.AppStarted:
                    ScoreLabelText = "0";
                    ScoreLabelBackgroundColor = Colors.Black;
                    ScoreLabelTextColor = Colors.White;
                    HighScoreLabelBackgroundColor = Colors.Black;
                    HighScoreLabelTextColor = Colors.White;
                    HighScoreLabelText = AppModel.HighScore.ToString();
                    ReserveProgressProgressBar = 0;
                    ReserveProgressBarIsVisible = true;
                    StartLabelIsVisible = true;
                    StartLabelFormattedText = AppStartFormattedText();
                    PauseActivityIndicatorIsVisible = false;
                    TargetInSecondsImageSource = String.Empty;
                    TargetInSecondsImageIsVisible = false;
                    UnitsLabelIsVisible = false;
                    PlayButtonIsEnabled = true;
                    PlayButtonIsVisible = true;
                    StopButtonIsEnabled = false;
                    StopButtonIsVisible = false;
                    StopButtonText = String.Empty;
                    GameOverLabelFormattedText= String.Empty;
                    GameOverLabelIsVisible = false;
                    break;
 
                case GameState.RoundStarting:
                    ScoreLabelText = _game.Score.ToString();
                    ScoreLabelBackgroundColor = Colors.Black;
                    ScoreLabelTextColor = Colors.White;
                    HighScoreLabelBackgroundColor = Colors.Black;
                    HighScoreLabelTextColor = Colors.White;
                    PlayButtonIsEnabled = false;
                    PlayButtonIsVisible = false;
                    TargetInSecondsImageIsVisible = false;
                    ReserveProgressProgressBar =_game.Reserve / Game.InitalReserve;
                    ReserveProgressBarIsVisible = true;
                    StartLabelIsVisible = false;
                    StartLabelFormattedText = String.Empty;
                    StopButtonIsEnabled = true;
                    UnitsLabelIsVisible = false;
                    AccuracyLabelIsVisible = true;
                    StopButtonImageSource = "pausebutton.svg";
                    HighScoreLabelText = AppModel.HighScore.ToString();
                    PauseActivityIndicatorColor = _game.Rounds.Count > 0 ? AccuracyColor() :Colors.White;
                    PauseActivityIndicatorIsVisible = true;
                    GameOverLabelFormattedText = String.Empty;
                    GameOverLabelIsVisible = false;
                    break;
                case GameState.RoundActive:
                    ScoreLabelText = _game.Score.ToString();
                    ScoreLabelBackgroundColor = Colors.Black;
                    HighScoreLabelBackgroundColor = Colors.Black;
                    HighScoreLabelTextColor = Colors.White;
                    ScoreLabelTextColor = Colors.White;
                    TargetInSecondsImageIsVisible = true;
                    StartLabelIsVisible = false;
                    StartLabelFormattedText = String.Empty;
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
                    ScoreLabelBackgroundColor = Colors.Black;
                    ScoreLabelTextColor = Colors.White;
                    HighScoreLabelBackgroundColor = Colors.Black;
                    HighScoreLabelTextColor = Colors.White;
                    ReserveProgressProgressBar = _game.Reserve / Game.InitalReserve;
                    StartLabelIsVisible = false;
                    StartLabelFormattedText = String.Empty;
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
                    GameOverLabelFormattedText = String.Empty;
                    GameOverLabelIsVisible = false;

                    break;
                case GameState.GameOver:
                    HighScoreLabelText = HighScoreLabelText = AppModel.HighScore.ToString();
                    HighScoreLabelBackgroundColor = _game.NewHighScore ? Color.FromArgb("05C405") : Colors.Black;
                    HighScoreLabelTextColor = _game.NewHighScore ? Colors.Black : Colors.White;
                    ScoreLabelBackgroundColor = Color.FromArgb("FF9900");
                    ScoreLabelTextColor = Colors.Black;
                    ReserveProgressProgressBar = 0;
                    StartLabelIsVisible = false;
                    StartLabelFormattedText = String.Empty;
                    StopButtonIsEnabled = false;
                    StopButtonIsVisible = false;
                    ReserveProgressBarIsVisible = false;
                    PlayButtonIsVisible = true;
                    PlayButtonIsEnabled = true;
                    TargetInSecondsImageIsVisible = false;
                    UnitsLabelIsVisible = false;
                    AccuracyLabelIsVisible = false;
                    GameOverLabelFormattedText = GameOverFormattedText();
                    GameOverLabelIsVisible = true;

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
                    case 1: return "onesecondfuschia.svg";
                    case 2: return "twosecondfuschia.svg";
                    case 3: return "threesecondfuschia.svg";
                    case 4: return "foursecondfuschia.svg";
                    case 5: return "fivesecondfuschia.svg";
                    default: return string.Empty;
                }
            }

            switch (_game.Round.TargetInSeconds.Value.Seconds)
            {
                case 1: return "onemississippifuschia.svg";
                case 2: return "twomississippifuschia.svg";
                case 3: return "threemississippifuschia.svg";
                case 4: return "fourmississippifuschia.svg";
                case 5: return "fivemississippifuschia.svg";
                default: return string.Empty;
            }


        }


        private FormattedString GameOverFormattedText()
        {
            FormattedString gameOverText = new FormattedString();

            gameOverText.Spans.Add(new Span { Text = "Game", TextColor = Colors.White, FontFamily= "RubikMonoOne-Regular", FontSize=48});
            gameOverText.Spans.Add(new Span { Text = Environment.NewLine });
            gameOverText.Spans.Add(new Span { Text = "Over", TextColor = Colors.White, FontFamily = "RubikMonoOne-Regular", FontSize = 48 });
            

            return gameOverText;

        }

        private FormattedString AppStartFormattedText()
        {
            FormattedString startText = new FormattedString();

            startText.Spans.Add(new Span { Text = "Count", TextColor = Colors.White, FontFamily = "RubikMonoOne-Regular", FontSize = 32 });
            startText.Spans.Add(new Span { Text = Environment.NewLine });
            startText.Spans.Add(new Span { Text = "the", TextColor = Colors.White, FontFamily = "RubikMonoOne-Regular", FontSize = 32 });
            startText.Spans.Add(new Span { Text = Environment.NewLine });
            startText.Spans.Add(new Span { Text = "seconds", TextColor = Colors.White, FontFamily = "RubikMonoOne-Regular", FontSize = 32 });


            return startText;

        }




    }


}

