using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day1
    {
        public static void SolutionPart1()
        {
            var content = ReadFile();

            var result = 0;
            foreach (var number in content)
            {
                var searchNumber = 2020 - number;

                var found = Array.Find(content, x => x == searchNumber);

                if (found != 0)
                {
                    Console.WriteLine($"Found it: {number} + {found}");
                    result = number * found;
                    break;
                }
            }

            Console.WriteLine("Result: " + result.ToString("N0"));
        }

        public static void SolutionPart2()
        {
            var content = ReadFile();

            var result = 0;
            foreach (var number in content)
            {
                var searchNumber = 2020 - number;

                var found = Array.Find(content, x => x == searchNumber);

                if (found != 0)
                {
                    Console.WriteLine($"Found it: {number} + {found}");
                    result = number * found;
                    break;
                }
            }

            Console.WriteLine("Result: " + result.ToString("N0"));
        }

        private static int[] ReadFile()
        {
            var content = File.ReadAllLines(@".\Data\Day1.txt");
            var converted = Array.ConvertAll(content, Convert.ToInt32);
            return converted;
        }
    }
}
