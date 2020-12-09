using System;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    internal class Day09 : Day
    {
        private long[] numbers;

        private long blackSheep;

        public Day09() : base("Day09.txt")
        {
        }

        protected override void Initialize()
        {
            //var content = GetExample();
            var content = ReadFile();

            numbers = Array.ConvertAll(content, e => long.Parse(e));

        }

        protected override void SolutionPart1()
        {
            blackSheep = 0;
            var blackSheepIsFound = false;
            var amountOfPreambleNumbers = 25;

            while (!blackSheepIsFound)
            {
                for (int count = 0; count < numbers.Length;)
                {
                    // setup
                    var currentNumber = numbers[count + amountOfPreambleNumbers];
                    var previousNumbers = numbers.Skip(count).Take(amountOfPreambleNumbers).ToArray();
                    var pairValueIsFound = false;

                    // try to find two numbers which sum to the current numbers
                    foreach (var number in previousNumbers)
                    {
                        var pairValue = currentNumber - number;
                        pairValueIsFound = Array.Exists(previousNumbers, e => e == pairValue);

                        if(pairValueIsFound)
                        {
                            count++;
                            break;
                        }
                    }

                    // break while loop when black sheep is found
                    if(!pairValueIsFound)
                    {
                        blackSheep = currentNumber;
                        blackSheepIsFound = true;
                        break;
                    }

                }
            }

            Console.WriteLine($"Result: {blackSheep}");
        }

        protected override void SolutionPart2()
        {
            var endIndex = 0;

            int startIndex;
            for (startIndex = 0; startIndex < numbers.Length; startIndex++)
            {
                if (CalculateRange(startIndex, out endIndex))
                {
                    break;
                }
            }

            if (endIndex <= 0)
            {
                Console.WriteLine("Unable to find range");
                return;
            }

            var resultRange = numbers.Skip(startIndex).Take(endIndex - startIndex + 1);
            var minimumNumberInRange = resultRange.Min();
            var maximumNumberInRange = resultRange.Max();

            var result = minimumNumberInRange + maximumNumberInRange;
            Console.WriteLine($"Result: {result}");
        }

        private bool CalculateRange(int startIndex, out int endIndex)
        {
            int index = startIndex;
            long sum = 0;

            endIndex = 0;
            while (sum < blackSheep)
            {
                sum += numbers[index];

                if (sum == blackSheep)
                {
                    endIndex = index;
                    return true;
                }

                index++;
            };

            return false;
        }

        private string[] GetExample()
        {
            var line01 = "1";
            var line02 = "2";
            var line03 = "3";
            var line04 = "4";
            var line05 = "5";
            var line06 = "6";
            var line07 = "7";
            var line08 = "8";
            var line09 = "9";
            var line10 = "10";
            var line11 = "11";
            var line12 = "12";
            var line13 = "13";
            var line14 = "14";
            var line15 = "15";
            var line16 = "16";
            var line17 = "17";
            var line18 = "18";
            var line19 = "19";
            var line20 = "20";
            var line21 = "21";
            var line22 = "22";
            var line23 = "23";
            var line24 = "24";
            var line25 = "25";
            var line26 = "26";
            var line27 = "33";
            var line28 = "29";
            var line29 = "100"; // should return 100



            var result = new[] { line01, line02, line03, line04, line05, line06, line07, line08, line09, line10,
                                 line11, line12, line13, line14, line15, line16, line17, line18, line19, line20,
                                 line21, line22, line23, line24, line25, line26, line27, line28, line29};

            return result;
        }
    }
}
