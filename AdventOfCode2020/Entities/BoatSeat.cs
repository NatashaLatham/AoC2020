using System;
using System.Collections.Generic;

namespace AdventOfCode2020.Entities
{
    /// <summary>
    /// Seat on a boat
    /// </summary>
    internal class BoatSeat
    {
        public int Row { get; }

        public int Column { get; }

        public bool IsFloor { get; }

        public bool IsTaken { get; private set; }

        public bool ChangeStatus { get; set; }

        public IEnumerable<BoatSeat> AdjacentSeats { get; set; }

        public IEnumerable<BoatSeat> VisibleSeats { get; set; }

        public BoatSeat(int row, int column, char status)
        {
            Row = row;
            Column = column;
            switch (status)
            {
                case '.':
                    IsFloor = true;
                    break;
                case '#':
                    IsTaken = true;
                    break;
                case 'L':
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status));
            }
        }

        public void Update()
        {
            IsTaken = !IsTaken;
        }

        public void Print()
        {
            var character = IsFloor ? "." : IsTaken ? "#" : "L";
            Console.Write(character);
        }
    }
}
