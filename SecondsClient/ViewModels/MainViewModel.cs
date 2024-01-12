using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SecondsClient.Models;
using SkiaSharp.Extended.UI.Controls;
using SkiaSharp.Extended.UI.Controls.Converters;


namespace SecondsClient.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        #region Observable Properties


        //Score Grid
        [ObservableProperty]
        private string _scoreLabelText = "0";
        [ObservableProperty]
        private Color _scoreLabelTextColor = Colors.White;
        [ObservableProperty]
        private Color _scoreLabelBackgroundColor = Colors.Black;
        [ObservableProperty]
        private string _highScoreLabelText; //set in constructor;
        [ObservableProperty]
        private Color _highScoreLabelTextColor = Colors.White;
        [ObservableProperty]
        private Color _highScoreLabelBackgroundColor = Colors.Black;

        [ObservableProperty]
        private bool _easyModeSwitchIsToggled = false;

        //Reserve Bar
        [ObservableProperty]
        private double _reserveProgressProgressBar = 0;


        //Start Page
        [ObservableProperty]
        private bool _startPageLabelIsVisible = true;
        [ObservableProperty]
        private FormattedString? _startPageLabelFormattedText = MainViewModel.StartPageFormattedText();

        //Get Ready Label
        [ObservableProperty]
        private string _getReadyLabelText = string.Empty;
        [ObservableProperty]
        private int _getReadyLabelFontSize = 28;
        [ObservableProperty]
        private bool _getReadyLabelIsVisible = false;

        //Pause Between Rounds Indicator
        [ObservableProperty]
        private SKFileLottieImageSource? _pauseAnimationSource;
        [ObservableProperty]
        private bool _pauseAnimationIsVisible = false;
    

        //Target Seconds
        [ObservableProperty]
        private string _targetSecondsImageSource = string.Empty;
        [ObservableProperty]
        private bool _targetSecondsImageIsVisible = false;

        //Accuracy Result
        [ObservableProperty]
        private string _accuracyLabelText = string.Empty;
        [ObservableProperty]
        private Color _accuracyLabelTextColor = Colors.White;
        [ObservableProperty]
        private bool _accuracyLabelIsVisible = false;

        //Game Over Page
        [ObservableProperty]
        private FormattedString? _gameOverLabelFormattedText= MainViewModel.GameOverFormattedText();
        [ObservableProperty]
        private bool _gameOverLabelIsVisible = false;


        //Play Button
        [ObservableProperty]
        private bool _playButtonIsEnabled = true;
        [ObservableProperty]
        private bool _playButtonIsVisible = true;

        //Stop Button
        [ObservableProperty]
        private bool _stopButtonIsEnabled =false;
        [ObservableProperty]
        private bool _stopButtonIsVisible = false;
        [ObservableProperty]
        private string _stopButtonImageSource=string.Empty;
        #endregion Observable Properties

        #region Fields
        //Model of the Game
        private Game _game = new();
        private IGameHistory _gameHistory;
        private MainViewModelDelays _delays;
        private readonly SKFileLottieImageSource _whiteactivityanimation;
        private readonly SKFileLottieImageSource _greenactivityanimation;
        private readonly SKFileLottieImageSource _amberactivityanimation;
        private readonly SKFileLottieImageSource _redactivityanimation;



        #endregion

        #region Enumerations
        private enum GameState
        {
            GameStarting,
            RoundActive,
            RoundEnded,
            GameOver
        }

        #endregion

        #region Constructor

        public MainViewModel(IGameHistory gameHistory, MainViewModelDelays delays)
        {
            _gameHistory = gameHistory;
            _delays = delays;


#if DEBUG
            _gameHistory.HighScore = 0;
#endif

            _highScoreLabelText = _gameHistory.HighScore.ToString();

            _whiteactivityanimation = (SKFileLottieImageSource)new SKLottieImageSourceConverter().ConvertFromString("whiteactivityanimation.json");
            _greenactivityanimation = (SKFileLottieImageSource)new SKLottieImageSourceConverter().ConvertFromString("greenactivityanimation.json");
            _amberactivityanimation = (SKFileLottieImageSource)new SKLottieImageSourceConverter().ConvertFromString("amberactivityanimation.json");
            _redactivityanimation = (SKFileLottieImageSource)new SKLottieImageSourceConverter().ConvertFromString("redactivityanimation.json");

        }


        #endregion

        #region RelayCommands
        [RelayCommand]
        private async Task StartGameAsync()
        {
            _game = new();

            await GameStartSequence();

            StartRound();
        }


        [RelayCommand]
        private async Task StopAsync()
        {

            _game.Round.StopStopwatch();
            _game.RoundOver();

            //Game over ?
            if (_game.IsGameOver)
            {
                GameOver();
                return;
            }

            //Game continuning for another round
            TransitionTo(GameState.RoundEnded);
            await Task.Delay(_delays.PauseBetweenRoundsDurationMs);
            StartRound();
            return;

        }


        #endregion

        #region Private Static Methods
        private static FormattedString GameOverFormattedText()
        {
            if (DeviceInfo.Idiom == DeviceIdiom.Tablet || DeviceInfo.Idiom == DeviceIdiom.Desktop)
            {
                return GameOverFormattedTextTabletOrDesktop();
            }

            return GameOverFormattedTextDefault();
        }

        private static FormattedString GameOverFormattedTextDefault()
        {
            FormattedString gameOverText = new();

            gameOverText.Spans.Add(new Span { Text = "Game", TextColor = Colors.White, FontFamily = "RubikMonoOneRegular", FontSize = 48 });
            gameOverText.Spans.Add(new Span { Text = Environment.NewLine });
            gameOverText.Spans.Add(new Span { Text = "Over", TextColor = Colors.White, FontFamily = "RubikMonoOneRegular", FontSize = 48 });

            return gameOverText;
        }

        private static FormattedString GameOverFormattedTextTabletOrDesktop()
        {
            FormattedString gameOverText = new();

            gameOverText.Spans.Add(new Span { Text = "Game", TextColor = Colors.White, FontFamily = "RubikMonoOneRegular", FontSize = 96 });
            gameOverText.Spans.Add(new Span { Text = Environment.NewLine });
            gameOverText.Spans.Add(new Span { Text = "Over", TextColor = Colors.White, FontFamily = "RubikMonoOneRegular", FontSize = 96 });

            return gameOverText;
        }


        private static FormattedString StartPageFormattedText()
        {
            if (DeviceInfo.Idiom == DeviceIdiom.Tablet || DeviceInfo.Idiom == DeviceIdiom.Desktop)
            {
                return StartPageFormattedTextTabletOrDesktop();
            }

            return StartPageFormattedTextDefault();
        }

        private static FormattedString StartPageFormattedTextTabletOrDesktop()
        {
            FormattedString startText = new();

            startText.Spans.Add(new Span { Text = "On", TextColor = Color.FromArgb("FF00FF"), FontFamily = "RubikMonoOneRegular", FontSize =76 });
            startText.Spans.Add(new Span { Text = " ", TextColor = Color.FromArgb("FF00FF"), FontFamily = "RubikMonoOneRegular", FontSize = 28 });
            startText.Spans.Add(new Span { Text = "The", TextColor = Color.FromArgb("FF00FF"), FontFamily = "RubikMonoOneRegular", FontSize = 76 });
            startText.Spans.Add(new Span { Text = " ", TextColor = Color.FromArgb("FF00FF"), FontFamily = "RubikMonoOneRegular", FontSize = 28 });
            startText.Spans.Add(new Span { Text = "Dot", TextColor = Color.FromArgb("FF00FF"), FontFamily = "RubikMonoOneRegular", FontSize = 76 });
            startText.Spans.Add(new Span { Text = Environment.NewLine });
            startText.Spans.Add(new Span { Text = Environment.NewLine });
            startText.Spans.Add(new Span { Text = "Feel the seconds", TextColor = Colors.White, FontFamily = "RubikRegular", FontSize = 56});

            //FontAttributes = FontAttributes.Italic

            return startText;
        }

        private static FormattedString StartPageFormattedTextDefault()
        {
            FormattedString startText = new();

            startText.Spans.Add(new Span { Text = "On", TextColor = Color.FromArgb("FF00FF"), FontFamily = "RubikMonoOneRegular", FontSize = 38 });
            startText.Spans.Add(new Span { Text = " ", TextColor = Color.FromArgb("FF00FF"), FontFamily = "RubikMonoOneRegular", FontSize = 14 });
            startText.Spans.Add(new Span { Text = "The", TextColor = Color.FromArgb("FF00FF"), FontFamily = "RubikMonoOneRegular", FontSize = 38 });
            startText.Spans.Add(new Span { Text = " ", TextColor = Color.FromArgb("FF00FF"), FontFamily = "RubikMonoOneRegular", FontSize = 14 });
            startText.Spans.Add(new Span { Text = "Dot", TextColor = Color.FromArgb("FF00FF"), FontFamily = "RubikMonoOneRegular", FontSize = 38 });
            startText.Spans.Add(new Span { Text = Environment.NewLine });
            startText.Spans.Add(new Span { Text = Environment.NewLine });
            startText.Spans.Add(new Span { Text = "Feel the seconds", TextColor = Colors.White, FontFamily = "RubikRegular", FontSize = 28 });

            //FontAttributes = FontAttributes.Italic

            return startText;
        }

        #endregion

        #region Private Instance Methods

        private async Task GameStartSequence()
        {
            
            if(DeviceInfo.Idiom == DeviceIdiom.Tablet || DeviceInfo.Idiom == DeviceIdiom.Desktop)
            {
                GetReadyLabelFontSize = 56;

            }
            else
            {
                GetReadyLabelFontSize = 28;
            }
            
            GetReadyLabelText = "Get" + Environment.NewLine + "Ready";
            TransitionTo(GameState.GameStarting);
            await Task.Delay(_delays.GetReadyVisisbleDurationMs);


            if (DeviceInfo.Idiom == DeviceIdiom.Tablet || DeviceInfo.Idiom == DeviceIdiom.Desktop)
            {
                GetReadyLabelFontSize = 96;

            }
            else
            {
                GetReadyLabelFontSize = 48;
            }
            GetReadyLabelText = "GO!";
            await Task.Delay(_delays.GoVisisbleDurationMs);
            GetReadyLabelIsVisible = false;
        }


        public void StartRound() //public for testing
        {
            _game.NewRound();
            TransitionTo(GameState.RoundActive);
            _game.Round.StartStopwatch();
        }

        private void GameOver()
        {
            if (_game.Score > _gameHistory.HighScore)
            {
                _gameHistory.HighScore = _game.Score;
                _game.NewHighScore = true;
            }

            TransitionTo(GameState.GameOver);
        }

        private void TransitionTo(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.GameStarting:
                    
                    ScoreLabelText = "0";
                    ScoreLabelTextColor = Colors.White;
                    ScoreLabelBackgroundColor = Colors.Black;
                    HighScoreLabelText = _gameHistory.HighScore.ToString();
                    HighScoreLabelTextColor = Colors.White;
                    HighScoreLabelBackgroundColor = Colors.Black;
                    ReserveProgressProgressBar = 1;
                    StartPageLabelIsVisible = false;
                    GetReadyLabelIsVisible = true;
                    PauseAnimationSource = _whiteactivityanimation;
                    PauseAnimationIsVisible = true;
                    TargetSecondsImageSource = string.Empty;
                    TargetSecondsImageIsVisible = false;
                    AccuracyLabelText = string.Empty;
                    AccuracyLabelTextColor = Colors.White;
                    AccuracyLabelIsVisible = false;
                    GameOverLabelIsVisible = false;
                    PlayButtonIsEnabled = false;
                    PlayButtonIsVisible = false;
                    StopButtonIsEnabled = false;
                    StopButtonIsVisible = true;
                    StopButtonImageSource = "pausebutton.png";

                    break;

                case GameState.RoundActive:
                    ScoreLabelText = _game.Score.ToString();
                    ScoreLabelTextColor = Colors.White;
                    ScoreLabelBackgroundColor = Colors.Black;
                    HighScoreLabelText = _gameHistory.HighScore.ToString();
                    HighScoreLabelTextColor = Colors.White;
                    HighScoreLabelBackgroundColor = Colors.Black;
                    ReserveProgressProgressBar = _game.Reserve / Game.InitalReserve;
                    StartPageLabelIsVisible = false;
                    GetReadyLabelText = string.Empty;
                    GetReadyLabelFontSize = 28;
                    GetReadyLabelIsVisible = false;
                    PauseAnimationSource = null;
                    PauseAnimationIsVisible = false;
                    TargetSecondsImageSource = TargetSecondsImage();
                    TargetSecondsImageIsVisible = true;
                    AccuracyLabelText = string.Empty;
                    AccuracyLabelTextColor = Colors.White;
                    AccuracyLabelIsVisible = false;
                    GameOverLabelIsVisible = false;
                    PlayButtonIsEnabled = false;
                    PlayButtonIsVisible = false;
                    StopButtonIsEnabled = true;
                    StopButtonIsVisible = true;
                    StopButtonImageSource = "stopbutton.png";

                    break;

                case GameState.RoundEnded:
                    ScoreLabelText = _game.Score.ToString();
                    ScoreLabelTextColor = Colors.White;
                    ScoreLabelBackgroundColor = Colors.Black;
                    HighScoreLabelText = _gameHistory.HighScore.ToString();
                    HighScoreLabelTextColor = Colors.White;
                    HighScoreLabelBackgroundColor = Colors.Black;
                    ReserveProgressProgressBar = _game.Reserve / Game.InitalReserve;
                    StartPageLabelIsVisible = false;
                    GetReadyLabelText = string.Empty;
                    GetReadyLabelFontSize = 28;
                    GetReadyLabelIsVisible = false;
                    TargetSecondsImageSource = string.Empty;
                    TargetSecondsImageIsVisible = false;

                    decimal accuracyRounded = Math.Round((decimal)_game.Round.Accuracy.TotalSeconds, 2);
                    decimal accuracyRoundedUnsigned = Math.Abs(accuracyRounded);

                    switch (accuracyRounded)
                    {
                        case > (decimal)0.00:
                            AccuracyLabelText = accuracyRoundedUnsigned.ToString() + Environment.NewLine + "Over";
                            break;
                        case < (decimal)0.00:
                            AccuracyLabelText = accuracyRoundedUnsigned.ToString() + Environment.NewLine + "Under";
                            break;

                        case (decimal)0.00:
                            AccuracyLabelText = accuracyRoundedUnsigned.ToString() + Environment.NewLine + "Perfect";
                            break;
                    }

                    PauseAnimationSource = PauseAnimationSourceForAccuracy();
                    PauseAnimationIsVisible = true;
                    AccuracyLabelTextColor = AccuracyColor();
                    AccuracyLabelIsVisible = true;

                    GameOverLabelIsVisible = false;
                    PlayButtonIsEnabled = false;
                    PlayButtonIsVisible = false;
                    StopButtonIsEnabled = false;
                    StopButtonIsVisible = true;
                    StopButtonImageSource = "pausebutton.png";
                    break;

                case GameState.GameOver:
                    ScoreLabelText = _game.Score.ToString();
                    ScoreLabelTextColor = Colors.Black; 
                    ScoreLabelBackgroundColor = Color.FromArgb("FF9900");
                    HighScoreLabelText = _gameHistory.HighScore.ToString();
                    HighScoreLabelTextColor = _game.NewHighScore ? Colors.Black : Colors.White;
                    HighScoreLabelBackgroundColor = _game.NewHighScore ? Color.FromArgb("05C405") : Colors.Black;
                    ReserveProgressProgressBar = 0;
                    StartPageLabelIsVisible = false;
                    GetReadyLabelText = string.Empty;
                    GetReadyLabelFontSize = 28;
                    GetReadyLabelIsVisible = false;
                    PauseAnimationSource = null;
                    PauseAnimationIsVisible = false;
                    TargetSecondsImageSource = string.Empty;
                    TargetSecondsImageIsVisible = false;
                    AccuracyLabelText = string.Empty;
                    AccuracyLabelTextColor = Colors.White;
                    AccuracyLabelIsVisible = false;
                    GameOverLabelIsVisible = true;
                    PlayButtonIsEnabled = true;
                    PlayButtonIsVisible = true;
                    StopButtonIsEnabled = false;
                    StopButtonIsVisible = false;
                    StopButtonImageSource = string.Empty;

                    break;
                default:
                    break;
            }
        }

        private Color AccuracyColor()
        {

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return _game.Round.AccuracyLevel switch
            {
                Round.LevelsOfAccuracy.VeryClose => Color.FromArgb("05C405"),
                Round.LevelsOfAccuracy.Close => Color.FromArgb("FF9900"),
                _ => Color.FromArgb("FE0000"),
            };
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }


        private SKFileLottieImageSource PauseAnimationSourceForAccuracy()
        {

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return _game.Round.AccuracyLevel switch
            {
                Round.LevelsOfAccuracy.VeryClose => _greenactivityanimation,
                Round.LevelsOfAccuracy.Close => _amberactivityanimation,
                _ => _redactivityanimation
            }; 
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }



        private string TargetSecondsImage()
        {

            if (!EasyModeSwitchIsToggled)
            {

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                return _game.Round.TargetInSeconds.Seconds switch
                {
                    1 => "onesecondfuschia.png",
                    2 => "twosecondfuschia.png",
                    3 => "threesecondfuschia.png",
                    4 => "foursecondfuschia.png",
                    5 => "fivesecondfuschia.png",
                    _ => string.Empty,
                };
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return _game.Round.TargetInSeconds.Seconds switch
            {
                1 => "onemississippifuschia.png",
                2 => "twomississippifuschia.png",
                3 => "threemississippifuschia.png",
                4 => "fourmississippifuschia.png",
                5 => "fivemississippifuschia.png",
                _ => string.Empty,
            };
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        #endregion

    }

}

