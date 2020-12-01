using System;
using System.IO;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
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
