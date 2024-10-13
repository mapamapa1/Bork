using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public class WordListProcessor
    {
        public Dictionary<string, string[]> ActionWordList;       
        public string[] PrepositionWordList;

        public WordListProcessor()
        {
            ActionWordList = Repository.LoadActionWordList();
            PrepositionWordList = Repository.LoadPrepositionWordList();
        }

        public string CheckActionSynonyms(string word)
        {

            //common words check
            foreach (string key in ActionWordList.Keys)
            {
                if (word == key)
                {
                    return word;

                }
            }

            //synonyms check
            foreach (KeyValuePair<string,string[]> pair in ActionWordList)
            {
                foreach(string synonym in pair.Value)
                {
                    if (word == synonym)
                    {
                        return pair.Key;

                    }          
                }
            }
            return word;
        }

        public string[] CheckPrepositionSynonyms(string[] playerInputSplittedString)
        {
            string[] returnArray = playerInputSplittedString;

            for(int i = 0; i< playerInputSplittedString.Length; i++)
            {
                for(int i2 = 0; i2 < PrepositionWordList.Length; i2++)
                {
                    if (playerInputSplittedString[i] == PrepositionWordList[i2])
                    {
                        returnArray[i] = "on";

                    }
                }
            }

            return returnArray;
        }
    }
}
