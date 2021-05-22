namespace SnakeGame.Source.GameOverStrategies
{
    public class GameOverStrategyBase
    {
        private static readonly int width = 30;
        private static readonly int height = 20;
        public bool IsHittingTheWalls(int headXCoordinate, int headYCoordinate)
        {
            var twiceWidth = 2 * width;
            var isYCoordinateWithinHeight = headYCoordinate >= 0 && headYCoordinate <= height;
            var isXCoordinateWithinWidth = headXCoordinate >= 0 && headXCoordinate <= twiceWidth;

            return headXCoordinate == 0 && isYCoordinateWithinHeight // left wall hit
                || headYCoordinate == height && isXCoordinateWithinWidth // bottom wall hit
                || headXCoordinate == twiceWidth && isYCoordinateWithinHeight // right wall hit
                || headYCoordinate == 0 && isXCoordinateWithinWidth; // top wall hit
        }
    }
}
