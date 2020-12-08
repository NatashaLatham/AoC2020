using AdventOfCode2020.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    internal class Day07 : Day
    {
        private const string ShinyGoldBag = "shiny gold";

        private string[] content;

        private List<BagRule> bags;

        public Day07() : base("Day07.txt")
        {
        }

        protected override void Initialize()
        {
            //content = GetExample();
            content = ReadFile();

            bags = new List<BagRule>();
            foreach (var line in content)
            {
                var splitOnContain = line.Split(" contain ");

                var bag = new BagRule(GetBagColorAndNumber(splitOnContain[0]).BagColor);

                if (splitOnContain[1] != "no other bags.")
                {
                    GetBagsInside(bag, splitOnContain[1]);
                }

                bags.Add(bag);
            }
        }

        protected override void SolutionPart1()
        {
            var totalBagsWithShinyGoldBag = new List<BagRule>();

            // Get the bags that can contain a shiny gold bag
            var bagsWithShinyGoldBag = GetBagsThatCanContainABag(ShinyGoldBag);
            totalBagsWithShinyGoldBag.AddRange(bagsWithShinyGoldBag);

            while (bagsWithShinyGoldBag.Any())
            {
                bagsWithShinyGoldBag = GetBagsThatCanContainABag(bagsWithShinyGoldBag);
                foreach (var bagWithShinyGoldBag in bagsWithShinyGoldBag)
                {
                    if (!totalBagsWithShinyGoldBag.Any(x => x.Bag == bagWithShinyGoldBag.Bag))
                    {
                        totalBagsWithShinyGoldBag.Add(bagWithShinyGoldBag);
                    }
                }
            }

            Console.WriteLine($"Number of bag colors that can carry a shiny gold bag: {totalBagsWithShinyGoldBag.Count()}");
        }

        protected override void SolutionPart2()
        {
            // Start with the shiny gold bag
            var shinyGoldBag = bags.Single(x => x.Bag == ShinyGoldBag);

            // Count the number of bags in this bag
            var numberOfBags = CountNumberOfBagsInBag(shinyGoldBag);

            Console.WriteLine($"Bags required: {numberOfBags}");
        }

        private BagInside GetBagColorAndNumber(string input)
        {
            char numberOfBagsChar = input.SingleOrDefault(x => char.IsDigit(x));
            int numberOfBags = numberOfBagsChar == '\0' ? 0 : Convert.ToInt32(numberOfBagsChar.ToString());
            var inputWithoutNumbers = new string(input.Where(x => !char.IsDigit(x)).ToArray());
            var indexOfBag = inputWithoutNumbers.IndexOf(" bag");
            var result = inputWithoutNumbers.Substring(0, indexOfBag);

            return new BagInside(numberOfBags, result.Trim());
        }

        private void GetBagsInside(BagRule bag, string input)
        {
            var splitOnComma = input.Split(',');

            foreach (var bagLine in splitOnComma)
            {
                bag.BagsInside.Add(GetBagColorAndNumber(bagLine));
            }
        }

        private IEnumerable<BagRule> GetBagsThatCanContainABag(string bagColor)
        {
            var bagsThatCanHaveBagColor = bags.Where(x => x.BagsInside.Any(x => x.BagColor == bagColor));
            return bagsThatCanHaveBagColor;
        }

        private IEnumerable<BagRule> GetBagsThatCanContainABag(IEnumerable<BagRule> bagColors)
        {
            var totalBagsThatCanHaveBagColor = new List<BagRule>();

            foreach (var bagColor in bagColors)
            {
                var bagsThatCanHaveBagColor = GetBagsThatCanContainABag(bagColor.Bag);
                foreach (var bag in bagsThatCanHaveBagColor)
                {
                    // Only add if it hasn't been added yet
                    if (!totalBagsThatCanHaveBagColor.Any(x => x.Bag == bag.Bag))
                    {
                        totalBagsThatCanHaveBagColor.Add(bag);
                    }
                }
            }

            return totalBagsThatCanHaveBagColor;
        }

        private long CountNumberOfBags(IEnumerable<BagRule> bags)
        {
            long numberOfBags = 0;

            foreach (var bag in bags)
            {
                numberOfBags += CountNumberOfBagsInBag(bag);
            }

            return numberOfBags;

        }

        private long CountNumberOfBagsInBag(BagRule bag)
        {
            long numberOfBags = 0;

            foreach (var bagInside in bag.BagsInside)
            {
                var bagsInside = bags.Where(x => x.Bag == bagInside.BagColor);
                numberOfBags += bagInside.NumberOfBags + bagInside.NumberOfBags * CountNumberOfBags(bagsInside);
            }

            return numberOfBags;
        }

        private string[] GetExample()
        {
            var line01 = "light red bags contain 1 bright white bag, 2 muted yellow bags.";
            var line02 = "dark orange bags contain 3 bright white bags, 4 muted yellow bags.";
            var line03 = "bright white bags contain 1 shiny gold bag.";
            var line04 = "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags, 5 dull magenta bags.";
            var line05 = "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.";
            var line06 = "dark olive bags contain 3 faded blue bags, 4 dotted black bags.";
            var line07 = "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.";
            var line08 = "faded blue bags contain no other bags.";
            var line09 = "dotted black bags contain no other bags.";

            var result = new[] { line01, line02, line03, line04, line05, line06, line07, line08, line09 };

            return result;
        }
    }
}
