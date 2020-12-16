namespace AdventOfCode2020.Entities
{
    internal class Bus
    {
        /// <summary>
        /// The number of the bus in the provided list
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// The Id of the bus that specifies the timeinterval a bus leaves
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The first departure time after the specified timespan (calculated)
        /// </summary>
        public int DepartureTime { get; set; }

        public Bus(int number, int id)
        {
            Number = number;
            Id = id;
        }
    }
}
