using SnakeGame.Source.Common;
using System.Collections.Generic;

namespace SnakeGame.Source.GameOverStrategies
{
    public class GameOverStrategyBase
    {
        public bool IsHittingTheWallsOrHittingItself(List<KeyValuePair<int, int>> coordinates)
        {
            return IsHittingTheWalls(coordinates)
                || IsHittingItself(coordinates);
        }
        private bool IsHittingTheWalls(List<KeyValuePair<int, int>> coordinates)
        {
            var headXCoordinate = coordinates[^1].Key;
            var headYCoordinate = coordinates[^1].Value;
            var twiceWidth = 2 * Constants.width;
            var isYCoordinateWithinHeight = headYCoordinate >= 0 && headYCoordinate <= Constants.height;
            var isXCoordinateWithinWidth = headXCoordinate >= 0 && headXCoordinate <= twiceWidth;

            return headXCoordinate == 0 && isYCoordinateWithinHeight // left wall hit
                || headYCoordinate == Constants.height && isXCoordinateWithinWidth // bottom wall hit
                || headXCoordinate == twiceWidth && isYCoordinateWithinHeight // right wall hit
                || headYCoordinate == 0 && isXCoordinateWithinWidth; // top wall hit
        }

        private bool IsHittingItself(List<KeyValuePair<int, int>> coordinates)
        {
            var headXCoordinate = coordinates[^1].Key;
            var headYCoordinate = coordinates[^1].Value;

            for(var i=0; i< coordinates.Count - 1; i++)
            {
                if(coordinates[i].Key == headXCoordinate && coordinates[i].Value == headYCoordinate)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
