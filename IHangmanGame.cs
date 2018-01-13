using System;
namespace Test.Hangman
{
    public interface IHangmanGame
    {
        void Start();
        
        bool HasWon { get; }
        
        string PrintableHiddenWordToGuess { get; }
        string PrintableGuessedCharsInWord { get; }
        bool IsGuessProcessable(string guess);
        void ProcessGuess(string guess);
    }
}
