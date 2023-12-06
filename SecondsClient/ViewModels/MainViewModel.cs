using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SecondsClient.Models;
using System.Text;


namespace SecondsClient.ViewModels
{
    partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? _scoreLabelText = String.Empty;

        [ObservableProperty]
        private Color? _scoreLabelBackgroundColor;

        [ObservableProperty]
        private Color? _scoreLabelTextColor;
    
        [ObservableProperty]
        private string _highScoreLabelText = String.Empty;

        [ObservableProperty]
        private Color? _highScoreLabelBackgroundColor;
        
        [ObservableProperty]
        private Color? _highScoreLabelTextColor;


        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(UnitsLabelText))]
        private bool _easyModeSwitchIsToggled = false;


        [ObservableProperty]
        private double _reserveProgressProgressBar  = 0;
 

        [ObservableProperty]
        private bool _startLabelIsVisible;

        [ObservableProperty]
        private FormattedString? _startLabelFormattedText;
   

        [ObservableProperty]
        private bool _pauseActivityIndicatorIsVisible;

        [ObservableProperty]
        private Color _pauseActivityIndicatorColor = Colors.White;

        [ObservableProperty]
        private string? _targetSecondsImageSource;
        [ObservableProperty]
        private bool _targetSecondsImageIsVisible = false;


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
        

        private Game _game = new();
   
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
                    StartLabelIsVisible = true;
                    StartLabelFormattedText = AppStartFormattedText();
                    PauseActivityIndicatorIsVisible = false;
                    TargetSecondsImageSource = String.Empty;
                    TargetSecondsImageIsVisible = false;
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
                    TargetSecondsImageIsVisible = false;
                    ReserveProgressProgressBar =_game.Reserve / Game.InitalReserve;
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
                    TargetSecondsImageIsVisible = true;
                    StartLabelIsVisible = false;
                    StartLabelFormattedText = String.Empty;
                    PauseActivityIndicatorIsVisible = false;
                    AccuracyLabelIsVisible = false;
                    UnitsLabelIsVisible = true;
                    StopButtonImageSource = "stopbutton.svg";
                    StopButtonIsVisible = true;
                    TargetSecondsImageSource = TargetSecondsImage();
 
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

                    var accuracy = (decimal)_game.Round.Accuracy.TotalSeconds;



                    if (accuracy >= 0)
                    {
                        AccuracyLabelText = Math.Round(accuracy, 2).ToString() + Environment.NewLine + "Over";
                    }
                    else
                    {
                        AccuracyLabelText = Math.Abs(Math.Round(accuracy, 2)).ToString() + Environment.NewLine + "Under";
                    }


                    TargetSecondsImageIsVisible = false;
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
                    PlayButtonIsVisible = true;
                    PlayButtonIsEnabled = true;
                    TargetSecondsImageIsVisible = false;
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

            return _game.Round.AccuracyLevel switch
            {
                Round.LevelsOfAccuracy.VeryClose => Color.FromArgb("05C405"),
                Round.LevelsOfAccuracy.Close => Color.FromArgb("FF9900"),
                _ => Color.FromArgb("FE0000"),
            };
        }


        private string TargetSecondsImage()
        {

            if (!EasyModeSwitchIsToggled)
            {

                return (object)_game.Round.TargetInSeconds.Seconds switch
                {
                    1 => "onesecondfuschia.svg",
                    2 => "twosecondfuschia.svg",
                    3 => "threesecondfuschia.svg",
                    4 => "foursecondfuschia.svg",
                    5 => "fivesecondfuschia.svg",
                    _ => string.Empty,
                };
            }

            return (object)_game.Round.TargetInSeconds.Seconds switch
            {
                1 => "onemississippifuschia.svg",
                2 => "twomississippifuschia.svg",
                3 => "threemississippifuschia.svg",
                4 => "fourmississippifuschia.svg",
                5 => "fivemississippifuschia.svg",
                _ => string.Empty,
            };
        }


        private static FormattedString GameOverFormattedText()
        {
            FormattedString gameOverText = new ();

            gameOverText.Spans.Add(new Span { Text = "Game", TextColor = Colors.White, FontFamily= "RubikMonoOne-Regular", FontSize=48});
            gameOverText.Spans.Add(new Span { Text = Environment.NewLine });
            gameOverText.Spans.Add(new Span { Text = "Over", TextColor = Colors.White, FontFamily = "RubikMonoOne-Regular", FontSize = 48 });
            

            return gameOverText;

        }

        private static FormattedString AppStartFormattedText()
        {
            FormattedString startText = new ();

            startText.Spans.Add(new Span { Text = "Count", TextColor = Colors.White, FontFamily = "RubikMonoOne-Regular", FontSize = 32 });
            startText.Spans.Add(new Span { Text = Environment.NewLine });
            startText.Spans.Add(new Span { Text = "the", TextColor = Colors.White, FontFamily = "RubikMonoOne-Regular", FontSize = 32 });
            startText.Spans.Add(new Span { Text = Environment.NewLine });
            startText.Spans.Add(new Span { Text = "seconds", TextColor = Colors.White, FontFamily = "RubikMonoOne-Regular", FontSize = 32 });


            return startText;

        }




    }


}

