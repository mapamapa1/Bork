using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public class InputProcessor
    {
        public string PlayerInput { get; set; }
        public string[] PlayerInputSplittedString { get; set; }
        public int IndexOfFirstSpace { get; set; }
        public string FirstWord { get; set; }
        public string SecondWordToEnd { get; set; }

        public WordListProcessor WordListProcessor { get; set; }

        public InputProcessor()
        {
            WordListProcessor = new WordListProcessor();
        }
        public void Process(string playerInput)
        {
 
            PlayerInput = playerInput.ToLower();
            PlayerInputSplittedString = PlayerInput.Split(" ");

            if (PlayerInputSplittedString.Length > 1)
            {
                IndexOfFirstSpace = PlayerInput.IndexOf(' ');
                FirstWord = PlayerInput.Substring(0, IndexOfFirstSpace);
                SecondWordToEnd = PlayerInput.Substring(IndexOfFirstSpace + 1);
                CheckAndReplacePrepositions();

            }
            else
            {
                IndexOfFirstSpace = -1;
                FirstWord = "";
                SecondWordToEnd = "";
            }

                      
            PlayerInputSplittedString[0] = WordListProcessor.CheckActionSynonyms(PlayerInputSplittedString[0]);
            
            
        }

        public void CheckAndReplacePrepositions()
        {
            PlayerInputSplittedString = WordListProcessor.CheckPrepositionSynonyms(PlayerInputSplittedString);
            PlayerInput = string.Join(" ", PlayerInputSplittedString);
            IndexOfFirstSpace = PlayerInput.IndexOf(' ');
            FirstWord = PlayerInput.Substring(0, IndexOfFirstSpace);
            SecondWordToEnd = PlayerInput.Substring(IndexOfFirstSpace + 1);

        }
    }
}
