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
    partial class MainViewModel:ObservableObject
    {
        [ObservableProperty]
        private int _score=0;
        [ObservableProperty]
        private decimal _reserve = 5;
        [ObservableProperty]
        private decimal _reserveProgress = 1;
        [ObservableProperty]
        private bool _reserveIsVisible = false;
        [ObservableProperty]
        private bool _pauseIsVisible = false;
        [ObservableProperty]
        private int _targetTime = 0;
        [ObservableProperty]
        private bool _targetIsVisible = false;
        [ObservableProperty]
        private decimal _accuracy = 0;
        [ObservableProperty]
        private bool _playIsEnabled = true;
        [ObservableProperty]
        private bool _playIsVisible = true;
        [ObservableProperty]
        private bool _stopIsEnabled = false;
        [ObservableProperty]
        private bool _stopIsVisible = false;
        [ObservableProperty]
        private string _stopText = String.Empty;

        [ObservableProperty]
        private string _playText = "Play";


        private DateTime _startTime;
        private DateTime _endTime;
        private Decimal _closenessTarget;
        

        [RelayCommand]
        private async Task Play()
        {
            PlayIsEnabled = false;
            PlayIsVisible = false;
            ReserveProgress = 1;
            ReserveIsVisible = true;
            StopIsEnabled = true;
            Reserve = 5;
  
            Score = 0;
            Accuracy = 0;
            TargetTime = 0;
            
            PauseIsVisible = true;
            await Task.Delay(1500);
            TargetIsVisible = true;
            PauseIsVisible = false;
            StopText = "Stop";
            StopIsVisible = true;
            _startTime = DateTime.UtcNow;
            TargetTime = new Random().Next(1, 5);
        }

        [RelayCommand]
        private async Task Stop()
        {
         
            _endTime = DateTime.UtcNow;


            Accuracy = ((decimal)(_endTime - _startTime).TotalSeconds) - TargetTime;
            _closenessTarget = Math.Abs(TargetTime - Math.Round((decimal)(_endTime - _startTime).TotalSeconds, 2));
            

            if   ((Reserve - _closenessTarget) < 0)
            {
                Reserve = 0;
                ReserveProgress = 0;
                StopIsEnabled = false;
                StopIsVisible = false;
                ReserveIsVisible = false;
                PlayIsVisible = false;
                PlayIsEnabled = true;
                TargetIsVisible = false;
                PlayText = "Play Again?";
            }
            else
            {
                 

                Reserve -= _closenessTarget;
                ReserveProgress = Reserve / 5;
                Score++;

                TargetTime = 0;
                StopIsEnabled = false;
                
                if (Accuracy >= 0 )
                {
                    StopText = Math.Round(Accuracy, 2).ToString() + " over";                                    
                }
                else
                { 
                    StopText = Math.Abs(Math.Round(Accuracy, 2)).ToString() + " under"; 
                }


                TargetIsVisible = false;
                PauseIsVisible = true;
                await Task.Delay(1500);
                PauseIsVisible = false;
                TargetIsVisible = true;
                StopText = "Stop";
                StopIsEnabled = true;
                TargetTime = new Random().Next(1, 5);
                _startTime = DateTime.UtcNow;

            };

   
        }


    }
}
