using System;
using Xunit;

namespace SnakeGame.Tests
{
    public class SnakeBuilderHelperTests
    {
        [Theory]
        [InlineData(Direction.DOWN, 20, 31)]
        [InlineData(Direction.UP, 20, 29)]
        [InlineData(Direction.LEFT, 19, 30)]
        [InlineData(Direction.RIGHT, 21, 30)]
        [InlineData(Direction.NONE, 20, 30)]
        public void GetNewCoordinatesForHead_GivenADirectionAndPixel_ReturnsNewCoordinates(Direction direction, int xCoordinate, int yCoordinate)
        {
            // Arrange
            var pixel = new Pixel(20, 30, ConsoleColor.Black, direction);

            // Act
            var (x,y) = SnakeBuilderHelper.GetNewCoordinatesForHead(pixel, direction);

            // Assert
            Assert.Equal(xCoordinate, x);
            Assert.Equal(yCoordinate, y);
        }

        [Theory]
        [InlineData(ConsoleKey.DownArrow, 20, 31)]
        [InlineData(ConsoleKey.UpArrow, 20, 29)]
        [InlineData(ConsoleKey.LeftArrow, 19, 30)]
        [InlineData(ConsoleKey.RightArrow, 21, 30)]
        public void GetNewCoordinatesForHead_GivenAKeyAndPixel_ReturnsNewCoordinates(ConsoleKey consoleKey, int xCoordinate, int yCoordinate)
        {
            // Arrange
            var pixel = new Pixel(20, 30, ConsoleColor.Black, Direction.RIGHT);

            // Act
            var (x, y) = SnakeBuilderHelper.GetNewCoordinatesForHead(pixel, consoleKey);

            // Assert
            Assert.Equal(xCoordinate, x);
            Assert.Equal(yCoordinate, y);
        }

        [Theory]
        [InlineData(ConsoleKey.DownArrow, Direction.DOWN)]
        [InlineData(ConsoleKey.UpArrow, Direction.UP)]
        [InlineData(ConsoleKey.LeftArrow, Direction.LEFT)]
        [InlineData(ConsoleKey.RightArrow, Direction.RIGHT)]
        public void GetNewCoordinatesForHead_GivenAKey_ReturnsADirection(ConsoleKey consoleKey, Direction direction)
        {
            // Arrange

            // Act
            var newDirection = SnakeBuilderHelper.GetNewDirectionForHead(consoleKey);

            // Assert
            Assert.Equal(direction, newDirection);
        }

        [Theory]
        [InlineData(Direction.DOWN, 20, 29)]
        [InlineData(Direction.UP, 20, 31)]
        [InlineData(Direction.LEFT, 21, 30)]
        [InlineData(Direction.RIGHT, 19, 30)]
        [InlineData(Direction.NONE, 20, 30)]
        public void GetNewCoordinatesForHead_GivenADirectionAndPixel_ReturnsNewCoordinatesForTail(Direction direction, int xCoordinate, int yCoordinate)
        {
            // Arrange
            var pixel = new Pixel(20, 30, ConsoleColor.Black, direction);

            // Act
            var (x, y) = SnakeBuilderHelper.GetNewCoordinatesForTail(pixel, direction);

            // Assert
            Assert.Equal(xCoordinate, x);
            Assert.Equal(yCoordinate, y);
        }
    }
}
