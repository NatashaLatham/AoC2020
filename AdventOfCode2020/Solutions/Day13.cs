using AdventOfCode2020.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    internal class Day13 : Day
    {
        private int earliestTimeStamp = 0;
        private List<Bus> busses;

        public Day13() : base("Day13.txt")
        {
        }

        protected override void Initialize()
        {
            //var content = GetExample();
            var content = ReadFile();

            earliestTimeStamp = int.Parse(content[0]);

            busses = new List<Bus>();
            var busIds = content[1].Split(',');
            for (var i = 0; i < busIds.Length; i++)
            {
                if (busIds[i] != "x")
                {
                    busses.Add(new Bus(i, long.Parse(busIds[i])));
                }
            }
        }

        protected override void SolutionPart1()
        {
            // Calculate the first departure time on or after the timestamp for each bus
            foreach (var bus in busses)
            {
                var wholeMinutes = Math.Abs(earliestTimeStamp / bus.Id);
                var departureTime = wholeMinutes * bus.Id;
                if (departureTime - earliestTimeStamp < 0)
                {
                    // Bus leaves too early, calculate when next one leaves
                    departureTime = departureTime += bus.Id;
                }
                ;
                bus.DepartureTime = departureTime;
            }

            // Find the one that departs first
            var firstBusToAirport = busses.OrderBy(x => x.DepartureTime).FirstOrDefault();

            // Calculate the bus id (Id * minutes needed to wait)
            var firstBusToAirportId = firstBusToAirport.Id * (firstBusToAirport.DepartureTime - earliestTimeStamp);

            Console.WriteLine($"Result: {firstBusToAirportId}");
        }

        protected override void SolutionPart2()
        {
            var departureTime = 0L;
            var step = busses.First().Id;
            foreach (var bus in busses.Skip(1))
            {
                // Find the departure time (t) where this bus leaves 'Number' after the first bus
                while ((departureTime + bus.Number) % bus.Id != 0L)
                {
                    departureTime += step;
                }
                step *= bus.Id;
            }

            Console.WriteLine($"Departure time: {departureTime}");
        }

        private string[] GetExample()
        {
            var line01 = "939";
            var line02 = "7,13,x,x,59,x,31,19";

            var result = new[] { line01, line02 };

            return result;
        }
    }
}
