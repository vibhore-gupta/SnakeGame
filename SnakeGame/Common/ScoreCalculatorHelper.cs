using System;

namespace SnakeGame.Source.Common
{
    public class ScoreCalculatorHelper
    {
        public static int CalculateScore(DateTime foodEatingTime, DateTime foodStartTime, int bodyPartCount, int level)
        {
            var elapsedTimeInSeconds = (foodEatingTime - foodStartTime).TotalSeconds;
            var timeCoefficient = 0.5;
            var lengthCoefficient = 0.25;
            var levelCoefficient = 0.25;
            var timeFactor = (elapsedTimeInSeconds >= 100 || elapsedTimeInSeconds < 0) ? 0
                : (100 - elapsedTimeInSeconds) * timeCoefficient;
            var snakeLengthFactor = bodyPartCount * lengthCoefficient;
            var levelFactor = level * levelCoefficient;
            var newScore = timeFactor * snakeLengthFactor * levelFactor;
            return (int)newScore;
        }
    }
}
