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
        private int _targetTime = 0;
        [ObservableProperty]
        private decimal _actualTime = 0;
        [ObservableProperty]
        private bool _playIsEnabled = true;
        [ObservableProperty]
        private bool _stopIsEnabled = false;

        private DateTime _startTime;
        private DateTime _endTime;
        private Decimal _closenessTarget;

        [RelayCommand]
        private async Task Play()
        {
            PlayIsEnabled = false;
            StopIsEnabled = true;
            Score = 0;
            ActualTime = 0;
            Reserve = 5;
            
            TargetTime = 0;
            await Task.Delay(2000);

            _startTime = DateTime.UtcNow;
            TargetTime = new Random().Next(1, 5);
        }

        [RelayCommand]
        private async Task Stop()
        {
         
            _endTime = DateTime.UtcNow;
           

            ActualTime = Math.Round((decimal)(_endTime - _startTime).TotalSeconds,2);

            _closenessTarget = Math.Abs(TargetTime - ActualTime);
            

            if   ((Reserve - _closenessTarget) < 0)
            {
                Reserve = 0;
                StopIsEnabled = false;
                PlayIsEnabled = true;
            }
            else
            {
                 

                Reserve -= _closenessTarget;
                Score++;

                TargetTime = 0;
                StopIsEnabled = false;
                await Task.Delay(2000);
                StopIsEnabled = true;
                TargetTime = new Random().Next(1, 5);
                _startTime = DateTime.UtcNow;

            };

   
        }


    }
}
