using AdventOfCode2020.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    internal class Day14 : Day
    {
        private List<DockingData> dockingData;

        public Day14() : base("Day14.txt")
        {
        }

        protected override void Initialize()
        {
            //var content = GetExample();
            //var content = GetExampleForPart2();
            var content = ReadFile();

            var mask = new string('X', 36);

            var counter = 0;
            dockingData = new List<DockingData>();
            foreach (var line in content)
            {
                var splitLine = line.Split(" = ");

                if (splitLine[0] == "mask")
                {
                    mask = splitLine[1];
                }
                else
                {
                    counter++;
                    dockingData.Add(new DockingData(mask, GetMemoryAddress(splitLine[0]), long.Parse(splitLine[1])));
                }
            }

            Console.WriteLine($"Added {counter} entities");
        }

        protected override void SolutionPart1()
        {
            // Initialize memory addresses
            var memoryAddresses = new Dictionary<long, long>();

            foreach (var dockingData in dockingData)
            {
                // Add address if it's not there yet
                if (!memoryAddresses.ContainsKey(dockingData.MemoryAddress))
                {
                    memoryAddresses.Add(dockingData.MemoryAddress, 0);
                }

                // Apply mask and update Value accordingly
                dockingData.ApplyMask();
                memoryAddresses[dockingData.MemoryAddress] = dockingData.ValueAfterMask;
            }

            var sumOfAllValuesLeftInMemory = memoryAddresses.Values.Sum(x => x);
            Console.WriteLine($"Sum of all values left in memory: {sumOfAllValuesLeftInMemory}");
        }

        protected override void SolutionPart2()
        {
            // Initialize memory addresses
            var memoryAddresses = new Dictionary<long, long>();

            foreach (var dockingData in dockingData)
            {
                // Apply mask
                dockingData.ApplyMaskVersion2();

                // Write memory addresses
                foreach (var memoryAddress in dockingData.FloatingMemoryAdresses)
                {
                    memoryAddresses[memoryAddress] = dockingData.Value;
                }
            }

            var sumOfAllValuesLeftInMemory = memoryAddresses.Values.Sum(x => x);
            Console.WriteLine($"Sum of all values left in memory: {sumOfAllValuesLeftInMemory}");
        }

        private long GetMemoryAddress(string command)
        {
            var splitLeft = command.Split("[");
            var splitRight = splitLeft[1].Split("]");
            var memoryAddress = long.Parse(splitRight[0]);
            return memoryAddress;
        }

        private string[] GetExample()
        {
            var line01 = "mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X";
            var line02 = "mem[8] = 11";
            var line03 = "mem[7] = 101";
            var line04 = "mem[8] = 0";

            var result = new[] { line01, line02, line03, line04 };

            return result;
        }

        private string[] GetExampleForPart2()
        {
            var line01 = "mask = 000000000000000000000000000000X1001X";
            var line02 = "mem[42] = 100";
            var line03 = "mask = 00000000000000000000000000000000X0XX";
            var line04 = "mem[26] = 1";

            var result = new[] { line01, line02, line03, line04 };

            return result;
        }
    }
}
