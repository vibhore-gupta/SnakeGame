using SnakeGame.Source.Common;
using SnakeGame.Source.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame.Source
{
    public class Snake
    {
        private int xCoordinate;
        private readonly int yCoordinate;
        public List<BodyPart> BodyParts { get; }
    
        public Snake(int bodyPartCount)
        {
            xCoordinate = 15;
            yCoordinate = 17;
            BodyParts = new List<BodyPart>();
            Generate(bodyPartCount);
        }

        public Snake(List<Pixel> pixels)
        {
            xCoordinate = 15;
            yCoordinate = 17;
            BodyParts = new List<BodyPart>();
            GenerateFromCoordinates(pixels);
        }

        private void GenerateFromCoordinates(List<Pixel> pixels)
        {
            if (pixels.Count == 0)
            {
                Generate(1);
                return;
            }
            for (var i = 0; i < pixels.Count; i++)
            {
                var pixel = pixels[i];
                BodyParts.Add(new BodyPart(BodyPartType.BODY, new Pixel(pixel.XCoordinate, pixel.YCoordinate,
                    pixel.ConsoleColor, pixel.CurrentDirection)));
            }
            BodyParts[^1].PartType = BodyPartType.HEAD;
            BodyParts[0].PartType = BodyPartType.TAIL;
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

        private static void SetDirection(Pixel pixel, ConsoleKey consoleKey)
        {
            pixel.CurrentDirection = SnakeBuilderHelper.GetNewDirectionForHead(consoleKey);
        }

        public void Reposition()
        {
            var snakeBodyParts = BodyParts;
            for (var i = 0; i < snakeBodyParts.Count; i++)
            {
                var xCoordinate = snakeBodyParts[i].Pixel.XCoordinate;
                var yCoordinate = snakeBodyParts[i].Pixel.YCoordinate;
                Console.SetCursorPosition(xCoordinate, yCoordinate);
                Console.Write(" ");
                snakeBodyParts[i].Pixel.XCoordinate = i + 15;
                snakeBodyParts[i].Pixel.YCoordinate = 17;
                snakeBodyParts[i].Pixel.CurrentDirection = Direction.RIGHT;
            }
        }

        private static void SetPixelCoordinates(Pixel pixel, int newXCoordinate, int newYCoordinate)
        {
            pixel.XCoordinate = newXCoordinate;
            pixel.YCoordinate = newYCoordinate;
        }

        public Pixel GetPixelByBodyType(BodyPartType bodyPartType)
        {
            return BodyParts.FirstOrDefault(p => p.PartType == bodyPartType).Pixel;
        }

        public List<KeyValuePair<int, int>> GetSnakeCoordinates()
        {
            return BodyParts.Select(x => new KeyValuePair<int, int>(x.Pixel.XCoordinate, x.Pixel.YCoordinate)).ToList();
        }

        private void Generate(int bodyPartsCount)
        {
            if(bodyPartsCount == 0)
            {
                bodyPartsCount = 1;
            }
            BodyParts.Add(new BodyPart(BodyPartType.TAIL, new Pixel(xCoordinate, yCoordinate, ConsoleColor.Yellow, Direction.RIGHT)));            
            for (var i = 0; i < bodyPartsCount; i++)
            {
                xCoordinate += 1;
                BodyParts.Add(new BodyPart(BodyPartType.BODY, new Pixel(xCoordinate, yCoordinate, ConsoleColor.Yellow, Direction.RIGHT)));
            }
            BodyParts.Add(new BodyPart(BodyPartType.HEAD, new Pixel(xCoordinate + 1, yCoordinate, ConsoleColor.Green, Direction.RIGHT)));
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
