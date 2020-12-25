using System.Collections.Generic;

namespace AdventOfCode2020.Entities
{
    internal class Rule
    {
        public int Number { get; }

        public List<string> Matches { get; }

        public Rule(int number)
        {
            Number = number;
            Matches = new List<string>();
        }
    }
}
