using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public static class Repository
    {


        public static List<Room> LoadRooms()
        {
            List<Room> rooms = new List<Room>();

            using (StreamReader reader = new StreamReader("D:\\repos\\Inlämningsuppgift3\\Inlämningsuppgift3\\Rooms.json"))
            { 
                string stringRooms = reader.ReadToEnd();
                rooms = JsonSerializer.Deserialize<List<Room>>(stringRooms);
            }
            return rooms;
        }

        public static Dictionary<string, string[]> LoadActionWordList()
        {
            Dictionary<string, string[]> wordList = new Dictionary<string, string[]>();

            using (StreamReader reader = new StreamReader("D:\\repos\\Inlämningsuppgift3\\Inlämningsuppgift3\\WordList_ActionSynonyms.json"))
            {
                string stringWordList = reader.ReadToEnd();
                wordList = JsonSerializer.Deserialize<Dictionary<string, string[]>>(stringWordList);

            }

            return wordList;

        }

        public static string[] LoadPrepositionWordList()
        {
            string[] wordList;

            using (StreamReader reader = new StreamReader("D:\\repos\\Inlämningsuppgift3\\Inlämningsuppgift3\\WordList_Prepositions.json"))
            {
                string stringWordList = reader.ReadToEnd();
                wordList = JsonSerializer.Deserialize<string[]>(stringWordList);

            }


            return wordList;
        }

    }
}
