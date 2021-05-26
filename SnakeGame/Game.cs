using SnakeGame.Source.FoodGenerationStrategies;
using SnakeGame.Source.GameOverStrategies;
using SnakeGame.Source.ObstacleLayoutStrategies;
using System;
using System.Linq;
using System.Threading;

namespace SnakeGame.Source
{
    public class Game
    {
        private static DateTime foodStartTime = DateTime.Now;
        private static readonly int height = 20;
        private static readonly int totalLevels = 3;
        private static readonly string hexUnicode = "\u25A0";
        private static int foodX = 0;
        private static int foodY = 0;
        private static bool isOver = false;
        private static Snake snake = new Snake(1);
        private static readonly LayoutContext layoutContext = new LayoutContext();
        private static readonly GameOverContext gameOverContext = new GameOverContext();
        private static readonly FoodDrawContext foodDrawContext = new FoodDrawContext();
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
        private static int remainingEatCounter = 10;
        private static int totalEatCounter = 0;
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
                    if (CheckIfAllLevelsDone())
                    {
                        DisplayChampionText();
                        break;
                    }
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

        private static void DisplayChampionText()
        {
            Console.SetCursorPosition(0, height + 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"You are a champion now.");
            Console.WriteLine("Please wait until new levels are added.");
            Console.WriteLine("Press Q to exit...");
        }

        private static bool CheckIfAllLevelsDone()
        {
            return level == totalLevels;
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
            return remainingEatCounter == 0;
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
            if (nextLevel)
            {
                snake = new Snake(level * 2);
            }
            else
            {                
                score = 0;
                level = 1;
                snake = new Snake(level);
                totalEatCounter = 0;
            }
            remainingEatCounter = 10;
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
            DecrementRemainingEatCounter();
            IncrementEatCounter();            
            (foodX, foodY) = foodDrawContext.DrawFoodForLevel(level, snake.GetSnakeCoordinates());
            CalculateScore();
            ChangeSleepTime();
            DisplayStatistics();            
        }

        private static void DecrementRemainingEatCounter()
        {
            remainingEatCounter -= 1;
        }

        private static void ChangeSleepTime()
        {
            sleepTime -= 40;
        }

        private static void IncrementEatCounter()
        {
            totalEatCounter += 1;
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

        private static void SetUp()
        {
            ClearConsole();
            SetForeGroundColor();
            layoutContext.DrawObstaclesForLevel(level);
            DrawSnake();
            (foodX, foodY) = foodDrawContext.DrawFoodForLevel(level, snake.GetSnakeCoordinates());
            DisplayStatistics();
            DisplayControls();
        }

        private static void DisplayLevel()
        {
            Console.SetCursorPosition(66, 1);
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

        private static void DisplayTotalEatCounter()
        {
            Console.SetCursorPosition(66, 3);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Total Fruits Eaten: {totalEatCounter}");
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

        private static void DisplayStatistics()
        {
            Console.SetCursorPosition(65, 0);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Game Statistics:");
            DisplayLevel();
            DisplayScore();
            DisplayTotalEatCounter();            
            DisplayFruitsToBeEatenAtThisLevel();
            DisplaySnakeSpeed();
        }

        private static void DisplayScore()
        {
            Console.SetCursorPosition(66, 2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Score: {score}");
        }

        private static void DisplayFruitsToBeEatenAtThisLevel()
        {
            Console.SetCursorPosition(66, 4);
            Console.ForegroundColor = ConsoleColor.Red;
            var label = remainingEatCounter == 10 ? $"{remainingEatCounter}" : $"0{remainingEatCounter}";
            Console.Write($"Fruits left at this level: {label}");
        }

        private static void DisplaySnakeSpeed()
        {
            Console.SetCursorPosition(66, 5);
            Console.ForegroundColor = ConsoleColor.Red;
            var label = $"{Math.Round(1000.0M/sleepTime, 1)} blocks/sec";
            Console.Write($"Speed: {label}");
        }

        private static void DisplayControls()
        {
            Console.SetCursorPosition(66, 8);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Controls");

            Console.SetCursorPosition(67, 9);
            Console.WriteLine("Move Up: Up Arrow");
            Console.SetCursorPosition(67, 10);
            Console.WriteLine("Move Down: Down Arrow");
            Console.SetCursorPosition(67, 11);
            Console.WriteLine("Move Left: Left Arrow");
            Console.SetCursorPosition(67, 12);
            Console.WriteLine("Move Right: Right Arrow");
        }

        private static void CalculateScore()
        {
            var elapsedTimeInSeconds = (DateTime.Now - foodStartTime).TotalSeconds;
            var timeCoefficient = 0.5;
            var lengthCoefficient = 0.25;
            var levelCoefficient = 0.25;
            var timeFactor = 100 - (elapsedTimeInSeconds * timeCoefficient) < 0 ? 0 
                : 100 - (elapsedTimeInSeconds * timeCoefficient);
            var snakeLengthFactor = snake.BodyParts.Count * lengthCoefficient;
            var levelFactor = level * levelCoefficient;
            var newScore = timeFactor * snakeLengthFactor * levelFactor;
            score += (int)newScore;
            foodStartTime = DateTime.Now;
        }
    }
}
