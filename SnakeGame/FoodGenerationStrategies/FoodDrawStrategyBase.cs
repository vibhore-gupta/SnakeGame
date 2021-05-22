using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame.Source.FoodGenerationStrategies
{
    public class FoodDrawStrategyBase
    {
        private readonly int width = 30;
        private readonly int height = 20;
        protected readonly string hexUnicode = "\u25A0";

        public (int, int) DrawFoodWithinWallsAndOnSnake(List<KeyValuePair<int, int>> coordinates)
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
            var foodX = random.Next(1, width);
            var foodY = random.Next(1, height);
            return (foodX, foodY);
        }

        protected void DrawFood(int foodX, int foodY)
        {
            Console.SetCursorPosition(foodX, foodY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(hexUnicode);
        }
    }
}
