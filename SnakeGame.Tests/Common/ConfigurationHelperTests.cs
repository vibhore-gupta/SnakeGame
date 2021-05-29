using Microsoft.Extensions.Configuration;
using SnakeGame.Source.Common;
using System.Collections.Generic;
using Xunit;

namespace SnakeGame.Tests.Common
{
    public class ConfigurationHelperTests
    {
        [Theory]
        [InlineData("level3ObstacleCoordinates")]
        public void ConfigurationHelper_WhenPassedLevel3Section_ReturnsConfigValues(string sectionName)
        {
            //  Arrange
            var configValuesList = new List<KeyValuePair<int, int>>();
            configValuesList.Add(new KeyValuePair<int, int>(16, 6));
            configValuesList.Add(new KeyValuePair<int, int>(17, 6));
            configValuesList.Add(new KeyValuePair<int, int>(19, 6));
            configValuesList.Add(new KeyValuePair<int, int>(20, 6));
            configValuesList.Add(new KeyValuePair<int, int>(22, 6));
            configValuesList.Add(new KeyValuePair<int, int>(23, 6));
            configValuesList.Add(new KeyValuePair<int, int>(16, 7));
            configValuesList.Add(new KeyValuePair<int, int>(17, 7));
            configValuesList.Add(new KeyValuePair<int, int>(37, 6));
            configValuesList.Add(new KeyValuePair<int, int>(38, 6));
            configValuesList.Add(new KeyValuePair<int, int>(40, 6));
            configValuesList.Add(new KeyValuePair<int, int>(41, 6));
            configValuesList.Add(new KeyValuePair<int, int>(43, 6));
            configValuesList.Add(new KeyValuePair<int, int>(44, 6));
            configValuesList.Add(new KeyValuePair<int, int>(43, 7));
            configValuesList.Add(new KeyValuePair<int, int>(44, 7));
            configValuesList.Add(new KeyValuePair<int, int>(16, 12));
            configValuesList.Add(new KeyValuePair<int, int>(17, 12));
            configValuesList.Add(new KeyValuePair<int, int>(16, 13));
            configValuesList.Add(new KeyValuePair<int, int>(17, 13));
            configValuesList.Add(new KeyValuePair<int, int>(19, 13));
            configValuesList.Add(new KeyValuePair<int, int>(20, 13));
            configValuesList.Add(new KeyValuePair<int, int>(22, 13));
            configValuesList.Add(new KeyValuePair<int, int>(23, 13));
            configValuesList.Add(new KeyValuePair<int, int>(43, 12));
            configValuesList.Add(new KeyValuePair<int, int>(44, 12));
            configValuesList.Add(new KeyValuePair<int, int>(37, 13));
            configValuesList.Add(new KeyValuePair<int, int>(38, 13));
            configValuesList.Add(new KeyValuePair<int, int>(44, 12));
            configValuesList.Add(new KeyValuePair<int, int>(40, 13));
            configValuesList.Add(new KeyValuePair<int, int>(41, 13));
            configValuesList.Add(new KeyValuePair<int, int>(43, 13));
            configValuesList.Add(new KeyValuePair<int, int>(44, 13));

            configValuesList.Add(new KeyValuePair<int, int>(2, 1));
            configValuesList.Add(new KeyValuePair<int, int>(4, 2));
            configValuesList.Add(new KeyValuePair<int, int>(6, 3));
            configValuesList.Add(new KeyValuePair<int, int>(8, 4));
            configValuesList.Add(new KeyValuePair<int, int>(10, 5));
            configValuesList.Add(new KeyValuePair<int, int>(2, 19));
            configValuesList.Add(new KeyValuePair<int, int>(4, 18));
            configValuesList.Add(new KeyValuePair<int, int>(6, 17));
            configValuesList.Add(new KeyValuePair<int, int>(8, 16));
            configValuesList.Add(new KeyValuePair<int, int>(10, 15));
            configValuesList.Add(new KeyValuePair<int, int>(58, 19));
            configValuesList.Add(new KeyValuePair<int, int>(56, 18));
            configValuesList.Add(new KeyValuePair<int, int>(54, 17));
            configValuesList.Add(new KeyValuePair<int, int>(52, 16));
            configValuesList.Add(new KeyValuePair<int, int>(50, 15));
            configValuesList.Add(new KeyValuePair<int, int>(58, 1));
            configValuesList.Add(new KeyValuePair<int, int>(56, 2));
            configValuesList.Add(new KeyValuePair<int, int>(54, 3));
            configValuesList.Add(new KeyValuePair<int, int>(52, 4));
            configValuesList.Add(new KeyValuePair<int, int>(50, 5));


            // Act
            var section = ConfigurationHelper.GetSection(sectionName);


            // Assert
            var index = 0;
            foreach (var subSection in section.GetChildren())
            {
                var xCoordinate = subSection.GetValue<int>("x");
                var yCoordinate = subSection.GetValue<int>("y");
                Assert.Equal(configValuesList[index].Key, xCoordinate);
                Assert.Equal(configValuesList[index].Value, yCoordinate);
                index += 1;
            }
        }

        [Theory]
        [InlineData("level2ObstacleCoordinates")]
        public void ConfigurationHelper_WhenPassedLevel2Section_ReturnsConfigValues(string sectionName)
        {
            //  Arrange
            var configValuesList = new List<KeyValuePair<int, int>>();
            var startingX = 6;
            var startingY = 20;
            for(var i = startingY; i > 5; i--)
            {
                configValuesList.Add(new KeyValuePair<int, int>(startingX, i));
            }
            startingX = 16;
            startingY = 0;
            for (var i = startingY; i < 16; i++)
            {
                configValuesList.Add(new KeyValuePair<int, int>(startingX, i));
            }
            startingX = 44;
            startingY = 0;
            for (var i = startingY; i < 16; i++)
            {
                configValuesList.Add(new KeyValuePair<int, int>(startingX, i));
            }
            startingX = 54;
            startingY = 20;
            for (var i = startingY; i > 5; i--)
            {
                configValuesList.Add(new KeyValuePair<int, int>(startingX, i));
            }

            // Act
            var section = ConfigurationHelper.GetSection(sectionName);


            // Assert
            var index = 0;
            foreach (var subSection in section.GetChildren())
            {
                var xCoordinate = subSection.GetValue<int>("x");
                var yCoordinate = subSection.GetValue<int>("y");
                Assert.Equal(configValuesList[index].Key, xCoordinate);
                Assert.Equal(configValuesList[index].Value, yCoordinate);
                index += 1;
            }
        }
    }
}
