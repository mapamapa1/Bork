using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public class GameInterface
    {
        public bool GameRunning { get; set; }
        public Player Player { get; set; }

        public List<Room> Rooms { get; set; }

        public InputProcessor InputProcessor { get; set; }

        public WordListProcessor WordListProcessor { get; set; }


        public GameInterface()
     
        {
            GameRunning = true;
            Player = new Player();
            Rooms = new List<Room>();
            Rooms = Repository.LoadRooms();
            InputProcessor = new InputProcessor();
            Player.Location = Rooms[0];

        }
        
        
        
        public void Game()
        {
            string playerInput;
            string[] playerInputSplittedString;
            int indexOfFirstSpace;
            string firstWord;
            string secondWordToEnd;                                                                                     
            bool newLocation = true;

            while (GameRunning)
            {

                if (newLocation)
                {
                    Console.WriteLine(Player.Location.Description);
                    Console.WriteLine(Player.Location.RoomContainsDescription());
                    newLocation = false;
                }

                Console.WriteLine();
                playerInput = Console.ReadLine();
                InputProcessor.Process(playerInput);

                playerInputSplittedString = playerInput.Split(" ");

                

                if (playerInputSplittedString.Length > 1)
                {
                    indexOfFirstSpace = playerInput.IndexOf(' ');
                    firstWord = playerInput.Substring(0, indexOfFirstSpace);
                    secondWordToEnd = playerInput.Substring(indexOfFirstSpace + 1);

                }
                else
                {
                    indexOfFirstSpace = -1;
                    firstWord = null;
                    secondWordToEnd = null;
                }

                playerInputSplittedString[0] = WordListProcessor.CheckActionSynonyms(playerInputSplittedString[0]);

                switch (playerInputSplittedString[0])
                {
                    case (null):
                    case (""):

                        Console.WriteLine("Type a command:");
                        break;

                    case ("look"):

                        if (playerInputSplittedString.Length == 0)
                        {

                            Console.WriteLine(Player.Location.Description);
                            Console.WriteLine(Player.Location.RoomContainsDescription());
                            break;
                        }
                        else
                        {

                            foreach (Items item in Player.Location.Items)
                            {
                                if (item.Name.ToLower() == secondWordToEnd.ToLower())
                                {
                                    Console.WriteLine(item.Description);
                                    
                                }
                            }
                            foreach (Items item in Player.Inventory)
                            {
                                if (item.Name.ToLower() == secondWordToEnd.ToLower())
                                {
                                    Console.WriteLine($"{item.Description} [in inventory]");
                                    
                                }
                            }
                        }
                        break;
                        


                    case ("inventory"):
                    case ("open inventory"):
                        ShowInventory(Player);
                        break;

                    //case ("go"):

                        



                }

                
                
                if (playerInputSplittedString[0].ToLower() == "take" && playerInputSplittedString.Length > 1)
                {
                    foreach (Items item in Player.Location.Items)
                    {
                        if (item.Name.ToLower() == secondWordToEnd.ToLower())
                        {
                            Player.Location.Items.Remove(item);
                            Player.Inventory.Add(item);
                            Console.WriteLine($"You pick up the {item.Name.ToLower()}.");
                            break;
                        }
                    }

                }

                if (playerInputSplittedString[0].ToLower() == "drop" && playerInputSplittedString.Length > 1)
                {

                    foreach (Items item in Player.Inventory)
                    {
                        if (item.Name.ToLower() == secondWordToEnd.ToLower())
                        {
                            Player.Location.Items.Add(item);
                            Player.Inventory.Remove(item);
                            Console.WriteLine($"You drop the {item.Name.ToLower()}.");

                            break;
                        }
                    }



                }


            }



        }
        
        public void InventoryOpened()
        {



        }
        
        public void InitializeGame()
        {


        }

        public void ShowInventory(Player player)
        {
           

            if (player.Inventory.Count == 0)
            {
                Console.WriteLine("You have no items in your inventory");
            }
            else
            {

                Console.WriteLine($"Inventory:");

                foreach (Items item in Player.Inventory)
                {
                    Console.WriteLine(item.Name);
                }

            }

        }

        public void Look(Player player)
        {





        }

    }
}
