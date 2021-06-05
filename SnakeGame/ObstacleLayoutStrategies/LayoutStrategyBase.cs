using SnakeGame.Source.Common;
using System;

namespace SnakeGame.Source.ObstacleLayoutStrategies
{
    public class LayoutStrategyBase
    {
        public void DrawBoundary()
        {
            Console.SetCursorPosition(0,0);
            Console.ForegroundColor = ConsoleColor.Gray;
            DrawWidth(Constants.width);
            DrawHeight();
            DrawWidth(Constants.width + 1);
            DrawHeight(true);
        }

        private void DrawHeight(bool setCursorPosition = false)
        {
            for (var i = 0; i < Constants.height; i++)
            {
                if (setCursorPosition)
                {
                    Console.SetCursorPosition(2 * Constants.width, i);
                }
                Console.WriteLine(Constants.hexUnicode);
            }
        }

        private void DrawWidth(int width)
        {
            for (var i = 0; i < width; i++)
            {
                Console.Write($"{Constants.hexUnicode} ");
            }
        }
    }
}
