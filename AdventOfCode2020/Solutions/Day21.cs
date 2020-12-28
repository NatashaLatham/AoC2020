using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    internal class Day21 : Day
    {
        // Key = Allergen, Value = List of possible Ingredients
        private Dictionary<string, List<string>> Allergens;

        // Key = Ingredient, Value = Number of times this ingredient is in the list
        private Dictionary<string, int> AllIngredients;

        public Day21() : base("Day21.txt")
        {
        }

        protected override void Initialize()
        {
            //var content = GetExample();
            var content = ReadFile();

            Allergens = new Dictionary<string, List<string>>();
            AllIngredients = new Dictionary<string, int>();

            foreach (var line in content)
            {
                var splitLine = line.Split(" (");
                var ingredients = splitLine[0].Split(" ");
                var allergenLine = splitLine[1].Substring(9, splitLine[1].Length - 9 - 1).Trim();
                var allergens = allergenLine.Split(", ");

                foreach (var allergen in allergens)
                {
                    if (!Allergens.ContainsKey(allergen))
                    {
                        Allergens.Add(allergen, ingredients.ToList());
                    }
                    else
                    {
                        Allergens[allergen] = Allergens[allergen].Intersect(ingredients).ToList();
                    }
                }

                foreach (var ingredient in ingredients)
                {
                    if (!AllIngredients.ContainsKey(ingredient))
                    {
                        AllIngredients.Add(ingredient, 1);
                    }
                    else
                    {
                        AllIngredients[ingredient]++;
                    }
                }
            }
        }

        protected override void SolutionPart1()
        {
            var allMapped = Allergens.All(x => x.Value.Count == 1);
            var previousNumberOfUnMapped = Allergens.Count(x => x.Value.Count != 1);
            while (!allMapped)
            {
                foreach (var allergen in Allergens)
                {
                    if (allergen.Value.Count == 1)
                    {
                        // Remove the ingredients from the other ingredient lists
                        var ingredientName = allergen.Value.Single();
                        var removeList = Allergens.Where(x => x.Value.Contains(ingredientName));
                        foreach (var remove in removeList)
                        {
                            if (remove.Key != allergen.Key)
                            {
                                Allergens[remove.Key].Remove(ingredientName);
                            }
                        }
                    }
                }

                allMapped = Allergens.All(x => x.Value.Count == 1);

                var numberOfUnMapped = Allergens.Count(x => x.Value.Count != 1);
                if (numberOfUnMapped == previousNumberOfUnMapped)
                {
                    throw new InvalidOperationException($"Something went wrong. Nothing mapped in this round!!!");
                }

                previousNumberOfUnMapped = numberOfUnMapped;
            }

            // Find the ingredients that don't have any allergens
            var ingredientsWithoutAllergens = AllIngredients.Select(x => x.Key).Except(Allergens.Values.SelectMany(x => x));

            //Console.WriteLine($"Ingredients without allergens: {string.Join(", ", ingredientsWithoutAllergens)}");

            // Count how often these ingredients appear in the list
            var numberOfTimesInList = AllIngredients.Where(x => ingredientsWithoutAllergens.Contains(x.Key))
                .ToDictionary(x => x.Key, x => x.Value)
                .Values
                .Sum(x => x);

            Console.WriteLine($"Result: {numberOfTimesInList}");
        }

        protected override void SolutionPart2()
        {
            // We now have a list of allergens and their ingredients (from part 1)
            // Sort them by allergen and put them in a list, separated by commas
            var sortedAllergens = Allergens.OrderBy(x => x.Key);

            var result = string.Join(",",sortedAllergens.SelectMany(x => x.Value));
            Console.WriteLine($"Result: {result}");
        }

        private string[] GetExample()
        {
            var line01 = "mxmxvkd kfcds sqjhc nhms (contains dairy, fish)";
            var line02 = "trh fvjkl sbzzf mxmxvkd (contains dairy)";
            var line03 = "sqjhc fvjkl (contains soy)";
            var line04 = "sqjhc mxmxvkd sbzzf (contains fish)";

            var result = new[] { line01, line02, line03, line04 };

            return result;
        }
    }
}
