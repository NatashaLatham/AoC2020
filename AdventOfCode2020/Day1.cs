using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day1
    {
        public static void Solutions()
        {
            SolutionPart1();
            SolutionPart2();
        }

        private static void SolutionPart1()
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

        private static void SolutionPart2()
        {
            var content = ReadFile();

            var result = 0;
            for (int i = 0; i < content.Length; i++)
            {
                for (int j = i + 1; j < content.Length; j++)
                {
                    var searchNumber2 = 2020 - content[i] - content[j];

                    var found = Array.Find(content, x => x == searchNumber2);

                    if (found != 0)
                    {
                        Console.WriteLine($"Found it: {content[i]} + {content[j]} + {found} equal to {content[i] + content[j] + found}");
                        result = content[i] * content[j] * found;
                        break;
                    }

                }

                if(result != 0)
                {
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
