using SnakeGame.Source.Enums;
using System.Collections.Generic;

namespace SnakeGame.Source.BonusRounds.Interfaces
{
    public interface IPrizeDeclarator
    {
        PrizeType GetPrizeWon(KeyValuePair<int, int> coordinates);
    }
}
