using System.Collections.Generic;

namespace SnakeGame.Source.Levels.Interfaces
{
    public interface ILevel
    {
        void Drawlayout();
        (int, int) GenerateFood(List<KeyValuePair<int, int>> coordinates);
        bool IsGameOver(List<KeyValuePair<int, int>> coordinates);
    }
}
