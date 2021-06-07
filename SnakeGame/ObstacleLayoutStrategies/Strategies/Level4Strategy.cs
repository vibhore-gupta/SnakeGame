using Microsoft.Extensions.Configuration;
using SnakeGame.Source.Common;
using SnakeGame.Source.ObstacleLayoutStrategies.Interfaces;
using System;

namespace SnakeGame.Source.ObstacleLayoutStrategies.Strategies
{
    public class Level4Strategy : LayoutStrategyBase, ILayoutDrawer
    {
        public void Draw()
        {
            DrawBoundary();
            SetObstacles();
        }

        private void SetObstacles()
        {
            var section = ConfigurationHelper.GetSection("level4ObstacleCoordinates");
            foreach (var subSection in section.GetChildren())
            {
                var xCoordinate = subSection.GetValue<int>("x");
                var yCoordinate = subSection.GetValue<int>("y");
                Console.SetCursorPosition(xCoordinate, yCoordinate);
                Console.Write(Constants.hexUnicode);
            }
        }
    }
}
