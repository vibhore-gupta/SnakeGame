using System;

namespace SnakeGame.ObstacleLayoutStrategies
{
    public class LayoutStrategyBase
    {
        private readonly int width = 30;
        private readonly int height = 20;
        protected readonly string hexUnicode = "\u25A0";

        public void DrawBoundary()
        {
            DrawWidth(width);
            DrawHeight();
            DrawWidth(width + 1);
            DrawHeight(true);
        }

        private void DrawHeight(bool setCursorPosition = false)
        {
            for (var i = 0; i < height; i++)
            {
                if (setCursorPosition)
                {
                    Console.SetCursorPosition(2 * width, i);
                }
                Console.WriteLine(hexUnicode);
            }
        }

        private void DrawWidth(int width)
        {
            for (var i = 0; i < width; i++)
            {
                Console.Write($"{hexUnicode} ");
            }
        }
    }
}
