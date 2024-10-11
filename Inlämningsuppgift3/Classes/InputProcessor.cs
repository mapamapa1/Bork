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

        public InputProcessor()
        {
            
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

            }
            else
            {
                IndexOfFirstSpace = -1;
                FirstWord = "";
                SecondWordToEnd = "";
            }

            

            WordListProcessor wordListProcessor = new WordListProcessor();
            PlayerInputSplittedString[0] = wordListProcessor.CheckActionSynonyms(PlayerInputSplittedString[0]);

        }

    }
}
