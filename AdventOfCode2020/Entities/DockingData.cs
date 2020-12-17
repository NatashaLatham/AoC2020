using System;

namespace AdventOfCode2020.Entities
{
    internal class DockingData
    {
        private const int MemoryAddressLenght = 36;

        public string Mask { get; set; }

        public long MemoryAddress { get; }

        public long Value { get; private set; }

        public string BinaryValue
        {
            // Check this one
            get => ConvertToBinary().PadLeft(MemoryAddressLenght, '0');
            set => Value = ConvertToLong(value);
        }

        public DockingData(string mask, long memoryAddress, long value)
        {
            Mask = mask;
            MemoryAddress = memoryAddress;
            Value = value;
        }

        /// <summary>
        /// Applies the mask to the binary value and update the Value accordingly
        /// </summary>
        public void ApplyMask()
        {
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

        private string ConvertToBinary()
        {
            var result = string.Empty;

            var value = Value;
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
