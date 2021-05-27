using SnakeGame.Source.GameOverStrategies.Interfaces;
using System.Collections.Generic;

namespace SnakeGame.Source.GameOverStrategies
{
    public class GameOverContext
    {
        private readonly Dictionary<int, IGameOver> levelToStrategy;
        public GameOverContext()
        {
            levelToStrategy = new Dictionary<int, IGameOver>
            {
                {1, new Level1Strategy() },
                {2, new Level2Strategy() },
                {3, new Level3Strategy() }
            };
        }
        public bool IsOver(int level, List<KeyValuePair<int, int>> coordinates)
        {
            var strategy = levelToStrategy[level];
            return strategy.IsOver(coordinates);
        }
    }
}
