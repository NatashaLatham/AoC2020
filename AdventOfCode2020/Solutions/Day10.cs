using System;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    internal class Day10 : Day
    {
        private int[] adapters;
        private int[] diffs;

        public Day10() : base("Day10.txt")
        {
        }

        protected override void Initialize()
        {
            //var content = GetExample();
            var content = GetExample2();
            //var content = ReadFile();

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
            diffs = new[] { 0, 0, 0 };

            // check the difference between the current and next joltage
            // increase the respective variable
            for (int i = 0; i < adapters.Length - 1; i++)
            {
                var diff = adapters[i + 1] - adapters[i];
                diffs[diff - 1]++;
            }

            // multiply oneDiff with threeDiff
            var result = diffs[0] * diffs[2];
            Console.WriteLine($"Result: {result}");
        }

        protected override void SolutionPart2()
        {
            long result = 1;

            var index = 0;

            while (index < adapters.Length)
            {
                // Get next adapter three jolts away
                var nextAdapter = adapters.SingleOrDefault(x => x == adapters[index] + 3);
                if (nextAdapter == 0)
                {
                    nextAdapter = adapters.SingleOrDefault(x => x == adapters[index] + 2);
                }

                if (nextAdapter == 0)
                {
                    index++;
                    //Console.WriteLine($"No next adapter, moving to next index: {index}");
                }
                else
                {
                    var nextIndex = Array.IndexOf(adapters, nextAdapter);
                    //Console.WriteLine($"Next adapter: {nextAdapter} at index: {nextIndex}");

                    // Get number of adapters in between
                    var diff = nextIndex - index;

                    switch(diff)
                    {
                        case 3:
                           result *= 4;
                            //Console.WriteLine($"Multiply by 4 --> {result}");
                            break;
                        case 2:
                            result *= 2;
                            //Console.WriteLine($"Multiply by 4 --> {result}");
                            break;
                        default:
                            break;
                    }

                    index = nextIndex;
                }
            }

            //result *= diffs[0];

            Console.WriteLine($"Result: {result}");
        }

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
