using SnakeGame.Source.GameOverStrategies.Interfaces;

namespace SnakeGame.Source.GameOverStrategies
{
    public class Level1Strategy : GameOverStrategyBase, IGameOver
    {
        public bool IsOver(int headXCoordinate, int headYCoordinate)
        {
            return IsHittingTheWalls(headXCoordinate, headYCoordinate);
        }
    }
}
