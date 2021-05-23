﻿using Microsoft.Extensions.Configuration;
using SnakeGame.Source.Common;
using SnakeGame.Source.GameOverStrategies.Interfaces;

namespace SnakeGame.Source.GameOverStrategies
{
    public class Level2Strategy : GameOverStrategyBase, IGameOver
    {
        public bool IsOver(int headXCoordinate, int headYCoordinate)
        {
            return IsHittingTheWalls(headXCoordinate, headYCoordinate)
                || IsHittingTheObstacles(headXCoordinate, headYCoordinate);
        }

        private bool IsHittingTheObstacles(int headXCoordinate, int headYCoordinate)
        {
            var section = ConfigurationHelper.GetSection("level2ObstacleCoordinates");
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