using Microsoft.Extensions.Configuration;
using SnakeGame.Source.Common;
using SnakeGame.Source.FoodGenerationStrategies.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame.Source.FoodGenerationStrategies
{
    public class Level2Strategy : FoodDrawStrategyBase, IFoodDrawer
    {
        public (int, int) Draw(List<KeyValuePair<int, int>> coordinates)
        {
            var (foodX, foodY) = DrawFoodWithinWallsAndOnSnake(coordinates);
            var section = ConfigurationHelper.GetSection("level2ObstacleCoordinates").GetChildren();
            while (section.Any(x => x.GetValue<int>("x") == foodX && x.GetValue<int>("y") == foodY))
            {
                (foodX, foodY) = DrawFoodWithinWallsAndOnSnake(coordinates);
            }            
            DrawFood(foodX, foodY);
            return (foodX, foodY);
        }
    }
}
