using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    public class HangmanGame
    {
        public HangmanGame()
        {
        }

        public void Start()
        {
            HasWon = false;
            this.GuessedCharsInWord = new List<char>();

            int wordPosition = new Random().Next(Vocabulary.Words.Length);
            this.WordToGuess = Vocabulary.Words[wordPosition].ToLower();

            HiddenWordToGuess = WordToGuess.Select(x => Constants.LETTER_PLACEHOLDER).ToArray();
        }

        public string WordToGuess
        {
            get;
            set;
        }

        private char[] HiddenWordToGuess
        {
            get;
            set;
        }

        public bool HasWon
        {
            get;
            set;
        }

        public bool IsGuessRight(string guess)
        {
            return !string.IsNullOrWhiteSpace(guess) && guess.Trim().Length == WordToGuess.Length;
        }
        
        public void ProcessGuess(string guessRaw)
        {
            if (!IsGuessRight(guessRaw)) return;

            string guess = guessRaw.ToLower();
            
            char[] guessCharsArray = guess.ToCharArray();
            for (int i = 0; i < guessCharsArray.Length; i++)
            {
                char guessChar = guessCharsArray[i];
                char rightChar = WordToGuess[i];
                
                if (HiddenWordToGuess[i] == Constants.LETTER_PLACEHOLDER && rightChar == guessChar)
                {
                    // this letter has just been discovered
                    HiddenWordToGuess[i] = rightChar;
                    RemoveGuessedChar(rightChar);
                    continue;
                }
                
                AddGuessedChar(guessChar);
            }

            this.HasWon = this.WordToGuess.ToLower() == guess.ToLower();
        }

        private List<char> GuessedCharsInWord
        {
            get;
            set;
        }
        
        private void RemoveGuessedChar(char guessChar)
        {
            if (!GuessedCharsInWord.Contains(guessChar)) return; // I don't have it, no need to check
            
            if (HasBeenDiscoveredEveryTime(guessChar))
            {
                GuessedCharsInWord.Remove(guessChar);
            }
        }
        
        private void AddGuessedChar(char guessChar)
        {
            if (GuessedCharsInWord.Contains(guessChar)) return; // I already have it, no need to check
            
            if (!HasBeenDiscoveredEveryTime(guessChar))
            {
                GuessedCharsInWord.Add(guessChar);
            }
        }
        
        private bool HasBeenDiscoveredEveryTime(char guessChar)
        {
            return WordToGuess.Count(x => x == guessChar) == HiddenWordToGuess.Count(x => x == guessChar);
        }

        public string PrintableGuessedCharsInWord
        {
            get
            {
                return string.Join(Constants.LETTER_SEPARATOR_GUESSED_CHAR_IN_WORD, GuessedCharsInWord);
            }
        }

        public string PrintableHiddenWordToGuess
        {
            get
            {
                return string.Join(Constants.LETTER_SEPARATOR.ToString(), HiddenWordToGuess);
            }
        }
    }
}
