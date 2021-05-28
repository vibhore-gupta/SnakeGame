using SnakeGame.Source.FoodGenerationStrategies.Interfaces;
using SnakeGame.Source.GameOverStrategies.Interfaces;
using SnakeGame.Source.ObstacleLayoutStrategies.Interfaces;

namespace SnakeGame.Source.Levels
{
    public class LevelBase
    {
        protected readonly IFoodDrawer _foodDrawer;
        protected readonly ILayoutDrawer _layoutDrawer;
        protected readonly IGameOver _gameOver;

        public LevelBase(IFoodDrawer foodDrawer, ILayoutDrawer layoutDrawer, IGameOver gameOver)
        {
            _foodDrawer = foodDrawer;
            _layoutDrawer = layoutDrawer;
            _gameOver = gameOver;
        }
    }
}
