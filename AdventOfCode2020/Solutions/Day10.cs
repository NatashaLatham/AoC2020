using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    internal class Day10 : Day
    {
        private int[] adapters;
        private List<int> deltas;

        public Day10() : base("Day10.txt")
        {
        }

        protected override void Initialize()
        {
            //var content = GetExample();
            //var content = GetExample2();
            var content = ReadFile();

            var jolts = Array.ConvertAll(content, e => int.Parse(e));
            var maxValue = jolts.Max();
            adapters = new int[jolts.Length + 2];
            Array.Copy(jolts, adapters, jolts.Length);
            adapters[adapters.Length - 2] = 0;             // add 0 as the initial value
            adapters[adapters.Length - 1] = maxValue + 3;  // add the device

            // sort array in ascending order
            Array.Sort(adapters);
        }

        protected override void SolutionPart1()
        {
            deltas = new List<int>();

            // check the difference between the current and next joltage
            // add to deltas list (needed for part 2)
            for (int i = 0; i < adapters.Length - 1; i++)
            {
                var diff = adapters[i + 1] - adapters[i];
                deltas.Add(diff);
            }

            // multiply oneDiff with threeDiff
            var oneDiff = deltas.Count(x => x == 1);
            var threeDiff = deltas.Count(x => x == 3);
            var result = oneDiff * threeDiff;
            Console.WriteLine($"Result: {result} ({oneDiff} 1 jolt difference, {threeDiff} 3 jolts difference");
        }

        protected override void SolutionPart2()
        {
            // There is always the entire combination of adapters
            long result = 1;

            // continous ones
            var index = 0;
            while (index < deltas.Count)
            {
                var nextIndex = deltas.IndexOf(3, index);

                if (nextIndex < 0)
                {
                    // No more adapters
                    break;
                }

                var sequenceLenght = nextIndex - index;

                if (sequenceLenght > 0)
                {
                    var possibleCombinations = PossibleCombinations(sequenceLenght);
                    result *= possibleCombinations;
                }

                index = nextIndex;
                index++;
            }

            Console.WriteLine($"Result: {result}");
        }

        // Couldn't find a formula, so hardcoded the possible combinations
        private long PossibleCombinations(int seqLength)
                => seqLength switch
                {
                    1 => 1,
                    2 => 2,
                    3 => 4,
                    4 => 7,
                    5 => 13,
                    6 => 22,
                    _ => 0
                };

        private string[] GetExample()
        {
            // difference is 7 - 1 jolt dif, 5 - 3 jolt dif, so 7 * 5 = 35
            var line01 = "16";
            var line02 = "10";
            var line03 = "15";
            var line04 = "5";
            var line05 = "1";
            var line06 = "11";
            var line07 = "7";
            var line08 = "19";
            var line09 = "6";
            var line10 = "12";
            var line11 = "4";
            var line12 = "18"; // Not from example

            var result = new[] { line01, line02, line03, line04, line05, line06, line07, line08, line09, line10, line11, line12 };

            return result;
        }

        private string[] GetExample2()
        {
            // difference is 22 - 1 jolt dif, 20 - 3 jolt dif, so 22 * 10 = 220
            var line01 =
@"28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3";

            var result = line01.Split(Environment.NewLine);

            return result;
        }
    }
}
