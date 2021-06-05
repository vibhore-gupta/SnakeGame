using SnakeGame.Source.Enums;
using System.Collections.Generic;

namespace SnakeGame.Source.BonusRounds.Interfaces
{
    public interface IPrizeSetter
    {
        void SetPrize(Dictionary<PrizeType, int> prizes);
    }
}
