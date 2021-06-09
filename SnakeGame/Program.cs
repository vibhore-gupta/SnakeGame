using SnakeGame.Source.GamingConsole;

namespace SnakeGame.Source
{
    class Program
    {   
        static void Main()
        {
            var gameConsole = new GameConsole();
            var game = new Game(gameConsole);
            game.Start();
            game.RestartLoop();
        }
    }
}
