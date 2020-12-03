using System;
using System.IO;

namespace AdventOfCode2020.Solutions
{
    internal abstract class Day
    {
        /// <summary>
        /// The file that contains the input data
        /// </summary>
        protected string DataFile { get; set; }

        public void Solution()
        {
            WriteStartLine();
            WritePartLine(1);
            SolutionPart1();
            WritePartLine(2);
            SolutionPart2();
        }

        protected abstract void SolutionPart1();

        protected abstract void SolutionPart2();

        protected int[] ReadFileAsIntegers()
        {
            var content = ReadFile();
            var converted = Array.ConvertAll(content, Convert.ToInt32);
            return converted;
        }

        protected string[] ReadFile()
        {
            var content = File.ReadAllLines(DataFile);
            return content;
        }

        private void WriteStartLine()
        {
            Console.WriteLine($"-------------------- {GetType().Name} --------------------");
        }

        private void WritePartLine(int part)
        {
            Console.WriteLine($"-------------------- {part}");
        }

        public static void WriteEndLine()
        {
            Console.WriteLine($"----------------------------------------------");
        }
    }
}
