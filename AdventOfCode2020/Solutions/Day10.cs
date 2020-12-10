using System;

namespace AdventOfCode2020.Solutions
{
    internal class Day10 : Day
    {
        private string[] content;
        private string[] jolts;

        public Day10() : base("Day10.txt")
        {
        }

        protected override void Initialize()
        {
            //content = GetExample();
            content = ReadFile();

            jolts = new string[content.Length + 1];
            Array.Copy(content, jolts, content.Length);
            jolts[content.Length] = "0";  // add 0 as the initial value
        }

        protected override void SolutionPart1()
        {
            var result = 0;

            var oneDiff = 0;
            var threeDiff = 0;
            var maxJoltDif = 3;

            var adapters = Array.ConvertAll(jolts, e => int.Parse(e));

            // sort array in ascending order
            Array.Sort(adapters);

            // check the difference between the current and next joltage
            // increase the respective variable
            for (int i = 0; i < adapters.Length; i++)
            {
                if (i == adapters.Length - 1) // if at the last adapter
                {
                    threeDiff++;
                    break;
                }

                var diff = adapters[i + 1] - adapters[i];

                if (diff == 1)
                {
                    oneDiff++;
                }

                if (diff == 3)
                {
                    threeDiff++;
                }
            }

            // multiply oneDiff with threeDiff
            result = oneDiff * threeDiff;
            Console.WriteLine($"Result: {result}");
        }

        protected override void SolutionPart2()
        {
            var result = 0;
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

            var result = new[] { line01, line02, line03, line04, line05, line06, line07, line08, line09, line10, line11 };

            return result;
        }
    }
}
