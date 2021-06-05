using SnakeGame.Source.Enums;
using System;

namespace SnakeGame.Source
{
    public class Pixel
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public ConsoleColor ConsoleColor { get; set; }
        public Direction CurrentDirection { get; set; }
        public Pixel(int xCoordinate, int yCoordinate, ConsoleColor consoleColor, Direction currentDirection)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            ConsoleColor = consoleColor;
            CurrentDirection = currentDirection;
        }
    }
}
