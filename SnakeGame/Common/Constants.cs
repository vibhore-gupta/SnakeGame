using System.Collections.Generic;

namespace SnakeGame.Source.Common
{
    public class Constants
    {
        public readonly static int width = 30;
        public readonly static int height = 20;
        public readonly static string hexUnicode = "\u25A0";
        public readonly static Dictionary<int, KeyValuePair<int, int>> levelToSleepTime = new Dictionary<int, KeyValuePair<int, int>>
        {
            {1,  new KeyValuePair<int, int>(400, 30)}, // 400 -> 100
            {2,  new KeyValuePair<int, int>(300, 20)}, // 300 -> 100
            {3,  new KeyValuePair<int, int>(300, 20)}, // 300 -> 100
            {4,  new KeyValuePair<int, int>(200, 10)}, // 200 -> 100
        };
    }
}
