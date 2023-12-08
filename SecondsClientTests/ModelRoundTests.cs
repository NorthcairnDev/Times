using FluentAssertions;
namespace SecondsClient.Tests.Unit
{
    public class ModelRoundTests
    {

        //Arrange, Act,Assert

        //MethodUnderTest_Scenario_ExpectedResult


        [Fact]
        public void Constructor_TargetInSeconds_Between1and5()
        {
            Round round = new();

            int targetInSeconds = round.TargetInSeconds.Seconds;

            targetInSeconds.Should().BeOneOf(1, 2, 3, 4, 5);
        }


        [Fact]
        public void Constructor_Accuracy_EqualsNegativeTargetInSeconds()
        {
            Round round = new();

            round.Accuracy.Should().Be(-round.TargetInSeconds);

        }


        [Fact]
        public void Constructor_AccuracyLevel_Distant()
        {
            Round round = new();

            round.AccuracyLevel.Should().Be(Round.LevelsOfAccuracy.Distant);

        }


        [Fact]
        public void StartStopwatch_Normal_Started()
        {
            Round round = new();

            round.StartStopwatch();

            round.Accuracy.Should().BeGreaterThan(-round.TargetInSeconds);

        }

        [Fact]
        public async void StopStopwatch_Normal_Stopped()
        {
            
            //Arrange
            Round round = new();
            round.StartStopwatch();

            //Act 
            round.StopStopwatch();
            var accuracy = round.Accuracy;
            await Task.Delay(100);

            //Assert
            round.Accuracy.Should().Be(accuracy);

        }


        [Theory]
        [InlineData(0, Round.LevelsOfAccuracy.Distant)]
        [InlineData(501, Round.LevelsOfAccuracy.Close)]
        [InlineData(751, Round.LevelsOfAccuracy.VeryClose)]
        [InlineData(1225, Round.LevelsOfAccuracy.VeryClose)]
        [InlineData(1300, Round.LevelsOfAccuracy.Close)]
        [InlineData(1500, Round.LevelsOfAccuracy.Distant)]
        public async void AccuracyLevel_AllDelays_AllLevelsAlignToDelays (int msDelay, Round.LevelsOfAccuracy expectedLevelOfAccuracy)
        {

            //Arrange

            Round round = new();

            while (round.TargetInSeconds.Seconds != 1) { round = new(); }


            round.StartStopwatch();
            await Task.Delay(msDelay);
            //Act 
            round.StopStopwatch();

            round.AccuracyLevel.Should().Be(expectedLevelOfAccuracy);

        }

    }
}


