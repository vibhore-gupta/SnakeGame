using Microsoft.Extensions.Configuration;
using SnakeGame.Source.Common;
using SnakeGame.Source.GameOverStrategies.Interfaces;
using System.Collections.Generic;

namespace SnakeGame.Source.GameOverStrategies
{
    public class Level3Strategy : GameOverStrategyBase, IGameOver
    {
        public bool IsOver(List<KeyValuePair<int, int>> coordinates)
        {
            return IsHittingTheWallsOrHittingItself(coordinates)
                || IsHittingTheObstacles(coordinates);
        }

        private bool IsHittingTheObstacles(List<KeyValuePair<int, int>> coordinates)
        {
            var headXCoordinate = coordinates[^1].Key;
            var headYCoordinate = coordinates[^1].Value;
            var section = ConfigurationHelper.GetSection("level3ObstacleCoordinates");
            foreach (var subSection in section.GetChildren())
            {
                var xCoordinate = subSection.GetValue<int>("x");
                var yCoordinate = subSection.GetValue<int>("y");
                if(headXCoordinate == xCoordinate && headYCoordinate == yCoordinate)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
