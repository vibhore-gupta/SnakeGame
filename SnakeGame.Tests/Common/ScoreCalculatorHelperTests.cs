using SnakeGame.Source.Common;
using System;
using Xunit;

namespace SnakeGame.Tests.Common
{
    public class ScoreCalculatorHelperTests
    {
        [Theory]
        [InlineData(-50, 3, 1, 4)]
        [InlineData(0, 3, 1, 9)]
        [InlineData(0, 10, 1, 31)]
        [InlineData(0, 10, 3, 93)]
        [InlineData(-15, 5, 3, 39)]
        [InlineData(-30, 3, 1, 6)]
        [InlineData(-45, 6, 2, 20)]
        [InlineData(45, 6, 2, 0)]
        [InlineData(75, 6, 2, 0)]
        public void ScoreCalculatorHelper_WhenCalledCalculateScore_ReturnsNewScore(int elapsedSeconds, int bodyPartCount, int level, int expectedScore)
        {
            // Arrange

            // Act
            var currentDateTime = DateTime.Now;
            var newScore = ScoreCalculatorHelper.CalculateScore(currentDateTime, currentDateTime.AddSeconds(elapsedSeconds), bodyPartCount, level);

            //Assert
            Assert.Equal(expectedScore, newScore);
        }
    }
}
