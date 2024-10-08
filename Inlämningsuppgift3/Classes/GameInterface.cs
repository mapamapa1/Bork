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

        public GameInterface()
        {
            GameRunning = true;

        }
        
        
        
        public void Game()
        {
            string playerInput;
            string[] playerInputSplittedString;
            
            Player player = new Player();
            WordListProcessor wordProcessor = new WordListProcessor();
            List<Room> rooms = new List<Room>();
            
            
            rooms = Repository.LoadRooms();
            
            
            player.Location = rooms[0];
            bool newLocation = true;

            while (GameRunning)
            {

                if (newLocation)
                {
                    Console.WriteLine(player.Location.Description);
                    Console.WriteLine(player.Location.RoomContainsDescription());
                    newLocation = false;
                }

                Console.WriteLine();
                playerInput = Console.ReadLine();

                playerInputSplittedString = playerInput.Split(" ");
                playerInputSplittedString[0] = wordProcessor.CheckActionSynonyms(playerInputSplittedString[0]);


                if (playerInput == null) {

                    playerInput = "";

                }

                if (playerInput == "look")
                {
                    Console.WriteLine(player.Location.Description);

                }

                if (playerInput == "inventory" || playerInput == "open inventory")
                {
                    string itemList = "";

                    if (player.Inventory.Count == 0)
                    {
                        Console.WriteLine("You have no items.");

                    }
                    else
                    {
                        foreach (Items item in player.Inventory)
                        {
                            itemList += item.Name;

                        }
                        Console.WriteLine($" Inventory: {itemList}");
                    }

                }
                if (playerInputSplittedString[0].ToLower() == "look" && playerInputSplittedString.Length > 1)
                {
                    foreach (Items item in player.Location.Items)
                    {
                        if (item.Name.ToLower() == playerInputSplittedString[1].ToLower())
                        {
                            Console.WriteLine(item.Description);
                            break;
                        }
                    }
                    foreach (Items item in player.Inventory)
                    {
                        if (item.Name.ToLower() == playerInputSplittedString[1].ToLower())
                        {
                            Console.WriteLine($"{item.Description} [in inventory]");
                            break;
                        }
                    }

                }
                if (playerInput.ToLower() == "inspect")
                {
                        Console.WriteLine(player.Location.Description);

                }
                
                if (playerInputSplittedString[0].ToLower() == "take" && playerInputSplittedString.Length > 1)
                {
                    foreach (Items item in player.Location.Items)
                    {
                        if (item.Name.ToLower() == playerInputSplittedString[1].ToLower())
                        {
                            player.Location.Items.Remove(item);
                            player.Inventory.Add(item);
                            Console.WriteLine($"You pick up the {item.Name.ToLower()}.");
                            break;
                        }
                    }

                }

                if (playerInputSplittedString[0].ToLower() == "drop" && playerInputSplittedString.Length > 1)
                {

                    foreach (Items item in player.Inventory)
                    {
                        if (item.Name.ToLower() == playerInputSplittedString[1].ToLower())
                        {
                            player.Location.Items.Add(item);
                            player.Inventory.Remove(item);
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


    }
}
