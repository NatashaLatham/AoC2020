using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    internal class Day16 : Day
    {
        private Dictionary<string, int[]> ValidTickets;
        private int[] MyTicket;
        private IList<int[]> NearbyTickets;

        private enum TypeOfData
        {
            TicketRules,
            MyTicket,
            NearbyTickets
        }

        public Day16() : base("Day16.txt")
        {
        }

        protected override void Initialize()
        {
            //var content = GetExample();
            var content = ReadFile();

            ValidTickets = new Dictionary<string, int[]>();
            NearbyTickets = new List<int[]>();
            var typeOfData = TypeOfData.TicketRules;
            foreach (var line in content)
            {
                if (line == string.Empty)
                {
                    typeOfData++;
                    continue;
                }
                if (line == "your ticket:" || line == "nearby tickets:")
                {
                    continue;
                }

                switch (typeOfData)
                {
                    case TypeOfData.TicketRules:
                        var fieldInfo = line.Split(":");
                        var validRanges = fieldInfo[1].Split(" or ");
                        var validNumbers = new List<int>();
                        foreach (var validRange in validRanges)
                        {
                            var edges = validRange.Split("-");
                            for (var i = int.Parse(edges[0]); i <= int.Parse(edges[1]); i++)
                            {
                                validNumbers.Add(i);
                            }
                        }
                        ValidTickets.Add(fieldInfo[0], validNumbers.ToArray());
                        break; 
                    case TypeOfData.MyTicket:
                        var ticketNumbers = line.Split(",");
                        MyTicket = new int[ticketNumbers.Length];
                        for (var i = 0; i < ticketNumbers.Length; i++)
                        {
                            MyTicket[i] = int.Parse(ticketNumbers[i]);
                        }
                        break;
                    case TypeOfData.NearbyTickets:
                        ticketNumbers = line.Split(",");
                        var nearbyTicketNumbers = new int[ticketNumbers.Length];
                        for (var i = 0; i < ticketNumbers.Length; i++)
                        {
                            nearbyTicketNumbers[i] = int.Parse(ticketNumbers[i]);
                        }
                        NearbyTickets.Add(nearbyTicketNumbers);
                        break;
                }
            }
        }

        protected override void SolutionPart1()
        {
            var wrongTicketValues = new List<int>();
            foreach (var nearbyTicket in NearbyTickets)
            {
                foreach (var number in nearbyTicket)
                {
                    if (!ValidTickets.Values.Any(x => x.Any(x => x == number)))
                    {
                        wrongTicketValues.Add(number);
                    }
                }
            }

            var ticketScanningErrorRate = wrongTicketValues.Sum();
            Console.WriteLine($"Ticket scanning error rate: {ticketScanningErrorRate}");
        }

        protected override void SolutionPart2()
        {
            var result = 0;
            Console.WriteLine($"Result: {result}");
        }

        private string[] GetExample()
        {
            var line01 = "class: 1-3 or 5-7";
            var line02 = "row: 6-11 or 33-44";
            var line03 = "seat: 13-40 or 45-50";
            var line04 = "";
            var line05 = "your ticket:";
            var line06 = "7,1,14";
            var line07 = "";
            var line08 = "nearby tickets:";
            var line09 = "7,3,47";
            var line10 = "40,4,50";
            var line11 = "55,2,20";
            var line12 = "38,6,12";

            var result = new[] { line01, line02, line03, line04, line05, line06, line07,
                                 line08, line09, line10, line11, line12 };

            return result;
        }
    }
}
