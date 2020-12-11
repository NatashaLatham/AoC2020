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
                    seats.Add(new BoatSeat(i, j, line[j]));
                }
            }

            foreach (var seat in seats)
            {
                seat.AdjacentSeats = GetAdjacentSeats(seat);
            }
        }

        protected override void SolutionPart1()
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
                    if (ExecuteRule1(seat))
                    {
                        isUpdated = true;
                    }

                    if (ExecuteRule2(seat))
                    {
                        isUpdated = true;
                    }
                }

                // Now actually update the seats
                foreach (var seat in seats.Where(x => x.ChangeStatus))
                {
                    seat.Update();
                }

                //PrintMap();

                round++;
            }

            var numberOfSeatsTaken = seats.Count(x => x.IsTaken);
            Console.WriteLine($"Number of occupied seats: {numberOfSeatsTaken}");
        }

        protected override void SolutionPart2()
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
                    if (ExecuteRule1(seat))
                    {
                        isUpdated = true;
                    }

                    if (ExecuteRule2(seat))
                    {
                        isUpdated = true;
                    }
                }

                // Now actually update the seats
                foreach (var seat in seats.Where(x => x.ChangeStatus))
                {
                    seat.Update();
                }

                //PrintMap();

                round++;
            }

            var numberOfSeatsTaken = seats.Count(x => x.IsTaken);
            Console.WriteLine($"Number of occupied seats: {numberOfSeatsTaken}");
        }

        /// <summary>
        /// If a seat is empty (L) and there are no occupied seats adjacent to it,
        /// the seat becomes occupied.
        /// </summary>
        private bool ExecuteRule1(BoatSeat seat)
        {
            if (seat.IsFloor)
            {
                //Console.WriteLine("Floor --> No update");
                return false;
            }

            if (!seat.IsTaken)
            {
                // Check adjacent seats
                //Console.WriteLine($"Empty --> Number of adjacent seats {adjacentSeats.Count()}");
                //Console.WriteLine($" Taken: {adjacentSeats.Count(x => x.IsTaken)} Floors: {adjacentSeats.Count(x => x.IsFloor)} Empty: {adjacentSeats.Count(x => !x.IsTaken)}");
                if (seat.AdjacentSeats.All(x => !x.IsTaken || x.IsFloor))
                {
                    //Console.WriteLine("Empty --> Taken");
                    seat.ChangeStatus = true;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// If a seat is occupied (#) and four or more seats adjacent to it are also occupied,
        /// the seat becomes empty
        /// </summary>
        private bool ExecuteRule2(BoatSeat seat)
        {
            if (seat.IsFloor)
            {
                return false;
            }

            if (seat.IsTaken)
            {
                // Check adjacent seats
                if (seat.AdjacentSeats.Count(x => x.IsTaken) >= 4)
                {
                    seat.ChangeStatus = true;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Get the adjacent seats
        /// </summary>
        private IEnumerable<BoatSeat> GetAdjacentSeats(BoatSeat seat)
        {
            var result = seats.Where(x => x.Row == seat.Row - 1 && x.Column == seat.Column - 1
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

            // left
            for (var i = seat.Column; i <= 0; i--)
            {
                var visibleSeat = seats.FirstOrDefault(x => x.Row == seat.Row && x.Column == seat.Column + i);
                if (visibleSeat != null)
                {
                    result.Add(visibleSeat);
                    break;
                }
            }

            // right
            for (var i = seat.Column; i <= 0; i++)
            {
                var visibleSeat = seats.FirstOrDefault(x => x.Row == seat.Row && x.Column == seat.Column + i);
                if (visibleSeat != null)
                {
                    result.Add(visibleSeat);
                    break;
                }
            }

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
