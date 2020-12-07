using System.Collections.Generic;

namespace AdventOfCode2020.Entities
{
    /// <summary>
    /// Rule for color coded bag
    /// </summary>
    internal class BagRule
    {
        public string Bag { get; }

        public List<BagInside> BagsInside { get; }

        public BagRule(string bag)
        {
            Bag = bag;
            BagsInside = new List<BagInside>();
        }

    }
}
