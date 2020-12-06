using System;
using System.IO;

namespace AdventOfCode2020.Solutions
{
    /// <summary>
    /// Base class for daily solutions
    /// </summary>
    internal abstract class Day
    {
        private const string fileBasePath = @".\Data\";

        // The file that contains the input data
        private readonly string dataFile;

        /// <summary>
        /// Constructor
        /// </summary>
        protected Day()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataFileName">The name of the datafile</param>
        protected Day(string dataFileName)
        {
            dataFile = Path.Combine(fileBasePath, dataFileName);
            Initialize();
        }

        /// <summary>
        /// Write part 1 and part 2 of the solution for this day to the console
        /// </summary>
        public void Solution()
        {
            WriteStartLine();
            WritePartLine(1);
            SolutionPart1();
            WritePartLine(2);
            SolutionPart2();
        }

        /// <summary>
        /// Initiliazation
        /// </summary>
        abstract protected void Initialize();

        /// <summary>
        /// Part 1 of the solution
        /// </summary>
        abstract protected void SolutionPart1();

        /// <summary>
        /// Part 2 of the solution
        /// </summary>
        abstract protected void SolutionPart2();

        /// <summary>
        /// Read all lines in the input file
        /// </summary>
        /// <returns></returns>
        protected string[] ReadFile()
        {
            var content = File.ReadAllLines(dataFile);
            return content;
        }

        private void WriteStartLine()
        {
            Console.WriteLine($"-------------------- {GetType().Name} --------------------");
        }

        private void WritePartLine(int part)
        {
            Console.WriteLine($"-------------------- Solution - {part}");
        }

        public static void WriteEndLine()
        {
            Console.WriteLine($"----------------------------------------------");
        }
    }
}
