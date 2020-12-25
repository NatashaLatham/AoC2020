using AdventOfCode2020.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    internal class Day19 : Day
    {
        private Dictionary<int, string> TempRules;
        private IList<Rule> Rules;
        private IList<string> Messages;

        public Day19() : base("Day19.txt")
        {
        }

        protected override void Initialize()
        {
            //var content = GetExample();
            var content = ReadFile();

            Rules = new List<Rule>();
            Messages = new List<string>();
            TempRules = new Dictionary<int, string>();
            var isRule = true;
            foreach (var line in content)
            {
                if (string.IsNullOrEmpty(line))
                {
                    isRule = false;
                    continue;
                }

                if (isRule)
                {
                    var rule = line.Split(": ");
                    var subRules = rule[1].Split(' ');
                    if (subRules.Length == 1 && subRules[0].Trim('"').All(x => char.IsLetter(x)))
                    {
                        // Match
                        var ruleWithoutQuotes = rule[1].Trim('"');
                        var newRule = new Rule(int.Parse(rule[0]));
                        newRule.Matches.Add(ruleWithoutQuotes);
                        Rules.Add(newRule);
                        Console.WriteLine($"Added rule for {newRule.Number}: {ruleWithoutQuotes}");
                    }
                    else
                    {
                        // Add the sub rules to the temp dictionary
                        TempRules.Add(int.Parse(rule[0]), rule[1]);
                    }
                }
                else
                {
                    // Message
                    Messages.Add(line);
                }
            }

            // Now loop through the temp rules and try to create a match for them
            foreach (var tempRule in TempRules)
            {
                // The rule already exists --> skip
                Console.WriteLine($"Trying to find match for Rule {tempRule.Key}: {tempRule.Value}");
                if (Rules.Any(x => x.Number == tempRule.Key))
                {
                    Console.WriteLine($" Rule already there --> Continue");
                    continue;
                }

                var newRule = new Rule(tempRule.Key);
                var newRuleMatches = FindRuleMatches(tempRule.Value);
                newRule.Matches.AddRange(newRuleMatches);
                Rules.Add(newRule);
            }
        }

        protected override void SolutionPart1()
        {
            var result = 0;

            foreach (var message in Messages)
            {
                if (Rules.Any(Rule => Rule.Matches.Any(match => match == message)))
                {
                    result++;
                }
            }

            Console.WriteLine($"Number of messages that match rule: {result}");
        }

        protected override void SolutionPart2()
        {
            var result = 0;
            Console.WriteLine($"Result: {result}");
        }

        private IList<string> FindRuleMatches(string ruleValue)
        {
            //Console.WriteLine($" FindRuleMatches for ruleValue {ruleValue}");

            var newMatchList = new List<string>();
            var or = ruleValue.Split(" | ");
            foreach (var subRule in or)
            {
                newMatchList.AddRange(FindMatchesForSubRule(subRule));
            }

            return newMatchList;
        }

        private IList<string> FindMatchesForSubRule(string subRule)
        {
            //Console.WriteLine($"  FindMatchesForSubRule for subRule {subRule}");

            var newMatchList = new List<string>();
            var ruleNumbers = subRule.Split(" ");
            foreach (var ruleNumber in ruleNumbers)
            {
                //Console.WriteLine($"   Rulenumber {ruleNumber}");
                // Is the rule already in the rules
                var ruleNumberNumeric = int.Parse(ruleNumber);
                var existingRule = Rules.SingleOrDefault(x => x.Number == ruleNumberNumeric);
                if (existingRule != null)
                {
                    newMatchList = AppendMatches(newMatchList, existingRule.Matches);
                    //Console.WriteLine($"   Exists -->  {string.Join(", ", newMatchList)}");
                }
                else
                {
                    // Find it in the tempRules
                    var tempRuleValue = TempRules[int.Parse(ruleNumber)];
                    var foundMatches = FindRuleMatches(tempRuleValue);
                    //Console.WriteLine($"   Looking in temp rules. Found --> {string.Join(", ", foundMatches)}");

                    // Add the rule to the list
                    var newRule = new Rule(ruleNumberNumeric);
                    newRule.Matches.AddRange(foundMatches);
                    Rules.Add(newRule);

                    newMatchList = AppendMatches(newMatchList, foundMatches);
                    //Console.WriteLine($"   NewMatchList --> {string.Join(", ", newMatchList)}");
                }
            }

            return newMatchList;
        }

        private List<string> AppendMatches(List<string> matchList, IList<string> foundMatches)
        {
            var newMatchList = new List<string>();
            // If there are already matches in the list, append the found matches to the list
            if (matchList.Any())
            {
                foreach (var match in matchList)
                {
                    foreach (var foundMatch in foundMatches)
                    {
                        newMatchList.Add($"{match}{foundMatch}");
                    }
                }
            }
            else
            {
                // Add the new matches to the list
                foreach (var match in foundMatches)
                {
                    newMatchList.Add(match);
                }
            }

            return newMatchList;
        }

        private string[] GetExample()
        {
            var line01 = "0: 4 1 5";
            var line02 = "1: 2 3 | 3 2";
            var line03 = "2: 4 4 | 5 5";
            var line04 = "3: 4 5 | 5 4";
            var line05 = "4: \"a\"";
            var line06 = "5: \"b\"";
            var line07 = "";
            var line08 = "ababbb";
            var line09 = "bababa";
            var line10 = "abbbab";
            var line11 = "aaabbb";
            var line12 = "aaaabbb";

            var result = new[] { line01, line02, line03, line04, line05, line06, line07, line08, line09, line10,
                                 line11, line12 };

            return result;
        }
    }
}
