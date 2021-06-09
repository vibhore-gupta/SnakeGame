using System;

namespace SnakeGame.Source.GamingConsole
{
    public class GameConsole : IConsole
    {
        public bool KeyAvailable => Console.KeyAvailable;

        public bool HideCursor() => Console.CursorVisible = false;

        public void Clear() => Console.Clear();

        public ConsoleKeyInfo ReadKey() => Console.ReadKey(true);

        public void SetCursorPosition(int xCoordinate, int yCoordinate) => Console.SetCursorPosition(xCoordinate, yCoordinate);

        public void Write(string text, ConsoleColor consoleColor = ConsoleColor.Gray)
        {
            Console.ForegroundColor = consoleColor;
            Console.Write(text);
        }

        public void SetForeGroundColor(ConsoleColor consoleColor) => Console.ForegroundColor = consoleColor;
    }
}
