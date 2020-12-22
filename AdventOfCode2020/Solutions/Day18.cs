using System;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    internal class Day18 : Day
    {
        private string[] content;

        public Day18() : base("Day18.txt")
        {
        }

        protected override void Initialize()
        {
            //content = GetExample();
            content = ReadFile();
        }

        protected override void SolutionPart1()
        {
            var sumOfResultingValues = 0L;

            foreach (var line in content)
            {
                var calculationLine = line;
                // Replace parentheses with calculation result
                var parenthesesFound = calculationLine.IndexOf("(") >= 0;
                while (parenthesesFound)
                {
                    calculationLine = ReplaceParentheses(calculationLine);
                    parenthesesFound = calculationLine.IndexOf("(") >= 0;
                }

                // Calculate the remainder
                var result = Calculate(calculationLine);
                sumOfResultingValues += result;

                //Console.WriteLine($"Result is: {result}");
            }

            Console.WriteLine($"Result: {sumOfResultingValues}");
        }

        protected override void SolutionPart2()
        {
            var result = 0;
            Console.WriteLine($"Result: {result}");
        }

        private string ReplaceParentheses(string input)
        {
            var elements = input.Split('(', ')');

            if (elements.Length == 1)
            {
                return input;
            }

            // The ones bigger than 4 contain a formula between parentheses
            var result = input;
            foreach (var element in elements.Where(x => x.Trim().Length > 4 && !x.StartsWith(' ') && !x.EndsWith(' ')))
            {
                var calculationResult = Calculate(element);
                result = result.Replace($"({element})", calculationResult.ToString());
            }

            result = ReplaceParentheses(result);
            return result;
        }

        private long Calculate(string line)
        {
            var elements = line.Split(" ");
            long result = 0;
            var leftElement = long.Parse(elements[0]);
            for (var i = 1; i < elements.Length; i += 2)
            {
                var operation = elements[i];
                var rightElement = long.Parse(elements[i + 1]);
                result = operation switch
                {
                    "+" => Add(leftElement, rightElement),
                    "*" => Multiply(leftElement, rightElement),
                    "-" => Subtract(leftElement, rightElement),
                    _ => throw new ArgumentOutOfRangeException(nameof(operation)),
                };
                leftElement = result;
            }

            return result;
        }

        private long Add(long a, long b)
        {
            return a + b;
        }

        private long Subtract(long a, long b)
        {
            return a - b;
        }

        private long Multiply(long a, long b)
        {
            return a * b;
        }

        private string[] GetExample()
        {
            var line01 = "1 + 2 * 3 + 4 * 5 + 6";        // 71
            var line02 = "1 + (2 * 3) + (4 * (5 + 6))";  // 51
            var line03 = "2 * 3 + (4 * 5)";              // 26
            var line04 = "5 + (8 * 3 + 9 + 3 * 4 * 3)";  // 437
            var line05 = "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))"; // 12240
            var line06 = "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2";  // 13631

            var result = new[] { line01, line02, line03, line04, line05, line06 };

            return result;
        }
    }
}
