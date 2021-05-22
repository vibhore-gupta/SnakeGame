using System.Collections.Generic;

namespace SnakeGame.Source.FoodGenerationStrategies.Interfaces
{
    public interface IFoodDrawer
    {
        (int, int) Draw(List<KeyValuePair<int, int>> coordinates);
    }
}
