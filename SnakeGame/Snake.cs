using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame
{
    public class Snake
    {
        public List<BodyPart> BodyParts { get; }
    
        public Snake()
        {
            BodyParts = new List<BodyPart>
            {
                new BodyPart (BodyPartType.TAIL, new Pixel(15, 16, ConsoleColor.Green, Direction.RIGHT)),
                new BodyPart (BodyPartType.BODY, new Pixel(16, 16, ConsoleColor.Green, Direction.RIGHT)),
                new BodyPart (BodyPartType.HEAD, new Pixel(17, 16, ConsoleColor.DarkBlue, Direction.RIGHT))
            };
        }

        public void ReBuildTailAndBody()
        {
            var snakeBodyParts = BodyParts;
            for (var i = 0; i < snakeBodyParts.Count - 1; i++)
            {
                var currentBodyPart = snakeBodyParts[i].Pixel;
                var nextBodyPart = snakeBodyParts[i + 1].Pixel;
                currentBodyPart.XCoordinate = nextBodyPart.XCoordinate;
                currentBodyPart.YCoordinate = nextBodyPart.YCoordinate;
            }
        }

        public void RebuildHead()
        {
            var headPixel = GetPixelByBodyType(BodyPartType.HEAD);
            var (newXCoordinate, newYCoordinate) = SnakeBuilderHelper.GetNewCoordinatesForHead(headPixel, headPixel.CurrentDirection);
            SetPixelCoordinates(headPixel, newXCoordinate, newYCoordinate);
        }

        public void ReBuildHeadAccordingToKey(ConsoleKey consoleKey)
        {
            var headPixel = GetPixelByBodyType(BodyPartType.HEAD);
            var (newXCoordinate, newYCoordinate) = SnakeBuilderHelper.GetNewCoordinatesForHead(headPixel, consoleKey);
            SetPixelCoordinates(headPixel, newXCoordinate, newYCoordinate);
            SetDirection(headPixel, consoleKey);
        }

        public void Enlarge()
        {
            var currentTail = BodyParts.FirstOrDefault(p => p.PartType == BodyPartType.TAIL);
            var currentTailPixel = GetPixelByBodyType(BodyPartType.TAIL);
            var (newTailXCoordinate, newTailYCoordinate) = SnakeBuilderHelper.GetNewCoordinatesForTail(currentTailPixel, currentTailPixel.CurrentDirection);
            var newTail = new Pixel(newTailXCoordinate, newTailYCoordinate, currentTailPixel.ConsoleColor, currentTailPixel.CurrentDirection);
            currentTail.PartType = BodyPartType.BODY;
            BodyParts.Insert(0, new BodyPart(BodyPartType.TAIL, newTail));
        }

        private void SetDirection(Pixel pixel, ConsoleKey consoleKey)
        {
            pixel.CurrentDirection = SnakeBuilderHelper.GetNewDirectionForHead(consoleKey);
        }

        private void SetPixelCoordinates(Pixel pixel, int newXCoordinate, int newYCoordinate)
        {
            pixel.XCoordinate = newXCoordinate;
            pixel.YCoordinate = newYCoordinate;
        }

        public Pixel GetPixelByBodyType(BodyPartType bodyPartType)
        {
            return BodyParts.FirstOrDefault(p => p.PartType == bodyPartType).Pixel;
        }
    }
    public class BodyPart
    {
        public BodyPartType PartType { get; set; }
        public Pixel Pixel { get; }
        public BodyPart(BodyPartType partType, Pixel pixel)
        {
            PartType = partType;
            Pixel = pixel;
        }
    }
}
