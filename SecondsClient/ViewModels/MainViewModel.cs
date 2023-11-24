using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SecondsClient.Models;
using System.Text;


namespace SecondsClient.ViewModels
{
    partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? _ScoreLabelText;
        
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
        private string? _targetSecondsLabelText;
        [ObservableProperty]
        private bool _targetSecondsLabelIsVisible;

        [ObservableProperty]
        private bool _unitsLabelIsVisible;

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


        private Game _game; 

        public string UnitsLabelText => EasyModeSwitchIsToggled ? "Mississippis": "Seconds";

        public MainViewModel()
        {
            _game = new Game();
            SetInitialViewState();
           
        }

             
        private void SetInitialViewState()
        {
            ScoreLabelText = _game.Score.ToString();
            HighScoreLabelText = _game.HighScore.ToString();
            ReserveProgressProgressBar = 1;
            ReserveProgressBarIsVisible = false;         
            FinalScoreLabelText = String.Empty;  
            FinalScoreLabelIsVisible = false;
            PauseActivityIndicatorIsVisible = false;
            TargetSecondsLabelText = String.Empty;
            TargetSecondsLabelIsVisible = false;
            UnitsLabelIsVisible = false;
            PlayButtonIsEnabled = true;
            PlayButtonIsVisible = true;
            PlayButtonText = "Play";
            StopButtonIsEnabled = false;
            StopButtonIsVisible = false;
            StopButtonText = String.Empty;
        }



        [RelayCommand]
        private async Task Play()
        {
            _game.ResetGame();
            SetPlayInitalViewState();
            await Task.Delay(1500);

            _game.StartTime = DateTime.UtcNow;
            SetPlayPlayingState();

        }
        private void SetPlayInitalViewState()
        {
            PlayButtonIsEnabled = false;
            PlayButtonIsVisible = false;
            FinalScoreLabelIsVisible = false;
            TargetSecondsLabelIsVisible = false;
            ReserveProgressProgressBar = _game.Reserve/_game.InitalReserve;
            ReserveProgressBarIsVisible = true;
            StopButtonIsEnabled = true;
            UnitsLabelIsVisible = false;
            ScoreLabelText = _game.Score.ToString();
            HighScoreLabelText = _game.HighScore.ToString();
            PauseActivityIndicatorIsVisible = true;
        }
        private void SetPlayPlayingState()
        {
            TargetSecondsLabelIsVisible = true;
            PauseActivityIndicatorIsVisible = false;
            UnitsLabelIsVisible = true;
            StopButtonText = "Stop";
            StopButtonIsVisible = true;
            TargetSecondsLabelText = _game.TargetSeconds.ToString();
        }

  

        [RelayCommand]
        private async Task Stop()
        {
            _game.Stopped();
                                    

            if (_game.Reserve<0)
            {
                HighScoreLabelText = _game.HighScore.ToString();
                ReserveProgressProgressBar = 0;
                StopButtonIsEnabled = false;
                StopButtonIsVisible = false;
                ReserveProgressBarIsVisible = false;
                PlayButtonIsVisible = true;
                PlayButtonIsEnabled = true;
                TargetSecondsLabelIsVisible = false;
                UnitsLabelIsVisible = false;
                FinalScoreLabelIsVisible = true;
                FinalScoreLabelText = "Game Over \n Score is " + ScoreLabelText;

                StringBuilder stringBuilder = new();
                stringBuilder.Append("Play Again?");
                PlayButtonText = stringBuilder.ToString();
                return;
            }

             
            ReserveProgressProgressBar = _game.Reserve / _game.InitalReserve;
            ScoreLabelText = _game.Score.ToString();

            StopButtonIsEnabled = false;

            if (_game.Accuracy >= 0)
            {
                StopButtonText = Math.Round(_game.Accuracy, 2).ToString() + " over";
            }
            else
            {
                StopButtonText = Math.Abs(Math.Round(_game.Accuracy, 2)).ToString() + " under";
            }


            TargetSecondsLabelIsVisible = false;
            UnitsLabelIsVisible = false;
            PauseActivityIndicatorIsVisible = true;
            await Task.Delay(1500);
            PauseActivityIndicatorIsVisible = false;
            UnitsLabelIsVisible = true;
            TargetSecondsLabelIsVisible = true;
            StopButtonText = "Stop";
            StopButtonIsEnabled = true;
            _game.NewTargetSeconds();
            TargetSecondsLabelText = _game.TargetSeconds.ToString();
            _game.StartTime = DateTime.UtcNow;

        }

        private bool IsGameOver()
        {
            return _game.Reserve - Math.Abs(_game.Accuracy) < 0;
        }

    }


    }

