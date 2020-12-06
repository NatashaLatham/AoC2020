using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    internal class Day06 : Day
    {
        private string[] content;

        public Day06() : base("Day06.txt")
        {
        }

        protected override void Initialize()
        {
            //content = GetExamples();
            content = ReadFile();
        }

        protected override void SolutionPart1()
        {
            var questionsAnsweredWithYesPerGroup = new List<string>();

            var totalNumberOfQuestionsAnweredWithYes = 0;

            foreach (var line in content)
            {
                if (line.Length == 0)
                {
                    // New group --> Count unique questions in previous group
                    totalNumberOfQuestionsAnweredWithYes += NumberOfUniqueQuestionsAnsweredWithYes(questionsAnsweredWithYesPerGroup);
                    questionsAnsweredWithYesPerGroup = new List<string>();
                }
                else
                {
                    questionsAnsweredWithYesPerGroup.Add(line);
                }
            }

            // Add last group
            totalNumberOfQuestionsAnweredWithYes += NumberOfUniqueQuestionsAnsweredWithYes(questionsAnsweredWithYesPerGroup);

            Console.WriteLine($"Sum of number of questions to which anyone answered yes: {totalNumberOfQuestionsAnweredWithYes}");
        }

        protected override void SolutionPart2()
        {
            var questionsAnsweredWithYesPerGroup = new List<string>();

            var totalNumberOfQuestionsAnweredWithYes = 0;

            foreach (var line in content)
            {
                if (line.Length == 0)
                {
                    // New group --> Count questions that everyone has in their list
                    totalNumberOfQuestionsAnweredWithYes += NumberOfQuestionsEveryoneAnsweredWithYes(questionsAnsweredWithYesPerGroup);
                    questionsAnsweredWithYesPerGroup = new List<string>();
                }
                else
                {
                    questionsAnsweredWithYesPerGroup.Add(line);
                }
            }

            // Add last group
            totalNumberOfQuestionsAnweredWithYes += NumberOfQuestionsEveryoneAnsweredWithYes(questionsAnsweredWithYesPerGroup);

            Console.WriteLine($"Sum of number of questions to which everyone answered yes: {totalNumberOfQuestionsAnweredWithYes}");
        }

        private int NumberOfUniqueQuestionsAnsweredWithYes(List<string> questions)
        {
            var result = questions.SelectMany(x => x).Distinct().Count();
            return result;
        }

        private int NumberOfQuestionsEveryoneAnsweredWithYes(List<string> questions)
        {
            var result = 0;

            var uniqueQuestions = questions.SelectMany(x => x).Distinct();
            foreach (var question in uniqueQuestions)
            {
                if (questions.All(x => x.Contains(question)))
                {
                    result++;
                }
            }

            return result;
        }

        private string[] GetExamples()
        {
            var line01 = "abc";     // Answers for person 1
            var line02 = "";        // -----------------------> New group
            var line03 = "a";       // Answers for person 1
            var line04 = "b";       // Answers for person 2
            var line05 = "c";       // Answers for person 3
            var line06 = "";        // -----------------------> New group
            var line07 = "ab";      // Answers for person 1
            var line08 = "ac";      // Answers for person 2
            var line09 = "";        // -----------------------> New group
            var line10 = "a";       // Answers for person 1
            var line11 = "a";       // Answers for person 2
            var line12 = "a";       // Answers for person 3
            var line13 = "a";       // Answers for person 4
            var line14 = "";        // -----------------------> New group
            var line15 = "b";       // Answers for person 1

            var result = new[] { line01, line02, line03, line04, line05, line06, line07, line08, line09,
                                 line10, line11, line12, line13, line14, line15 };

            return result;
        }
    }
}
