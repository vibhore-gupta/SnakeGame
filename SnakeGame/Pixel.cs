using System;

namespace SnakeGame
{
    public class Pixel
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public ConsoleColor ConsoleColor { get; }
        public Direction CurrentDirection { get; set; }
        public Pixel(int xCoordinate, int yCoordinate, ConsoleColor consoleColor, Direction currentDirection)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            ConsoleColor = consoleColor;
            CurrentDirection = currentDirection;
        }

        public void SetConsoleColor(ConsoleColor color)
        {
            Console.SetCursorPosition(XCoordinate, YCoordinate);
            Console.ForegroundColor = color;
            Console.Write("■");
        }
    }
}
