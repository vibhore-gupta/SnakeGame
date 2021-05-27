using System.Collections.Generic;

namespace SnakeGame.Source.GameOverStrategies.Interfaces
{
    public interface IGameOver
    {
        bool IsOver(List<KeyValuePair<int, int>> coordinates);
    }
}
