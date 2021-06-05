using SnakeGame.Source.BonusRounds.Interfaces;
using SnakeGame.Source.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame.Source.BonusRounds.Prize_Declarators
{
    public class BonusRound1PrizeDeclarator : BonusRoundBase, IPrizeDeclarator
    {
        public PrizeType GetPrizeWon(KeyValuePair<int, int> coordinates)
        {
            for(var i = 0; i < doorToCoordinates.Count; i++)
            {
                var xCoordinateRange = doorToCoordinates.ElementAt(i).Value["x"];
                var yCoordinateRange = doorToCoordinates.ElementAt(i).Value["y"];
                var doorNumber = doorToCoordinates.ElementAt(i).Key;
                if (Enumerable.Range(xCoordinateRange[0], xCoordinateRange[1]  - xCoordinateRange[0] + 1).Contains(coordinates.Key)
                    && Enumerable.Range(yCoordinateRange[0], yCoordinateRange[1] - yCoordinateRange[0] + 1).Contains(coordinates.Value))
                {
                    var prize =  doorToPrize[doorNumber];
                    Initialize();
                    return prize;
                }
            }
            return PrizeType.NONE;
        }
    }
}
