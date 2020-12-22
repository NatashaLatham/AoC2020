using AdventOfCode2020.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Solutions
{
    internal class Day17 : Day
    {
        const int NumberOfCycli = 6;

        string[] content;
        private List<Cube> Cubes;

        public Day17() : base("Day17.txt")
        {
        }

        protected override void Initialize()
        {
            //content = GetExample();
            content = ReadFile();
        }

        protected override void SolutionPart1()
        {
            InitializeCubesThreeDimensions();

            for (var cycle = 1; cycle <= NumberOfCycli; cycle++)
            {
                // Reset the change state
                Cubes.ForEach(x => x.ChangeState = false);

                foreach (var cube in Cubes)
                {
                    cube.Neighbours = GetNeighboursThreeDimensions(cube);
                    cube.ApplyCycle();
                }

                var minX = Cubes.Min(Cube => Cube.X);
                var maxX = Cubes.Max(Cube => Cube.X);
                var minY = Cubes.Min(Cube => Cube.Y);
                var maxY = Cubes.Max(Cube => Cube.Y);
                var minZ = Cubes.Min(Cube => Cube.Z);
                var maxZ = Cubes.Max(Cube => Cube.Z);

                // Check the column on the left for changes
                ApplyCycleToCubesOutsideOfMapThreeDimensions(minX - 1, minX - 1, minY, maxY, minZ, maxZ);

                minX = Cubes.Min(Cube => Cube.X);

                // Check the column on the left for changes
                ApplyCycleToCubesOutsideOfMapThreeDimensions(maxX + 1, maxX + 1, minY, maxY, minZ, maxZ);

                maxX = Cubes.Max(Cube => Cube.X);

                // Check the row on the top for changes
                ApplyCycleToCubesOutsideOfMapThreeDimensions(minX, maxX, minY - 1, minY - 1, minZ, maxZ);

                minY = Cubes.Min(Cube => Cube.Y);

                // Check the row on the bottom for changes
                ApplyCycleToCubesOutsideOfMapThreeDimensions(minX, maxX, maxY + 1, maxY + 1, minZ, maxZ);

                maxY = Cubes.Max(Cube => Cube.Y);

                // Check the dimension on the 'front' for changes
                ApplyCycleToCubesOutsideOfMapThreeDimensions(minX, maxX, minY, maxY, minZ - 1, minZ - 1);

                // Check the dimension on the 'back' for changes
                ApplyCycleToCubesOutsideOfMapThreeDimensions(minX, maxX, minY, maxY, maxZ + 1, maxZ + 1);

                foreach (var cube in Cubes)
                {
                    cube.ApplyChanges();
                }

                //PrintMap(cycle);
            }

            var result = Cubes.Count(x => x.State == CubeState.Active);
            Console.WriteLine($"Number of active cubes: {result}");
        }

        protected override void SolutionPart2()
        {
            InitializeCubesFourDimensions();

            for (var cycle = 1; cycle <= NumberOfCycli; cycle++)
            {
                Console.WriteLine($"Cyle {cycle}");

                // Reset the change state
                Cubes.ForEach(x => x.ChangeState = false);
                Cubes.ForEach(x => x.Seen = false);

                foreach (var cube in Cubes)
                {
                    cube.Neighbours = GetNeighboursFourDimensions(cube);
                    cube.ApplyCycle();
                    cube.Seen = true;
                }

                var minX = Cubes.Min(Cube => Cube.X);
                var maxX = Cubes.Max(Cube => Cube.X);
                var minY = Cubes.Min(Cube => Cube.Y);
                var maxY = Cubes.Max(Cube => Cube.Y);
                var minZ = Cubes.Min(Cube => Cube.Z);
                var maxZ = Cubes.Max(Cube => Cube.Z);
                var minW = Cubes.Min(Cube => Cube.W);
                var maxW = Cubes.Max(Cube => Cube.W);

                // Check the boundaries for changes
                ApplyCycleToCubesOutsideOfMapFourDimensions(minX - 1, maxX + 1, minY - 1, maxY + 1, minZ - 1, maxZ + 1, minW - 1, maxW + 1);

                foreach (var cube in Cubes)
                {
                    cube.ApplyChanges();
                }

                //PrintMapFourDimensions(cycle);
            }

            var result = Cubes.Count(x => x.State == CubeState.Active);
            Console.WriteLine($"Number of active cubes: {result}");
        }

        private void InitializeCubesThreeDimensions()
        {
            Cubes = new List<Cube>();
            for (var x = 0; x < content.Length; x++)
            {
                for (var y = 0; y < content.Length; y++)
                {
                    var cube = new Cube(x, y, 0, content[x][y]);
                    Cubes.Add(cube);
                }
            }

            // Add the neighbours for each cube
            foreach (var cube in Cubes)
            {
                cube.Neighbours = GetNeighboursThreeDimensions(cube);
            }
        }

        private void InitializeCubesFourDimensions()
        {
            Cubes = new List<Cube>();
            for (var x = 0; x < content.Length; x++)
            {
                for (var y = 0; y < content.Length; y++)
                {
                    var cube = new Cube(x, y, 0, 0, content[x][y]);
                    Cubes.Add(cube);
                }
            }

            // Add the neighbours for each cube
            foreach (var cube in Cubes)
            {
                cube.Neighbours = GetNeighboursFourDimensions(cube);
            }
        }

        /// <summary>
        /// Apply the cycle to cubes that have not been added to the list yet (three dimensions)
        /// </summary>
        private void ApplyCycleToCubesOutsideOfMapThreeDimensions(int startX, int endX, int startY, int endY, int startZ, int endZ)
        {
            var thereWereChanges = false;
            var cubesList = new List<Cube>();
            for (var x = startX; x <= endX; x++)
            {
                for (var y = startY; y <= endY; y++)
                {
                    for (var z = startZ; z <= endZ; z++)
                    {
                        var cube = new Cube(x, y, z, '.');
                        cube.Neighbours = GetNeighboursThreeDimensions(cube);
                        cube.ApplyCycle();
                        if (cube.ChangeState)
                        {
                            // The state has been changed, we need to add this to the list
                            thereWereChanges = true;
                        }
                        cubesList.Add(cube);
                    }
                }
            }

            // If there were any changes, than we need to add these cubes to the Cubes list
            if (thereWereChanges)
            {
                Cubes.AddRange(cubesList);
            }
        }

        /// <summary>
        /// Apply the cycle to cubes that have not been added to the list yet (four dimensions)
        /// </summary>
        private void ApplyCycleToCubesOutsideOfMapFourDimensions(int startX, int endX, int startY, int endY, int startZ, int endZ, int startW, int endW)
        {
            var thereWereChanges = false;
            var cubesList = new List<Cube>();
            for (var x = startX; x <= endX; x++)
            {
                Console.WriteLine($"    Checking X {x} of {endX}");
                for (var y = startY; y <= endY; y++)
                {
                    for (var z = startZ; z <= endZ; z++)
                    {
                        for (var w = startW; w <= endW; w++)
                        {
                            var existingCube = Cubes.SingleOrDefault(cube => cube.X == x && cube.Y == y && cube.Z == z && cube.W == w);
                            if (existingCube != null)
                            {
                                // If the cube already exists, it should already have been handled
                                if (!existingCube.Seen)
                                {
                                    throw new InvalidOperationException("Something went wrong. Cube has not been Seen");
                                }

                                continue;
                            }

                            var cube = new Cube(x, y, z, w, '.');
                            cube.Neighbours = GetNeighboursFourDimensions(cube);
                            cube.ApplyCycle();
                            cube.Seen = true;
                            if (cube.ChangeState)
                            {
                                // The state has been changed, we need to add this to the list
                                thereWereChanges = true;
                                cubesList.Add(cube);
                            }
                        }
                    }
                }
            }

            // If there were any changes, than we need to add these cubes to the Cubes list
            if (thereWereChanges)
            {
                Cubes.AddRange(cubesList);
            }
        }

        private IList<Cube> GetNeighboursThreeDimensions(Cube baseCube)
        {
            var neighbours = new List<Cube>();
            for (var x = baseCube.X - 1; x <= baseCube.X + 1; x++)
            {
                for (var y = baseCube.Y - 1; y <= baseCube.Y + 1; y++)
                {
                    for (var z = baseCube.Z - 1; z <= baseCube.Z + 1; z++)
                    {
                        // Eclude the cube itself
                        if (baseCube.X == x && baseCube.Y == y && baseCube.Z == z)
                        {
                            continue;
                        }

                        // If the neighbour doesn't exist it's not relevant (because it is inactive)
                        var neighbour = Cubes.SingleOrDefault(cube => cube.X == x && cube.Y == y && cube.Z == z);
                        if (neighbour != null)
                        {
                            neighbours.Add(neighbour);
                        }
                    }
                }
            }

            return neighbours;
        }

        private IList<Cube> GetNeighboursFourDimensions(Cube baseCube)
        {
            if (baseCube.HasAllNeighbours)
            {
                return baseCube.Neighbours;
            }

            var neighbours = new List<Cube>();
            for (var x = baseCube.X - 1; x <= baseCube.X + 1; x++)
            {
                for (var y = baseCube.Y - 1; y <= baseCube.Y + 1; y++)
                {
                    for (var z = baseCube.Z - 1; z <= baseCube.Z + 1; z++)
                    {
                        for (var w = baseCube.W - 1; w <= baseCube.W + 1; w++)
                        {
                            // Exclude the cube itself
                            if (baseCube.X == x && baseCube.Y == y && baseCube.Z == z && baseCube.W == w)
                            {
                                continue;
                            }

                            // If the neighbour doesn't exist it's not relevant (because it is inactive)
                            var neighbour = Cubes.SingleOrDefault(cube => cube.X == x && cube.Y == y && cube.Z == z && cube.W == w);
                            if (neighbour != null)
                            {
                                neighbours.Add(neighbour);
                            }
                        }
                    }
                }
            }

            return neighbours;
        }

        private string[] GetExample()
        {
            var line01 = ".#.";
            var line02 = "..#";
            var line03 = "###";

            var result = new[] { line01, line02, line03 };

            return result;
        }

        private void PrintMap(int cycle)
        {
            var minX = Cubes.Min(Cube => Cube.X);
            var previousX = minX - 1;
            var previousZ = Cubes.Min(Cube => Cube.Z) - 1;

            Console.WriteLine($"After cycle: {cycle}");
            var cubesSorted = Cubes.OrderBy(cube => cube.Z).ThenBy(cube => cube.X).ThenBy(cube => cube.Y);
            foreach (var cube in cubesSorted)
            {
                if (cube.Z > previousZ)
                {
                    Console.WriteLine();
                    Console.WriteLine($"z = {cube.Z}");
                    previousZ = cube.Z;
                    previousX = minX;
                }
                else if (cube.X > previousX)
                {
                    Console.WriteLine();
                    previousX = cube.X;
                }

                cube.Print();
            }

            Console.WriteLine();
        }

        private void PrintMapFourDimensions(int cycle)
        {
            var minX = Cubes.Min(Cube => Cube.X);
            var previousX = minX - 1;
            var previousZ = Cubes.Min(Cube => Cube.Z) - 1;
            var previousW = Cubes.Min(Cube => Cube.W) - 1;

            Console.WriteLine($"After cycle: {cycle}");
            var cubesSorted = Cubes.OrderBy(cube => cube.W).ThenBy(cube => cube.Z).ThenBy(cube => cube.X).ThenBy(cube => cube.Y);
            foreach (var cube in cubesSorted)
            {
                if (cube.W > previousW)
                {
                    Console.WriteLine();
                    Console.WriteLine($"z = {cube.Z} w = {cube.W}");
                    previousZ = cube.Z;
                    previousW = cube.W;
                    previousX = minX;
                }
                else if (cube.Z > previousZ)
                {
                    Console.WriteLine();
                    Console.WriteLine($"z = {cube.Z} w = {cube.W}");
                    previousZ = cube.Z;
                    previousW = cube.W;
                    previousX = minX;
                }
                else if (cube.X > previousX)
                {
                    Console.WriteLine();
                    previousX = cube.X;
                }

                cube.Print();
            }

            Console.WriteLine();
        }
    }
}
