using AdventOfCode2020.Entities;
using System;
using System.Collections.Generic;

namespace AdventOfCode2020.Solutions
{
    internal class Day12 : Day
    {
        private readonly List<Operation> instructions = new List<Operation>();

        private string boatDirection = "E";

        private long shipEastPosition = 0;
        private long shipNorthPosition = 0;

        private long waypointEastPosition = 10;
        private long waypointNorthPosition = 1;

        public Day12() : base("Day12.txt")
        {
        }

        protected override void Initialize()
        {
            //var content = GetExample();
            var content = ReadFile();

            foreach (var instruction in content)
            {
                var operationName = instruction[0].ToString();
                var argument = int.Parse(instruction.Substring(1));

                instructions.Add(new Operation(operationName, argument));
            }
        }

        protected override void SolutionPart1()
        {
            foreach (var instruction in instructions)
            {
                switch (instruction.Name)
                {
                    case "L":
                    case "R":
                        ChangeDirection(instruction);
                        break;
                    case "N":
                    case "S":
                    case "E":
                    case "W":
                        MoveInDirection(instruction.Name, instruction.Argument);
                        break;
                    case "F":
                        MoveForward(instruction.Argument);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(instruction.Name));
                }
            }

            var manhattanDistance = Math.Abs(shipEastPosition) + Math.Abs(shipNorthPosition);
            Console.WriteLine($"Manhattan Distance: {manhattanDistance}");
        }

        protected override void SolutionPart2()
        {
            // Reset ship
            shipEastPosition = 0;
            shipNorthPosition = 0;

            foreach (var instruction in instructions)
            {
                switch (instruction.Name)
                {
                    case "L":
                    case "R":
                        RotateWaypoint(instruction);
                        Console.WriteLine($"Rotated waypoint to:  {waypointEastPosition}-{waypointNorthPosition}");
                        break;
                    case "N":
                    case "S":
                    case "E":
                    case "W":
                        MoveWaypoint(instruction.Name, instruction.Argument);
                        Console.WriteLine($"Moved waypoint to: {waypointEastPosition}-{waypointNorthPosition}");
                        break;
                    case "F":
                        MoveToWaypoint(instruction.Argument);
                        Console.WriteLine($"Forwarded to: {shipEastPosition}-{shipNorthPosition}");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(instruction.Name));
                }
            }

            var manhattanDistance = Math.Abs(shipEastPosition) + Math.Abs(shipNorthPosition);
            Console.WriteLine($"Manhattan Distance: {manhattanDistance}");
        }

        private void ChangeDirection(Operation instruction)
        {
            var timesNinetyDegrees = instruction.Argument / 90;
            switch (instruction.Name)
            {
                case "L":
                    for (var i = 1; i <= timesNinetyDegrees; i++)
                    {
                        boatDirection = GetNewDirectionLeft(boatDirection);
                    };
                    break;
                case "R":
                    for (var i = 1; i <= timesNinetyDegrees; i++)
                    {
                        boatDirection = GetNewDirectionRight(boatDirection);
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(instruction.Name));
            }
        }

        private void RotateWaypoint(Operation instruction)
        {
            var timesNinetyDegrees = instruction.Argument / 90;
            switch (instruction.Name)
            {
                case "L":
                    for (var i = 1; i <= timesNinetyDegrees; i++)
                    {
                        RotateWaypoint90DegreesLeft();
                    };
                    break;
                case "R":
                    for (var i = 1; i <= timesNinetyDegrees; i++)
                    {
                        RotateWaypoint90DegreesRight();
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(instruction.Name));
            }
        }

        private void RotateWaypoint90DegreesLeft()
        {
            var oldEastPostion = waypointEastPosition;

            waypointEastPosition = -1 * waypointNorthPosition;
            waypointNorthPosition = oldEastPostion;
        }

        private void RotateWaypoint90DegreesRight()
        {
            var oldEastPostion = waypointEastPosition;

            waypointEastPosition = waypointNorthPosition;
            waypointNorthPosition = -1 * oldEastPostion;
        }

        /// <summary>
        /// Return a new direction based on 90 degrees left
        /// </summary>
        /// <returns></returns>
        private string GetNewDirectionLeft(string currentDirection)
        {
            return currentDirection switch
            {
                "E" => "N",
                "S" => "E",
                "W" => "S",
                "N" => "W",
                _ => throw new ArgumentOutOfRangeException(nameof(currentDirection))
            };
        }

        /// <summary>
        /// Return a new direction based on 90 degrees right
        /// </summary>
        /// <returns></returns>
        private string GetNewDirectionRight(string currentDirection)
        {
            return currentDirection switch
            {
                "E" => "S",
                "S" => "W",
                "W" => "N",
                "N" => "E",
                _ => throw new ArgumentOutOfRangeException(nameof(currentDirection))
            };
        }

        private void MoveInDirection(string direction, int distance)
        {
            switch (direction)
            {
                case "E":
                    shipEastPosition += distance;
                    break;
                case "W":
                    shipEastPosition -= distance;
                    break;
                case "N":
                    shipNorthPosition += distance;
                    break;
                case "S":
                    shipNorthPosition -= distance;
                    break;
            }
        }

        private void MoveWaypoint(string direction, int distance)
        {
            switch (direction)
            {
                case "E":
                    waypointEastPosition += distance;
                    break;
                case "W":
                    waypointEastPosition -= distance;
                    break;
                case "N":
                    waypointNorthPosition += distance;
                    break;
                case "S":
                    waypointNorthPosition -= distance;
                    break;
            }
        }

        private void MoveForward(int distance)
        {
            MoveInDirection(boatDirection, distance);
        }

        private void MoveToWaypoint(int distance)
        {
            shipEastPosition += distance * waypointEastPosition;
            shipNorthPosition += distance * waypointNorthPosition;
        }

        private string[] GetExample()
        {
            var line01 = "F10";
            var line02 = "N3";
            var line03 = "F7";
            var line04 = "R90";
            var line05 = "F11";

            var result = new[] { line01, line02, line03, line04, line05 };

            return result;
        }
    }
}
