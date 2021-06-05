using SnakeGame.Source.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame.Source.FoodGenerationStrategies
{
    public class FoodDrawStrategyBase
    {
        public (int, int) DrawFoodWithinWallsAndNotOnSnake(List<KeyValuePair<int, int>> coordinates)
        {
            var (foodX, foodY) = GenerateFoodWithinWalls();
            while (coordinates.Any(x => x.Key == foodX && x.Value == foodY))
            {
                (foodX, foodY) = GenerateFoodWithinWalls();
            }
            return (foodX, foodY);       
        }

        private (int, int) GenerateFoodWithinWalls()
        {
            var random = new Random();
            var foodX = random.Next(1, 2 * Constants.width);
            var foodY = random.Next(1, Constants.height);
            return (foodX, foodY);
        }

        protected void DrawFood(int foodX, int foodY)
        {
            Console.SetCursorPosition(foodX, foodY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(Constants.hexUnicode);
        }
    }
}
