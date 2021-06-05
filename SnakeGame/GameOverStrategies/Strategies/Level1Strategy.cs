using SnakeGame.Source.GameOverStrategies.Interfaces;
using System.Collections.Generic;

namespace SnakeGame.Source.GameOverStrategies.Strategies
{
    public class Level1Strategy : GameOverStrategyBase, IGameOver
    {
        public bool IsOver(List<KeyValuePair<int, int>> coordinates)
        {
            return IsHittingTheWallsOrHittingItself(coordinates);
        }
    }
}
