using SnakeGame.Source.Enums;
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
            return direction switch
            {
                Direction.DOWN => (pixel.XCoordinate, pixel.YCoordinate + 1),
                Direction.UP => (pixel.XCoordinate, pixel.YCoordinate - 1),
                Direction.LEFT => (pixel.XCoordinate - 1, pixel.YCoordinate),
                Direction.RIGHT => (pixel.XCoordinate + 1, pixel.YCoordinate),
                _ => (pixel.XCoordinate, pixel.YCoordinate),
            };
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
            return direction switch
            {
                Direction.DOWN => (pixel.XCoordinate, pixel.YCoordinate - 1),
                Direction.UP => (pixel.XCoordinate, pixel.YCoordinate + 1),
                Direction.LEFT => (pixel.XCoordinate + 1, pixel.YCoordinate),
                Direction.RIGHT => (pixel.XCoordinate - 1, pixel.YCoordinate),
                _ => (pixel.XCoordinate, pixel.YCoordinate),
            };
        }
    }
}
