using AdventOfCode2020.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    internal class Day08 : Day
    {
        private ICollection<int> visitedLines;
        private IList<Operation> operations = new List<Operation>();
        private int accumulator = 0;

        public Day08() : base("Day08.txt")
        {
        }

        protected override void Initialize()
        {
            //var content = GetExample();
            var content = ReadFile();

            // process input into operation and argument
            foreach (var operation in content)
            {
                var parts = operation.Split(' ');
                var operationName = parts[0];
                var argument = int.Parse(parts[1]);

                operations.Add(new Operation(operationName, argument));
            }
        }

        protected override void SolutionPart1()
        {
            accumulator = 0;
            visitedLines = new List<int>();

            RunCode();

            Console.WriteLine($"The value of the accumulator is: {accumulator}");
        }

        protected override void SolutionPart2()
        {
            int lastReplacedJump = -1;

            var finished = TryRunCode(lastReplacedJump, "jmp", "nop");
            if (finished)
            {
                Console.WriteLine("FINISHED!!!!!");
            }
            else
            {
                finished = TryRunCode(lastReplacedJump, "nop", "jmp");
                if (finished)
                {
                    Console.WriteLine("FINISHED!!!!!");
                }
            }

            Console.WriteLine($"The value of the accumulator is: {accumulator}");
        }

        private bool TryRunCode(int lastReplacedOperation, string changeFrom, string changeTo)
        {
            accumulator = 0;
            visitedLines = new List<int>();

            Console.WriteLine($"TryRunCode. LastReplaced {changeFrom} is [{lastReplacedOperation}]");

            var finished = RunCode();

            if (!finished)
            {
                // Restore previously replaced jump
                if (lastReplacedOperation > -1)
                {
                    Console.WriteLine($"Changing {changeTo} back to {changeFrom} line [{lastReplacedOperation}]");
                    operations[lastReplacedOperation].Name = changeFrom;
                }

                // Replace jump
                var lineNumberOfNextJump = Array.IndexOf(operations.Select(x => x.Name).ToArray(), changeFrom, lastReplacedOperation + 1);

                if (lineNumberOfNextJump == -1)
                {
                    // No more jmp statements to replace
                    Console.WriteLine($"No more {changeFrom} statements to replace....");
                    return finished;
                }

                Console.WriteLine($"Changing {changeFrom} to {changeTo} line [{lineNumberOfNextJump}]");
                operations[lineNumberOfNextJump].Name = changeTo;
                finished = TryRunCode(lineNumberOfNextJump, changeFrom, changeTo);
            }

            return finished;
        }

        private bool RunCode()
        {
            var lineNumber = 0;

            while (lineNumber < operations.Count)
            {
                if (Visited(lineNumber))
                {
                    Console.WriteLine($" ---> Visited linenumber {lineNumber}. Returning false");
                    return false;
                }

                visitedLines.Add(lineNumber);

                lineNumber = ExecuteCommand(lineNumber, operations[lineNumber].Name, operations[lineNumber].Argument);
            }

            return true;
        }

        /// <summary>
        /// Executes the command an return the next line number
        /// </summary>
        private int ExecuteCommand(int lineNumber, string command, int argument)
        {
            var newLineNumber = lineNumber;

            switch (command)
            {
                case "acc":
                    accumulator += argument;
                    newLineNumber++;
                    break;
                case "jmp":  
                    newLineNumber += argument;
                    break;
                default: // nop -- next
                    newLineNumber++;
                    break;
            }

            return newLineNumber;
        }

        private bool Visited(int lineNumber)
        {
            return visitedLines.Contains(lineNumber);
        }

        private string[] GetExample()
        {
            var line01 = "nop +0";
            var line02 = "acc +1";
            var line03 = "jmp +4";
            var line04 = "acc +3";
            var line05 = "jmp -3";
            var line06 = "acc -99";
            var line07 = "acc +1";
            var line08 = "jmp -4";
            var line09 = "acc +6";

            var result = new[] { line01, line02, line03, line04, line05, line06, line07, line08, line09 };

            return result;
        }
    }
}
