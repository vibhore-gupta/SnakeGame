using SnakeGame.Source.Contexts;
using SnakeGame.Source.Levels.Interfaces;
using SnakeGame.Source.Levels.Stages;
using System.Collections.Generic;

namespace SnakeGame.Source.Levels
{
    public class LevelContext : Context<ILevel>
    {
        public LevelContext()
        {
            Mappings = new Dictionary<int, ILevel>
            {
                {1, new Level1(new FoodGenerationStrategies.Strategies.Level1Strategy(),
                               new ObstacleLayoutStrategies.Strategies.Level1Strategy(),
                               new GameOverStrategies.Strategies.Level1Strategy())                                     
                },
                {2, new Level2(new FoodGenerationStrategies.Strategies.Level2Strategy(),
                               new ObstacleLayoutStrategies.Strategies.Level2Strategy(),
                               new GameOverStrategies.Strategies.Level2Strategy())                                     
                },
                {3, new Level3(new FoodGenerationStrategies.Strategies.Level3Strategy(),
                               new ObstacleLayoutStrategies.Strategies.Level3Strategy(),
                               new GameOverStrategies.Strategies.Level3Strategy())                                  
                }
            };
        }

        public override ILevel Get(int level)
        {
            return Mappings[level];
        }
    }
}
