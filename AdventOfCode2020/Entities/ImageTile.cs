using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Entities
{
    internal class ImageTile
    {
        public int Id { get; }

        public string[] Image { get; }

        public List<string> Edges { get; private set; }

        public List<ImageTile> AdjacentTiles { get; }

        public ImageTile(int id, int graphSize)
        {
            Id = id;
            Image = new string[graphSize];
            AdjacentTiles = new List<ImageTile>();
        }

        public void SetEdges()
        {
            var topEdge = Image[0];
            var leftEdgeChars = new char[Image.Length];
            var rightEdgeChars = new char[Image.Length];
            var bottomEdge = Image[Image.Length - 1];

            for (var i = 0; i < Image.Length; i++)
            {
                leftEdgeChars[i] = Image[i][0];
                rightEdgeChars[i] = Image[i][Image.Length - 1];
            }

            var leftEdge = new string(leftEdgeChars);
            var rightEdge = new string(rightEdgeChars);
            Edges = new List<string> { topEdge, leftEdge, rightEdge, bottomEdge,
                    Reverse(topEdge), Reverse(leftEdge), Reverse(rightEdge), Reverse(bottomEdge) };
        }

        /// <summary>
        /// Reverse the characters in the given string (for flipping the tile)
        /// </summary>
        private string Reverse(string line)
        {
            var result = new StringBuilder();
            for (var i = line.Length - 1; i >= 0; i--)
            {
                result.Append(line[i]);
            }

            return result.ToString();
        }
    }
}
