


namespace SecondsClientTests
{
    public class ModelGameTests
    {

        //Arrange, Act,Assert

        //MethodUnderTest_Scenario_ExpectedResult


        [Fact]
        public void Constructor_Score_SetTo0()
        {
            Game game = new();

            Assert.Equal(0, game.Score);
        }


        [Fact]
        public void Constructor_Reserve_SetTo5()
        {
            Game game = new();

            Assert.Equal(5, game.Reserve);
        }

        [Fact]
        public void Constructor_Round_IsNull()
        {
            Game game = new();

            Assert.Null(game.Round);
        }


        [Fact]
        public void Constructor_IsGameOver_SetToFalse()
        {
            Game game = new();

            Assert.False(game.IsGameOver);
        }


        [Fact]
        public void Constructor_IsNewHighScore_SetToFalse()
        {
            Game game = new();

            Assert.False(game.NewHighScore);
        }

        [Fact]
        public void NewRound_Normal_RoundSet()
        {
            Game game = new();
            game.NewRound();

            Assert.NotNull(game.Round);
        }

        [Fact]
        public void RoundOver_Normal_GameOver()
        {
            //arrange
            var round = Substitute.For<IRound>();

            round.Accuracy.Returns(TimeSpan.FromSeconds(6));

            Game game = new()
            {
                Round = round
            };

            //act
            game.RoundOver();

            //assert

            Assert.True(game.IsGameOver);

        }

        [Fact]
        public void RoundOver_Normal_GameOverScoreNotIncremented()
        {
            //arrange
            var round = Substitute.For<IRound>();

            round.Accuracy.Returns(TimeSpan.FromSeconds(6));

            Game game = new()
            {
                Round = round
            };

            //act
            game.RoundOver();

            //assert


            Assert.Equal(0, game.Score);

        }

        [Fact]
        public void RoundOver_Normal_GameNotOver()
        {
            //arrange
            var round = Substitute.For<IRound>();

            round.Accuracy.Returns(TimeSpan.FromSeconds(4));

            Game game = new()
            {
                Round = round
            };

            //act
            game.RoundOver();

            //assert

            Assert.False(game.IsGameOver);

        }


        [Fact]
        public void RoundOver_Normal_GameNotOverScoreIncremented()
        {
            //arrange
            var round = Substitute.For<IRound>();

            round.Accuracy.Returns(TimeSpan.FromSeconds(4));

            Game game = new()
            {
                Round = round
            };

            //act
            game.RoundOver();

            //assert

            Assert.Equal(1,game.Score);

        }

    }
 } 
