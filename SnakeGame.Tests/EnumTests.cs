using SnakeGame.Source.Enums;
using Xunit;

namespace SnakeGame.Tests
{
    public class EnumTests
    {
        [Theory]
        [InlineData(Direction.DOWN, 2)]
        [InlineData(Direction.LEFT, 3)]
        [InlineData(Direction.NONE, 0)]
        [InlineData(Direction.RIGHT, 4)]
        [InlineData(Direction.UP, 1)]
        public void DirectionEnum_TestForValues(Direction direction, int number)
        {
            // Arrange

            // Act

            // Assert
            Assert.Equal(number, (int)direction);
        }

        [Theory]
        [InlineData(BodyPartType.BODY, 1)]
        [InlineData(BodyPartType.TAIL, 2)]
        [InlineData(BodyPartType.HEAD, 0)]
        public void BodyPartEnum_TestForValues(BodyPartType bodyPartType, int number)
        {
            // Arrange

            // Act

            // Assert
            Assert.Equal(number, (int)bodyPartType);
        }

        [Theory]
        [InlineData(PrizeType.DEAD, 3)]
        [InlineData(PrizeType.LIFEBOOSTER, 1)]
        [InlineData(PrizeType.NONE, 0)]
        [InlineData(PrizeType.SCOREBOOSTER, 2)]
        public void PrizeTypeEnum_TestForValues(PrizeType bodyPartType, int number)
        {
            // Arrange

            // Act

            // Assert
            Assert.Equal(number, (int)bodyPartType);
        }
    }
}
