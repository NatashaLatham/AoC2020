using AdventOfCode2020.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    internal class Day08 : Day
    {
        private string[] content;

        private List<Operation> operations = new List<Operation>();

        public Day08() : base("Day08.txt")
        {
        }

        protected override void Initialize()
        {
            content = GetExamples();
            //content = ReadFile();

            // process input into operation and arg
            foreach (var operation in content)
            {
                var parts = operation.Split(' ');
                var operationName = parts[0];
                var argument = int.Parse(parts[1]);

                // create object to remember if visited
                operations.Add(new Operation(operationName, argument));
            }
        }

        protected override void SolutionPart1()
        {
            var accumulator = 0;

            // execute operations and mark as visited
            for (int i = 0; i < operations.Count;)
            {
                if (operations[i].Visited)
                {
                    break;
                }

                operations[i].Visited = true;

                switch (operations[i].Name)
                {
                    case "acc":
                        accumulator += operations[i].Argument;
                        i++;
                        break;
                    case "jmp": // execute the  
                        i += operations[i].Argument;
                        break;
                    default: // nop -- next
                        i++;
                        break;
                }
            }

            Console.WriteLine($"Result: {accumulator}");
        }

        protected override void SolutionPart2()
        {
            // reset visited
            operations.ForEach(o => o.Visited = false);

            var accumulator = 0;
            Operation prevOperation = null;
            var prevOperationPosition = 0;

            // execute operations and mark as visited
            for (int i = 0; i < operations.Count;)
            {
                if (operations[i].Visited)
                {
                    if(prevOperation != null && prevOperation.Name == "jmp")
                    {
                        operations[prevOperationPosition].Name = "nop";
                        operations[prevOperationPosition].Visited = false;
                        i = prevOperationPosition;
                    }

                    else if (prevOperation != null && prevOperation.Name == "nop")
                    {
                        operations[prevOperationPosition].Name = "jmp";
                        operations[prevOperationPosition].Visited = false;
                        i = prevOperationPosition;
                    }
                }

                operations[i].Visited = true;
                prevOperation = operations[i];
                prevOperationPosition = i;

                switch (operations[i].Name)
                {
                    case "acc":
                        accumulator += operations[i].Argument;
                        i++;
                        break;
                    case "jmp": // execute the  
                        i += operations[i].Argument;
                        break;
                    default: // nop -- next
                        i++;
                        break;
                }
            }

            Console.WriteLine($"Result: {accumulator}");
        }

        private string[] GetExamples()
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
