using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public class WordListProcessor
    {
        public Dictionary<string, string[]> WordList;

        public WordListProcessor()
        {
            WordList = Repository.LoadWordList();
        }

        public string CheckActionSynonyms(string word)
        {

            //common words check
            foreach (string key in WordList.Keys)
            {
                if (word == key)
                {
                    return word;

                }

            }

            //synonyms check
            foreach (KeyValuePair<string,string[]> pair in WordList)
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



    }
}
