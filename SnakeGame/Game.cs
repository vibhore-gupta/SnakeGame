using System;
using System.Linq;
using System.Threading;

namespace SnakeGame
{
    public class Game
    {
        private static readonly int width = 30;
        private static readonly int height = 20;
        private static readonly string hexUnicode = "\u25A0";
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
        private static readonly ConsoleKey restartKey = ConsoleKey.R;
        private static int score = 0;
        private static int eatCounter = 0;
        private static int sleepTime = 500;
        private static int level = 1;

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
                    else if(key == restartKey)
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
        }
        private static void GameLoop()
        {
            while (true)
            {
                if (IsLevelCompleted())
                {
                    DisplayLevelCompleted();
                    break;
                }
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
                    DisplayGameOver();
                    break;
                }
                Thread.Sleep(sleepTime);
            }
        }

        private static void DisplayLevelCompleted()
        {
            Console.SetCursorPosition(0, height + 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Congrats Level-{level} completed...");
            Console.WriteLine("Press U to start next level...");
        }

        private static bool IsLevelCompleted()
        {
            return eatCounter == 10;
        }

        private static void DrawWidth(int width)
        {
            for (var i = 0; i < width; i++)
            {
                Console.Write($"{hexUnicode} ");
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
                Console.WriteLine(hexUnicode);
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
            snake = new Snake();
            score = 0;
            eatCounter = 0;
            sleepTime = 500;
            level = 1;
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
            Console.WriteLine("Press R to restart...");
            Console.WriteLine("Press Q to exit...");
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
            IncrementEatCounter();
            DrawFood();
            ChangeSleepTime();
        }

        private static void ChangeSleepTime()
        {
            sleepTime -= 40;
        }

        private static void IncrementEatCounter()
        {
            eatCounter += 1;
        }

        private static void EnlargeSnake()
        {
            snake.Enlarge();
        }
        private static void ClearTail()
        {
            var tailPixel = snake.GetTailPixel();
            Console.SetCursorPosition(tailPixel.XCoordinate, tailPixel.YCoordinate);
            Console.Write(" ");
        }
        private static void DrawFood()
        {
            var random = new Random();
            foodX = random.Next(1, width-1);
            foodY = random.Next(1, height-1);
            Console.SetCursorPosition(foodX, foodY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(hexUnicode);
        }
        private static void SetUp()
        {
            ClearConsole();
            SetForeGroundColor();
            //SetUpWindowSize();   // this will throw on platforms macOS, linux. Not supported
            DrawWalls();
            DrawSnake();
            DrawFood();
            DisplayScore();
            DisplayLevel();
        }

        private static void DisplayLevel()
        {
            Console.SetCursorPosition(65, 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Level:{level}");
        }

        private static void SetForeGroundColor()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
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
            Console.Write($"Score:{score}");
        }
        private static void DrawSnake()
        {
            var snakeBodyParts = snake.BodyParts;
            for (var i = 0; i < snakeBodyParts.Count; i++)
            {
                var snakePart = snakeBodyParts[i].Pixel;
                Console.SetCursorPosition(snakePart.XCoordinate, snakePart.YCoordinate);
                Console.ForegroundColor = snakePart.ConsoleColor;
                Console.Write(hexUnicode);
            }
            Console.CursorVisible = false;
        }
        private static void IncrementScore()
        {
            score += 1;
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
