﻿using System;
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
        public string FirstItemString { get; set; }
        public string SecondItemString { get; set; }
        public WordListProcessor WordListProcessor { get; set; }

        private Player _player;

        public InputProcessor(Player player)
        {
            WordListProcessor = new WordListProcessor();
        }

        public void Process(string playerInput)
        {
            PlayerInput = playerInput.ToLower();
            PlayerInputSplittedString = PlayerInput.Split(" ");

            if (PlayerInputSplittedString.Length > 1)
            {
                UpdateProperties();
                CheckAndReplacePrepositions();

                if (SecondWordToEnd.Contains(" on "))
                {
                    GetItemsFromInput();
                }
            }
            else
            {
                IndexOfFirstSpace = -1;
                FirstWord = "";
                SecondWordToEnd = "";
            }

            if (PlayerInput.Contains("pick up"))
            {
                PlayerInput = PlayerInput.Replace("pick up", "pick");               
            }
            if (PlayerInput.Contains("the"))
            {
                PlayerInput = PlayerInput.Replace("the","");               
            }

            RemoveExtraSpaces();
            UpdateProperties();


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

        public void GetItemsFromInput()
        {
            int indexBeforeOn = SecondWordToEnd.IndexOf("on");
            FirstItemString = SecondWordToEnd.Substring(0, indexBeforeOn - 1);
            SecondItemString = SecondWordToEnd.Substring(indexBeforeOn + "on".Length + 1);
        }

        public void UpdateProperties()
        {
            PlayerInput = PlayerInput.ToLower();
            if (PlayerInputSplittedString.Length > 1)
            {
                PlayerInputSplittedString = PlayerInput.Split(" ");
                IndexOfFirstSpace = PlayerInput.IndexOf(' ');
                FirstWord = PlayerInput.Substring(0, IndexOfFirstSpace);
                SecondWordToEnd = PlayerInput.Substring(IndexOfFirstSpace + 1);
            }
            
        }

        public void RemoveExtraSpaces()
        {
            string[] splitString = PlayerInput.Split(" ");
            List<string> splitStringList = new List<string>(splitString);
            splitStringList.Remove("");
            PlayerInput = string.Join(" ", splitStringList);
         
        }
    }
}
