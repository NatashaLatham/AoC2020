namespace AdventOfCode2020.Entities
{
    /// <summary>
    /// Seat on a flight
    /// </summary>
    internal class Seat
    {
        public int Row { get; }

        public int Column { get; }

        public int SeatId { get; private set; }

        public bool Taken { get; private set; }

        public Seat(int row, int column)
        {
            Row = row;
            Column = column;
            Taken = false;
        }

        public void Update(int seatId)
        {
            SeatId = seatId;
            Taken = true;
        }
    }
}
