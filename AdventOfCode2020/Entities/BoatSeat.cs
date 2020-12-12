using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Entities
{
    /// <summary>
    /// Seat on a boat
    /// </summary>
    internal class BoatSeat
    {
        public int Row { get; }

        public int Column { get; }

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
                case '#':
                    IsTaken = true;
                    break;
                case 'L':
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status));
            }
        }

        public void ApplyRules_Part1()
        {
            if (!IsTaken)
            {
                // If a seat is empty(L) and there are no occupied seats adjacent to it,
                // the seat becomes occupied.
                if (AdjacentSeats.All(x => !x.IsTaken))
                {
                    ChangeStatus = true;
                }
                return;
            }

            // If a seat is occupied (#) and four or more seats adjacent to it are also occupied,
            // the seat becomes empty.
            if (AdjacentSeats.Count(x => x.IsTaken) >= 4)
            {
                ChangeStatus = true;
            }
        }

        public void ApplyRules_Part2()
        {
            if (!IsTaken)
            {
                // If a seat is empty(L) and there are no occupied seats visible from it,
                // the seat becomes occupied.
                if (VisibleSeats.All(x => !x.IsTaken))
                {
                    ChangeStatus = true;
                }
                return;
            }

            // If a seat is occupied (#) and five or more visible seats from it are also occupied,
            // the seat becomes empty.
            if (VisibleSeats.Count(x => x.IsTaken) >= 5)
            {
                ChangeStatus = true;
            }
        }

        public void Update()
        {
            IsTaken = !IsTaken;
        }

        public void Print()
        {
            var character = IsTaken ? "#" : "L";
            Console.Write(character);
        }
    }
}
