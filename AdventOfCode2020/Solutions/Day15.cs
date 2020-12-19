using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    internal class Day15 : Day
    {
        private IList<int> startingNumbers;
        private int[] spokenNumbers;

        public Day15() : base("Day15.txt")
        {
        }

        protected override void Initialize()
        {
            var exampleNumber = 1;

            //var content = GetExamples();
            var content = ReadFile();

            var numbers = content[exampleNumber - 1].Split(",");

            // Add the starting numbers to the starting numbers list
            startingNumbers = new List<int>();
            foreach (var number in numbers)
            {
                startingNumbers.Add(int.Parse(number));
            }
        }

        protected override void SolutionPart1()
        {
            const int noOfNumbersToSpeak = 2020;
            spokenNumbers = new int[noOfNumbersToSpeak];

            var numberSpoken = -1;
            var turnsSinceLastSpoken = 0;
            var firstTime = false;

            for (var turn = 1; turn <= noOfNumbersToSpeak; turn++)
            {
                numberSpoken = SpeakNumber(turn, ref firstTime, ref turnsSinceLastSpoken);
            }

            Console.WriteLine($"The 2020th number spoken was: {numberSpoken}");
        }

        protected override void SolutionPart2()
        {
            const int noOfNumbersToSpeak = 30000000;
            spokenNumbers = new int[noOfNumbersToSpeak];

            var numberSpoken = -1;
            var turnsSinceLastSpoken = 0;
            var firstTime = false;

            for (var turn = 1; turn <= noOfNumbersToSpeak; turn++)
            {
                numberSpoken = SpeakNumber(turn, ref firstTime, ref turnsSinceLastSpoken);
            }

            Console.WriteLine($"The 30000000th number spoken was: {numberSpoken}");
        }

        private bool Starting(int turn)
        {
            return turn <= startingNumbers.Count();
        }

        private int SpeakNumber(int turn, ref bool firstTime, ref int turnsSinceLastSpoken)
        {
            int numberSpoken;
            // Take starting number if there is one left
            if (Starting(turn))
            {
                numberSpoken = startingNumbers[turn - 1];
            }
            else if (firstTime)   // If first time, return zero
            {
                numberSpoken = 0;
            }
            else
            {
                numberSpoken = turnsSinceLastSpoken;  // Otherwise return number of turns ago number was spoken
            }

            if (numberSpoken > -1)
            {
                firstTime = spokenNumbers[numberSpoken] == 0;
            }

            if (!firstTime)
            {
                turnsSinceLastSpoken = turn - spokenNumbers[numberSpoken];
            }

            spokenNumbers[numberSpoken] = turn;

            return numberSpoken;
        }

        private string[] GetExamples()
        {
            var line01 = "0,3,6";  // First example
            var line02 = "1,3,2";  // Second example
            var line03 = "2,1,3";  // Third example
            var line04 = "1,2,3";  // Fourth example
            var line05 = "2,3,1";  // Fifth example
            var line06 = "3,2,1";  // Sixt example
            var line07 = "3,1,2";  // Seventh example

            var result = new[] { line01, line02, line03, line04, line05, line06, line07 };

            return result;
        }
    }
}
