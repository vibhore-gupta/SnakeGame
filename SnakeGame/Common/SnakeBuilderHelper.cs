using System;
using System.Collections.Generic;

namespace SnakeGame.Source.Common
{
    public class SnakeBuilderHelper
    {
        private static readonly Dictionary<ConsoleKey, Direction> KeyToDirection = new Dictionary<ConsoleKey, Direction>
        {
            {ConsoleKey.UpArrow, Direction.UP },
            {ConsoleKey.DownArrow, Direction.DOWN },
            {ConsoleKey.LeftArrow, Direction.LEFT },
            {ConsoleKey.RightArrow, Direction.RIGHT }
        };

        public static (int, int) GetNewCoordinatesForHead(Pixel pixel, Direction direction)
        {
            switch (direction)
            {
                case Direction.DOWN:
                    return (pixel.XCoordinate, pixel.YCoordinate + 1);
                case Direction.UP:
                    return (pixel.XCoordinate, pixel.YCoordinate - 1);
                case Direction.LEFT:
                    return (pixel.XCoordinate - 1, pixel.YCoordinate);
                case Direction.RIGHT:
                    return (pixel.XCoordinate + 1, pixel.YCoordinate);
                default:
                    return (pixel.XCoordinate, pixel.YCoordinate);
            }
        }

        public static (int, int) GetNewCoordinatesForHead(Pixel pixel, ConsoleKey consoleKey)
        {
            var newDirection = KeyToDirection[consoleKey];
            return GetNewCoordinatesForHead(pixel, newDirection);
        }

        public static Direction GetNewDirectionForHead(ConsoleKey consoleKey)
        {
            return KeyToDirection[consoleKey];
        }

        public static (int, int) GetNewCoordinatesForTail(Pixel pixel, Direction direction)
        {
            switch (direction)
            {
                case Direction.DOWN:
                    return (pixel.XCoordinate, pixel.YCoordinate - 1);
                case Direction.UP:
                    return (pixel.XCoordinate, pixel.YCoordinate + 1);
                case Direction.LEFT:
                    return (pixel.XCoordinate + 1, pixel.YCoordinate);
                case Direction.RIGHT:
                    return (pixel.XCoordinate - 1, pixel.YCoordinate);
                default:
                    return (pixel.XCoordinate, pixel.YCoordinate);
            }
        }
    }
}
