using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    internal class Day04 : Day
    {
        private readonly string[] mandatoryPassportCodes = new[]
        {
            "byr",  // (Birth Year)
            "iyr",  // (Issue Year)
            "eyr",  // (Expiration Year)
            "hgt",  // (Height)
            "hcl",  // (Hair Color)
            "ecl",  // (Eye Color)
            "pid",  // (Passport ID)
        };

        private readonly string[] optionalPassportCodes = new[]
        {
            "cid"  //(Country ID)"
        };

        private ICollection<Dictionary<string, string>> passports;

        private string[] content;

        public Day04() : base("Day04.txt")
        {
        }

        protected override void Initialize()
        {
            // content = GetExampleData();
            content = ReadFile();

            passports = new List<Dictionary<string, string>>();
            var passport = new Dictionary<string, string>();

            foreach (var line in content)
            {
                if (line.Length == 0)
                {
                    // Start of new passport
                    passports.Add(passport);
                    passport = new Dictionary<string, string>();
                }
                else
                {
                    // Add code(s) to passport
                    var codesAndValues = line.Split(' ');
                    foreach (var code in codesAndValues)
                    {
                        var codeAndValue = code.Split(':');
                        passport.Add(codeAndValue[0], codeAndValue[1]);
                    }
                }
            }

            // Add the last passport
            passports.Add(passport);
        }

        protected override void SolutionPart1()
        {
            var numberOfValidPassports = 0;

            foreach (var passport in passports)
            {
                if (IsValidPassport(passport, false))
                {
                    numberOfValidPassports++;
                }
            }

            Console.WriteLine($"Number of valid passports: {numberOfValidPassports}");
        }

        protected override void SolutionPart2()
        {
            var numberOfValidPassports = 0;

            foreach (var passport in passports)
            {
                if (IsValidPassport(passport, true))
                {
                    numberOfValidPassports++;
                }
            }

            Console.WriteLine($"Number of valid passports: {numberOfValidPassports}");
        }

        private bool IsValidPassport(Dictionary<string, string> passport, bool validateCodes)
        {
            foreach (var mandatoryPassportCode in mandatoryPassportCodes)
            {
                if (!passport.ContainsKey(mandatoryPassportCode))
                {
                    return false;
                }

                if (validateCodes && !IsValidCode(mandatoryPassportCode, passport[mandatoryPassportCode]))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsValidCode(string code, string value)
        {
            var isValid = code switch
            {
                "byr" => IsNumberAndInRange(value, 1920, 2002),
                "iyr" => IsNumberAndInRange(value, 2010, 2020),
                "eyr" => IsNumberAndInRange(value, 2020, 2030),
                "hgt" => IsValidHeight(value),
                "hcl" => IsValidHairColor(value),
                "ecl" => IsValidEyeColor(value),
                "pid" => IsValidPassportId(value),
                _ => throw new ArgumentOutOfRangeException(code),
            };

            return isValid;
        }

        private bool IsNumberAndInRange(string value, int minValue, int maxValue)
        {
            if (!int.TryParse(value, out var number))
            {
                return false;
            }

            if (number >= minValue && number <= maxValue)
            {
                return true;
            }

            return false;
        }

        private bool IsValidHeight(string value)
        {
            var isValid = false;

            var height = value[0..^2];
            var unit = value.Substring(value.Length - 2, 2);

            switch (unit)
            {
                case "in":
                    isValid = IsNumberAndInRange(height, 59, 76);
                    break;
                case "cm":
                    isValid = IsNumberAndInRange(height, 150, 193);
                    break;
                default:
                    break;
            }

            return isValid;
        }

        private bool IsValidHairColor(string value)
        {
            var isValid = false;

            if (value.Length != 7)
            {
                return false;
            }

            if (value[0] != '#')
            {
                return false;
            }

            var color = value[1..];

            isValid = color.All(x => "0123456789abcdef".Contains(x));

            return isValid;
        }

        private bool IsValidEyeColor(string value)
        {
            var validEyeColors = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

            var isValid = validEyeColors.Contains(value);

            return isValid;
        }

        private bool IsValidPassportId(string value)
        {
            if (value.Length != 9)
            {
                return false;
            }

            var isValid = int.TryParse(value, out _);

            return isValid;
        }

        private string[] GetExampleData()
        {
            var line01 = "ecl:gry pid:860033327 eyr:2020 hcl:#fffffd";
            var line02 = "byr:1937 iyr:2017 cid:147 hgt:183cm";
            var line03 = "";
            var line04 = "iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884"; // missing height
            var line05 = "hcl:#cfa07d byr:1929";
            var line06 = "";
            var line07 = "hcl:#ae17e1 iyr:2013";
            var line08 = "eyr:2024";
            var line09 = "ecl:brn pid:760753108 byr:1931";
            var line10 = "hgt:179cm";
            var line11 = "";
            var line12 = "hcl:#cfa07d eyr:2025 pid:166559648"; // missing byr
            var line13 = "iyr:2011 ecl:brn hgt:59in";

            var result = new[] { line01, line02, line03, line04, line05, line06, line07, line08, line09,
                                 line10, line11, line12, line13};

            return result;
        }
    }
}
