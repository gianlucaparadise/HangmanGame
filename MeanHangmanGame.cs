using System;
using System.Collections.Generic;
using System.Linq;

namespace Test.Hangman
{
    public class MeanHangmanGame : IHangmanGame
    {
        IHangmanGame NiceHangmanGame;

        public void Start()
        {
            int wordPosition = new Random().Next(Vocabulary.Words.Length);
            string wordToGuess = Vocabulary.Words[wordPosition].ToLower();
            WordToGuessLength = wordToGuess.Length;
            HiddenWordToGuess = string.Join(Constants.LETTER_SEPARATOR.ToString(), wordToGuess.Select(x => Constants.LETTER_PLACEHOLDER));
            
            // I pick all the words with same length as randomly picked one
            PossibleWordsBank = Vocabulary.Words.Where(x => x.Length == WordToGuessLength).ToList();
        }

        private int WordToGuessLength { get; set; }

        public bool IsGuessProcessable(string guess)
        {
            if (NiceHangmanGame != null)
            {
                return NiceHangmanGame.IsGuessProcessable(guess);
            }

            return !string.IsNullOrWhiteSpace(guess) && guess.Trim().Length == WordToGuessLength;
        }

        /// <summary>
        /// Processes the guess.
        /// </summary>
        /// <param name="guess">Guess.</param>
        public void ProcessGuess(string guess)
        {
            if (NiceHangmanGame != null)
            {
                NiceHangmanGame.ProcessGuess(guess);
                return;
            }

            if (!IsGuessProcessable(guess)) return;

            if (TryToSwitchToNiceHangman())
            {
                // I reprocess the guess to let NiceHangman come in
                this.ProcessGuess(guess);
                return;
            }

            string wordSaved = this.PossibleWordsBank.Last(); // Maybe pick randomly and check that is different from guess?
            
            this.PossibleWordsBank.RemoveAll(word => 
            {
                // I remove all the words that have same char at same position
                for (int i = 0; i < guess.Length; i++)
                {
                    if (word.Contains(guess[i])) return true;
                }

                return false;
            });
            
            if (PossibleWordsBank.Count == 0)
            {
                // I have removed all the words, I reuse the saved one
                PossibleWordsBank.Add(wordSaved);
                ProcessGuess(guess);
            }
        }
        
        private bool TryToSwitchToNiceHangman()
        {
            if (PossibleWordsBank.Count == 1)
            {
                NiceHangmanGame = new HangmanGame(PossibleWordsBank.First());
                NiceHangmanGame.Start();
                return true;
            }

            return false;
        }

        public List<string> PossibleWordsBank
        {
            get;
            set;
        }

        public bool HasWon
        {
            get
            {
                if (NiceHangmanGame != null)
                {
                    return NiceHangmanGame.HasWon;
                }

                return false;
            }
        }

        public string PrintableGuessedCharsInWord
        {
            get
            {
                if (NiceHangmanGame != null)
                {
                    return NiceHangmanGame.PrintableGuessedCharsInWord;
                }

                return string.Empty;
            }
        }

        string HiddenWordToGuess;
        public string PrintableHiddenWordToGuess
        {
            get
            {
                if (NiceHangmanGame != null)
                {
                    return NiceHangmanGame.PrintableHiddenWordToGuess;
                }
                
                return HiddenWordToGuess;
            }
        }
    }
}
