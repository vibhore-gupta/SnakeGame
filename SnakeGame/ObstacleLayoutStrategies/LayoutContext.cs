using SnakeGame.ObstacleLayoutStrategies.Interfaces;
using System.Collections.Generic;

namespace SnakeGame.ObstacleLayoutStrategies
{
    public class LayoutContext
    {
        private readonly Dictionary<int, ILayoutDrawer> levelToStrategy;
        public LayoutContext()
        {
            levelToStrategy = new Dictionary<int, ILayoutDrawer>
            {
                {1, new Level1Strategy() },
                {2, new Level2Strategy() }
            };
        }
        public void DrawObstaclesForLevel(int level)
        {
            var strategy = levelToStrategy[level];
            strategy.Draw();
        }
    }
}
