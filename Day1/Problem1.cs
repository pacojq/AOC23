using System;
using System.IO;

namespace AOC23.Day1
{
    public static class Problem1
    {
        public static void Solve(string inputPath)
        {
            string[] lines = File.ReadAllLines(inputPath);
            Console.WriteLine(SumCalibrationValues(lines));
        }

        private static int SumCalibrationValues(string[] lines)
        {
            int sum = 0;
            for (int i = 0; i < lines.Length; i++)
                sum += GetCalibrationValue(lines[i]);
            return sum;
        }

        private static int GetCalibrationValue(string line)
        {
            int firstDigit = 0;
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c >= '0' && c <= '9')
                {
                    firstDigit = c - '0';
                    break;
                }
            }
            
            int secondDigit = 0;
            for (int i = line.Length-1; i >= 0; i--)
            {
                char c = line[i];
                if (c >= '0' && c <= '9')
                {
                    secondDigit = c - '0';
                    break;
                }
            }

            return firstDigit * 10 + secondDigit;
        }

        
    }
}