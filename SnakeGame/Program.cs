namespace SnakeGame.Source
{
    class Program
    {   
        static void Main()
        {
            var game = new Game();
            game.Start();
            game.RestartLoop();
        }
    }
}
