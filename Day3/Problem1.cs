using System;
using System.Collections.Generic;
using System.IO;

namespace AOC23.Day3
{
    public static class Problem1
    {
        public static void Solve(string inputPath)
        {
            string[] lines = File.ReadAllLines(inputPath);

            List<int> numbers = new List<int>();
            for (int i = 0; i < lines.Length; i++)
                ParseLine(lines, i, numbers);

            int sum = 0;
            for (int i = 0; i < numbers.Count; i++)
                sum += numbers[i];
            
            Console.WriteLine(sum);
        }

        private static void ParseLine(string[] lines, int lineIndex, List<int> numbers)
        {
            string line = lines[lineIndex];

            bool willAdd = false;
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
                        willAdd = FindNeighbourSymbols(lines, lineIndex, i - 1);
                    }
                    currentNumber *= 10;
                    currentNumber += c - '0';
                    willAdd |= FindNeighbourSymbols(lines, lineIndex, i);
                }
                if (!isDigit || i == line.Length-1)
                {
                    if (currentNumber >= 0)
                    {
                        willAdd |= FindNeighbourSymbols(lines, lineIndex, i);
                        if (willAdd) numbers.Add(currentNumber);
                    }
                    willAdd = false;
                    currentNumber = -1;
                }
            }
        }

        private static bool FindNeighbourSymbols(string[] lines, int lineIndex, int column)
        {
            char c;
            bool result = false;
            if (TryGetChar(lines, lineIndex - 1, column, out c)) result |= IsSymbol(c);
            if (TryGetChar(lines, lineIndex, column, out c))    result |= IsSymbol(c);
            if (TryGetChar(lines, lineIndex + 1, column, out c)) result |= IsSymbol(c);
            return result;
        }

        private static bool IsSymbol(char c) => c != '.' && !(c >= '0' && c <= '9');

        private static bool TryGetChar(string[] lines, int line, int col, out char c)
        {
            c = '.';
            if (line < 0 || line >= lines.Length)
                return false;
            if (col < 0 || col >= lines[line].Length)
                return false;
            c = lines[line][col];
            return true;
        }
    }
}