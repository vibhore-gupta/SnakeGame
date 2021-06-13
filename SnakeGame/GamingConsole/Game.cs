using SnakeGame.Source.BonusRounds.Interfaces;
using SnakeGame.Source.Common;
using SnakeGame.Source.Contexts;
using SnakeGame.Source.Enums;
using SnakeGame.Source.Levels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SnakeGame.Source.GamingConsole
{
    public class Game
    {
        private int score;
        private int remainingEatCounter = 10;
        private int totalEatCounter;
        private int sleepTime;
        private bool isCurrentLevelCompleted;
        private DateTime foodStartTime;
        private ILevel level;
        private IBonusRound bonusRound;
        private readonly IConsole console;
        private int foodX;
        private int foodY;
        private bool isOver;
        private Snake snake;
        private int levelNumber = 1;
        private bool isBonusRound;
        private bool restartRound;
        private int lives;

        private readonly Context<ILevel> levelContext;
        private readonly Context<IBonusRound> bonusContext;
        private static readonly int totalLevels = 4;
        private static readonly ConsoleKey[] AllowedKeys = new ConsoleKey[]
        {
            ConsoleKey.UpArrow,
            ConsoleKey.DownArrow,
            ConsoleKey.LeftArrow,
            ConsoleKey.RightArrow
        };

        public Game(IConsole console, Context<ILevel> levelContextBase, Context<IBonusRound> bonusContextBase)
        {
            this.console = console;
            levelContext = levelContextBase;
            bonusContext = bonusContextBase;
            level = levelContext.Get(levelNumber);
            snake = new Snake(1);
            sleepTime = Constants.levelToSleepTime[levelNumber].Key;
            foodStartTime = DateTime.Now;            
        }

        public void RestartLoop()
        {
            while (true)
            {
                if (console.KeyAvailable)
                {
                    var key = console.ReadKey().Key;
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
                console.Clear();
                DisplayStatisticsAndGameControls();
                bonusRound.DrawBonusRoundLayout();
                bonusRound.SetPrizesBehindDoors(new Dictionary<PrizeType, int> { { PrizeType.LIFEBOOSTER, 1 } });
                snake = new Snake(1);
                DrawSnake(snake);
                while (true)
                {
                    DrawSnakeInLoop();
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
            DisplayText(0, Constants.height + 1, $"{label}\nPress L to start next level...", ConsoleColor.Red);
        }

        private void DisplayBonusRoundInstructions()
        {
            if (isBonusRound)
            {
                console.Clear();
                console.SetForeGroundColor(ConsoleColor.Gray);
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
                DrawSnakeInLoop();
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

        private void DrawSnakeInLoop()
        {
            ClearTail();
            if (console.KeyAvailable)
            {
                var key = console.ReadKey().Key;
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

        private void DisplayChampionText()
        {
            DisplayText(0, Constants.height + 1, "You are a champion now.\nPlease wait until new levels are added.\nPress Q to exit...",
                ConsoleColor.Red);
            console.SetForeGroundColor(ConsoleColor.White);
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
            var textToDisplay = $"Congrats Level-{levelNumber} completed...\n";
            if (isBonusRound)
            {
                textToDisplay += "Hurray!!! Next up is Bonus round.\nPress B to continue...";
            }
            else
            {
                textToDisplay += "Press L to start next level...";
            }
            DisplayText(0, Constants.height + 1,textToDisplay, ConsoleColor.Red);           
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

        private void ClearConsoleByCoordinates(List<KeyValuePair<int, int>> keyValuePairs, int endingX)
        {
            for(var i =0;i< keyValuePairs.Count; i++)
            {
                console.SetCursorPosition(keyValuePairs[i].Key, keyValuePairs[i].Value);
                for (var j = 0; j < endingX; j++)
                {
                    console.Write(" ");
                }
            }            
        }

        private void DisplayGameOver()
        {
            if(lives >= 0)
            {
                restartRound = true;
                DisplayText(0, Constants.height + 1, $"Now, you have only {lives} live(s) left. Stay tight.\nPress C to continue...",
                    ConsoleColor.Red);
            }
            else
            {
                DisplayText(0, Constants.height + 1, "Game Over!!!\nPress R to restart...\nPress Q to exit...", ConsoleColor.Red);
                console.SetForeGroundColor(ConsoleColor.White);
                isOver = true;
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
            console.SetCursorPosition(tailPixel.XCoordinate, tailPixel.YCoordinate);
            console.Write(" ");
        }

        private void SetUp()
        {
            console.Clear();
            console.SetForeGroundColor(ConsoleColor.Gray);
            level.Drawlayout();
            DrawSnake(snake);
            (foodX, foodY) = level.GenerateFood(snake.GetSnakeCoordinates());
            DisplayStatisticsAndGameControls();
        }

        private void DrawSnake(Snake snake)
        {
            var snakeBodyParts = snake.BodyParts;
            for (var i = 0; i < snakeBodyParts.Count; i++)
            {
                var snakePart = snakeBodyParts[i].Pixel;
                DisplayText(snakePart.XCoordinate, snakePart.YCoordinate, Constants.hexUnicode, snakePart.ConsoleColor);
            }
            console.HideCursor();
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
            DisplayText(65, 0, "Game Statistics:", ConsoleColor.Red);
            var levelText = isBonusRound ? "Bonus Round" : $"{levelNumber } of {totalLevels}";
            DisplayText(66, 1, $"Level: {levelText}", ConsoleColor.Red);
            DisplayText(66, 2, $"Score: {score}", ConsoleColor.Red);
            DisplayText(66, 3, $"Life: {lives} *", ConsoleColor.Red);
            DrawSnake(snake);
            DisplayText(66, 4, $"Total Fruits Eaten: {totalEatCounter}", ConsoleColor.Red);
            var label = remainingEatCounter == 10 ? $"{remainingEatCounter}" : $"0{remainingEatCounter}";
            DisplayText(66, 5, $"Fruits left at this level: {label}", ConsoleColor.Red);
            label = $"{Math.Round(1000.0M / sleepTime, 1)} blocks/sec";
            DisplayText(66, 6, $"Speed: {label}", ConsoleColor.Red);
            DisplayText(66, 9, "Controls", ConsoleColor.Red);
            DisplayText(67, 10, "Move Up: Up Arrow", ConsoleColor.Red);
            DisplayText(67, 11, "Move Down: Down Arrow", ConsoleColor.Red);
            DisplayText(67, 12, "Move Left: Left Arrow", ConsoleColor.Red);
            DisplayText(67, 13, "Move Right: Right Arrow", ConsoleColor.Red);
        }

        private void DisplayText(int xCoordinate, int yCoordinate, string text, ConsoleColor color)
        {
            console.SetCursorPosition(xCoordinate, yCoordinate);
            console.Write(text, color);
        }

        private void CalculateScore(DateTime foodEatingTime)
        {            
            score += ScoreCalculatorHelper.CalculateScore(foodEatingTime, foodStartTime, snake.BodyParts.Count, levelNumber);
            foodStartTime = DateTime.Now;
        }
    }
}
