using System;
using System.Collections.Generic;
using System.IO;

namespace AOC23.Day5
{
    public static class Problem1
    {
        private struct Mapping
        {
            public long SourceStart;
            public long DestinationStart;
            public long Length;
        }
        
        private class MappingTable
        {
            public readonly List<Mapping> Mappings = new List<Mapping>();

            public long Map(long n)
            {
                for (int i = 0; i < Mappings.Count; i++)
                {
                    long offset = n - Mappings[i].SourceStart;
                    if (offset >= 0 && offset < Mappings[i].Length)
                        return Mappings[i].DestinationStart + offset;
                }
                return n;
            }
        }
        
        
        public static void Solve(string inputPath)
        {
            string[] lines = File.ReadAllLines(inputPath);
            Parse(lines, out var seeds, out var mappingTables);

            long minResult = long.MaxValue;

            for (int i = 0; i < seeds.Count; i++)
            {
                long value = seeds[i];
                for (int j = 0; j < mappingTables.Count; j++)
                {
                    value = mappingTables[j].Map(value);
                }

                if (value < minResult)
                    minResult = value;
            }
            
            Console.WriteLine(minResult);
        }

        private static void Parse(
            string[] lines,
            out List<long> seeds,
            out List<MappingTable> mappingTables)
        {
            // Parse seeds
            {
                seeds = new List<long>();
                string[] tokens = lines[0].Split(SPACE, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 1; i< tokens.Length; i++)
                    seeds.Add(long.Parse(tokens[i]));
            }
            // Parse mappings
            {
                mappingTables = new List<MappingTable>();
                int lineIndex = 1;
                while (lineIndex < lines.Length)
                {
                    lineIndex++;
                    string line = lines[lineIndex];
                    if (string.IsNullOrEmpty(line))
                        continue;

                    if (line[line.Length - 1] == ':')
                        mappingTables.Add(ParseMapping(lines, ref lineIndex));
                }
            }
        }
        
        private static MappingTable ParseMapping(string[] lines, ref int lineIndex)
        {
            MappingTable table = new MappingTable();

            lineIndex++;
            
            while (!string.IsNullOrEmpty(lines[lineIndex]))
            {
                string[] tokens = lines[lineIndex].Split(SPACE, StringSplitOptions.RemoveEmptyEntries);
                table.Mappings.Add(new Mapping()
                {
                    SourceStart = long.Parse(tokens[1]),
                    DestinationStart = long.Parse(tokens[0]),
                    Length = long.Parse(tokens[2])
                });
                
                lineIndex++;
                if (lineIndex >= lines.Length)
                    break;
            }
            
            return table;
        }

        private static readonly char[] SPACE = new[] { ' ' };
    }
}