using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test.Hangman;

namespace Test
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //GenerateVocabulary();
            //return;

            Console.WriteLine("Welcome");
            IHangmanGame game = new AssholeHangmanGame();
            game.Start();

            while (!game.HasWon)
            {
                Console.Write($"\nWord to guess: { game.PrintableHiddenWordToGuess }");
                Console.WriteLine($"\tGuessed chars in word: { game.PrintableGuessedCharsInWord}");

                Console.Write("Write your guess: ");
                string guess = Console.ReadLine();

                if (!game.IsGuessProcessable(guess))
                {
                    Console.WriteLine("\nGuess must be the same length as word to guess");
                    continue;
                }

                game.ProcessGuess(guess);
            }

            Console.WriteLine("You win!");
        }
        
        private static void GenerateVocabulary()
        {
            List<string> result = new List<string>();
            
            for (int i = 3; i < 8; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    result.Add(RandomString(i));
                }
            }
            
            Console.WriteLine(string.Join("\",\n\"", result));
        }
        static Random rand = new Random();
        private static string RandomString(int length)
        {
            const string pool = "abcdefghijklmnopqrstuvwxyz";
            var builder = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                var c = pool[rand.Next(0, pool.Length)];
                builder.Append(c);
            }

            return builder.ToString();
        }
    }
}
