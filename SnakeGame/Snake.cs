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
            var headPixel = GetHeadPixel();
            var (newXCoordinate, newYCoordinate) = SnakeBuilderHelper.GetNewCoordinatesForHead(headPixel, headPixel.CurrentDirection);
            SetHeadPixels(headPixel, newXCoordinate, newYCoordinate);
        }

        public void ReBuildHeadAccordingToKey(ConsoleKey consoleKey)
        {
            var headPixel = GetHeadPixel();
            var (newXCoordinate, newYCoordinate) = SnakeBuilderHelper.GetNewCoordinatesForHead(headPixel, consoleKey);
            SetHeadPixels(headPixel, newXCoordinate, newYCoordinate);
            SetHeadDirection(headPixel, consoleKey);
        }

        public void Enlarge()
        {
            var currentTail = BodyParts.FirstOrDefault(p => p.PartType == BodyPartType.TAIL);
            var currentTailPixel = GetTailPixel();
            var (newTailXCoordinate, newTailYCoordinate) = SnakeBuilderHelper.GetNewCoordinatesForTail(currentTailPixel, currentTailPixel.CurrentDirection);
            var newTail = new Pixel(newTailXCoordinate, newTailYCoordinate, currentTailPixel.ConsoleColor, currentTailPixel.CurrentDirection);
            currentTail.PartType = BodyPartType.BODY;
            BodyParts.Insert(0, new BodyPart(BodyPartType.TAIL, newTail));
        }

        private void SetHeadDirection(Pixel headPixel, ConsoleKey consoleKey)
        {
            headPixel.CurrentDirection = SnakeBuilderHelper.GetNewDirectionForHead(consoleKey);
        }

        private void SetHeadPixels(Pixel headPixel, int newXCoordinate, int newYCoordinate)
        {
            headPixel.XCoordinate = newXCoordinate;
            headPixel.YCoordinate = newYCoordinate;
        }

        public Pixel GetHeadPixel()
        {
            return BodyParts.FirstOrDefault(p => p.PartType == BodyPartType.HEAD).Pixel;
        }

        public Pixel GetTailPixel()
        {
            return BodyParts.FirstOrDefault(p => p.PartType == BodyPartType.TAIL).Pixel;
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
