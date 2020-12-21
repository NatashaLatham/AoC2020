using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Entities
{
    internal enum CubeState
    {
        Active,
        Inactive
    }

    internal class Cube
    {
        public int X { get; }

        public int Y { get; }

        public int Z { get; }

        public CubeState State { get; private set; }

        public bool ChangeState { get; set; }

        public IList<Cube> Neighbours { get; set; }

        public Cube(int x, int y, int z, char state)
        {
            X = x;
            Y = y;
            Z = z;

            State = state switch
            {
                '#' => CubeState.Active,
                '.' => CubeState.Inactive,
                _ => throw new ArgumentOutOfRangeException(nameof(state)),
            };

            Neighbours = new List<Cube>();
        }

        public void ApplyCycle()
        {
            var activeNeighbours = Neighbours.Count(x => x.State == CubeState.Active);
            switch (State)
            {
                case CubeState.Active:
                    // If a cube is active and exactly 2 or 3 of its neighbours are also active, 
                    // the cube remains active. Otherwise, the cube becomes inactive.
                    if (activeNeighbours < 2 || activeNeighbours > 3)
                    {
                        ChangeState = true;
                    }
                    break;
                case CubeState.Inactive:
                    // If a cube is inactive but exactly 3 of its neighbours are active, 
                    // the cube becomes active. Otherwise, the cube remains inactive.
                    if (activeNeighbours == 3)
                    {
                        ChangeState = true;
                    }
                    break;
            }
        }

        public void ApplyChanges()
        {
            if (ChangeState)
            {
                State = State == CubeState.Active ? CubeState.Inactive : CubeState.Active;
            }
        }

        public void Print()
        {
            var character = State switch
            {
                CubeState.Active => '#',
                CubeState.Inactive => '.',
                _ => throw new ArgumentOutOfRangeException(nameof(State)),
            };

            Console.Write(character);
        }
    }
}
