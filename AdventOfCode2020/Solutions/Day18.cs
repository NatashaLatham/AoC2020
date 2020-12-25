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
                    calculationLine = ReplaceParentheses(calculationLine, 1);
                    parenthesesFound = calculationLine.IndexOf("(") >= 0;
                }

                // Calculate the remainder
                var result = Calculate(calculationLine);
                sumOfResultingValues += result;

                //Console.WriteLine($"Result is: {result}");
            }

            Console.WriteLine($"Sum of result values: {sumOfResultingValues}");
        }

        protected override void SolutionPart2()
        {
            var sumOfResultingValues = 0L;

            foreach (var line in content)
            {
                var calculationLine = line;

                // Replace parentheses with calculation result
                var parenthesesFound = calculationLine.IndexOf("(") >= 0;
                while (parenthesesFound)
                {
                    calculationLine = ReplaceParentheses(calculationLine, 2);
                    parenthesesFound = calculationLine.IndexOf("(") >= 0;
                }

                // Calculate the remainder
                var result = CalculatePart2(calculationLine);
                sumOfResultingValues += result;

                Console.WriteLine($"Result is: {result}");
            }

            Console.WriteLine($"Sum of result values: {sumOfResultingValues}");
        }

        private string ReplaceParentheses(string input, int part)
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
                long calculationResult;
                if (part == 1)
                {
                    calculationResult = Calculate(element);
                }
                else
                {
                    calculationResult = CalculatePart2(element);
                }
                result = result.ReplaceFirst($"({element})", calculationResult.ToString());
            }

            result = ReplaceParentheses(result, part);
            return result;
        }

        private long CalculatePart2(string input)
        {
            // Replace additions with their result 
            var elements = input.Split('*');

            // The ones bigger than 4 contain the additions
            var calculationLine = input;
            foreach (var element in elements.Where(x => x.Trim().Length > 4))
            {
                var trimmedElement = element.Trim();
                var calculationResult = Calculate(trimmedElement);
                calculationLine = calculationLine.ReplaceFirst($"{trimmedElement}", calculationResult.ToString());
            }

            // Calculate the remainder (only multiplications should be left)
            var result = Calculate(calculationLine);

            return result;
        }

        private long Calculate(string line)
        {
            var elements = line.Split(" ");

            if (elements.Length == 1)
            {
                return long.Parse(line);
            }

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

        private long Multiply(long a, long b)
        {
            return a * b;
        }

        private string[] GetExample()
        {
            var line01 = "1 + 2 * 3 + 4 * 5 + 6";        // 71 - 231
            var line02 = "1 + (2 * 3) + (4 * (5 + 6))";  // 51 - 51
            var line03 = "2 * 3 + (4 * 5)";              // 26 - 46
            var line04 = "5 + (8 * 3 + 9 + 3 * 4 * 3)";  // 437 - 1445
            var line05 = "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))"; // 12240 - 669060
            var line06 = "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2";  // 13631 - 23340
            var line07 = "8 * 3 + (6 + 5 + 2 * 6 * 6) * 2"; // 984 - 7536
            var line08 = "4 + (5 + (5 * 5 + 3 + 2) + (6 + 4 * 9 * 2 * 8) * 6 + (7 * 5 * 2) * (2 * 8 * 2)) + (8 * 7 + 7) * 6 * 9 * (5 + 9)"; // 215843292 - 2748782736
            var line09 = "3 + 5"; // 8 - 8
            var line10 = "2 * 2 * ((7 * 2 + 2 * 5 + 2 + 2) + 9 + 7) * 2 * (4 * 4 * (6 + 3) + 8 * 6 + 3) + (7 * 5 + 4 + 3)"; // 732042 - 5428608

            var result = new[] { line01, line02, line03, line04, line05, line06, line07, line08, line09, line10 };

            return result;
        }
    }

    /// <summary>
    /// Extension method to only replace the first occurance of a string in a string
    /// </summary>
    internal static class StringExtensionMethods
    {
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);

            if (pos < 0)
            {
                return text;
            }

            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }
    }
}
