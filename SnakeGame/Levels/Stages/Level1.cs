using SnakeGame.Source.FoodGenerationStrategies.Interfaces;
using SnakeGame.Source.GameOverStrategies.Interfaces;
using SnakeGame.Source.Levels.Interfaces;
using SnakeGame.Source.ObstacleLayoutStrategies.Interfaces;
using System.Collections.Generic;

namespace SnakeGame.Source.Levels.Stages
{
    public class Level1 : LevelBase, ILevel
    {
        public Level1(IFoodDrawer foodDrawer, ILayoutDrawer layoutDrawer, IGameOver gameOver) : base(foodDrawer, layoutDrawer, gameOver)
        {
        }

        public void Drawlayout()
        {
            _layoutDrawer.Draw();
        }

        public (int, int) GenerateFood(List<KeyValuePair<int, int>> coordinates)
        {
            return _foodDrawer.Draw(coordinates);
        }

        public bool IsGameOver(List<KeyValuePair<int, int>> coordinates)
        {
            return _gameOver.IsOver(coordinates);
        }
    }
}
