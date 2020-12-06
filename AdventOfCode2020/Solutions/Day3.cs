using System;
using System.Drawing;

namespace AdventOfCode2020.Solutions
{
    internal class Day3 : Day
    {
        private string[] content;
        private int mapWidth = 0;
        private int mapHeight = 0;

        public Day3() : base("Day3.txt")
        {
        }

        protected override void Initialize()
        {
            //content = GetTestMap();
            content = ReadFile();

            mapWidth = content[0].Length;
            mapHeight = content.Length;
        }

        /// <summary>
        /// Find the number of trees
        /// </summary>
        protected override void SolutionPart1()
        {
            var numberOfTrees = NumberOfTreesInSlope(3, 1);
            Console.WriteLine($"Result: {numberOfTrees}");
        }

        protected override void SolutionPart2()
        {
            var numberOfTreesSlope1 = NumberOfTreesInSlope(1, 1);
            var numberOfTreesSlope2 = NumberOfTreesInSlope(3, 1);
            var numberOfTreesSlope3 = NumberOfTreesInSlope(5, 1);
            var numberOfTreesSlope4 = NumberOfTreesInSlope(7, 1);
            var numberOfTreesSlope5 = NumberOfTreesInSlope(1, 2);

            long result = numberOfTreesSlope1 * numberOfTreesSlope2 * numberOfTreesSlope3 * numberOfTreesSlope4 * numberOfTreesSlope5;

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Result (formatted): {result:N0}");
        }

        private long NumberOfTreesInSlope(int stepsRight, int stepsDown)
        {
            long numberOfTrees = 0;

            var position = new Point(0, 0);
            while (position.Y < mapHeight)
            {
                // Check for tree
                if (IsTree(content, position))
                {
                    numberOfTrees += 1;
                }

                // Move
                position = Move(position, stepsRight, stepsDown);
            }

            Console.WriteLine($"Number of Trees: {numberOfTrees}");

            return numberOfTrees;
        }

        private bool IsTree(string[] content, Point position)
        {
            // Check for tree
            var line = content[position.Y];
            var chr = line[position.X];

            if (chr == '#')
            {
                return true;
            }

            return false;
        }

        private Point Move(Point position, int stepsRight, int stepsDown)
        {
            var newPosition = new Point(position.X, position.Y);
            newPosition.X += stepsRight;
            newPosition.Y += stepsDown;

            if (newPosition.X > mapWidth - 1)
            {
                newPosition.X -= mapWidth;
            }

            return newPosition;
        }

        private string[] GetTestMap()
        {
            var line1 = "..##.......";
            var line2 = "#...#...#..";
            var line3 = ".#....#..#.";
            var line4 = "..#.#...###";

            var result = new[] { line1, line2, line3, line4 };
            return result;
        }
    }
}
