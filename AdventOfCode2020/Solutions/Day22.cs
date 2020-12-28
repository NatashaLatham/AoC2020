using System;
using System.Collections.Generic;

namespace AdventOfCode2020.Solutions
{
    internal class Day22 : Day
    {
        private Queue<int> DeckPlayer1;
        private Queue<int> DeckPlayer2;

        public Day22() : base("Day22.txt")
        {
        }

        protected override void Initialize()
        {
            //var content = GetExample();
            var content = ReadFile();

            var player = 0;
            DeckPlayer1 = new Queue<int>();
            DeckPlayer2 = new Queue<int>();
            foreach (var line in content)
            {
                if (line.Length == 0)
                {
                    continue;
                }
                else if (line.StartsWith("Player"))
                {
                    player = int.Parse(line.Substring(7, 1));
                }
                else
                {
                    if (player == 1)
                    {
                        DeckPlayer1.Enqueue(int.Parse(line));
                    }
                    else
                    {
                        DeckPlayer2.Enqueue(int.Parse(line));
                    }
                }
            }
        }

        protected override void SolutionPart1()
        {
            var round = 0;
            while (DeckPlayer1.Count != 0 && DeckPlayer2.Count != 0)
            {
                round++;

                //Console.WriteLine($"Round: {round}");

                var cardPlayer1 = DeckPlayer1.Dequeue();
                var cardPlayer2 = DeckPlayer2.Dequeue();

                if (cardPlayer1 > cardPlayer2)
                {
                    DeckPlayer1.Enqueue(cardPlayer1);
                    DeckPlayer1.Enqueue(cardPlayer2);
                }

                if (cardPlayer2 > cardPlayer1)
                {
                    DeckPlayer2.Enqueue(cardPlayer2);
                    DeckPlayer2.Enqueue(cardPlayer1);
                }
            }

            Console.WriteLine($"Number of played rounds: {round}");

            var score = 0L;
            if (DeckPlayer1.Count == 0)
            {
                Console.WriteLine("Player 2 won");
                score = GetScore(DeckPlayer2);
            }
            else
            {
                Console.WriteLine("Player 1 won");
                score = GetScore(DeckPlayer1);
            }

            Console.WriteLine($"Winning score is: {score}");
        }

        protected override void SolutionPart2()
        {
            var result = 0;
            Console.WriteLine($"Result: {result}");
        }

        private long GetScore(Queue<int> deck)
        {
            var multiplier = deck.Count;
            var score = 0;
            while (deck.Count != 0)
            {
                var card = deck.Dequeue();
                score += multiplier * card;
                multiplier--;
            }

            return score;
        }

        private string[] GetExample()
        {
            var line01 = "Player 1:";
            var line02 = "9";
            var line03 = "2";
            var line04 = "6";
            var line05 = "3";
            var line06 = "1";
            var line07 = "";
            var line08 = "Player 2:";
            var line09 = "5";
            var line10 = "8";
            var line11 = "4";
            var line12 = "7";
            var line13 = "10";

            var result = new[] { line01, line02, line03, line04, line05, line06, line07, line08, line09, line10,
                                 line11, line12, line13 };

            return result;
        }
    }
}
