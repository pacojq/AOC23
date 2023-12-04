using System;
using System.Collections.Generic;
using System.IO;

namespace AOC23.Day3
{
    public static class Problem2
    {
        public static void Solve(string inputPath)
        {
            string[] lines = File.ReadAllLines(inputPath);
            
            List<int> numbers = new List<int>();
            Dictionary<(int, int), List<int>> gears = new Dictionary<(int, int), List<int>>();
            
            for (int i = 0; i < lines.Length; i++)
                ParseLine(lines, i, numbers, gears);

            int sum = 0;
            foreach (var gear in gears)
            {
                if (gear.Value.Count != 2)
                    continue;

                int indexA = gear.Value[0];
                int indexB = gear.Value[1];
                sum += numbers[indexA] * numbers[indexB];
            }
            
            Console.WriteLine(sum);
        }

        private static void ParseLine(
            string[] lines,
            int lineIndex,
            List<int> numbers,
            Dictionary<(int, int), List<int>> gears)
        {
            string line = lines[lineIndex];
            int currentNumber = -1;
            
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                bool isDigit = c >= '0' && c <= '9';
                
                if (isDigit)
                {
                    if (currentNumber < 0)
                    {
                        currentNumber = 0;
                        RegisterToNeighbourGears(lines, lineIndex, i - 1, gears, numbers.Count);
                    }
                    currentNumber *= 10;
                    currentNumber += c - '0';
                    RegisterToNeighbourGears(lines, lineIndex, i, gears, numbers.Count);
                }
                if (!isDigit || i == line.Length-1)
                {
                    if (currentNumber >= 0)
                    {
                        RegisterToNeighbourGears(lines, lineIndex, i, gears, numbers.Count);
                        numbers.Add(currentNumber);
                    }
                    currentNumber = -1;
                }
            }
        }

        private static void RegisterToNeighbourGears(
            string[] lines,
            int lineIndex,
            int column,
            Dictionary<(int, int), List<int>> gears,
            int numberIndex)
        {
            if (IsGear(lines, lineIndex - 1, column))
                AddGearAdjacency(gears, lineIndex - 1, column, numberIndex);
            if (IsGear(lines, lineIndex, column))
                AddGearAdjacency(gears, lineIndex, column, numberIndex);
            if (IsGear(lines, lineIndex + 1, column))
                AddGearAdjacency(gears, lineIndex + 1, column, numberIndex);
        }

        private static bool IsGear(string[] lines, int line, int col)
        {
            if (line < 0 || line >= lines.Length)
                return false;
            if (col < 0 || col >= lines[line].Length)
                return false;
            return lines[line][col] == '*';
        }

        private static void AddGearAdjacency(
            Dictionary<(int, int), List<int>> gears,
            int line,
            int col,
            int numberIndex)
        {
            var key = (line, col);
            if (!gears.ContainsKey(key))
                gears.Add(key, new List<int>());
            gears[key].Add(numberIndex);
        }
    }
}