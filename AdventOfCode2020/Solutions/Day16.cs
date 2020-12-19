using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    internal class Day16 : Day
    {
        private Dictionary<string, int[]> ValidTicketRules;
        private int[] MyTicket;
        private IList<int[]> NearbyTickets;
        private IList<int[]> ValidNearbyTickets;
        private Dictionary<int, List<string>> TicketPositionFields;

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
            //var content = GetExamplePart2();
            var content = ReadFile();

            ValidTicketRules = new Dictionary<string, int[]>();
            var numberOfPositions = 0;
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
                        ValidTicketRules.Add(fieldInfo[0], validNumbers.ToArray());
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
                        numberOfPositions = ticketNumbers.Length;
                        var nearbyTicketNumbers = new int[ticketNumbers.Length];
                        for (var i = 0; i < ticketNumbers.Length; i++)
                        {
                            nearbyTicketNumbers[i] = int.Parse(ticketNumbers[i]);
                        }
                        NearbyTickets.Add(nearbyTicketNumbers);
                        break;
                }
            }

            // Add all fields as possible field to each position
            TicketPositionFields = new Dictionary<int, List<string>>();
            for (var i = 1; i <= numberOfPositions; i++)
            {
                TicketPositionFields.Add(i, ValidTicketRules.Keys.ToList());
            }
        }

        protected override void SolutionPart1()
        {
            ValidNearbyTickets = new List<int[]>();
            var wrongTicketValues = new List<int>();
            foreach (var nearbyTicket in NearbyTickets)
            {
                var validTicket = true;
                foreach (var number in nearbyTicket)
                {
                    if (!ValidTicketRules.Values.Any(x => x.Any(x => x == number)))
                    {
                        validTicket = false;
                        wrongTicketValues.Add(number);
                    }
                }

                // Fill the list with valid nearby tickets for part 2
                if (validTicket)
                {
                    ValidNearbyTickets.Add(nearbyTicket);
                }
            }

            var ticketScanningErrorRate = wrongTicketValues.Sum();
            Console.WriteLine($"Ticket scanning error rate: {ticketScanningErrorRate}");
        }

        protected override void SolutionPart2()
        {
            var allFieldsMapped = TicketPositionFields.All(x => x.Value.Count == 1);
            var previousNumberOfUnMappedFields = TicketPositionFields.Count(x => x.Value.Count != 1);
            while (!allFieldsMapped)
            {
                FindFieldForPostion();

                // Check if there is a position with only one field left
                var onlyOneField = TicketPositionFields.Where(x => x.Value.Count() == 1);

                // Remove this field from all other lists
                foreach (var field in onlyOneField)
                {
                    var fieldName = field.Value.Single();
                    var removeList = TicketPositionFields.Where(x => x.Value.Contains(fieldName));
                    foreach (var remove in removeList)
                    {
                        if (remove.Key != field.Key)
                        {
                            TicketPositionFields[remove.Key].Remove(fieldName);
                        }
                    }
                }

                allFieldsMapped = TicketPositionFields.All(x => x.Value.Count == 1);

                var numberOfUnMappedFields = TicketPositionFields.Count(x => x.Value.Count != 1);
                Console.WriteLine($"Number of unmapped fields: {numberOfUnMappedFields}");
                if (numberOfUnMappedFields == previousNumberOfUnMappedFields)
                {
                    throw new InvalidOperationException($"Something went wrong. No fields mapped in this round!!!");
                }

                previousNumberOfUnMappedFields = numberOfUnMappedFields;
            }

            foreach (var ticketPosition in TicketPositionFields)
            {
                Console.WriteLine($"{ticketPosition.Key}: {ticketPosition.Value.Single()}");
            }

            // Find fields starting with "departure"
            var departureInfo = TicketPositionFields.Where(x => x.Value.Any(x => x.StartsWith("departure")));
            if (departureInfo.Count() != 6)
            {
                throw new InvalidOperationException($"Something went wrong. There should be 6 fields starting with departure!");
            }

            // Find fields in my ticket and multiply them
            long result = 1;
            foreach (var departureField in departureInfo)
            {
                result *= MyTicket[departureField.Key - 1];
            }

            Console.WriteLine($"Result: {result}");
        }

        private void FindFieldForPostion()
        {
            // Check for each position in which rule it fits
            for (var position = 0; position < ValidTicketRules.Count; position++)
            {
                // Has the field for this position already been found?
                var fieldsForPosition = TicketPositionFields[position + 1];
                var positionAlreadyFound = fieldsForPosition.Count == 1;
                if (positionAlreadyFound)
                {
                    break;
                }

                // Start with a list of remaining possible rules for this position
                var possibleRulesList = ValidTicketRules
                    .Where(x => fieldsForPosition.Contains(x.Key))
                    .ToDictionary(x => x.Key, x => x.Value);

                foreach (var validNearbyTicket in ValidNearbyTickets)
                {
                    // Find the rules that match for the value in this position
                    var matchingRules = possibleRulesList.Where(x => x.Value.Any(x => x == validNearbyTicket[position])).ToDictionary(x => x.Key, x => x.Value).Keys.ToList();

                    // Remove the rules that do no match from the possible rules list
                    var removeRules = possibleRulesList.Keys.ToList().Except(matchingRules).ToList();
                    foreach (var removeRule in removeRules)
                    {
                        //Console.WriteLine($"  Removing rule: {removeRule}");
                        possibleRulesList.Remove(removeRule);
                    }
                }

                // Update the list with possible fields
                TicketPositionFields[position + 1] = possibleRulesList.Keys.ToList();
            }
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

        private string[] GetExamplePart2()
        {
            var line01 = "class: 0-1 or 4-19";
            var line02 = "row: 0-5 or 8-19";
            var line03 = "seat: 0-13 or 16-19";
            var line04 = "";
            var line05 = "your ticket:";
            var line06 = "11,12,13";
            var line07 = "";
            var line08 = "nearby tickets:";
            var line09 = "3,9,18";
            var line10 = "15,1,5";
            var line11 = "5,14,9";

            var result = new[] { line01, line02, line03, line04, line05, line06, line07,
                                 line08, line09, line10, line11 };

            return result;
        }
    }
}
