using SnakeGame.GameOverStrategies;
using SnakeGame.ObstacleLayoutStrategies;
using System;
using System.Linq;
using System.Threading;

namespace SnakeGame
{
    public class Game
    {
        private static readonly int width = 30;
        private static readonly int height = 20;
        private static readonly int totalLevels = 5;
        private static readonly string hexUnicode = "\u25A0";
        private static int foodX = 0;
        private static int foodY = 0;
        private static bool isOver = false;
        private static Snake snake = new Snake();
        private static readonly LayoutContext layoutContext = new LayoutContext();
        private static readonly GameOverContext gameOverContext = new GameOverContext();
        private static readonly ConsoleKey[] AllowedKeys = new ConsoleKey[]
        {
            ConsoleKey.UpArrow,
            ConsoleKey.DownArrow,
            ConsoleKey.LeftArrow,
            ConsoleKey.RightArrow
        };
        private static readonly ConsoleKey quitKey = ConsoleKey.Q;
        private static readonly ConsoleKey restartKey = ConsoleKey.R;
        private static readonly ConsoleKey levelUpgradeKey = ConsoleKey.L;
        private static int score = 0;
        private static int eatCounter = 0;
        private static int sleepTime = 500;
        private static int level = 1;
        private static bool isCurrentLevelCompleted = false;

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
                    else if(key == levelUpgradeKey)
                    {
                        Upgrade();
                    }
                }
            }
        }

        private static void Upgrade()
        {
            if (isCurrentLevelCompleted)
            {
                CleanUp(true);
                Start();
            }
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
                    MarkCurrentLevelCompleted();
                    IncrementLevel();
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
                var headPixel = snake.GetPixelByBodyType(BodyPartType.HEAD);
                if (gameOverContext.IsOver(level, headPixel.XCoordinate, headPixel.YCoordinate))
                {
                    DisplayGameOver();
                    break;
                }
                Thread.Sleep(sleepTime);
            }
        }

        private static void IncrementLevel()
        {
            level += 1;
        }

        private static void MarkCurrentLevelCompleted()
        {
            isCurrentLevelCompleted = true;
        }

        private static void DisplayLevelCompleted()
        {
            Console.SetCursorPosition(0, height + 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Congrats Level-{level} completed...");
            Console.WriteLine("Press L to start next level...");
        }

        private static bool IsLevelCompleted()
        {
            return eatCounter == 10;
        }

        private static void Restart()
        {
            if (isOver)
            {
                CleanUp();
                Start();
            }            
        }
        private static void CleanUp(bool nextLevel = false)
        {
            if (!nextLevel)
            {
                score = 0;
                level = 1;
            }
            snake = new Snake();
            eatCounter = 0;
            sleepTime = 500;
            isCurrentLevelCompleted = false;
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
            var headPixel = snake.GetPixelByBodyType(BodyPartType.HEAD);
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
            var tailPixel = snake.GetPixelByBodyType(BodyPartType.TAIL);
            Console.SetCursorPosition(tailPixel.XCoordinate, tailPixel.YCoordinate);
            Console.Write(" ");
        }
        private static void DrawFood()
        {
            var random = new Random();
            foodX = random.Next(1, width);
            foodY = random.Next(1, height);
            Console.SetCursorPosition(foodX, foodY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(hexUnicode);
        }
        private static void SetUp()
        {
            ClearConsole();
            SetForeGroundColor();
            layoutContext.DrawObstaclesForLevel(level);
            DrawSnake();
            DrawFood();
            DisplayScore();
            DisplayLevel();
        }

        private static void DisplayLevel()
        {
            Console.SetCursorPosition(65, 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Level: {level} of {totalLevels}");
        }

        private static void SetForeGroundColor()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void ClearConsole()
        {
            Console.Clear();
        }

        private static void DisplayScore()
        {
            Console.SetCursorPosition(65, 0);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Score: {score}");
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
