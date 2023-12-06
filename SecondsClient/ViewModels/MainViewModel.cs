using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SecondsClient.Models;



namespace SecondsClient.ViewModels
{
    partial class MainViewModel : ObservableObject
    {
        
        //Score Grid
        [ObservableProperty]
        private string _scoreLabelText = "0";
        [ObservableProperty]
        private Color _scoreLabelBackgroundColor = Colors.Black;
        [ObservableProperty]
        private Color _scoreLabelTextColor = Colors.White;
        [ObservableProperty]
        private string _highScoreLabelText = AppModel.HighScore.ToString();
        [ObservableProperty]
        private Color _highScoreLabelBackgroundColor = Colors.Black;
        [ObservableProperty]
        private Color _highScoreLabelTextColor = Colors.White;
        [ObservableProperty]
        private bool _easyModeSwitchIsToggled = false;

        //Reserve Bar
        [ObservableProperty]
        private double _reserveProgressProgressBar  = 0;

        //Start Page
        [ObservableProperty]
        private bool _startPageLabelIsVisible = true;
        [ObservableProperty]
        private FormattedString? _startPageLabelFormattedText = MainViewModel.StartPageFormattedText();

        //Get Ready Label
        [ObservableProperty]
        private string _getReadyLabelText = string.Empty;
        [ObservableProperty]
        private bool _getReadyLabelIsVisible = false;



        //Pause Between Rounds Indicator
        [ObservableProperty]
        private bool _pauseActivityIndicatorIsVisible = false;
        [ObservableProperty]
        private Color _pauseActivityIndicatorColor = Colors.White;

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
        private FormattedString? _gameOverLabelFormattedText = MainViewModel.GameOverFormattedText();
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
        
        //Model of the Game
        private Game _game = new();
   
     

        private enum GameState
        {
            GameStarting,
            RoundActive,
            RoundEnded,
            GameOver,
        }

        public MainViewModel()
        {

#if DEBUG
            AppModel.HighScore = 0;
#endif
 
        }


        [RelayCommand]
        private async Task StartGameAsync() 
        {
            _game = new ();

            
            //Get Ready
            GetReadyLabelText = "Get" + "\n" + "Ready";
            TransitionTo(GameState.GameStarting);       
            await Task.Delay(TimeSpan.FromSeconds(0.75));
            GetReadyLabelText = "GO!";
            await Task.Delay(TimeSpan.FromSeconds(0.75));
            GetReadyLabelIsVisible = false;


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
                };

                TransitionTo(GameState.GameOver);
                return;
            }

            TransitionTo(GameState.RoundEnded);

            await Task.Delay(TimeSpan.FromSeconds(1.5));
            StartRound();

        }


        private void TransitionTo(GameState gameState)
        {
 
           // ScoreLabelText = _game.Score.ToString();

     
            


            switch (gameState)
            {
                case GameState.GameStarting:

                    ScoreLabelTextColor = Colors.White;
                    ScoreLabelBackgroundColor = Colors.Black;

                    HighScoreLabelTextColor = Colors.White;
                    HighScoreLabelBackgroundColor = Colors.Black;

                    ReserveProgressProgressBar = _game.Reserve / Game.InitalReserve;

                    StartPageLabelIsVisible = false;
                    GameOverLabelIsVisible = false;

                    PauseActivityIndicatorColor = Colors.White;
                    PauseActivityIndicatorIsVisible = true;
                    
                    GetReadyLabelIsVisible = true;

                    PlayButtonIsVisible = false;
                    PlayButtonIsEnabled = false;

                    break;

                case GameState.RoundActive:
                    PauseActivityIndicatorIsVisible = false;
                    AccuracyLabelIsVisible = false;

                    TargetSecondsImageIsVisible = true;
                    TargetSecondsImageSource = TargetSecondsImage();

                    StopButtonImageSource = "stopbutton.svg";
                    StopButtonIsVisible = true;
                    StopButtonIsEnabled = true;

 
                    break;
                case GameState.RoundEnded:

                    TargetSecondsImageIsVisible = false;


                    ReserveProgressProgressBar = _game.Reserve / Game.InitalReserve;

                    ScoreLabelText = _game.Score.ToString();

                    decimal accuracy = Math.Round((decimal)_game.Round.Accuracy.TotalSeconds, 2);

                    switch (accuracy)
                    {
                        case > (decimal)0.00:
                            AccuracyLabelText = accuracy.ToString() + Environment.NewLine + "Over";
                            break;
                        case <  (decimal)0.00:
                            AccuracyLabelText = accuracy.ToString() + Environment.NewLine + "Under";
                            break;

                        case (decimal)0.00:
                            AccuracyLabelText = accuracy.ToString() + Environment.NewLine + "Perfect";
                            break;
                    }


                    AccuracyLabelTextColor = AccuracyColor();
                    AccuracyLabelIsVisible = true;

                    PauseActivityIndicatorColor = AccuracyColor();
                    PauseActivityIndicatorIsVisible = true;
                    StopButtonIsEnabled = false;
                    StopButtonImageSource = "pausebutton.svg";
 

                    break;
                case GameState.GameOver:
                    HighScoreLabelText = HighScoreLabelText = AppModel.HighScore.ToString();
                    HighScoreLabelBackgroundColor = _game.NewHighScore ? Color.FromArgb("05C405") : Colors.Black;
                    HighScoreLabelTextColor = _game.NewHighScore ? Colors.Black : Colors.White;
                    ScoreLabelBackgroundColor = Color.FromArgb("FF9900");
                    ScoreLabelTextColor = Colors.Black;
                    ReserveProgressProgressBar = 0;
                    StopButtonIsVisible = false;
                    StopButtonIsEnabled = false;
                    PlayButtonIsVisible = true;
                    PlayButtonIsEnabled = true;
                    TargetSecondsImageIsVisible = false;
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

                return _game.Round.TargetInSeconds.Seconds switch
                {
                    1 => "onesecondfuschia.svg",
                    2 => "twosecondfuschia.svg",
                    3 => "threesecondfuschia.svg",
                    4 => "foursecondfuschia.svg",
                    5 => "fivesecondfuschia.svg",
                    _ => string.Empty,
                };
            }

            return _game.Round.TargetInSeconds.Seconds switch
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

        private static FormattedString StartPageFormattedText()
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

