namespace AdventOfCode2020.Entities
{
    internal class Operation
    {
        public string Name { get; set; }

        public int Argument { get; }

        public Operation(string name, int argument)
        {
            Name = name;
            Argument = argument;
        }
    }
}
