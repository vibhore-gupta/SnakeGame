using SnakeGame.Source.FoodGenerationStrategies;
using System.Collections.Generic;
using Xunit;

namespace SnakeGame.Tests.FoodGenerationStrategies
{
    public class FoodDrawStrategyBaseTests
    {
        [Fact]
        public void FoodDrawStrategyBase_WhenCalledDrawFoodWithinWallsAndNotOnSnake_GenerateFood()
        {
            // Arrange
            var foodDrawStrategyBase = new FoodDrawStrategyBase();
            var keyPairs = new List<KeyValuePair<int, int>>
            {
               new KeyValuePair<int, int>(15,16),
               new KeyValuePair<int, int>(16,16),
               new KeyValuePair<int, int>(17,16),
               new KeyValuePair<int, int>(18,16),
               new KeyValuePair<int, int>(19,16),
            };


            // Act
            var (foodX, foodY) = foodDrawStrategyBase.DrawFoodWithinWallsAndNotOnSnake(keyPairs);
            var resultPair = new KeyValuePair<int, int>(foodX, foodY);

            // Assert
            for(var i = 0; i < keyPairs.Count; i++)
            {
                Assert.NotEqual(keyPairs[i].ToString(), resultPair.ToString());
            }
            Assert.NotEqual(60, foodX);
            Assert.NotEqual(20, foodY);
        }
    }
}
