﻿using FluentAssertions;

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

            MainViewModelDelays standardDelays = new() { InstructionVisisbleDurationMs = 0, GetReadyVisisbleDurationMs = 0, GoVisisbleDurationMs = 0, PauseBetweenRoundsDurationMs = 100 };

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
            _vm.StartPageLabelFormattedText.ToString().Should().Be("HOT" + Environment.NewLine + "SECOND");
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
        void Constructor_PauseAnimationIsVisible_False()
        {
            _vm.PauseAnimationIsVisible.Should().Be(false);
        }
        [Fact]
        void Constructor_PauseAnimationSource_IsNull()
        {
            _vm.PauseAnimationSource.Should().BeNull();
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
            _vm.AccuracyLabelText.Should().BeNull();
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

            _gameHistory = Substitute.For<IGameHistory>();
            _gameHistory.HighScore.Returns(0);

            MainViewModelDelays altDelays = new() { InstructionVisisbleDurationMs=0, GetReadyVisisbleDurationMs = 100, GoVisisbleDurationMs = 0, PauseBetweenRoundsDurationMs = 100 };

            this._vm = new(_gameHistory, altDelays);

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
            _vm.PauseAnimationSource.File.Should().Be("whiteactivityanimation.json");
            _vm.PauseAnimationIsVisible.Should().Be(true);
            _vm.TargetSecondsImageSource.Should().Be(string.Empty);
            _vm.TargetSecondsImageIsVisible.Should().Be(false);
            _vm.AccuracyLabelText.Should().BeNull();
            _vm.AccuracyLabelTextColor.Should().Be(Colors.White);
            _vm.AccuracyLabelIsVisible.Should().Be(false);
            _vm.GameOverLabelIsVisible.Should().Be(false);
            _vm.PlayButtonIsEnabled.Should().Be(false);
            _vm.PlayButtonIsVisible.Should().Be(false);
            _vm.StopButtonIsEnabled.Should().Be(false);
            _vm.StopButtonIsVisible.Should().Be(true);
            _vm.StopButtonImageSource.Should().Be("pausebutton.png");


        }

        [Fact]
        async void StartGameCommand_GetReadyLabelText_Go()
        {

            MainViewModelDelays altDelays = new() { InstructionVisisbleDurationMs = 0, GetReadyVisisbleDurationMs = 0, GoVisisbleDurationMs = 100, PauseBetweenRoundsDurationMs = 100 };

            this._vm = new(_gameHistory, altDelays);

            _vm.StartGameCommand.ExecuteAsync(null);

            await Task.Delay(50);

            _vm.GetReadyLabelText.ToString().Should().Be("GO!");
            _vm.GetReadyLabelFontSize.Should().Be(48);
            _vm.GetReadyLabelIsVisible.Should().Be(true);

            _vm.ScoreLabelText.ToString().Should().Be("0");
            _vm.ScoreLabelTextColor.Should().Be(Colors.White);
            _vm.ScoreLabelBackgroundColor.Should().Be(Colors.Black);
            _vm.HighScoreLabelTextColor.Should().Be(Colors.White);
            _vm.HighScoreLabelText.Should().Be(_gameHistory.HighScore.ToString());
            _vm.HighScoreLabelBackgroundColor.Should().Be(Colors.Black);
            _vm.ReserveProgressProgressBar.Should().Be(1);
            _vm.StartPageLabelIsVisible.Should().Be(false);
            _vm.PauseAnimationSource.File.Should().Be("whiteactivityanimation.json");
            _vm.PauseAnimationIsVisible.Should().Be(true);
            _vm.TargetSecondsImageSource.Should().Be(string.Empty);
            _vm.TargetSecondsImageIsVisible.Should().Be(false);
            _vm.AccuracyLabelText.Should().BeNull();
            _vm.AccuracyLabelTextColor.Should().Be(Colors.White);
            _vm.AccuracyLabelIsVisible.Should().Be(false);
            _vm.GameOverLabelIsVisible.Should().Be(false);
            _vm.PlayButtonIsEnabled.Should().Be(false);
            _vm.PlayButtonIsVisible.Should().Be(false);
            _vm.StopButtonIsEnabled.Should().Be(false);
            _vm.StopButtonIsVisible.Should().Be(true);
            _vm.StopButtonImageSource.Should().Be("pausebutton.png");
        }

        [Fact]
        async void StartRound_TargetSecondsNotEasyMode_Between1and5()
        {

            _vm.EasyModeSwitchIsToggled = false;

            _vm.StartRound();

            _vm.TargetSecondsImageSource.Should().BeOneOf("onesecondfuschia.png",
                                                            "twosecondfuschia.png",
                                                            "threesecondfuschia.png",
                                                            "foursecondfuschia.png",
                                                            "fivesecondfuschia.png");


            _vm.TargetSecondsImageIsVisible.Should().Be(true);

            _vm.ScoreLabelText.ToString().Should().Be("0");
            _vm.ScoreLabelTextColor.Should().Be(Colors.White);
            _vm.ScoreLabelBackgroundColor.Should().Be(Colors.Black);
            _vm.HighScoreLabelTextColor.Should().Be(Colors.White);
            _vm.HighScoreLabelText.Should().Be(_gameHistory.HighScore.ToString());
            _vm.HighScoreLabelBackgroundColor.Should().Be(Colors.Black);
            _vm.ReserveProgressProgressBar.Should().Be(1);
            _vm.StartPageLabelIsVisible.Should().Be(false);
            _vm.GetReadyLabelText.ToString().Should().Be(string.Empty);
            _vm.GetReadyLabelFontSize.Should().Be(28);
            _vm.GetReadyLabelIsVisible.Should().Be(false);
            _vm.PauseAnimationSource.Should().BeNull();
            _vm.PauseAnimationIsVisible.Should().Be(false);
            _vm.AccuracyLabelText.Should().BeNull();
            _vm.AccuracyLabelTextColor.Should().Be(Colors.White);
            _vm.AccuracyLabelIsVisible.Should().Be(false);
            _vm.GameOverLabelIsVisible.Should().Be(false);
            _vm.PlayButtonIsEnabled.Should().Be(false);
            _vm.PlayButtonIsVisible.Should().Be(false);
            _vm.StopButtonIsEnabled.Should().Be(true);
            _vm.StopButtonIsVisible.Should().Be(true);
            _vm.StopButtonImageSource.Should().Be("stopbutton.png");

        }


        [Fact]
        async void StartRound_TargetSecondsEasyMode_Between1and5()
        {
            //arrange
            _vm.EasyModeSwitchIsToggled = true;

            //act
            _vm.StartRound();
            //assert
            _vm.TargetSecondsImageSource.Should().BeOneOf("onemississippifuschia.png",
                                                                "twomississippifuschia.png",
                                                                "threemississippifuschia.png",
                                                                "fourmississippifuschia.png",
                                                                "fivemississippifuschia.png");


            _vm.TargetSecondsImageIsVisible.Should().Be(true);

            _vm.ScoreLabelText.ToString().Should().Be("0");
            _vm.ScoreLabelTextColor.Should().Be(Colors.White);
            _vm.ScoreLabelBackgroundColor.Should().Be(Colors.Black);
            _vm.HighScoreLabelTextColor.Should().Be(Colors.White);
            _vm.HighScoreLabelText.Should().Be(_gameHistory.HighScore.ToString());
            _vm.HighScoreLabelBackgroundColor.Should().Be(Colors.Black);
            _vm.ReserveProgressProgressBar.Should().Be(1);
            _vm.StartPageLabelIsVisible.Should().Be(false);
            _vm.GetReadyLabelText.ToString().Should().Be(string.Empty);
            _vm.GetReadyLabelFontSize.Should().Be(28);
            _vm.GetReadyLabelIsVisible.Should().Be(false);
            _vm.PauseAnimationSource.Should().BeNull();
            _vm.PauseAnimationIsVisible.Should().Be(false);
            _vm.AccuracyLabelText.Should().BeNull();
            _vm.AccuracyLabelTextColor.Should().Be(Colors.White);
            _vm.AccuracyLabelIsVisible.Should().Be(false);
            _vm.GameOverLabelIsVisible.Should().Be(false);
            _vm.PlayButtonIsEnabled.Should().Be(false);
            _vm.PlayButtonIsVisible.Should().Be(false);
            _vm.StopButtonIsEnabled.Should().Be(true);
            _vm.StopButtonIsVisible.Should().Be(true);
            _vm.StopButtonImageSource.Should().Be("stopbutton.png");

        }

        [Fact]
        async void StopCommand_RoundOver_GameNotOver() 
        {

            //Arrange
            MainViewModelDelays altDelays = new() { InstructionVisisbleDurationMs=0, GetReadyVisisbleDurationMs = 0, GoVisisbleDurationMs = 0, PauseBetweenRoundsDurationMs = 1000 };
            this._vm = new(_gameHistory, altDelays);

            await _vm.StartGameCommand.ExecuteAsync(null);


            //Act
            _vm.StopCommand.ExecuteAsync(null);

            //assert

            _vm.ScoreLabelText.ToString().Should().Be("1");
            _vm.ScoreLabelTextColor.Should().Be(Colors.White);
            _vm.ScoreLabelBackgroundColor.Should().Be(Colors.Black);
            _vm.HighScoreLabelTextColor.Should().Be(Colors.White);
            _vm.HighScoreLabelText.Should().Be(_gameHistory.HighScore.ToString());
            _vm.HighScoreLabelBackgroundColor.Should().Be(Colors.Black);
            _vm.ReserveProgressProgressBar.Should().BeInRange(0.00000001,1);
            _vm.StartPageLabelIsVisible.Should().Be(false);
            _vm.GetReadyLabelText.ToString().Should().Be(string.Empty);
            _vm.GetReadyLabelFontSize.Should().Be(28);
            _vm.GetReadyLabelIsVisible.Should().Be(false);
            _vm.PauseAnimationSource.File.Should().Be("redactivityanimation.json");
            _vm.PauseAnimationIsVisible.Should().Be(true);
            _vm.TargetSecondsImageSource.Should().Be(string.Empty);
            _vm.TargetSecondsImageIsVisible.Should().Be(false);
            _vm.AccuracyLabelText.Should().NotBeNull();
            _vm.AccuracyLabelTextColor.Should().Be(Color.FromArgb("FE0000"));
            _vm.AccuracyLabelIsVisible.Should().Be(true);
            _vm.GameOverLabelIsVisible.Should().Be(false);
            _vm.PlayButtonIsEnabled.Should().Be(false);
            _vm.PlayButtonIsVisible.Should().Be(false);
            _vm.StopButtonIsEnabled.Should().Be(false);
            _vm.StopButtonIsVisible.Should().Be(true);
            _vm.StopButtonImageSource.Should().Be("pausebutton.png");

        }

        [Fact]
        async void StopCommand_RoundOver_GameOverNotNewHighScore()
        {

            //Arrange
            MainViewModelDelays altDelays = new() {  InstructionVisisbleDurationMs=0, GetReadyVisisbleDurationMs = 0, GoVisisbleDurationMs = 0, PauseBetweenRoundsDurationMs = 0 };
            this._vm = new(_gameHistory, altDelays);

            await _vm.StartGameCommand.ExecuteAsync(null);

            await Task.Delay(10500);
            //Act
            _vm.StopCommand.ExecuteAsync(null);

            //assert

            _vm.ScoreLabelText.ToString().Should().Be("0");
            _vm.ScoreLabelTextColor.Should().Be(Colors.Black);
            _vm.ScoreLabelBackgroundColor.Should().Be(Color.FromArgb("FF9900"));
            _vm.HighScoreLabelTextColor.Should().Be(Colors.White);
            _vm.HighScoreLabelText.Should().Be(_gameHistory.HighScore.ToString());
            _vm.HighScoreLabelBackgroundColor.Should().Be(Colors.Black);
            _vm.ReserveProgressProgressBar.Should().Be(0);
            _vm.StartPageLabelIsVisible.Should().Be(false);
            _vm.GetReadyLabelText.ToString().Should().BeEmpty();
            _vm.GetReadyLabelFontSize.Should().Be(28);
            _vm.GetReadyLabelIsVisible.Should().Be(false);
            _vm.PauseAnimationSource.Should().BeNull();
            _vm.PauseAnimationIsVisible.Should().Be(false);
            _vm.TargetSecondsImageSource.Should().BeEmpty();
            _vm.TargetSecondsImageIsVisible.Should().Be(false);
            _vm.AccuracyLabelText.Should().BeNull(); ;
            _vm.AccuracyLabelTextColor.Should().Be(Colors.White);
            _vm.AccuracyLabelIsVisible.Should().Be(false);
            _vm.GameOverLabelIsVisible.Should().Be(true);
            _vm.PlayButtonIsEnabled.Should().Be(true);
            _vm.PlayButtonIsVisible.Should().Be(true);
            _vm.StopButtonIsEnabled.Should().Be(false);
            _vm.StopButtonIsVisible.Should().Be(false);
            _vm.StopButtonImageSource.Should().BeEmpty();

        }

        [Fact]
        async void StopCommand_RoundOver_GameOverNewHighScore()
        {

            //Arrange
            MainViewModelDelays altDelays = new() { InstructionVisisbleDurationMs=0, GetReadyVisisbleDurationMs = 0, GoVisisbleDurationMs = 0, PauseBetweenRoundsDurationMs = 0 };
            this._vm = new(_gameHistory, altDelays);

            await _vm.StartGameCommand.ExecuteAsync(null);

            await Task.Delay(100);
            _vm.StopCommand.ExecuteAsync(null);

            await Task.Delay(10500); 
            //Act

            _vm.StopCommand.ExecuteAsync(null);



            //assert

            _vm.ScoreLabelText.ToString().Should().Be("1");
            _vm.ScoreLabelTextColor.Should().Be(Colors.Black);
            _vm.ScoreLabelBackgroundColor.Should().Be(Color.FromArgb("FF9900"));
            _vm.HighScoreLabelTextColor.Should().Be(Colors.Black);
            _vm.HighScoreLabelText.Should().Be(_gameHistory.HighScore.ToString());
            _vm.HighScoreLabelBackgroundColor.Should().Be(Color.FromArgb("05C405"));
            _vm.ReserveProgressProgressBar.Should().Be(0);
            _vm.StartPageLabelIsVisible.Should().Be(false);
            _vm.GetReadyLabelText.ToString().Should().BeEmpty();
            _vm.GetReadyLabelFontSize.Should().Be(28);
            _vm.GetReadyLabelIsVisible.Should().Be(false);
            _vm.PauseAnimationSource.Should().BeNull();
            _vm.PauseAnimationIsVisible.Should().Be(false);
            _vm.TargetSecondsImageSource.Should().BeEmpty();
            _vm.TargetSecondsImageIsVisible.Should().Be(false);
            _vm.AccuracyLabelText.Should().BeNull();
            _vm.AccuracyLabelTextColor.Should().Be(Colors.White);
            _vm.AccuracyLabelIsVisible.Should().Be(false);
            _vm.GameOverLabelIsVisible.Should().Be(true);
            _vm.PlayButtonIsEnabled.Should().Be(true);
            _vm.PlayButtonIsVisible.Should().Be(true);
            _vm.StopButtonIsEnabled.Should().Be(false);
            _vm.StopButtonIsVisible.Should().Be(false);
            _vm.StopButtonImageSource.Should().BeEmpty();

        }



    }



}

