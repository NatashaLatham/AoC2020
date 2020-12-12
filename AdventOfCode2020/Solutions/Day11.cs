using AdventOfCode2020.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    internal class Day11 : Day
    {
        private List<BoatSeat> seats;

        public Day11() : base("Day11.txt")
        {
        }

        protected override void Initialize()
        {
            //var content = GetExample();
            var content = ReadFile();

            seats = new List<BoatSeat>();
            for (var i = 0; i < content.Length; i++)
            {
                var line = content[i];
                for (var j = 0; j < line.Length; j++)
                {
                    // Don't add the floors
                    if (line[j] != '.')
                    {
                        seats.Add(new BoatSeat(i, j, line[j]));
                    }
                }
            }

            foreach (var seat in seats)
            {
                seat.AdjacentSeats = GetAdjacentSeats(seat);
                seat.VisibleSeats = GetVisibleSeats(seat);
            }
        }

        protected override void SolutionPart1()
        {
            Solution(1);
        }

        protected override void SolutionPart2()
        {
            Initialize();
            Solution(2);
        }

        private void Solution(int part)
        {
            var isUpdated = true;
            var round = 1;
            while (isUpdated)
            {
                // Reset ChangeStatus
                seats.ForEach(x => x.ChangeStatus = false);

                Console.WriteLine($"Round: {round}");

                isUpdated = false;
                foreach (var seat in seats)
                {
                    if (part == 1)
                    {
                        seat.ApplyRules_Part1();
                    }
                    else
                    {
                        seat.ApplyRules_Part2();
                    }
                }

                // Now actually update the seats
                // (This has to be done after the checks otherwise updating a seat will influence the checks)
                foreach (var seat in seats.Where(x => x.ChangeStatus))
                {
                    isUpdated = true;
                    seat.Update();
                }

                //PrintMap();

                round++;
            }

            var numberOfSeatsTaken = seats.Count(x => x.IsTaken);
            Console.WriteLine($"Number of occupied seats: {numberOfSeatsTaken}");
        }

        /// <summary>
        /// Get the adjacent seats
        /// </summary>
        private IEnumerable<BoatSeat> GetAdjacentSeats(BoatSeat seat)
        {
            var result = seats.Where(x =>
               x.Row == seat.Row - 1 && x.Column == seat.Column - 1
            || x.Row == seat.Row - 1 && x.Column == seat.Column
            || x.Row == seat.Row - 1 && x.Column == seat.Column + 1
            || x.Row == seat.Row && x.Column == seat.Column - 1
            || x.Row == seat.Row && x.Column == seat.Column + 1
            || x.Row == seat.Row + 1 && x.Column == seat.Column - 1
            || x.Row == seat.Row + 1 && x.Column == seat.Column
            || x.Row == seat.Row + 1 && x.Column == seat.Column + 1);

            return result;
        }

        /// <summary>
        /// Get the visible seats
        /// </summary>
        private IEnumerable<BoatSeat> GetVisibleSeats(BoatSeat seat)
        {
            var result = new List<BoatSeat>();

            var leftDiagonalUp = seats.Where(x => x.Row < seat.Row && x.Column == seat.Column + (seat.Row - x.Row)).OrderByDescending(x => x.Row).FirstOrDefault();
            var up = seats.Where(x => x.Row < seat.Row && x.Column == seat.Column).OrderByDescending(x => x.Row).FirstOrDefault();
            var rightDiagonalUp = seats.Where(x => x.Row < seat.Row && x.Column == seat.Column - (seat.Row - x.Row)).OrderByDescending(x => x.Row).FirstOrDefault();
            var left = seats.Where(x => x.Row == seat.Row && x.Column < seat.Column).OrderByDescending(x => x.Column).FirstOrDefault();
            var right = seats.Where(x => x.Row == seat.Row && x.Column > seat.Column).OrderBy(x => x.Column).FirstOrDefault();
            var leftDiagonalDown = seats.Where(x => x.Row > seat.Row && x.Column == seat.Column + (seat.Row - x.Row)).OrderBy(x => x.Row).FirstOrDefault();
            var down = seats.Where(x => x.Row > seat.Row && x.Column == seat.Column).OrderBy(x => x.Row).FirstOrDefault();
            var rightDiagonalDown = seats.Where(x => x.Row > seat.Row && x.Column == seat.Column - (seat.Row - x.Row)).OrderBy(x => x.Row).FirstOrDefault();

            if (leftDiagonalUp != null) { result.Add(leftDiagonalUp); }
            if (up != null) { result.Add(up); }
            if (rightDiagonalUp != null) { result.Add(rightDiagonalUp); }
            if (left != null) { result.Add(left); }
            if (right != null) { result.Add(right); }
            if (leftDiagonalDown != null) { result.Add(leftDiagonalDown); }
            if (down != null) { result.Add(down); }
            if (rightDiagonalDown != null) { result.Add(rightDiagonalDown); }

            return result;
        }

        private string[] GetExample()
        {
            var line01 = "L.LL.LL.LL";
            var line02 = "LLLLLLL.LL";
            var line03 = "L.L.L..L..";
            var line04 = "LLLL.LL.LL";
            var line05 = "L.LL.LL.LL";
            var line06 = "L.LLLLL.LL";
            var line07 = "..L.L.....";
            var line08 = "LLLLLLLLLL";
            var line09 = "L.LLLLLL.L";
            var line10 = "L.LLLLL.LL";

            var result = new[] { line01, line02, line03, line04, line05, line06, line07, line08, line09, line10 };

            return result;
        }

        private void PrintMap()
        {
            var previousRow = 0;
            foreach (var seat in seats)
            {
                if (seat.Row > previousRow)
                {
                    Console.WriteLine();
                    previousRow = seat.Row;
                }

                seat.Print();
            }
        }
    }
}
