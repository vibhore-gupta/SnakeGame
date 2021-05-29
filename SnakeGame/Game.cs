using SnakeGame.Source.Common;
using SnakeGame.Source.Levels;
using SnakeGame.Source.Levels.Interfaces;
using System;
using System.Linq;
using System.Threading;

namespace SnakeGame.Source
{
    public class Game
    {
        private int score;
        private int remainingEatCounter = 10;
        private int totalEatCounter;
        private int sleepTime = 500;
        private bool isCurrentLevelCompleted;
        private DateTime foodStartTime = DateTime.Now;
        private ILevel level;
        private int foodX;
        private int foodY;
        private bool isOver;
        private Snake snake;

        private static int levelNumber = 1;
        private static readonly LevelContext levelContext = new LevelContext();
        private static readonly int height = 20;
        private static readonly int totalLevels = 3;
        private static readonly string hexUnicode = "\u25A0";
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

        public Game()
        {
            level = levelContext.Get(levelNumber);
            snake = new Snake(1);
        }

        public void RestartLoop()
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

        private void Upgrade()
        {
            if (isCurrentLevelCompleted)
            {
                CleanUp(true);
                Start();
            }
        }

        public void Start()
        {
            SetUp();
            GameLoop();
        }

        private void GameLoop()
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
                    GetNewLevelFromLevelContext();
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
                if (level.IsGameOver(snake.GetSnakeCoordinates()))
                {
                    DisplayGameOver();
                    break;
                }
                Thread.Sleep(sleepTime);
            }
        }

        private void GetNewLevelFromLevelContext()
        {
            level = levelContext.Get(levelNumber);
        }

        private static void DisplayChampionText()
        {
            DisplayText(0, height + 1, "You are a champion now.");
            Console.WriteLine("");
            Console.WriteLine("Please wait until new levels are added.");
            Console.WriteLine("Press Q to exit...");
        }

        private static bool CheckIfAllLevelsDone()
        {
            return levelNumber == totalLevels;
        }

        private static void IncrementLevel()
        {
            levelNumber += 1;
        }

        private void MarkCurrentLevelCompleted()
        {
            isCurrentLevelCompleted = true;
        }

        private void DisplayLevelCompleted()
        {
            DisplayText(0, height + 1, $"Congrats Level-{levelNumber} completed...");
            Console.WriteLine("");
            Console.WriteLine("Press L to start next level...");
        }

        private bool IsLevelCompleted()
        {
            return remainingEatCounter == 0;
        }

        private void Restart()
        {
            if (isOver)
            {
                CleanUp();
                Start();
            }            
        }

        private void CleanUp(bool nextLevel = false)
        {
            if (nextLevel)
            {
                snake = new Snake(levelNumber * 2);
            }
            else
            {                
                score = 0;
                levelNumber = 1;
                snake = new Snake(levelNumber);
                GetNewLevelFromLevelContext();
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

        private void DisplayGameOver()
        {
            DisplayText(0, height + 1, "Game Over!!!");
            Console.WriteLine("");
            Console.WriteLine("Press R to restart...");
            Console.WriteLine("Press Q to exit...");
            isOver = true;
        }

        private void CheckIfFoodEaten()
        {
            if (IsFoodEaten())
            {
                PerformPostFoodEatingActivities();
            }
        }

        private bool IsFoodEaten()
        {
            var headPixel = snake.GetPixelByBodyType(BodyPartType.HEAD);
            return foodX == headPixel.XCoordinate &&
                foodY == headPixel.YCoordinate;
        }

        private void PerformPostFoodEatingActivities()
        {
            CalculateScore(DateTime.Now);
            EnlargeSnake();
            DecrementRemainingEatCounter();
            IncrementEatCounter();            
            (foodX, foodY) = level.GenerateFood(snake.GetSnakeCoordinates());            
            ChangeSleepTime();
            DisplayStatisticsAndGameControls();            
        }

        private void DecrementRemainingEatCounter()
        {
            remainingEatCounter -= 1;
        }

        private void ChangeSleepTime()
        {
            sleepTime -= 40;
        }

        private void IncrementEatCounter()
        {
            totalEatCounter += 1;
        }

        private void EnlargeSnake()
        {
            snake.Enlarge();
        }

        private void ClearTail()
        {
            var tailPixel = snake.GetPixelByBodyType(BodyPartType.TAIL);
            Console.SetCursorPosition(tailPixel.XCoordinate, tailPixel.YCoordinate);
            Console.Write(" ");
        }

        private void SetUp()
        {
            ClearConsole();
            SetForeGroundColor();
            level.Drawlayout();
            DrawSnake();
            (foodX, foodY) = level.GenerateFood(snake.GetSnakeCoordinates());
            DisplayStatisticsAndGameControls();
        }

        private static void SetForeGroundColor()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void ClearConsole()
        {
            Console.Clear();
        }

        private void DrawSnake()
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

        private void RebuildSnake(ConsoleKey consoleKey = ConsoleKey.Escape)
        {
            snake.ReBuildTailAndBody();
            if(consoleKey != ConsoleKey.Escape)
            {
                snake.ReBuildHeadAccordingToKey(consoleKey);
                return;
            }
            snake.RebuildHead();
        }

        private void DisplayStatisticsAndGameControls()
        {
            Console.SetCursorPosition(65, 0);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Game Statistics:");
            DisplayText(66, 1, $"Level: {levelNumber} of {totalLevels}");
            DisplayText(66, 2, $"Score: {score}");
            DisplayText(66, 3, $"Total Fruits Eaten: {totalEatCounter}");
            var label = remainingEatCounter == 10 ? $"{remainingEatCounter}" : $"0{remainingEatCounter}";
            DisplayText(66, 4, $"Fruits left at this level: {label}");
            label = $"{Math.Round(1000.0M / sleepTime, 1)} blocks/sec";
            DisplayText(66, 5, $"Speed: {label}");
            DisplayText(66, 8, "Controls");
            DisplayText(67, 9, "Move Up: Up Arrow");
            DisplayText(67, 10, "Move Down: Down Arrow");
            DisplayText(67, 11, "Move Left: Left Arrow");
            DisplayText(67, 12, "Move Right: Right Arrow");
        }

        private static void DisplayText(int xCoordinate, int yCoordinate, string text)
        {
            Console.SetCursorPosition(xCoordinate, yCoordinate);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
        }

        private void CalculateScore(DateTime foodEatingTime)
        {            
            score += ScoreCalculatorHelper.CalculateScore(foodEatingTime, foodStartTime, snake.BodyParts.Count, levelNumber);
            foodStartTime = DateTime.Now;
        }
    }
}
