using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day3
    {
        public static void Solutions()
        {
            SolutionPart1();
            SolutionPart2();
        }

        /// <summary>
        /// How many passwords are valid?
        /// </summary>
        private static void SolutionPart1()
        {
            var content = ReadFile();

            // TODO: Create map and traverse that (how many rows are there? 11?)

            var rightPosition = 3;
            //var downPosition = 0;
            long numberOfTrees = 0;

            for (int i = 1; i < content.Length; i++)
            {

                var line = content[i];
                // avoid reaching the end of the length
                if(rightPosition >= line.Length)
                {
                    rightPosition -= line.Length;
                    i++;
                } 

                line = content[i];
                var chr = line[rightPosition];

                if(chr == '#')
                {
                    numberOfTrees += 1;
                }

                rightPosition += 3;
            }

            Console.WriteLine($"Result: {numberOfTrees}");
        }

        private static void SolutionPart2()
        {
            

            Console.WriteLine($"Result: ");
        }

        private static string[] ReadFile()
        {
            var content = File.ReadAllLines(@".\Data\Day3.txt");
            return content;
        }

        //private string[] CreateMap(string[] content)
        //{



        //}
    }
}
