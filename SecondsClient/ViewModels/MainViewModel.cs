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
        private FormattedString? _reportLabelFormattedText;
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
            ReportLabelFormattedText = String.Empty;
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
            ReportLabelFormattedText = String.Empty;
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
                FinalScoreLabelText = "Game Over";

                StringBuilder stringBuilder = new();
                stringBuilder.Append("Play Again?");
                PlayButtonText = stringBuilder.ToString();

                ReportLabelFormattedText = String.Empty;
                
                var report =new StringBuilder();
                var formattedReport = new FormattedString();

                foreach (Round round in _game.Rounds)
                {
                    report.Append((_game.Rounds.IndexOf(round)+1).ToString());
                    report.Append(" -> ");
                    report.Append(round.TargetInSeconds.Value.TotalSeconds.ToString());
                    report.Append(" -> ");
                    var reportaccuracy = (decimal)round.Accuracy.Value.TotalSeconds;
                    report.Append(Math.Round(reportaccuracy, 2).ToString());
                    report.AppendLine();

                    switch (round.AccuracyLevel)
                    {
                        case Round.LevelsOfAccuracy.VeryClose
                :
                            formattedReport.Spans.Add(new Span { Text = report.ToString(), TextColor = Colors.Green });

                            break;
                        case Round.LevelsOfAccuracy.Close
                :
                            formattedReport.Spans.Add(new Span { Text = report.ToString(), TextColor = Colors.Green });

                            break;
                        default
                :
                            formattedReport.Spans.Add(new Span { Text = report.ToString(), TextColor = Colors.Red });

                            break;
                           

                    }

                    report.Clear();


                    //FormattedString formattedString = new FormattedString();
                    //formattedString.Spans.Add(new Span { Text = "Red bold, ", TextColor = Colors.Red, FontAttributes = FontAttributes.Bold });

                    //Span span = new Span { Text = "default, " };
                    //span.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(async () => await DisplayAlert("Tapped", "This is a tapped Span.", "OK")) });
                    //formattedString.Spans.Add(span);
                    //formattedString.Spans.Add(new Span { Text = "italic small.", FontAttributes = FontAttributes.Italic, FontSize = 14 });

                    //Label label = new Label { FormattedText = formattedString };



                }
                ReportLabelFormattedText = formattedReport;
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

