using SnakeGame.GameOverStrategies.Interfaces;

namespace SnakeGame.GameOverStrategies
{
    public class Level1Strategy : GameOverStrategyBase, IGameOver
    {
        public bool IsOver(int headXCoordinate, int headYCoordinate)
        {
            return IsHittingTheWalls(headXCoordinate, headYCoordinate);
        }
    }
}
