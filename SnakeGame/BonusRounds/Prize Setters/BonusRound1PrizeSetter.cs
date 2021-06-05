using SnakeGame.Source.BonusRounds.Interfaces;
using SnakeGame.Source.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame.Source.BonusRounds.Prize_Setters
{
    public class BonusRound1PrizeSetter : BonusRoundBase, IPrizeSetter
    {
        public void SetPrize(Dictionary<PrizeType, int> prizes)
        {
            var choosenDoors = new List<int>();
            var random = new Random();

            for (var i = 0; i < prizes.Count; i++)
            {
                var key = prizes.ElementAt(i).Key;
                var values = prizes.ElementAt(i).Value;
                for (var j = 0; j < values; j++)
                {
                    var door = random.Next(1, 4);
                    while (choosenDoors.IndexOf(door) > -1)
                    {
                        door = random.Next(1, 4);
                    }
                    choosenDoors.Add(door);
                    doorToPrize[door] = key;
                }
            }
        }
    }
}
