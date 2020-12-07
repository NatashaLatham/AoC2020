namespace AdventOfCode2020.Entities
{
    internal class BagInside
    {
        public int NumberOfBags { get; }

        public string BagColor { get; }

        public BagInside(int numberOfBags, string bagColor)
        {
            NumberOfBags = numberOfBags;
            BagColor = bagColor;
        }
    }
}
