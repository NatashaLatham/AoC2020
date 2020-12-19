using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Entities
{
    internal class DockingData
    {
        private const int MemoryAddressLenght = 36;

        public string Mask { get; set; }

        public long MemoryAddress { get; }

        public long Value { get; }

        public long ValueAfterMask { get; private set; }

        public IList<long> FloatingMemoryAdresses { get; private set; }

        public string BinaryValue
        {
            get => ConvertToBinary(ValueAfterMask).PadLeft(MemoryAddressLenght, '0');
            set => ValueAfterMask = ConvertToLong(value);
        }

        public string BinaryMemoryAddress => ConvertToBinary(MemoryAddress).PadLeft(MemoryAddressLenght, '0');

        public DockingData(string mask, long memoryAddress, long value)
        {
            Mask = mask;
            MemoryAddress = memoryAddress;
            Value = value;
            FloatingMemoryAdresses = new List<long>();
        }

        /// <summary>
        /// Applies the mask to the binary value and updates the ValueAfterMask accordingly
        /// </summary>
        public void ApplyMask()
        {
            ValueAfterMask = Value;
            var binaryValue = BinaryValue.ToCharArray();

            // Apply mask
            for (var i = 0; i < Mask.Length; i++)
            {
                if (Mask[i] != 'X')
                {
                    var position = (MemoryAddressLenght) - (Mask.Length - i);
                    binaryValue[position] = Mask[i];
                }
            }

            BinaryValue = new string(binaryValue);
        }

        /// <summary>
        /// Applies the mask to the binary value and updates the Value accordingly
        /// </summary>
        public void ApplyMaskVersion2()
        {
            var binaryValue = BinaryMemoryAddress.ToCharArray();

            // Apply mask
            for (var i = 0; i < Mask.Length; i++)
            {
                if (Mask[i] != '0')
                {
                    var position = (MemoryAddressLenght) - (Mask.Length - i);
                    binaryValue[position] = Mask[i];
                }
            }

            // Get the values for all floating bits combinations
            SetFloatingValues(binaryValue);
        }

        private void SetFloatingValues(char[] binaryValue)
        {
            // Get the values for all floating bits combinations
            var noOfX = binaryValue.Count(x => x == 'X');
            var noOfMemoryAddresses = (int)Math.Pow(2, noOfX);

            // Create a list for the total of different values
            var memoryAddresses = new List<char[]>();
            for (var i = 1; i <= noOfMemoryAddresses; i++)
            {
                memoryAddresses.Add(new string(binaryValue).ToCharArray()); // Make a copy
            }

            // Replace the first X
            var floatingBitIndex = Array.IndexOf(binaryValue, 'X', 0);
            var half = noOfMemoryAddresses / 2;
            var firstHalf = memoryAddresses.Take(half).ToList();
            var secondHalf = memoryAddresses.Skip(half).ToList();
            ReplaceX(floatingBitIndex, firstHalf, secondHalf);

            // Get the values and add them to the list
            foreach (var memoryAddress in memoryAddresses)
            {
                BinaryValue = new string(memoryAddress);
                FloatingMemoryAdresses.Add(ValueAfterMask);
            }
        }

        private void ReplaceX(int floatingBitIndex, List<char[]> firstHalf, List<char[]> secondHalf)
        {
            // Nothing more to replace
            if (floatingBitIndex < 0)
            {
                return;
            }

            // Replace the X with 0 for the first half of the list
            foreach (var memoryAddress in firstHalf)
            {
                memoryAddress[floatingBitIndex] = '0';
            }

            // Replace the X with 1 for the second half of the list
            foreach (var memoryAddress in secondHalf)
            {
                memoryAddress[floatingBitIndex] = '1';
            }

            floatingBitIndex = Array.IndexOf(firstHalf.First(), 'X', floatingBitIndex + 1);
            var half = firstHalf.Count / 2;
            ReplaceX(floatingBitIndex, firstHalf.Take(half).ToList(), firstHalf.Skip(half).ToList());
            ReplaceX(floatingBitIndex, secondHalf.Take(half).ToList(), secondHalf.Skip(half).ToList());
        }

        private string ConvertToBinary(long value)
        {
            var result = string.Empty;

            while (value > 1)
            {
                var remainder = value % 2;
                result = Convert.ToString(remainder) + result;
                value /= 2;
            }

            result = Convert.ToString(value) + result;

            return result;
        }

        private long ConvertToLong(string value)
        {
            long result = 0;

            var array = value.ToCharArray();
            Array.Reverse(array);

            for (var i = 0; i < value.Length; i++)
            {
                if (array[i] == '1')
                {
                    result += (long)Math.Pow(2, i);
                }
            }

            return result;
        }
    }
}
