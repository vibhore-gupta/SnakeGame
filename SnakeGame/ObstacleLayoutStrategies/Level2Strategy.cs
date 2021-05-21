using Microsoft.Extensions.Configuration;
using SnakeGame.ObstacleLayoutStrategies.Interfaces;
using System;
using System.IO;

namespace SnakeGame.ObstacleLayoutStrategies
{
    public class Level2Strategy : LayoutStrategyBase, ILayoutDrawer
    {
        public void Draw()
        {
            DrawBoundary();
            SetObstacles();
        }

        private void SetObstacles()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var section = builder.Build().GetSection("level2ObstacleCoordinates");
            foreach (var subSection in section.GetChildren())
            {
                var xCoordinate = subSection.GetValue<int>("x");
                var yCoordinate = subSection.GetValue<int>("y");
                Console.SetCursorPosition(xCoordinate, yCoordinate);
                Console.Write(hexUnicode);
            }
        }

    }
}
