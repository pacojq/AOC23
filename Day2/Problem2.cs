using System;
using System.IO;

namespace AOC23.Day2
{
    public static class Problem2
    {
        private struct Game
        {
            public int MaxR;
            public int MaxG;
            public int MaxB;
        }
        
        public static void Solve(string inputPath)
        {
            string[] lines = File.ReadAllLines(inputPath);
            Game[] games = new Game[lines.Length];
            for (int i = 0; i < games.Length; i++)
                games[i] = ParseGame(lines[i], i);
            

            int sum = 0;
            for (int i = 0; i < games.Length; i++)
            {
                Game g = games[i];
                sum += g.MaxR * g.MaxB * g.MaxG;
            }
            
            Console.WriteLine(sum);
        }

        
        
        private static int DigitCount(int n) => (int)(Math.Log10(n) + 1);

        private static Game ParseGame(string line, int gameIndex)
        {
            Game game = new Game();
            
            int digitCount = DigitCount(gameIndex+1);
            line = line.Substring(7 + digitCount);
            string[] rounds = line.Split(';');

            for (int i = 0; i < rounds.Length; i++)
            {
                Game round = ParseGameRound(rounds[i]);
                if (round.MaxR > game.MaxR) game.MaxR = round.MaxR;
                if (round.MaxG > game.MaxG) game.MaxG = round.MaxG;
                if (round.MaxB > game.MaxB) game.MaxB = round.MaxB;
            }
            return game;
        }
        
        private static Game ParseGameRound(string round)
        {
            Game game = new Game();
         
            string[] tokens = round.Split(SPACE, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tokens.Length; i += 2)
            {
                int n = int.Parse(tokens[i]);
                char channel = tokens[i + 1][0];

                switch (channel)
                {
                    case 'r': game.MaxR = n; break;
                    case 'g': game.MaxG = n; break;
                    case 'b': game.MaxB = n; break;
                }
            }
            
            return game;
        }
    
        private static readonly char[] SPACE = new[] { ' ' };

    }
}