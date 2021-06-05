using SnakeGame.Source.BonusRounds.Interfaces;
using SnakeGame.Source.Enums;
using SnakeGame.Source.ObstacleLayoutStrategies;
using SnakeGame.Source.ObstacleLayoutStrategies.Interfaces;
using System.Collections.Generic;

namespace SnakeGame.Source.BonusRounds
{
    public class BonusRoundBase : LayoutStrategyBase
    {
        protected IBonusLayoutDrawer _bonusLayout;
        protected IPrizeSetter _prizeSetter;
        protected IPrizeDeclarator _prizeDeclarator;

        protected static Dictionary<int, PrizeType> doorToPrize = new Dictionary<int, PrizeType> 
        { 
            { 1, PrizeType.DEAD },
            { 2, PrizeType.DEAD },
            { 3, PrizeType.DEAD }
        };
        protected static Dictionary<int, Dictionary<string, List<int>>> doorToCoordinates = new Dictionary<int, Dictionary<string, List<int>>>();
        protected int doorsCount = 3;
        public BonusRoundBase()
        {
        }

        public BonusRoundBase(IBonusLayoutDrawer bonusLayout, IPrizeSetter prizeSetter, IPrizeDeclarator prizeDeclarator)
        {
            _bonusLayout = bonusLayout;
            _prizeDeclarator = prizeDeclarator;
            _prizeSetter = prizeSetter;
        }
    }
}
