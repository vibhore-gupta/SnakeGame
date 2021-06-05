using SnakeGame.Source.BonusRounds.Interfaces;
using SnakeGame.Source.Enums;
using SnakeGame.Source.ObstacleLayoutStrategies.Interfaces;
using System.Collections.Generic;

namespace SnakeGame.Source.BonusRounds.Rounds
{
    public class BonusRound1: BonusRoundBase, IBonusRound
    {
        public BonusRound1(IBonusLayoutDrawer bonusLayoutDrawer , IPrizeSetter prizeSetter, IPrizeDeclarator prizeDeclarator) : 
            base(bonusLayoutDrawer, prizeSetter, prizeDeclarator)
        {

        }
        public void DrawBonusRoundLayout()
        {
            _bonusLayout.DrawBonusRoundLayout();
        }

        public PrizeType GetPrizeWon(KeyValuePair<int, int> coordinates)
        {
            return _prizeDeclarator.GetPrizeWon(coordinates);
        }

        public void SetPrizesBehindDoors(Dictionary<PrizeType, int> prizes)
        {
            _prizeSetter.SetPrize(prizes);
        }

        public void ShowBonusRoundInstructions()
        {
            _bonusLayout.DisplayBonusRoundInstructions();
        }
    }
}
