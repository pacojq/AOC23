using System;
using System.Collections.Generic;
using System.IO;

namespace AOC23.Day4
{
    public static class Problem1
    {
        public static void Solve(string inputPath)
        {
            string[] lines = File.ReadAllLines(inputPath);

            int sum = 0;
            for (int i = 0; i < lines.Length; i++)
                sum += ComputeLine(lines[i]);
            
            Console.WriteLine(sum);
        }

        private static int ComputeLine(string line)
        {
            ParseLine(line, out var winners, out var current);

            int score = 0;
            for (int i = 0; i < current.Count; i++)
            {
                for (int j = winners.Count - 1; j >= 0; j--)
                {
                    if (current[i] == winners[j])
                    {
                        if (score == 0) score = 1;
                        else score *= 2;
                        winners.RemoveAt(j);
                        break;
                    }
                }
            }

            return score;
        }

        private static void ParseLine(
            string line,
            out List<int> winners,
            out List<int> current)
        {
            string[] tokens = line.Split(SPACE, StringSplitOptions.RemoveEmptyEntries);

            winners = new List<int>();
            current = new List<int>();

            bool isCurrent = false;
            
            // i=2 so we skip line intro
            for (int i = 2; i < tokens.Length; i++)
            {
                if (tokens[i] == "|")
                {
                    isCurrent = true;
                    continue;
                }
                if (isCurrent) current.Add(int.Parse(tokens[i]));
                else winners.Add(int.Parse(tokens[i]));
            }
        }
        
        private static readonly char[] SPACE = new[] { ' ' };
    }
}