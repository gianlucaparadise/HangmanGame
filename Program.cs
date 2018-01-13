using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome");
            HangmanGame game = new HangmanGame();
            game.Start();
            
            Console.WriteLine(game.WordToGuess);

            while (!game.HasWon)
            {
                Console.Write($"\nWord to guess: { game.PrintableHiddenWordToGuess }");
                Console.WriteLine($"\tGuessed chars in word: { game.PrintableGuessedCharsInWord}");
                
                Console.Write("Write your guess: ");
                string guess = Console.ReadLine();

                if(!game.IsGuessRight(guess))
                {
                    Console.WriteLine("\nGuess must be the same length as word to guess");
                    continue;
                }
                
                game.ProcessGuess(guess);
            }
            
            Console.WriteLine("You win!");
        }
        
        
    }
}
