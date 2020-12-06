using System;

namespace AdventOfCode2020.Solutions
{
    internal class Day09 : Day
    {
        private string[] content;

        public Day09() : base("Day09.txt")
        {
        }

        protected override void Initialize()
        {
            content = GetExamples();
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

        private string[] GetExamples()
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
