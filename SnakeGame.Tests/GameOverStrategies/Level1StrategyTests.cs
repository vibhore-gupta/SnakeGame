using SnakeGame.Source;
using SnakeGame.Source.GameOverStrategies.Strategies;
using Xunit;

namespace SnakeGame.Tests.GameOverStrategies
{
    public class Level1StrategyTests
    {
        [Theory]
        [InlineData(0, 15, true)]
        [InlineData(30, 20, true)]
        [InlineData(60, 10, true)]
        [InlineData(30, 0, true)]
        [InlineData(0, 0, true)]
        [InlineData(60, 0, true)]
        [InlineData(0, 20, true)]
        [InlineData(60, 20, true)]
        [InlineData(10, 15, false)]
        [InlineData(1, 1, false)]
        [InlineData(59, 1, false)]
        [InlineData(1, 59, false)]
        [InlineData(59, 19, false)]
        public void Level1Strategy_WhenSnakeIsHittingTheWalls_ReturnsIfGameIsOver(int headXCoordinate, int headYCoordinate,
            bool expectedResult)
        {
            // Arrange
            var level1Strategy = new Level1Strategy();
            var snake = new Snake(1);
            snake.BodyParts[^1].Pixel.XCoordinate = headXCoordinate;
            snake.BodyParts[^1].Pixel.YCoordinate = headYCoordinate;

            // Act
            var actualResponse = level1Strategy.IsOver(snake.GetSnakeCoordinates());

            // Assert
            Assert.Equal(expectedResult, actualResponse);
        }

        [Theory]
        [InlineData(15, 17, true)]
        [InlineData(16, 17, true)]
        [InlineData(18, 17, false)]
        public void Level1Strategy_WhenSnakeIsHittingItSelf_ReturnsIfGameIsOver(int headXCoordinate, int headYCoordinate,
            bool expectedResult)
        {
            // Arrange
            var level1Strategy = new Level1Strategy();
            var snake = new Snake(1);
            snake.BodyParts[^1].Pixel.XCoordinate = headXCoordinate;
            snake.BodyParts[^1].Pixel.YCoordinate = headYCoordinate;

            // Act
            var actualResponse = level1Strategy.IsOver(snake.GetSnakeCoordinates());

            // Assert
            Assert.Equal(expectedResult, actualResponse);
        }

        [Theory]
        [InlineData(18, 16, false)]
        [InlineData(50, 45, false)]
        [InlineData(15, 28, false)]
        [InlineData(53, 16, false)]
        public void Level1Strategy_WhenSnakeIsNeitherHittingWallsNorHittingItSelf_ReturnsIfGameIsOver(int headXCoordinate, int headYCoordinate,
            bool expectedResult)
        {
            // Arrange
            var level1Strategy = new Level1Strategy();
            var snake = new Snake(1);
            snake.BodyParts[^1].Pixel.XCoordinate = headXCoordinate;
            snake.BodyParts[^1].Pixel.YCoordinate = headYCoordinate;

            // Act
            var actualResponse = level1Strategy.IsOver(snake.GetSnakeCoordinates());

            // Assert
            Assert.Equal(expectedResult, actualResponse);
        }
    }
}
