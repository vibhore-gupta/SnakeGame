using System;

namespace SnakeGame.Source.GamingConsole
{
    public interface IConsole
    {
        void Write(string text, ConsoleColor consoleColor = ConsoleColor.Gray);
        void SetCursorPosition(int xCoordinate, int yCoordinate);
        ConsoleKeyInfo ReadKey();
        void Clear();
        bool KeyAvailable { get; }
        void SetForeGroundColor(ConsoleColor consoleColor);
        bool HideCursor();
    }
}
