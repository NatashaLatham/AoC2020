using AdventOfCode2020.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    internal class Day20 : Day
    {
        private List<ImageTile> Tiles;

        public Day20() : base("Day20.txt")
        {
        }

        protected override void Initialize()
        {
            //var content = GetExample();
            var content = ReadFile();

            Tiles = new List<ImageTile>();
            ImageTile imageTile = null;
            var imageLineIndex = 0;
            foreach (var line in content)
            {
                var imageSize = line.Length;
                if (line == string.Empty)
                {
                    // End of tile
                    imageLineIndex = 0;
                }
                else if (line.StartsWith("Tile"))
                {
                    // Start of new tile
                    var splitLine = line.TrimEnd(':').Split(' ');
                    var tileNumber = int.Parse(splitLine[1]);
                    imageTile = new ImageTile(tileNumber, imageSize);
                    Tiles.Add(imageTile);
                }
                else
                {
                    // Image
                    imageTile.Image[imageLineIndex] = line;
                    imageLineIndex++;
                }
            }

            // Set the edges for each tile
            foreach (var tile in Tiles)
            {
                tile.SetEdges();
            }
        }

        protected override void SolutionPart1()
        {
            // All we have to do to get the correct answer is find the corner tiles
            // This should be the tiles with two adjacent tiles

            // Find all the adjacent tile for all the tile
            foreach (var tile in Tiles)
            {
                var matches = Tiles.Where(x => x.Id != tile.Id && x.Edges.Any(y => tile.Edges.Contains(y)));
                tile.AdjacentTiles.AddRange(matches);
            }

            // Find the ones with 2 adjacent tiles
            var hasTwoAdjacentTiles = Tiles.Where(x => x.AdjacentTiles.Count == 2);

            Console.WriteLine($"Found corners: {string.Join(", ", hasTwoAdjacentTiles.Select(x => x.Id))}");

            var result = 1L;
            foreach (var tile in hasTwoAdjacentTiles)
            {
                result *= tile.Id;
            }

            Console.WriteLine($"Result: {result}");
        }

        protected override void SolutionPart2()
        {
            var result = 0;
            Console.WriteLine($"Result: {result}");
        }

        private string[] GetExample()
        {
            var result = ReadFile(@".\Data\Day20_Example.txt");

            return result;
        }
    }
}
