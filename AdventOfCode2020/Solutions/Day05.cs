using AdventOfCode2020.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    internal class Day05 : Day
    {
        private const int numberOfRows = 127;
        private const int numberOfColumns = 7;

        private string[] boardingPasses;
        private ICollection<Seat> seats;

        public Day05() : base("Day05.txt")
        {
        }

        protected override void Initialize()
        {
            //boardingPasses = GetExamples();
            boardingPasses = ReadFile();

            // Initialize the list of seats
            seats = new List<Seat>();
            for (int row = 0; row <= numberOfRows; row++)
            {
                for (int column = 0; column <= numberOfColumns; column++)
                {
                    seats.Add(new Seat(row, column));
                }
            }
        }

        protected override void SolutionPart1()
        {
            var maxSeatId = 0;

            foreach (var boardingPass in boardingPasses)
            {
                var rowRange = new Range(0, numberOfRows);
                var columnRange = new Range(0, numberOfColumns);

                foreach (var binaryPartitionCode in boardingPass)
                {
                    switch (binaryPartitionCode)
                    {
                        // Get the rown umber
                        case 'F':
                        case 'B':
                            rowRange = GetRange(rowRange, binaryPartitionCode);
                            break;
                        // Get the colum number
                        case 'R':
                        case 'L':
                            columnRange = GetRange(columnRange, binaryPartitionCode);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(binaryPartitionCode.ToString());
                    }
                }

                if (!(IsValidResult(rowRange) && IsValidResult(columnRange)))
                {
                    throw new InvalidOperationException("Oh no! Something went wrong. Invalid range result detected");
                }

                var seatId = GetSeatId(rowRange.Start.Value, columnRange.Start.Value);
                maxSeatId = Math.Max(maxSeatId, seatId);

                // Update the seat list (for solution 2)
                var seat = seats.Single(x => x.Row == rowRange.Start.Value && x.Column == columnRange.Start.Value);
                seat.Update(seatId);
            }

            Console.WriteLine($"Heighest seat ID: {maxSeatId}");
        }

        protected override void SolutionPart2()
        {
            var mySeatId = 0;

            // Get the seats that are not taken yet
            var openSeats = seats.Where(seat => !seat.Taken);

            // Check if the seat with seatId +1 and -1 exist in the seats list
            foreach (var openSeat in openSeats)
            {
                var openSeatId = GetSeatId(openSeat.Row, openSeat.Column);

                var correctSeat = seats.Any(x => x.SeatId == openSeatId + 1) && seats.Any(x => x.SeatId == openSeatId - 1);
                if (correctSeat)
                {
                    mySeatId = GetSeatId(openSeat.Row, openSeat.Column);
                    break;
                }
            }

            Console.WriteLine($"My seat ID: {mySeatId}");
        }

        private Range GetRange(Range range, char binaryPartitionCode)
        {
            var start = range.Start.Value;
            var end = range.End.Value;

            switch (binaryPartitionCode)
            {
                case 'F':
                case 'L':
                    end -= GetHalf(range);
                    break;
                case 'B':
                case 'R':
                    start += GetHalf(range);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(binaryPartitionCode.ToString());
            }

            var result = new Range(start, end);
            return result;
        }

        private int GetHalf(Range range)
        {
            var result = Math.Abs((range.End.Value - range.Start.Value) / 2) + 1;
            return result;
        }

        /// <summary>
        /// The end result of the range should be an exact row or column number
        /// </summary>
        private bool IsValidResult(Range range)
        {
            var isValid = range.Start.Value == range.End.Value;
            return isValid;
        }

        private int GetSeatId(int rowNumber, int columnNumber)
        {
            var result = rowNumber * 8 + columnNumber;
            return result;
        }

        private string[] GetExamples()
        {
            var line01 = "FBFBBFFRLR";  // row 44, column 5, seat ID 357
            var line02 = "BFFFBBFRRR";  // row 70, column 7, seat ID 567
            var line03 = "FFFBBBFRRR";  // row 14, column 7, seat ID 119
            var line04 = "BBFFBBFRLL";  // row 102, column 4, seat ID 820

            var result = new[] { line01, line02, line03, line04 };

            return result;
        }
    }
}
