using SnakeGame.Source.Levels.Interfaces;
using System.Collections.Generic;

namespace SnakeGame.Source.Levels
{
    public class LevelContext
    {
        private readonly Dictionary<int, ILevel> levelMappings;

        public LevelContext()
        {
            levelMappings = new Dictionary<int, ILevel>
            {
                {1, new Level1(new FoodGenerationStrategies.Level1Strategy(),
                               new ObstacleLayoutStrategies.Level1Strategy(),
                               new GameOverStrategies.Level1Strategy())                                     
                },
                {2, new Level2(new FoodGenerationStrategies.Level2Strategy(),
                               new ObstacleLayoutStrategies.Level2Strategy(),
                               new GameOverStrategies.Level2Strategy())                                     
                },
                {3, new Level3(new FoodGenerationStrategies.Level3Strategy(),
                               new ObstacleLayoutStrategies.Level3Strategy(),
                               new GameOverStrategies.Level3Strategy())                                  
                }
            };
        }

        public ILevel Get(int level)
        {
            return levelMappings[level];
        }
    }
}
