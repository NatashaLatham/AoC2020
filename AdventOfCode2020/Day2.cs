using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day2
    {
        public static void Solutions()
        {
            SolutionPart1();
            SolutionPart2();
        }

        /// <summary>
        /// How many passwords are valid?
        /// </summary>
        private static void SolutionPart1()
        {
            var content = ReadFile();

            var numberOfValidPasswords = 0;
            foreach (var line in content)
            {
                // split data by spaces
                string[] parts = line.Split(' ');

                // part 1 - min/max
                string[] policyNumbers = parts[0].Split("-");
                var policyMin = int.Parse(policyNumbers[0]);
                var policyMax = int.Parse(policyNumbers[1]);

                // part 2 - letter
                var policyLetter = parts[1].Substring(0, parts[1].Length - 1)[0]; // remove :

                // part 3 - password
                var password = parts[2];

                // if the password is within the 
                var numberOfCharactersInPassword = Array.FindAll(password.ToCharArray(), e => e == policyLetter).Length; 

                if (numberOfCharactersInPassword >= policyMin && numberOfCharactersInPassword <= policyMax)
                {
                    numberOfValidPasswords += 1;
                }

            }

            Console.WriteLine("Result: " + numberOfValidPasswords); // 465
        }

        private static void SolutionPart2()
        {
            var content = ReadFile();

            var numberOfValidPasswords = 0;
            foreach (var line in content)
            {
                // split data by spaces
                string[] parts = line.Split(' ');

                // part 1 - min/max
                string[] policyNumbers = parts[0].Split("-");

                // make policy positions 0-based
                var policyPosition1 = int.Parse(policyNumbers[0]) - 1; // policy letter should be at this position
                var policyPosition2 = int.Parse(policyNumbers[1]) - 1; // policy letter should NOT be at this position

                // part 2 - letter
                var policyLetter = parts[1].Substring(0, parts[1].Length - 1)[0]; // remove : and take first char

                // part 3 - password
                var password = parts[2];

                // if the password is within the 
                var letterAtPosition1 = password[policyPosition1];
                var letterAtPosition2 = password[policyPosition2];

                if (letterAtPosition1 == policyLetter ^ letterAtPosition2 == policyLetter )
                {
                    numberOfValidPasswords += 1;
                }
            }

            Console.WriteLine("Result: " + numberOfValidPasswords);
        }

        private static string[] ReadFile()
        {
            var content = File.ReadAllLines(@".\Data\Day2.txt");
            return content;
        }
    }
}
