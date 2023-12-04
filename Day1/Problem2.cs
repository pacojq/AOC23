using System;
using System.IO;

namespace AOC23.Day1
{
    public static class Problem2
    {
        public static void Solve(string inputPath)
        {
            string[] lines = File.ReadAllLines(inputPath);
            Console.WriteLine(SumCalibrationValues(lines));
        }

        private static readonly string[] DIGITS = new string[]
        {
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine"
        };
        
        
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
                if (TryMatchDigitFwd(line, i, out int d))
                {
                    firstDigit = d;
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
                if (TryMatchDigitBack(line, i, out int d))
                {
                    secondDigit = d;
                    break;
                }
            }

            return firstDigit * 10 + secondDigit;
        }

        
        private static bool TryMatchDigitFwd(string line, int c, out int digit)
        {
            for (int d = 0; d < DIGITS.Length; d++)
            {
                if (MatchDigitFwd(line, c, d))
                {
                    digit = d+1;
                    return true;
                }
            }
            digit = 0;
            return false;
        }
        
        private static bool MatchDigitFwd(string line, int c, int digit)
        {
            string word = DIGITS[digit];
            for (int i = 0; i < word.Length; i++)
            {
                int index = c + i;
                if (index >= line.Length)
                    return false;

                if (line[index] != word[i])
                    return false;
            }
            return true;
        }

        
        private static bool TryMatchDigitBack(string line, int c, out int digit)
        {
            for (int d = 0; d < DIGITS.Length; d++)
            {
                if (MatchDigitBack(line, c, d))
                {
                    digit = d+1;
                    return true;
                }
            }
            digit = 0;
            return false;
        }
        
        private static bool MatchDigitBack(string line, int c, int digit)
        {
            string word = DIGITS[digit];
            for (int i = 0; i < word.Length; i++)
            {
                int index = c - i;
                if (index < 0)
                    return false;

                if (line[index] != word[word.Length - (i+1)])
                    return false;
            }
            return true;
        }

        
    }
}