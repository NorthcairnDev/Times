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
        private Color _pauseActivityIndicatorColor;

        [ObservableProperty]
        private string? _roundTargetInSecondsLabelText;
        [ObservableProperty]
        private bool _roundTargetInSecondsLabelIsVisible;

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
        [ObservableProperty]
        private string? _reportLabelText;
        [ObservableProperty]
        private bool _reportLabelVisible;



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
            RoundTargetInSecondsLabelText = String.Empty;
            RoundTargetInSecondsLabelIsVisible = false;
            UnitsLabelIsVisible = false;
            PlayButtonIsEnabled = true;
            PlayButtonIsVisible = true;
            PlayButtonText = "Play";
            StopButtonIsEnabled = false;
            StopButtonIsVisible = false;
            StopButtonText = String.Empty;
            ReportLabelText = String.Empty;
            ReportLabelVisible = false;
        }



        [RelayCommand]
        private async Task Play()
        {
            _game.ResetGame();
            SetPlayInitalViewState();
            await Task.Delay(1500);

            _game.NewRound();
            SetPlayPlayingState();

        }
        private void SetPlayInitalViewState()
        {
            PlayButtonIsEnabled = false;
            PlayButtonIsVisible = false;
            FinalScoreLabelIsVisible = false;
            RoundTargetInSecondsLabelIsVisible = false;
            ReserveProgressProgressBar = _game.Reserve/_game.InitalReserve;
            ReserveProgressBarIsVisible = true;
            StopButtonIsEnabled = true;
            UnitsLabelIsVisible = false;
            ScoreLabelText = _game.Score.ToString();
            HighScoreLabelText = _game.HighScore.ToString();
            PauseActivityIndicatorColor = Colors.Blue;
            PauseActivityIndicatorIsVisible = true;
            ReportLabelVisible = false;
            ReportLabelText = String.Empty;
        }
        private void SetPlayPlayingState()
        {
            RoundTargetInSecondsLabelIsVisible = true;
            PauseActivityIndicatorIsVisible = false;
            UnitsLabelIsVisible = true;
            StopButtonText = "Stop";
            StopButtonIsVisible = true;
            RoundTargetInSecondsLabelText = _game.Round.TargetInSeconds.Value.Seconds.ToString();

        }  

        [RelayCommand]
        private async Task Stop()
        {
            _game.RoundOver();
                                    

            if (_game.Reserve<0)
            {
                HighScoreLabelText = _game.HighScore.ToString();
                ReserveProgressProgressBar = 0;
                StopButtonIsEnabled = false;
                StopButtonIsVisible = false;
                ReserveProgressBarIsVisible = false;
               
                PlayButtonIsVisible = true;
                PlayButtonIsEnabled = true;
                RoundTargetInSecondsLabelIsVisible = false;
                UnitsLabelIsVisible = false;
                FinalScoreLabelIsVisible = true;
                FinalScoreLabelText = "Game Over \n Score is " + ScoreLabelText;

                StringBuilder stringBuilder = new();
                stringBuilder.Append("Play Again?");
                PlayButtonText = stringBuilder.ToString();

                ReportLabelText = String.Empty;
                
                var report =new StringBuilder();

                foreach (Round round in _game.Rounds)
                {


                    var reportaccuracy = (decimal)round.Accuracy.Value.TotalSeconds;
                    report.AppendLine(Math.Round(reportaccuracy, 2).ToString());
                }
                ReportLabelText = report.ToString();
                ReportLabelVisible = true;

                return;
            }

             
            ReserveProgressProgressBar = _game.Reserve / _game.InitalReserve;
            ScoreLabelText = _game.Score.ToString();

            StopButtonIsEnabled = false;

            var accuracy = (decimal)_game.Round.Accuracy.Value.TotalSeconds;



            if(accuracy >= 0)
            {
                StopButtonText = Math.Round(accuracy, 2).ToString() + " over";
            }
            else
            {
                StopButtonText = Math.Abs(Math.Round(accuracy, 2)).ToString() + " under";
            }


            RoundTargetInSecondsLabelIsVisible = false;
            UnitsLabelIsVisible = false;
            PauseActivityIndicatorIsVisible = true;
            PauseActivityIndicatorColor = AccuracyColor();
            await Task.Delay(1500);
            PauseActivityIndicatorIsVisible = false;
            UnitsLabelIsVisible = true;
            RoundTargetInSecondsLabelIsVisible = true;
            StopButtonText = "Stop";
            StopButtonIsEnabled = true;
            _game.NewRound();
            RoundTargetInSecondsLabelText = _game.Round.TargetInSeconds.Value.Seconds.ToString();


        }

   

        private Color AccuracyColor() 
        {

            switch (_game.Round.AccuracyLevel)
            {
                case Round.LevelsOfAccuracy.VeryClose
                : return Colors.Green;
                case Round.LevelsOfAccuracy.Close
                : return Colors.Orange;
                default
                : return Colors.Red;
            }


        }

    }


    }

