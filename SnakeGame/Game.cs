using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    public class Game
    {
        private static readonly int width = 30;
        private static readonly int height = 20;
        private static int foodX = 0;
        private static int foodY = 0;
        private static bool isOver = false;
        private static Snake snake = new Snake();
        private static readonly ConsoleKey[] AllowedKeys = new ConsoleKey[]
        {
            ConsoleKey.UpArrow,
            ConsoleKey.DownArrow,
            ConsoleKey.LeftArrow,
            ConsoleKey.RightArrow
        };
        private static readonly ConsoleKey quitKey = ConsoleKey.Q;
        public static int Score { get; private set; } = 0;

        public static void RestartLoop()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    if (key == quitKey)
                    {
                        End();
                    }
                    else
                    {
                        Restart();
                    }
                }
            }
        }
        private static void DrawWalls()
        {
            DrawWidth(width);
            DrawHeight();
            DrawWidth(width + 1);
            DrawHeight(true);
        }
        public static void Start()
        {
            SetUp();
            GameLoop();
            DisplayGameOver();
        }
        private static void GameLoop()
        {
            while (true)
            {
                ClearTail();
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    if (AllowedKeys.Contains(key))
                    {
                        RebuildSnake(key);
                    }
                    else
                    {
                        RebuildSnake();
                    }
                }
                else
                {
                    RebuildSnake();
                }
                DrawSnake();
                CheckIfFoodEaten();
                if (IsOver())
                {
                    break;
                }
                Thread.Sleep(500);
            }
        }
        private static void DrawWidth(int width)
        {
            for (var i = 0; i < width; i++)
            {
                Console.Write("■ ");
            }
        }
        private static void DrawHeight(bool setCursorPosition = false)
        {
            for (var i = 0; i < height; i++)
            {
                if (setCursorPosition)
                {
                    Console.SetCursorPosition(2 * width, i);
                }                
                Console.WriteLine("■");
            }
        }
        private static bool IsOver()
        {
            var headPixel = snake.GetHeadPixel();
            var headXCoordinate = headPixel.XCoordinate;
            var headYCoordinate = headPixel.YCoordinate;
            var twiceWidth = 2 * width;

            return headXCoordinate == 0 && headYCoordinate >= 0 && headYCoordinate <= height // left wall hit
                || headYCoordinate == height && headXCoordinate >= 0 && headXCoordinate <= twiceWidth // bottom wall hit
                || headXCoordinate == twiceWidth && headYCoordinate >= 0 && headYCoordinate <= height // right wall hit
                || headYCoordinate == 0 && headXCoordinate >= 0 && headXCoordinate <= twiceWidth; // top wall hit
        }
        private static void Restart()
        {
            if (isOver)
            {
                CleanUp();
                Start();
            }            
        }
        private static void CleanUp()
        {
            Console.Clear();
            snake = new Snake();
            Console.ForegroundColor = ConsoleColor.Gray;
            Score = 0;
        }
        private static void End()
        {
            Environment.Exit(0);
        }
        private static void DisplayGameOver()
        {
            Console.SetCursorPosition(0, height + 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Game Over!!!");
            Console.WriteLine("Press any key to restart...");
            Console.WriteLine("Press q to exit...");
            isOver = true;
        }
        private static void CheckIfFoodEaten()
        {
            if (IsFoodEaten())
            {
                PerformPostFoodEatingActivities();
            }
        }
        private static bool IsFoodEaten()
        {
            var headPixel = snake.GetHeadPixel();
            return foodX == headPixel.XCoordinate &&
                foodY == headPixel.YCoordinate;
        }
        private static void PerformPostFoodEatingActivities()
        {
            EnlargeSnake();
            IncrementScore();
            DrawFood();
        }
        private static void EnlargeSnake()
        {
            snake.Enlarge();
        }
        private static void ClearTail()
        {
            var tailPixel = snake.GetTailPixel();
            tailPixel.SetConsoleColor(Console.BackgroundColor);
        }
        private static void DrawFood()
        {
            var random = new Random();
            foodX = random.Next(1, width-1);
            foodY = random.Next(1, height-1);
            Console.SetCursorPosition(foodX, foodY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("■");
        }
        private static void SetUp()
        {
            ClearConsole();
            SetUpWindowSize();
            DrawWalls();
            DrawSnake();
            DrawFood();
            DisplayScore();
        }
        private static void ClearConsole()
        {
            Console.Clear();
        }
        private static void SetUpWindowSize()
        {
            Console.WindowHeight = 25;
            Console.WindowWidth = 75;
        }
        private static void DisplayScore()
        {
            Console.SetCursorPosition(65, 0);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Score:{Score}");
        }
        private static void DrawSnake()
        {
            var snakeBodyParts = snake.BodyParts;
            for (var i = 0; i < snakeBodyParts.Count; i++)
            {
                var snakePart = snakeBodyParts[i].Pixel;
                Console.SetCursorPosition(snakePart.XCoordinate, snakePart.YCoordinate);
                Console.ForegroundColor = snakePart.ConsoleColor;
                Console.Write("■");
            }
            Console.CursorVisible = false;
        }
        private static void IncrementScore()
        {
            Score += 1;
            DisplayScore();
        }
        private static void RebuildSnake(ConsoleKey consoleKey = ConsoleKey.Escape)
        {
            snake.ReBuildTailAndBody();
            if(consoleKey != ConsoleKey.Escape)
            {
                snake.ReBuildHeadAccordingToKey(consoleKey);
                return;
            }
            snake.RebuildHead();
        }
    }
}
