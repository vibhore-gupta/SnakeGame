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
        //public static (int, int) ReDraw(List<KeyValuePair<int, int>> coordinates, Direction headDirection, int foodX, int foodY)
        //{
        //    var headX = coordinates[^1].Key;
        //    var headY = coordinates[^1].Value;
        //    var newFoodX = foodX;
        //    var newFoodY = foodY;
        //    (newFoodX, newFoodY) = GetNewCoordinatesForFood(newFoodX, newFoodY, headDirection);
        //    if (NewFoodIsOutSideBoundary(newFoodX, newFoodY))
        //    {
        //        if (newFoodX <= 0 || newFoodX >= 60)
        //        {
        //            if (foodY == headY)
        //            {
        //                var diff1 = foodY;
        //                var diff2 = 20 - foodY;
        //                if (diff1 > diff2)
        //                {
        //                    (newFoodX, newFoodY) = GetNewCoordinatesForFood(foodX, foodY, Direction.UP);
        //                }
        //                else
        //                {
        //                    (newFoodX, newFoodY) = GetNewCoordinatesForFood(foodX, foodY, Direction.DOWN);
        //                }
        //            }
        //            else if (foodY > headY)
        //            {
        //                (newFoodX, newFoodY) = GetNewCoordinatesForFood(foodX, foodY, Direction.DOWN);
        //            }
        //            else
        //            {
        //                (newFoodX, newFoodY) = GetNewCoordinatesForFood(foodX, foodY, Direction.UP);
        //            }
        //        }
        //        else if (newFoodY >= 20 || newFoodY <= 0)
        //        {
        //            if (foodX == headX)
        //            {
        //                var diff1 = foodX;
        //                var diff2 = 60 - foodX;
        //                if (diff1 > diff2)
        //                {
        //                    (newFoodX, newFoodY) = GetNewCoordinatesForFood(foodX, foodY, Direction.LEFT);
        //                }
        //                else
        //                {
        //                    (newFoodX, newFoodY) = GetNewCoordinatesForFood(foodX, foodY, Direction.RIGHT);
        //                }
        //            }
        //            else if (foodX > headX)
        //            {
        //                (newFoodX, newFoodY) = GetNewCoordinatesForFood(foodX, foodY, Direction.RIGHT);
        //            }
        //            else
        //            {
        //                (newFoodX, newFoodY) = GetNewCoordinatesForFood(foodX, foodY, Direction.LEFT);
        //            }
        //        }
        //    }
        //    return (newFoodX, newFoodY);
        //}

        //private static bool NewFoodIsOutSideBoundary(int newFoodX, int newFoodY)
        //{
        //    return newFoodX <= 0 || newFoodX >= 60 || newFoodY <= 0 || newFoodY >= 20;
        //}

        //private static (int, int) GetNewCoordinatesForFood(int newFoodX, int newFoodY, Direction direction)
        //{
        //    switch (direction)
        //    {
        //        case Direction.DOWN:
        //            return (newFoodX, newFoodY + 2);
        //        case Direction.UP:
        //            return (newFoodX, newFoodY - 2);
        //        case Direction.LEFT:
        //            return (newFoodX - 1, newFoodY);
        //        case Direction.RIGHT:
        //            return (newFoodX + 1, newFoodY);
        //        default:
        //            return (newFoodX, newFoodY);
        //    }
        //}
    }
}
