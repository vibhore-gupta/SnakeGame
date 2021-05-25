using SnakeGame.Source.FoodGenerationStrategies.Interfaces;
using System.Collections.Generic;

namespace SnakeGame.Source.FoodGenerationStrategies
{
    public class Level1Strategy : FoodDrawStrategyBase, IFoodDrawer
    {
        public (int, int) Draw(List<KeyValuePair<int, int>> coordinates)
        {
            var (foodX, foodY) = DrawFoodWithinWallsAndNotOnSnake(coordinates);
            DrawFood(foodX, foodY);
            return (foodX, foodY);
        }
    }
}
