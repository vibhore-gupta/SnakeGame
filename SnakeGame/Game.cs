using SnakeGame.Source.BonusRounds;
using SnakeGame.Source.BonusRounds.Interfaces;
using SnakeGame.Source.Common;
using SnakeGame.Source.Enums;
using SnakeGame.Source.Levels;
using SnakeGame.Source.Levels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SnakeGame.Source
{
    public class Game
    {
        private int score;
        private int remainingEatCounter = 10;
        private int totalEatCounter;
        private int sleepTime;
        private bool isCurrentLevelCompleted;
        private DateTime foodStartTime = DateTime.Now;
        private ILevel level;
        private IBonusRound bonusRound;
        private int foodX;
        private int foodY;
        private bool isOver;
        private Snake snake;
        private int levelNumber = 1;
        private bool isBonusRound;
        private bool restartRound;
        private int lives;

        private static readonly LevelContext levelContext = new LevelContext();
        private static readonly BonusContext bonusContext = new BonusContext();
        private static readonly int totalLevels = 4;
        private static readonly ConsoleKey[] AllowedKeys = new ConsoleKey[]
        {
            ConsoleKey.UpArrow,
            ConsoleKey.DownArrow,
            ConsoleKey.LeftArrow,
            ConsoleKey.RightArrow
        };

        public Game()
        {
            level = levelContext.Get(levelNumber);
            snake = new Snake(1);
            sleepTime = Constants.levelToSleepTime[levelNumber].Key;
        }

        public void RestartLoop()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.Q)
                    {
                        End();
                    }
                    else if(key == ConsoleKey.R)
                    {
                        Restart();
                    }
                    else if(key == ConsoleKey.L && !isBonusRound)
                    {
                        Upgrade();
                    }
                    else if (key == ConsoleKey.B)
                    {
                        DisplayBonusRoundInstructions();
                    }
                    else if (key == ConsoleKey.S)
                    {
                        StartBonusRound();
                    }
                    else if (key == ConsoleKey.C)
                    {
                        RestartRound();
                    }
                }
            }
        }

        private void RestartRound()
        {
            if (restartRound)
            {
                ClearConsoleByCoordinates(new List<KeyValuePair<int, int>>
                {
                    new KeyValuePair<int, int>(0, Constants.height + 1),
                    new KeyValuePair<int, int>(0, Constants.height + 2)
                }, 100);
                
                snake.Reposition();
                level.Drawlayout();
                DisplayStatisticsAndGameControls();
                restartRound = false;
                GameLoop();
            }            
        }

        private void StartBonusRound()
        {            
            if (isBonusRound)
            {
                ClearConsole();
                DisplayStatisticsAndGameControls();
                bonusRound.DrawBonusRoundLayout();
                bonusRound.SetPrizesBehindDoors(new Dictionary<PrizeType, int> { { PrizeType.LIFEBOOSTER, 1 } });
                snake = new Snake(1);
                DrawSnake(snake);
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
                    DrawSnake(snake);
                    var headX = snake.GetPixelByBodyType(BodyPartType.HEAD).XCoordinate;
                    var headY = snake.GetPixelByBodyType(BodyPartType.HEAD).YCoordinate;
                    var prize = bonusRound.GetPrizeWon(new KeyValuePair<int, int>(headX, headY));
                    if (prize != PrizeType.NONE)
                    {
                        ApplyPrizeBooster(prize);
                        DisplayPrize(prize);                        
                        isBonusRound = false;
                        break;
                    }
                    Thread.Sleep(400);
                }
            }
        }

        private void ApplyPrizeBooster(PrizeType prize)
        {
            switch (prize)
            {
                case PrizeType.LIFEBOOSTER:
                    lives++;
                    break;
                case PrizeType.SCOREBOOSTER:
                    score += 100;
                    break;
            }
        }

        private void DisplayPrize(PrizeType prize)
        {
            var label = "Hard Luck!!! You won nothing this time. Keep up the spirits.\nYou are doing very well. Better luck next time.";
            switch (prize)
            {
                case PrizeType.LIFEBOOSTER:
                    label = "Yay!!! You are rewarded with one extra life for the snake.\nMake sure you make it count.";
                    break;
                case PrizeType.SCOREBOOSTER:
                    label = "Yay!!! You are rewarded with an extra score of 100.\nYou are on your way to become the undisputed champion.";
                    break;
            }
            DisplayText(0, Constants.height + 1, label);
            Console.WriteLine("");
            Console.WriteLine("Press L to start next level...");
        }

        private void DisplayBonusRoundInstructions()
        {
            if (isBonusRound)
            {
                ClearConsole();
                SetForeGroundColor(ConsoleColor.Gray);
                bonusRound.ShowBonusRoundInstructions();
                DisplayStatisticsAndGameControls();
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
                //if (false)
                //{
                //    if (IsSnakeApproaching())
                //    {
                //        Console.SetCursorPosition(foodX, foodY);
                //        Console.Write(" ");
                //        (foodX, foodY) = GetNewFoodCoordinates();
                //        RedrawFood();
                //    }
                //}
                if (IsLevelCompleted())
                {
                    if (CheckIfAllLevelsDone())
                    {
                        DisplayChampionText();
                        break;
                    }
                    SetAndCheckBonusRound();
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
                DrawSnake(snake);
                CheckIfFoodEaten();
                if (level.IsGameOver(snake.GetSnakeCoordinates()))
                {
                    lives--;
                    DisplayGameOver();
                    break;
                }
                Thread.Sleep(sleepTime);
            }
        }

        private void RedrawFood()
        {
            Console.SetCursorPosition(foodX, foodY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(Constants.hexUnicode);
        }

        //private (int foodX, int foodY) GetNewFoodCoordinates()
        //{
        //    var headPixel = snake.GetPixelByBodyType(BodyPartType.HEAD);
        //    return SnakeBuilderHelper.ReDraw(snake.GetSnakeCoordinates(), headPixel.CurrentDirection, foodX, foodY);
        //}

        private bool IsSnakeApproaching()
        {
            var headX = snake.BodyParts[^1].Pixel.XCoordinate;
            var headY = snake.BodyParts[^1].Pixel.YCoordinate;

            return (foodX - headX >= 0 && foodX - headX <= 3 && foodY - headY >= 0 && foodY - headY <= 1)
                || (foodX - headX >= 0 && foodX - headX <= 3 && headY - foodY >= 0 && headY - foodY <= 1) // left side approach
                || (foodY - headY >= 0 && foodY - headY <= 1 && foodX - headX >= 0 && foodX - headX <= 3)
                || (foodY - headY >= 0 && foodY - headY <= 1 && headX - foodX >= 0 && headX - foodX <= 3) // top side approach
                || (headX - foodX >= 0 && headX - foodX <= 3 && foodY - headY >= 0 && foodY - headY <= 1)
                || (headX - foodX >= 0 && headX - foodX <= 3 && headY - foodY >= 0 && headY - foodY <= 1) // right side approach
                || (headY - foodY >= 0 && headY - foodY <= 1 && foodX - headX >= 0 && foodX - headX <= 3)
                || (headY - foodY >= 0 && headY - foodY <= 1 && headX - foodX >= 0 && headX - foodX <= 3); // bottom side approach
        }

        private void SetAndCheckBonusRound()
        {
            isBonusRound = levelNumber % 2 == 0;
            if (isBonusRound)
            {
                bonusRound = bonusContext.Get(levelNumber + 1);
            }
        }

        private void GetNewLevelFromLevelContext()
        {
            level = levelContext.Get(levelNumber);
        }

        private static void DisplayChampionText()
        {
            DisplayText(0, Constants.height + 1, "You are a champion now.");
            Console.WriteLine("");
            Console.WriteLine("Please wait until new levels are added.");
            Console.WriteLine("Press Q to exit...");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private bool CheckIfAllLevelsDone()
        {
            return levelNumber == totalLevels;
        }

        private void IncrementLevel()
        {
            levelNumber += 1;
        }

        private void MarkCurrentLevelCompleted()
        {
            isCurrentLevelCompleted = true;
        }

        private void DisplayLevelCompleted()
        {
            DisplayText(0, Constants.height + 1, $"Congrats Level-{levelNumber} completed...");
            Console.WriteLine("");
            if (isBonusRound)
            {
                Console.WriteLine("Hurray!!! Next up is Bonus round. Press B to continue...");
            }
            else
            {
                Console.WriteLine("Press L to start next level...");
            }            
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
                lives = 0;
                isOver = false;                
            }
            remainingEatCounter = 10;
            sleepTime = Constants.levelToSleepTime[levelNumber].Key;
            isCurrentLevelCompleted = false;
        }

        private static void End()
        {
            Environment.Exit(0);
        }

        private static void ClearConsoleByCoordinates(List<KeyValuePair<int, int>> keyValuePairs, int endingX)
        {
            for(var i =0;i< keyValuePairs.Count; i++)
            {
                Console.SetCursorPosition(keyValuePairs[i].Key, keyValuePairs[i].Value);
                for (var j = 0; j < endingX; j++)
                {
                    Console.Write(" ");
                }
            }            
        }

        private void DisplayGameOver()
        {
            if(lives >= 0)
            {
                restartRound = true;
                DisplayText(0, Constants.height + 1, $"Now, you have only {lives} live(s) left. Stay tight.\n");
                Console.WriteLine("Press C to continue...");
            }
            else
            {
                DisplayText(0, Constants.height + 1, "Game Over!!!");
                Console.WriteLine("");
                Console.WriteLine("Press R to restart...");
                Console.WriteLine("Press Q to exit...");
                isOver = true;
                Console.ForegroundColor = ConsoleColor.White;
            }            
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
            sleepTime -= Constants.levelToSleepTime[levelNumber].Value;
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
            SetForeGroundColor(ConsoleColor.Gray);
            level.Drawlayout();
            DrawSnake(snake);
            (foodX, foodY) = level.GenerateFood(snake.GetSnakeCoordinates());
            DisplayStatisticsAndGameControls();
        }

        private static void SetForeGroundColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        private static void ClearConsole()
        {
            Console.Clear();
        }

        private void DrawSnake(Snake snake)
        {
            var snakeBodyParts = snake.BodyParts;
            for (var i = 0; i < snakeBodyParts.Count; i++)
            {
                var snakePart = snakeBodyParts[i].Pixel;
                Console.SetCursorPosition(snakePart.XCoordinate, snakePart.YCoordinate);
                Console.ForegroundColor = snakePart.ConsoleColor;
                Console.Write(Constants.hexUnicode);
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
            var snake = new Snake(new List<Pixel>
            {
                new Pixel(76, 3, ConsoleColor.Yellow, Direction.RIGHT),
                new Pixel(77, 3, ConsoleColor.Yellow, Direction.RIGHT),
                new Pixel(78, 3, ConsoleColor.Green, Direction.RIGHT),
            });
            Console.SetCursorPosition(65, 0);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Game Statistics:");
            var levelText = isBonusRound ? "Bonus Round" : $"{levelNumber } of {totalLevels}";
            DisplayText(66, 1, $"Level: {levelText}");
            DisplayText(66, 2, $"Score: {score}");
            DisplayText(66, 3, $"Life: {lives} *");
            DrawSnake(snake);
            DisplayText(66, 4, $"Total Fruits Eaten: {totalEatCounter}");
            var label = remainingEatCounter == 10 ? $"{remainingEatCounter}" : $"0{remainingEatCounter}";
            DisplayText(66, 5, $"Fruits left at this level: {label}");
            label = $"{Math.Round(1000.0M / sleepTime, 1)} blocks/sec";
            DisplayText(66, 6, $"Speed: {label}");
            DisplayText(66, 9, "Controls");
            DisplayText(67, 10, "Move Up: Up Arrow");
            DisplayText(67, 11, "Move Down: Down Arrow");
            DisplayText(67, 12, "Move Left: Left Arrow");
            DisplayText(67, 13, "Move Right: Right Arrow");
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
