using SnakeGame.Source.Enums;
using System.Collections.Generic;

namespace SnakeGame.Source.BonusRounds.Interfaces
{
    public interface IBonusRound
    {
        void ShowBonusRoundInstructions();
        void DrawBonusRoundLayout();
        void SetPrizesBehindDoors(Dictionary<PrizeType, int> prizes);
        PrizeType GetPrizeWon(KeyValuePair<int, int> coordinates);
    }
}
