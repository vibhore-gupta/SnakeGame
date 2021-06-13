using SnakeGame.Source.BonusRounds;
using SnakeGame.Source.GamingConsole;
using SnakeGame.Source.Levels;

namespace SnakeGame.Source
{
    class Program
    {   
        static void Main()
        {
            var gameConsole = new GameConsole();
            var levelContext = new LevelContext();
            var bonusContext = new BonusContext();
            var game = new Game(gameConsole, levelContext, bonusContext);
            game.Start();
            game.RestartLoop();
        }
    }
}
