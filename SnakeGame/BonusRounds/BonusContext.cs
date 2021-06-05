using SnakeGame.Source.BonusRounds.Interfaces;
using SnakeGame.Source.BonusRounds.Prize_Declarators;
using SnakeGame.Source.BonusRounds.Prize_Setters;
using SnakeGame.Source.BonusRounds.Rounds;
using SnakeGame.Source.Contexts;
using SnakeGame.Source.ObstacleLayoutStrategies.BonusRoundLayouts;
using System.Collections.Generic;

namespace SnakeGame.Source.BonusRounds
{
    public class BonusContext: Context<IBonusRound>
    {
        public BonusContext()
        {
            Mappings = new Dictionary<int, IBonusRound>
            {
                {3, new BonusRound1(new BonusRound1Strategy(), new BonusRound1PrizeSetter(), new BonusRound1PrizeDeclarator())}
            };
        }

        public override IBonusRound Get(int level)
        {
            return Mappings[level];
        }
    }
}
