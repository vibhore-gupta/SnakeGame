using Microsoft.Extensions.Configuration;
using SnakeGame.Source.BonusRounds;
using SnakeGame.Source.Common;
using SnakeGame.Source.ObstacleLayoutStrategies.Interfaces;
using System;
using System.Collections.Generic;

namespace SnakeGame.Source.ObstacleLayoutStrategies.BonusRoundLayouts
{
    public class BonusRound1Strategy : BonusRoundBase, ILayoutDrawer, IBonusLayoutDrawer
    {
        public void DisplayBonusRoundInstructions()
        {
            Draw();
            DrawBox();
            DisplayText();
        }

        public void Draw()
        {
            DrawBoundary();
        }

        public void DrawBonusRoundLayout()
        {
            MakeRewardDoors();
            DisplayDoorNumbers();
        }

        private void DisplayDoorNumbers()
        {
            for (var i = 3; i < 8; i += 1)
            {
                Console.SetCursorPosition(10, i);
                Console.Write(Constants.hexUnicode);
            }
            for (var i = 28; i < 34; i += 2)
            {
                Console.SetCursorPosition(i, 3);
                Console.Write(Constants.hexUnicode);
            }
            for (var i = 4; i < 6; i += 1)
            {
                Console.SetCursorPosition(32, i);
                Console.Write(Constants.hexUnicode);
            }
            for (var i = 28; i < 32; i += 2)
            {
                Console.SetCursorPosition(i, 5);
                Console.Write(Constants.hexUnicode);
            }
            for (var i = 6; i < 8; i += 1)
            {
                Console.SetCursorPosition(28, i);
                Console.Write(Constants.hexUnicode);
            }
            for (var i = 30; i < 34; i += 2)
            {
                Console.SetCursorPosition(i, 7);
                Console.Write(Constants.hexUnicode);
            }

            for (var i = 48; i < 54; i += 2)
            {
                Console.SetCursorPosition(i, 3);
                Console.Write(Constants.hexUnicode);
            }
            for (var i = 4; i < 6; i += 1)
            {
                Console.SetCursorPosition(52, i);
                Console.Write(Constants.hexUnicode);
            }
            for (var i = 48; i < 52; i += 2)
            {
                Console.SetCursorPosition(i, 5);
                Console.Write(Constants.hexUnicode);
            }
            for (var i = 6; i < 8; i += 1)
            {
                Console.SetCursorPosition(52, i);
                Console.Write(Constants.hexUnicode);
            }
            for (var i = 48; i < 54; i += 2)
            {
                Console.SetCursorPosition(i, 7);
                Console.Write(Constants.hexUnicode);
            }
        }

        private void MakeRewardDoors()
        {
            var doorWidth = 12;
            var doorHeight = 13;
            Console.ForegroundColor = ConsoleColor.Green;
            var section = ConfigurationHelper.GetSection("bonusRound1RewardDoorsCoordinates");
            var doorCounter = 1;
            foreach (var subSection in section.GetChildren())
            {
                var xCoordinate = subSection.GetValue<int>("x");
                var yCoordinate = subSection.GetValue<int>("y");
                for (var i = 0; i < doorWidth; i += 2)
                {
                    Console.SetCursorPosition(i + xCoordinate, yCoordinate);
                    Console.Write(Constants.hexUnicode);
                }
                for (var i = 0; i < doorHeight; i += 1)
                {
                    Console.SetCursorPosition(xCoordinate, i + yCoordinate);
                    Console.Write(Constants.hexUnicode);
                }
                for (var i = 0; i < doorHeight; i += 1)
                {
                    Console.SetCursorPosition(doorWidth + xCoordinate, i + yCoordinate);
                    Console.Write(Constants.hexUnicode);
                }
                for (var i = 2; i < doorWidth; i += 2)
                {
                    Console.SetCursorPosition(i + xCoordinate, doorHeight + 1);
                    Console.Write(Constants.hexUnicode);
                }
                Console.SetCursorPosition(xCoordinate + doorWidth - 1 , yCoordinate + doorHeight/2);
                Console.Write(Constants.hexUnicode);
                doorToCoordinates.Add(doorCounter, new Dictionary<string, List<int>>
                {
                    {"x", new List<int>{ xCoordinate , doorWidth + xCoordinate} },
                    {"y", new List<int>{ yCoordinate, doorHeight -1 + yCoordinate} }
                });
                doorCounter += 1;
            }
        }

        private void DisplayText()
        {
            Console.ForegroundColor = ConsoleColor.White;
            var text = "It's time to try your luck!!!";
            Console.SetCursorPosition(14, 6);
            Console.WriteLine(text);

            text = "You will be given three boxes.";
            Console.SetCursorPosition(10, 8);
            Console.WriteLine(text);

            text = "Direct the snake and hit the walls of ";
            Console.SetCursorPosition(10, 9);
            Console.WriteLine(text);

            text = "the box you want to choose. If you are ";
            Console.SetCursorPosition(10, 10);
            Console.WriteLine(text);

            text = "lucky, you can win score or life ";
            Console.SetCursorPosition(10, 11);
            Console.WriteLine(text);

            text = "boosters. Let the fun begin.";
            Console.SetCursorPosition(10, 12);
            Console.WriteLine(text);

            text = "Press S when you are ready...";
            Console.SetCursorPosition(14, 14);
            Console.WriteLine(text);
        }

        private void DrawBox()
        {
            var boxWidth = 42;
            var boxHeight = 10;
            var star = "*";
            var xCoordinate = 8;
            var yCoordinate = 5;
            Console.ForegroundColor = ConsoleColor.Green;
            for (var i = 0; i < boxWidth; i += 2)
            {
                Console.SetCursorPosition(i + xCoordinate, yCoordinate);
                Console.Write(star);
            }
            for (var i = 0; i < boxHeight; i += 1)
            {
                Console.SetCursorPosition(xCoordinate, i + yCoordinate);
                Console.Write(star);
            }
            for (var i = 0; i < boxHeight; i += 1)
            {
                Console.SetCursorPosition(boxWidth + xCoordinate, i + yCoordinate);
                Console.Write(star);
            }
            for (var i = 0; i < boxWidth + 1; i += 2)
            {
                Console.SetCursorPosition(i + xCoordinate, boxHeight + yCoordinate);
                Console.Write(star);
            }
        }
    }
}
