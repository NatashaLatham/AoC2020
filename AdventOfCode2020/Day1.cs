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
            foreach (var line in content)
            {
                var number = int.Parse(line);
                var searchNumber = 2020 - number;

                var found = Array.Find(content, x => x == searchNumber.ToString());

                if (found != null)
                {
                    Console.WriteLine($"Found it: {line} + {found}");
                    result = number * int.Parse(found);
                    break;
                }
            }

            Console.WriteLine("Result: " + result.ToString("N0"));
        }

        private static string[] ReadFile()
        {
            var content = File.ReadAllLines(@".\Data\Day1.txt");
            return content;
        }
    }
}
