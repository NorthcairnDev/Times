using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace SecondsClient.ViewModels
{
    partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _ScoreLabelText;
        
        [ObservableProperty]
        private int _highScoreLabelText;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(UnitsLabelText))]
        private bool _easyModeSwitchIsToggled;


        [ObservableProperty]
        private decimal _reserveProgressProgressBar;
        [ObservableProperty]
        private bool _reserveProgressBarIsVisible;

        [ObservableProperty]
        private string? _finalScoreLabelText;
        [ObservableProperty]
        private bool _finalScoreLabelIsVisible;


        [ObservableProperty]
        private bool _pauseActivityIndicatorIsVisible;

        [ObservableProperty]
        private int _targetSecondsLabelText;
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


        private decimal _reserve = 5;
        private decimal _accuracy = 0;
        private DateTime _startTime;
        private DateTime _endTime;
        private Decimal _closenessTarget;

        public string UnitsLabelText => EasyModeSwitchIsToggled ? "Mississippis": "Seconds";

        public MainViewModel()
        {
            SetInitialState();
           
        }

        private void SetInitialState()
        {
           ScoreLabelText = 0;
           HighScoreLabelText = Preferences.Default.Get<int>("HighScore", 0);
           ReserveProgressProgressBar = 1;
           ReserveProgressBarIsVisible = false;         
           FinalScoreLabelText = String.Empty;  
           FinalScoreLabelIsVisible = false;
           PauseActivityIndicatorIsVisible = false;
           TargetSecondsLabelText = 0;
           TargetSecondsLabelIsVisible = false;
           UnitsLabelIsVisible = false;
           PlayButtonIsEnabled = true;
           PlayButtonIsVisible = true;
           PlayButtonText = "Play";
           StopButtonIsEnabled = false;
           StopButtonIsVisible = false;
           StopButtonText = String.Empty;
        }


        private void SetPlayInitalState()
        {
            PlayButtonIsEnabled = false;
            PlayButtonIsVisible = false;
            FinalScoreLabelIsVisible = false;
            ReserveProgressProgressBar = 1;
            ReserveProgressBarIsVisible = true;
            StopButtonIsEnabled = true;
            UnitsLabelIsVisible = false;
            ScoreLabelText = 0;
            TargetSecondsLabelText = 0;
            HighScoreLabelText = Preferences.Default.Get<int>("HighScore", 0);
            _reserve = 5;
            _accuracy = 0;
           
        }

        private  async Task SetPlayGetReadyState()
        {
            PauseActivityIndicatorIsVisible = true;
            await Task.Delay(1500);

        }

        private void SetPlayPlayingState()
        {
            TargetSecondsLabelIsVisible = true;
            PauseActivityIndicatorIsVisible = false;
            UnitsLabelIsVisible = true;
            StopButtonText = "Stop";
            StopButtonIsVisible = true;
            _startTime = DateTime.UtcNow;
            TargetSecondsLabelText = new Random().Next(1, 6);

        }

   


        [RelayCommand]
        private async Task Play()
        {
           SetPlayInitalState();
           await SetPlayGetReadyState();
            SetPlayPlayingState();


        }

        [RelayCommand]
        private async Task Stop()
        {
         
            _endTime = DateTime.UtcNow;


            _accuracy = ((decimal)(_endTime - _startTime).TotalSeconds) - TargetSecondsLabelText;
            _closenessTarget = Math.Abs(TargetSecondsLabelText - Math.Round((decimal)(_endTime - _startTime).TotalSeconds, 2));
            

            if   ((_reserve - _closenessTarget) < 0)
            {
                _reserve = 0;
                ReserveProgressProgressBar = 0;
                StopButtonIsEnabled = false;
                StopButtonIsVisible = false;
                ReserveProgressBarIsVisible = false;
                PlayButtonIsVisible = true;
                PlayButtonIsEnabled = true;
                TargetSecondsLabelIsVisible = false;
                UnitsLabelIsVisible = false;

                if  (Preferences.Default.Get<int>("HighScore", 0) < ScoreLabelText)
                {
                    Preferences.Default.Set<int>("HighScore", ScoreLabelText);
                    HighScoreLabelText = ScoreLabelText;
                }

                FinalScoreLabelIsVisible =true;
                FinalScoreLabelText =  "Game Over \n Score is " + ScoreLabelText;

                StringBuilder stringBuilder = new();

                stringBuilder.Append("Play Again?");
               

                PlayButtonText = stringBuilder.ToString();

            }
            else
            {
                 

                _reserve -= _closenessTarget;
                ReserveProgressProgressBar = _reserve / 5;
                ScoreLabelText++;

                TargetSecondsLabelText = 0;
                StopButtonIsEnabled = false;
                
                if (_accuracy >= 0 )
                {
                    StopButtonText = Math.Round(_accuracy, 2).ToString() + " over";                                    
                }
                else
                { 
                    StopButtonText = Math.Abs(Math.Round(_accuracy, 2)).ToString() + " under"; 
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
                TargetSecondsLabelText = new Random().Next(1, 5);
                _startTime = DateTime.UtcNow;

            };

   
        }


    }
}
