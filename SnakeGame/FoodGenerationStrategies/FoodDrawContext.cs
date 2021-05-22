using SnakeGame.Source.FoodGenerationStrategies.Interfaces;
using System.Collections.Generic;

namespace SnakeGame.Source.FoodGenerationStrategies
{
    public class FoodDrawContext
    {
        private readonly Dictionary<int, IFoodDrawer> levelToStrategy;
        public FoodDrawContext()
        {
            levelToStrategy = new Dictionary<int, IFoodDrawer>
            {
                {1, new Level1Strategy() },
                {2, new Level2Strategy() },
                {3, new Level3Strategy() }
            };
        }
        public (int, int) DrawFoodForLevel(int level, List<KeyValuePair<int, int>> coordinates)
        {
            var strategy = levelToStrategy[level];
            return strategy.Draw(coordinates);
        }
    }
}
