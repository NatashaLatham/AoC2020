using System;

namespace AdventOfCode2020.Solutions
{
    internal class Day01 : Day
    {
        private int[] content;

        public Day01() : base("Day01.txt")
        {
        }

        protected override void Initialize()
        {
            var contentAsStrings = ReadFile();
            content = Array.ConvertAll(contentAsStrings, Convert.ToInt32);
        }

        protected override void SolutionPart1()
        {
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

        protected override void SolutionPart2()
        {
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

                if (result != 0)
                {
                    break;
                }
            }

            Console.WriteLine("Result: " + result.ToString("N0"));
        }
    }
}
