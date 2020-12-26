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
            //var content = GetExample2();
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
                if (Rules.Any(x => x.Number == tempRule.Key))
                {
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
                if (Rules.Any(rule => rule.Number == 0 && rule.Matches.Any(match => match == message)))
                {
                    result++;
                }
            }

            Console.WriteLine($"Number of messages that match rule 0: {result}");
        }

        protected override void SolutionPart2()
        {
            var result = 0;

            var rule42Matches = Rules.Single(x => x.Number == 42).Matches;
            var rule31Matches = Rules.Single(x => x.Number == 31).Matches;

            // The rules for rule 31 and rule 42 must have the same length
            var matchLength42 = rule42Matches.Max(x => x.Length);
            var matchLength31 = rule31Matches.Max(x => x.Length);

            if (matchLength42 != matchLength31)
            {
                throw new InvalidOperationException("Matchlength is different!!!");
            }

            foreach (var message in Messages)
            {
                if (MatchesNewRule8And11(message, rule42Matches, rule31Matches, matchLength42))
                {
                    result++;
                }
            }

            Console.WriteLine($"Number of messages that match rule 0: {result}");
        }

        /// <summary>
        /// Should match a combination that starts with a combination of 42 and ends with a combination of 31
        /// There should be more 42's than 31's
        /// </summary>
        private bool MatchesNewRule8And11(string message, List<string> rule42Matches, List<string> rule31Matches, int matchLength)
        {
            // If the message is not a multiple of the match length --> it won't match
            if (message.Length % matchLength != 0)
            {
                return false;
            }

            // The message has to start with rule 42
            if (!rule42Matches.Any(x => message.StartsWith(x)))
            {
                return false;
            }

            // The message has to end with rule 31
            if (!rule31Matches.Any(x => message.EndsWith(x)))
            {
                return false;
            }

            // The first part of the substrings inbetween have to match with rule 42
            var numberOf42 = 1;
            var numberOf31 = 1;
            var ruleMatches = false;
            var startIndex = matchLength;
            for (var start = startIndex; start < message.Length - matchLength; start += matchLength)
            {
                ruleMatches = SubstringMatchesRule(message, start, matchLength, rule42Matches);
                startIndex = start;
                if (!ruleMatches)
                {
                    break;
                }
                numberOf42++;
            }

            // if the rule matches we're done
            if (ruleMatches)
            {
                return numberOf42 > 1 && numberOf42 > numberOf31;
            }

            // The last part of the substrings inbetween have to match with rule 31
            for (var start = startIndex; start < message.Length - matchLength; start += matchLength)
            {
                ruleMatches = SubstringMatchesRule(message, start, matchLength, rule31Matches);
                if (!ruleMatches)
                {
                    return false;
                }
                numberOf31++;
            }

            return numberOf42 > 1 && numberOf42 > numberOf31;
        }

        private bool SubstringMatchesRule(string message, int start, int length, List<string> matches)
        {
            var substring = message.Substring(start, length);
            var result = matches.Any(x => x == substring);
            return result;
        }

        private IList<string> FindRuleMatches(string ruleValue)
        {
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
            var newMatchList = new List<string>();
            var ruleNumbers = subRule.Split(" ");
            foreach (var ruleNumber in ruleNumbers)
            {
                // Is the rule already in the rules
                var ruleNumberNumeric = int.Parse(ruleNumber);
                var existingRule = Rules.SingleOrDefault(x => x.Number == ruleNumberNumeric);
                if (existingRule != null)
                {
                    newMatchList = AppendMatches(newMatchList, existingRule.Matches);
                }
                else
                {
                    // Find it in the tempRules
                    var tempRuleValue = TempRules[int.Parse(ruleNumber)];
                    var foundMatches = FindRuleMatches(tempRuleValue);

                    // Add the rule to the list
                    var newRule = new Rule(ruleNumberNumeric);
                    newRule.Matches.AddRange(foundMatches);
                    Rules.Add(newRule);

                    newMatchList = AppendMatches(newMatchList, foundMatches);
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

        private string[] GetExample2()
        {
            var line01 = "42: 9 14 | 10 1";
            var line02 = "9: 14 27 | 1 26";
            var line03 = "10: 23 14 | 28 1";
            var line04 = "1: \"a\"";
            var line05 = "11: 42 31";
            var line06 = "5: 1 14 | 15 1";
            var line07 = "19: 14 1 | 14 14";
            var line08 = "12: 24 14 | 19 1";
            var line09 = "16: 15 1 | 14 14";
            var line10 = "31: 14 17 | 1 13";
            var line11 = "6: 14 14 | 1 14";
            var line12 = "2: 1 24 | 14 4";
            var line13 = "0: 8 11";
            var line14 = "13: 14 3 | 1 12";
            var line15 = "15: 1 | 14";
            var line16 = "17: 14 2 | 1 7";
            var line17 = "23: 25 1 | 22 14";
            var line18 = "28: 16 1";
            var line19 = "4: 1 1";
            var line20 = "20: 14 14 | 1 15";
            var line21 = "3: 5 14 | 16 1";
            var line22 = "27: 1 6 | 14 18";
            var line23 = "14: \"b\"";
            var line24 = "21: 14 1 | 1 14";
            var line25 = "25: 1 1 | 1 14";
            var line26 = "22: 14 14";
            var line27 = "8: 42";
            var line28 = "26: 14 22 | 1 20";
            var line29 = "18: 15 15";
            var line30 = "7: 14 5 | 1 21";
            var line31 = "24: 14 1";
            var line32 = "";
            var line33 = "abbbbbabbbaaaababbaabbbbabababbbabbbbbbabaaaa";
            var line34 = "bbabbbbaabaabba";
            var line35 = "babbbbaabbbbbabbbbbbaabaaabaaa";
            var line36 = "aaabbbbbbaaaabaababaabababbabaaabbababababaaa";
            var line37 = "bbbbbbbaaaabbbbaaabbabaaa";
            var line38 = "bbbababbbbaaaaaaaabbababaaababaabab";
            var line39 = "ababaaaaaabaaab";
            var line40 = "ababaaaaabbbaba";
            var line41 = "baabbaaaabbaaaababbaababb";
            var line42 = "abbbbabbbbaaaababbbbbbaaaababb";
            var line43 = "aaaaabbaabaaaaababaa";
            var line44 = "aaaabbaaaabbaaa";
            var line45 = "aaaabbaabbaaaaaaabbbabbbaaabbaabaaa";
            var line46 = "babaaabbbaaabaababbaabababaaab";
            var line47 = "aabbbbbaabbbaaaaaabbbbbababaaaaabbaaabba";
            var line48 = "aababaaaaabaabaabababaaabbabababbbbabbaaabbaaababbbbbbbb";

            var result = new[] { line01, line02, line03, line04, line05, line06, line07, line08, line09, line10,
                                 line11, line12, line13, line14, line15, line16, line17, line18, line19, line20,
                                 line21, line22, line23, line24, line25, line26, line27, line28, line29, line30,
                                 line31, line32, line33, line34, line35, line36, line37, line38, line39, line40,
                                 line41, line42, line43, line44, line45, line46, line47, line48 };

            return result;
        }
    }
}
