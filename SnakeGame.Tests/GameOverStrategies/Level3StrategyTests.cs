using SnakeGame.Source;
using SnakeGame.Source.GameOverStrategies.Strategies;
using Xunit;

namespace SnakeGame.Tests.GameOverStrategies
{
    public class Level3StrategyTests
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
        [InlineData(10, 15, true)]
        [InlineData(1, 1, false)]
        [InlineData(59, 1, false)]
        [InlineData(1, 59, false)]
        [InlineData(59, 19, false)]
        public void Level3Strategy_WhenSnakeIsHittingTheWalls_ReturnsIfGameIsOver(int headXCoordinate, int headYCoordinate,
            bool expectedResult)
        {
            // Arrange
            var level3Strategy = new Level3Strategy();
            var snake = new Snake(1);
            snake.BodyParts[^1].Pixel.XCoordinate = headXCoordinate;
            snake.BodyParts[^1].Pixel.YCoordinate = headYCoordinate;

            // Act
            var actualResponse = level3Strategy.IsOver(snake.GetSnakeCoordinates());

            // Assert
            Assert.Equal(expectedResult, actualResponse);
        }

        [Theory]
        [InlineData(15, 17, true)]
        [InlineData(16, 17, true)]
        [InlineData(18, 17, false)]
        public void Level3Strategy_WhenSnakeIsHittingItSelf_ReturnsIfGameIsOver(int headXCoordinate, int headYCoordinate,
            bool expectedResult)
        {
            // Arrange
            var level3Strategy = new Level3Strategy();
            var snake = new Snake(1);
            snake.BodyParts[^1].Pixel.XCoordinate = headXCoordinate;
            snake.BodyParts[^1].Pixel.YCoordinate = headYCoordinate;

            // Act
            var actualResponse = level3Strategy.IsOver(snake.GetSnakeCoordinates());

            // Assert
            Assert.Equal(expectedResult, actualResponse);
        }

        [Theory]
        [InlineData(16, 6, true)]
        [InlineData(17, 6, true)]
        [InlineData(37, 6, true)]
        [InlineData(38, 6, true)]
        [InlineData(16, 12, true)]
        [InlineData(17, 12, true)]
        [InlineData(40, 13, true)]
        [InlineData(41, 13, true)]
        [InlineData(2, 1, true)]
        [InlineData(4, 2, true)]
        [InlineData(2, 19, true)]
        [InlineData(4, 18, true)]
        [InlineData(58, 19, true)]
        [InlineData(56, 18, true)]
        [InlineData(56, 2, true)]
        [InlineData(54, 3, true)]
        [InlineData(20, 28, false)]
        [InlineData(35, 16, false)]
        public void Level3Strategy_WhenSnakeIsNotHittingWallsOrItselfButHittingObstacles_ReturnsIfGameIsOver(int headXCoordinate, int headYCoordinate,
            bool expectedResult)
        {
            // Arrange
            var level3Strategy = new Level3Strategy();
            var snake = new Snake(1);
            snake.BodyParts[^1].Pixel.XCoordinate = headXCoordinate;
            snake.BodyParts[^1].Pixel.YCoordinate = headYCoordinate;

            // Act
            var actualResponse = level3Strategy.IsOver(snake.GetSnakeCoordinates());

            // Assert
            Assert.Equal(expectedResult, actualResponse);
        }

        [Theory]
        [InlineData(18, 16, false)]
        [InlineData(50, 45, false)]
        [InlineData(15, 28, false)]
        [InlineData(53, 16, false)]
        public void Level3Strategy_WhenSnakeIsNeitherHittingWallsNorHittingItSelf_ReturnsIfGameIsOver(int headXCoordinate, int headYCoordinate,
            bool expectedResult)
        {
            // Arrange
            var level3Strategy = new Level3Strategy();
            var snake = new Snake(1);
            snake.BodyParts[^1].Pixel.XCoordinate = headXCoordinate;
            snake.BodyParts[^1].Pixel.YCoordinate = headYCoordinate;

            // Act
            var actualResponse = level3Strategy.IsOver(snake.GetSnakeCoordinates());

            // Assert
            Assert.Equal(expectedResult, actualResponse);
        }
    }
}
