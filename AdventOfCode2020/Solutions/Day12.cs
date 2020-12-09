using System;

namespace AdventOfCode2020.Solutions
{
    internal class Day12 : Day
    {
        private string[] content;

        public Day12() : base("Day12.txt")
        {
        }

        protected override void Initialize()
        {
            content = GetExample();
            //content = ReadFile();
        }

        protected override void SolutionPart1()
        {
            var result = 0;
            Console.WriteLine($"Result: {result}");
        }

        protected override void SolutionPart2()
        {
            var result = 0;
            Console.WriteLine($"Result: {result}");
        }

        private string[] GetExample()
        {
            var line01 = "";
            var line02 = "";
            var line03 = "";
            var line04 = "";

            var result = new[] { line01, line02, line03, line04 };

            return result;
        }
    }
}
