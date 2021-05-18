using System;
using Xunit;

namespace SnakeGame.Tests
{
    public class PixelTests
    {
        [Fact]
        public void SetConsoleColor_ShouldSetConsoleForeGroundColor()
        {
            // Arrange
            var pixel = new Pixel(20, 30, ConsoleColor.Black, Direction.RIGHT);

            // Act
            pixel.SetConsoleColor(ConsoleColor.Blue);

            // Assert
            Assert.Equal(ConsoleColor.Blue, Console.ForegroundColor);
        }
    }
}
