using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Entities
{
    public class Operation
    {
        public string Name { get; set; }

        public int Argument { get; set; }

        public bool Visited { get; set; }

        public Operation(string name, int argument)
        {
            Name = name;
            Argument = argument;
        }
    }
}
