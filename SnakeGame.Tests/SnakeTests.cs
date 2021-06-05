﻿using SnakeGame.Source;
using SnakeGame.Source.Enums;
using System;
using System.Collections.Generic;
using Xunit;

namespace SnakeGame.Tests
{
    public class SnakeTests
    {
        [Theory]
        [InlineData(BodyPartType.HEAD, Direction.RIGHT, 17, 16, ConsoleColor.Green)]
        [InlineData(BodyPartType.TAIL, Direction.RIGHT, 15, 16, ConsoleColor.Yellow)]
        [InlineData(BodyPartType.BODY, Direction.RIGHT, 16, 16, ConsoleColor.Yellow)]
        public void Snake_GivenABodyPart_ReturnsBodyPixel(BodyPartType bodyPartType, Direction direction,
            int xCoordinate, int yCoordinate, ConsoleColor consoleColor)
        {
            // Arrange
            var snake = new Snake(1);

            // Act
            var pixel = snake.GetPixelByBodyType(bodyPartType);

            // Assert
            Assert.Equal(xCoordinate, pixel.XCoordinate);
            Assert.Equal(yCoordinate, pixel.YCoordinate);
            Assert.Equal(consoleColor, pixel.ConsoleColor);
            Assert.Equal(direction, pixel.CurrentDirection);
        }
        
        [Fact]
        public void Snake_WhenCalledEnlarge_AddOneMorePixelToTheStart()
        {
            // Arrange
            var snake = new Snake(1);
            var currentTailPixel = snake.GetPixelByBodyType(BodyPartType.TAIL);
            var currentTailPart = snake.BodyParts[0];

            // Act
            snake.Enlarge();

            // Assert
            Assert.Equal(4, snake.BodyParts.Count);
            var newTailPart = snake.BodyParts[0];
            Assert.Equal(BodyPartType.TAIL, newTailPart.PartType);
            Assert.Equal(currentTailPixel.ConsoleColor, newTailPart.Pixel.ConsoleColor);
            Assert.Equal(currentTailPixel.CurrentDirection, newTailPart.Pixel.CurrentDirection);
            Assert.Equal(currentTailPixel.XCoordinate -1, newTailPart.Pixel.XCoordinate);
            Assert.Equal(currentTailPixel.YCoordinate, newTailPart.Pixel.YCoordinate);

            Assert.Equal(BodyPartType.BODY, currentTailPart.PartType);
            Assert.Equal(currentTailPixel.XCoordinate, snake.BodyParts[1].Pixel.XCoordinate);
            Assert.Equal(currentTailPixel.YCoordinate, snake.BodyParts[1].Pixel.YCoordinate);
            Assert.Equal(currentTailPixel.ConsoleColor, snake.BodyParts[1].Pixel.ConsoleColor);
            Assert.Equal(currentTailPixel.CurrentDirection, snake.BodyParts[1].Pixel.CurrentDirection);
        }

        [Fact]
        public void Snake_WhenCalledReBuildTailAndBody_ChangesTheTailAndBodyCoordinates()
        {
            // Arrange
            var snake = new Snake(1);
            var tailPixel = snake.GetPixelByBodyType(BodyPartType.TAIL);
            var bodyPixel = snake.GetPixelByBodyType(BodyPartType.BODY);
            var headPixel = snake.GetPixelByBodyType(BodyPartType.HEAD);

            var tailX = tailPixel.XCoordinate;
            var tailY = tailPixel.YCoordinate;
            var bodyX = bodyPixel.XCoordinate;
            var bodyY = bodyPixel.YCoordinate;
            var headX = headPixel.XCoordinate;
            var headY = headPixel.YCoordinate;

            // Act
            snake.ReBuildTailAndBody();

            // Assert
            Assert.Equal(tailX + 1, snake.BodyParts[0].Pixel.XCoordinate);
            Assert.Equal(tailY, snake.BodyParts[0].Pixel.YCoordinate);

            Assert.Equal(bodyX + 1, snake.BodyParts[1].Pixel.XCoordinate);
            Assert.Equal(bodyY, snake.BodyParts[1].Pixel.YCoordinate);

            Assert.Equal(headX, snake.BodyParts[2].Pixel.XCoordinate);
            Assert.Equal(headY, snake.BodyParts[2].Pixel.YCoordinate);

        }

        [Fact]
        public void Snake_WhenCalledRebuildHead_ChangesHeadCoordinates()
        {
            // Arrange
            var snake = new Snake(1);
            var headPixel = snake.GetPixelByBodyType(BodyPartType.HEAD);
            var headX = headPixel.XCoordinate;
            var headY = headPixel.YCoordinate;
            var headCurrentDirection = headPixel.CurrentDirection;
            var headConsoleColor = headPixel.ConsoleColor;

            // Act
            snake.RebuildHead();

            // Assert
            Assert.Equal(headX + 1, snake.BodyParts[2].Pixel.XCoordinate);
            Assert.Equal(headY, snake.BodyParts[2].Pixel.YCoordinate);
            Assert.Equal(headCurrentDirection, snake.BodyParts[2].Pixel.CurrentDirection);
            Assert.Equal(headConsoleColor, snake.BodyParts[2].Pixel.ConsoleColor);


        }

        [Theory]
        [InlineData(ConsoleKey.RightArrow, Direction.RIGHT, 18, 16, ConsoleColor.Green)]
        [InlineData(ConsoleKey.LeftArrow, Direction.LEFT, 16, 16, ConsoleColor.Green)]
        [InlineData(ConsoleKey.UpArrow, Direction.UP, 17, 15, ConsoleColor.Green)]
        [InlineData(ConsoleKey.DownArrow, Direction.DOWN, 17, 17, ConsoleColor.Green)]
        public void Snake_WhenCalledReBuildHeadAccordingToKey_ChangesHeadCoordinates(ConsoleKey key, Direction direction,
            int xCoordinate, int yCoordinate, ConsoleColor consoleColor)
        {
            // Arrange
            var snake = new Snake(1);

            // Act
            snake.ReBuildHeadAccordingToKey(key);

            // Assert
            Assert.Equal(xCoordinate, snake.BodyParts[2].Pixel.XCoordinate);
            Assert.Equal(yCoordinate, snake.BodyParts[2].Pixel.YCoordinate);
            Assert.Equal(direction, snake.BodyParts[2].Pixel.CurrentDirection);
            Assert.Equal(consoleColor, snake.BodyParts[2].Pixel.ConsoleColor);
        }

        [Fact]
        public void Snake_WhenCalledGetSnakeCoordinates_ReturnsAllBodyPartsCoordinates()
        {
            // Arrange
            var snake = new Snake(1);
            var headPixel = snake.GetPixelByBodyType(BodyPartType.HEAD);
            var headX = headPixel.XCoordinate;
            var headY = headPixel.YCoordinate;
            var tailPixel = snake.GetPixelByBodyType(BodyPartType.TAIL);
            var tailX = tailPixel.XCoordinate;
            var tailY = tailPixel.YCoordinate;
            var bodyPixel = snake.GetPixelByBodyType(BodyPartType.BODY);
            var bodyX = bodyPixel.XCoordinate;
            var bodyY = bodyPixel.YCoordinate;

            // Act
            var coordinates = snake.GetSnakeCoordinates();

            // Assert
            Assert.Equal(3, coordinates.Count);
            Assert.Equal(tailX, coordinates[0].Key);
            Assert.Equal(tailY, coordinates[0].Value);
            Assert.Equal(bodyX, coordinates[1].Key);
            Assert.Equal(bodyY, coordinates[1].Value);
            Assert.Equal(headX, coordinates[2].Key);
            Assert.Equal(headY, coordinates[2].Value);
        }

        [Theory]
        [InlineData(0, 3)]
        [InlineData(1, 3)]
        [InlineData(2, 4)]
        [InlineData(10, 12)]
        public void Snake_WhenInstanciatedWithANumber_ReturnsSnakeOfGivenLength(int bodyPartCount, int snakeLength)
        {
            // Arrange
            var snake = new Snake(bodyPartCount);

            // Act

            // Assert
            Assert.Equal(snakeLength, snake.BodyParts.Count);
            Assert.Equal(BodyPartType.TAIL, snake.BodyParts[0].PartType);
            Assert.Equal(BodyPartType.HEAD, snake.BodyParts[snakeLength-1].PartType);
        }

        [Fact]
        public void Snake_WhenInstanciatedWithListOfZeroPixels_CreatesSnake()
        {
            // Arrange
            var snake = new Snake(new List<Pixel> ());

            // Act

            // Assert
            Assert.Equal(3, snake.BodyParts.Count);
            Assert.Equal(BodyPartType.TAIL, snake.BodyParts[0].PartType);
            Assert.Equal(15, snake.BodyParts[0].Pixel.XCoordinate);
            Assert.Equal(16, snake.BodyParts[0].Pixel.YCoordinate);
            Assert.Equal(Direction.RIGHT, snake.BodyParts[0].Pixel.CurrentDirection);
            Assert.Equal(ConsoleColor.Yellow, snake.BodyParts[0].Pixel.ConsoleColor);

            Assert.Equal(16, snake.BodyParts[1].Pixel.XCoordinate);
            Assert.Equal(16, snake.BodyParts[1].Pixel.YCoordinate);
            Assert.Equal(Direction.RIGHT, snake.BodyParts[1].Pixel.CurrentDirection);
            Assert.Equal(ConsoleColor.Yellow, snake.BodyParts[1].Pixel.ConsoleColor);

            Assert.Equal(17, snake.BodyParts[^1].Pixel.XCoordinate);
            Assert.Equal(16, snake.BodyParts[^1].Pixel.YCoordinate);
            Assert.Equal(Direction.RIGHT, snake.BodyParts[^1].Pixel.CurrentDirection);
            Assert.Equal(ConsoleColor.Green, snake.BodyParts[^1].Pixel.ConsoleColor);
            Assert.Equal(BodyPartType.HEAD, snake.BodyParts[^1].PartType);
            Assert.Equal(BodyPartType.BODY, snake.BodyParts[1].PartType);
        }

        [Fact]
        public void Snake_WhenInstanciatedWithListOfPixels_CreatesSnake()
        {
            // Arrange
            var snake = new Snake(new List<Pixel> 
            { new Pixel(0, 1, ConsoleColor.Red, Direction.RIGHT ),
            new Pixel(2, 3, ConsoleColor.Yellow, Direction.LEFT ),
            new Pixel(3, 4, ConsoleColor.Green, Direction.UP ),
            new Pixel(4, 5, ConsoleColor.Black, Direction.DOWN ),
            new Pixel(4, 6, ConsoleColor.Blue, Direction.RIGHT )}
            );

            // Act

            // Assert
            Assert.Equal(5, snake.BodyParts.Count);

            Assert.Equal(0, snake.BodyParts[0].Pixel.XCoordinate);
            Assert.Equal(1, snake.BodyParts[0].Pixel.YCoordinate);
            Assert.Equal(Direction.RIGHT, snake.BodyParts[0].Pixel.CurrentDirection);
            Assert.Equal(ConsoleColor.Red, snake.BodyParts[0].Pixel.ConsoleColor);

            Assert.Equal(2, snake.BodyParts[1].Pixel.XCoordinate);
            Assert.Equal(3, snake.BodyParts[1].Pixel.YCoordinate);
            Assert.Equal(Direction.LEFT, snake.BodyParts[1].Pixel.CurrentDirection);
            Assert.Equal(ConsoleColor.Yellow, snake.BodyParts[1].Pixel.ConsoleColor);

            Assert.Equal(3, snake.BodyParts[2].Pixel.XCoordinate);
            Assert.Equal(4, snake.BodyParts[2].Pixel.YCoordinate);
            Assert.Equal(Direction.UP, snake.BodyParts[2].Pixel.CurrentDirection);
            Assert.Equal(ConsoleColor.Green, snake.BodyParts[2].Pixel.ConsoleColor);

            Assert.Equal(4, snake.BodyParts[3].Pixel.XCoordinate);
            Assert.Equal(5, snake.BodyParts[3].Pixel.YCoordinate);
            Assert.Equal(Direction.DOWN, snake.BodyParts[3].Pixel.CurrentDirection);
            Assert.Equal(ConsoleColor.Black, snake.BodyParts[3].Pixel.ConsoleColor);

            Assert.Equal(4, snake.BodyParts[4].Pixel.XCoordinate);
            Assert.Equal(6, snake.BodyParts[4].Pixel.YCoordinate);
            Assert.Equal(Direction.RIGHT, snake.BodyParts[4].Pixel.CurrentDirection);
            Assert.Equal(ConsoleColor.Blue, snake.BodyParts[4].Pixel.ConsoleColor);

            Assert.Equal(BodyPartType.TAIL, snake.BodyParts[0].PartType);
            Assert.Equal(BodyPartType.HEAD, snake.BodyParts[^1].PartType);
            Assert.Equal(BodyPartType.BODY, snake.BodyParts[1].PartType);
            Assert.Equal(BodyPartType.BODY, snake.BodyParts[2].PartType);
            Assert.Equal(BodyPartType.BODY, snake.BodyParts[3].PartType);
        }
    }
}
