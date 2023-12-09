using FluentAssertions;

namespace SecondsClient.Tests.Unit
{

    public class MainViewModelTests
    {
        //Arrange, Act,Assert

        //MethodUnderTest_Scenario_ExpectedResult


        MainViewModel _vm;
        IGameHistory _gameHistory;


        public MainViewModelTests()
        {
            _gameHistory = Substitute.For<IGameHistory>();
            _gameHistory.HighScore.Returns(0);

            MainViewModelDelays standardDelays = new() { GetReadyVisisbleDurationMs = 0, GoVisisbleDurationMs = 0, PauseBetweenRoundsDurationMs = 100 };

            this._vm = new(_gameHistory, standardDelays);

        }

        [Fact]
        void Constructor_ScoreLabelText_0()
        {
            _vm.ScoreLabelText.Should().Be("0");
        }

        [Fact]
        void Constructor_ScoreLabelTextColor_White()
        {
            _vm.ScoreLabelTextColor.Should().Be(Colors.White);
        }

        [Fact]
        void Constructor_ScoreLabelBackgroundColor_Black()
        {
            _vm.ScoreLabelBackgroundColor.Should().Be(Colors.Black);
        }

        [Fact]
        void Constructor_HighScoreLabelText_0()
        {
            _vm.HighScoreLabelText = "0";
        }
        [Fact]
        void Constructor_HighScoreLabelBackgroundColor_Black()
        {
            _vm.HighScoreLabelBackgroundColor.Should().Be(Colors.Black);
        }
        [Fact]
        void Constructor_HighScoreLabelTextColor_White()
        {
            _vm.HighScoreLabelTextColor.Should().Be(Colors.White);
        }
        [Fact]
        void Constructor_EasyModeSwitchIsToggled_False()
        {
            _vm.EasyModeSwitchIsToggled.Should().Be(false);
        }
        [Fact]
        void Constructor_ReserveProgressProgressBar_0()
        {
            _vm.ReserveProgressProgressBar.Should().Be(0);
        }
        [Fact]
        void Constructor_StartPageLabelIsVisible_True()
        {
            _vm.StartPageLabelIsVisible.Should().Be(true);
        }
        [Fact]
        void Constructor_StartPageLabelFormattedText_CountTheSeconds()
        {
            _vm.StartPageLabelFormattedText.ToString().Should().Be("Count" + Environment.NewLine + "the" + Environment.NewLine + "seconds");
        }

        [Fact]
        void Constructor_GetReadyLabelText_Empty()
        {
            _vm.GetReadyLabelText.Should().Be(string.Empty);
        }
        [Fact]
        void Constructor_GetReadyLabelFontSize_28()
        {
            _vm.GetReadyLabelFontSize.Should().Be(28);
        }
        [Fact]
        void Constructor_GetReadyLabelIsVisible_False()
        {
            _vm.GetReadyLabelIsVisible.Should().Be(false);
        }
        [Fact]
        void Constructor_PauseActivityIndicatorIsVisible_False()
        {
            _vm.PauseActivityIndicatorIsVisible.Should().Be(false);
        }
        [Fact]
        void Constructor_PauseActivityIndicatorColor_White()
        {
            _vm.PauseActivityIndicatorColor.Should().Be(Colors.White);
        }
        [Fact]
        void Constructor_TargetSecondsImageSource_Empty()
        {
            _vm.TargetSecondsImageSource.Should().Be(string.Empty);
        }
        [Fact]
        void Constructor_TargetSecondsImageIsVisible_False()
        {
            _vm.TargetSecondsImageIsVisible.Should().Be(false);
        }
        [Fact]
        void Constructor_AccuracyLabelText_Empty()
        {
            _vm.AccuracyLabelText.Should().Be(string.Empty);
        }
        [Fact]
        void Constructor_AccuracyLabelTextColor_White()
        {
            _vm.AccuracyLabelTextColor.Should().Be(Colors.White);
        }
        [Fact]
        void Constructor_AccuracyLabelIsVisible_False()
        {
            _vm.AccuracyLabelIsVisible.Should().Be(false);
        }
        [Fact]
        void Constructor_GameOverLabelFormattedText_GameOver()
        {
            _vm.GameOverLabelFormattedText.ToString().Should().Be("Game" + Environment.NewLine + "Over");
        }
        [Fact]
        void Constructor_GameOverLabelIsVisible_False()
        {
            _vm.GameOverLabelIsVisible.Should().Be(false);
        }
        [Fact]
        void Constructor_PlayButtonIsEnabled_True()
        {
            _vm.PlayButtonIsEnabled.Should().Be(true);
        }
        [Fact]
        void Constructor_PlayButtonIsVisible_True()
        {
            _vm.PlayButtonIsVisible.Should().Be(true);
        }
        [Fact]
        void Constructor_StopButtonIsEnabled_False()
        {
            _vm.StopButtonIsEnabled.Should().Be(false);
        }
        [Fact]
        void Constructor_StopButtonIsVisible_False()
        {
            _vm.StopButtonIsVisible.Should().Be(false);
        }
        [Fact]
        void Constructor_StopButtonImageSource_Empty()
        {
            _vm.StopButtonImageSource.Should().Be(string.Empty);
        }




        [Fact]
        async void StartGameCommand_GetReadyLabelText_GetReady()
        {


            MainViewModelDelays standardDelays = new() { GetReadyVisisbleDurationMs = 100, GoVisisbleDurationMs = 0, PauseBetweenRoundsDurationMs = 100 };

            this._vm = new(_gameHistory, standardDelays);
            
            _vm.StartGameCommand.ExecuteAsync(null);

            await Task.Delay(50);

            _vm.GetReadyLabelText.ToString().Should().Be("Get" + Environment.NewLine + "Ready");
            _vm.GetReadyLabelFontSize.Should().Be(28);
            _vm.GetReadyLabelIsVisible.Should().Be(true);


            _vm.ScoreLabelText.ToString().Should().Be("0");
            _vm.ScoreLabelTextColor.Should().Be(Colors.White);
            _vm.ScoreLabelBackgroundColor.Should().Be(Colors.Black);
            _vm.HighScoreLabelTextColor.Should().Be(Colors.White);
            _vm.HighScoreLabelText.Should().Be(_gameHistory.HighScore.ToString());
            _vm.HighScoreLabelBackgroundColor.Should().Be(Colors.Black);
            _vm.ReserveProgressProgressBar.Should().Be(1);
            _vm.StartPageLabelIsVisible.Should().Be(false);
            _vm.PauseActivityIndicatorColor.Should().Be(Colors.White);
            _vm.PauseActivityIndicatorIsVisible.Should().Be(true);
            _vm.TargetSecondsImageSource.Should().Be(string.Empty);
            _vm.TargetSecondsImageIsVisible.Should().Be(false);
            _vm.AccuracyLabelText.Should().Be(string.Empty);
            _vm.AccuracyLabelTextColor.Should().Be(Colors.White);
            _vm.AccuracyLabelIsVisible.Should().Be(false);
            _vm.GameOverLabelIsVisible.Should().Be(false);
            _vm.PlayButtonIsEnabled.Should().Be(false);
            _vm.PlayButtonIsVisible.Should().Be(false);
            _vm.StopButtonIsEnabled.Should().Be(false);
            _vm.StopButtonIsVisible.Should().Be(true);
            _vm.StopButtonImageSource.Should().Be("pausebutton.svg");


        }

        [Fact]
        async void StartGameCommand_GetReadyLabelText_Go()
        {

            MainViewModelDelays standardDelays = new() { GetReadyVisisbleDurationMs = 0, GoVisisbleDurationMs = 100, PauseBetweenRoundsDurationMs = 100 };

            this._vm = new(_gameHistory, standardDelays);

            _vm.StartGameCommand.ExecuteAsync(null);

            await Task.Delay(50);

            _vm.GetReadyLabelText.ToString().Should().Be("GO!");
            _vm.GetReadyLabelIsVisible.Should().Be(true);
        }

        [Fact]
        async void StartGameCommand_GetReadyLabelVisisble_False()
        {

            await  _vm.StartGameCommand.ExecuteAsync(null);

            _vm.GetReadyLabelIsVisible.Should().Be(false);
        }

    }

}

