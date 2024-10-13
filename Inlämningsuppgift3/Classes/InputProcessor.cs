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

        public string FirstItemString { get; set; }

        public string SecondItemString { get; set; }

        private Player _player;

        public WordListProcessor WordListProcessor { get; set; }

        public InputProcessor(Player player)
        {
            WordListProcessor = new WordListProcessor();
            _player = player;


        }
        public void Process(string playerInput)
        {
 
            PlayerInput = playerInput.ToLower();
            PlayerInputSplittedString = PlayerInput.Split(" ");

            if (PlayerInputSplittedString.Length > 1)
            {
                UpdateProperties();
                CheckAndReplacePrepositions();
                ReplaceItemSynonyms();
                if (SecondWordToEnd.Contains("on"))
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

        public void ReplaceItemSynonyms()
        {
            int synonymsNum = 0;
            string foundSynonym = "";
            string itemName = "";
            List<string> synonymList = new List<string>();
            List<string> itemNameList = new List<string>();

            foreach (Item item in _player.Inventory)
            {
                foreach (string synonym in item.Synonyms)
                {
                    for (int i= 0; i < PlayerInputSplittedString.Length; i++)
                    {
                        if (PlayerInputSplittedString[i].Contains(synonym))
                        {
                            synonymsNum += 1;
                            synonymList.Add(synonym);
                            foundSynonym = synonym;
                            itemName = item.Name;

                        }

                    }
                    
                }           
            }
            if (synonymsNum == 1)
            {
                PlayerInput = PlayerInput.Replace(foundSynonym, itemName);
                UpdateProperties();

            }
            else if (synonymsNum > 1)
            {
                bool doubleFound = false;

                foreach(string string1 in synonymList)
                {
                    foreach(string string2 in synonymList)
                    {
                        if(string1 == string2)
                        {
                            foundSynonym = string1;
                            doubleFound = true;
                            break;

                        }

                    }


                }

                if (!doubleFound)
                {
                    Console.WriteLine($"Which {foundSynonym}?");

                }
                else
                {
                    foreach (string synonym in synonymList)
                    {
                        foreach (string item in itemNameList)
                        {

                            PlayerInput.Replace(synonym, item);
    
                        }
                    }
                }

                //compare synonyms in synonymslist for doubles


            }
            
            //foreach (Item item in _player.Location)
            //{
            //    foreach (string synonym in item.Synonyms)
            //    {
            //        if (PlayerInput.Contains(synonym))
            //        {
            //            PlayerInput.Replace(synonym, item.Name);

            //        }

            //    }
            //}

        }

        public void UpdateProperties()
        {
            PlayerInput = PlayerInput.ToLower();
            PlayerInputSplittedString = PlayerInput.Split(" ");
            IndexOfFirstSpace = PlayerInput.IndexOf(' ');
            FirstWord = PlayerInput.Substring(0, IndexOfFirstSpace);
            SecondWordToEnd = PlayerInput.Substring(IndexOfFirstSpace + 1);

        }

        public void PrintProperties()
        {
            Console.WriteLine("PlayerInput: " + PlayerInput);
            Console.WriteLine("PlayerInputSplittedString: " + string.Join(", ", PlayerInputSplittedString));
            Console.WriteLine("IndexOfFirstSpace: " + IndexOfFirstSpace);
            Console.WriteLine("FirstWord: " + FirstWord);
            Console.WriteLine("SecondWordToEnd: " + SecondWordToEnd);
            Console.WriteLine("FirstItemString: " + FirstItemString);
            Console.WriteLine("SecondItemString: " + SecondItemString);

        }
    }
}
