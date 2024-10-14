using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
            GameRunning = false;
            Player = new Player();
            Rooms = new List<Room>();
            Rooms = Repository.LoadRooms();           
            Player.Location = Rooms[0];
            InputProcessor = new InputProcessor(Player);

        }

        public void Game()
        {
            string playerInput;
            bool newLocation = true;
            int moveCounter = 0;

            Intro();

            while (GameRunning)
            {

                if (newLocation)
                {                   
                    Console.WriteLine(Player.Location.RoomDescription());
                    
                    newLocation = false;

                    if (Player.Location.Name.ToLower() == "final room")
                    {
                        Console.WriteLine($"Congratulations! You have beaten BORK! You finished the game in {moveCounter} moves.\n" +
                            $"Press any key to QUIT.");

                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                }

                Console.WriteLine();
                playerInput = Console.ReadLine();

                InputProcessor.Process(playerInput);

                switch (InputProcessor.PlayerInputSplittedString[0])
                {
                    case (null):
                    case (""):

                        Console.WriteLine("Write a command.");
                        break;

                    case ("look"):

                        Player.Look(InputProcessor.PlayerInput, InputProcessor.SecondWordToEnd, InputProcessor.PlayerInputSplittedString);
                        break;

                    case ("inventory"):
                        Player.ShowInventory();
                        break;

                    case ("go"):

                        newLocation = Player.Move(InputProcessor.PlayerInputSplittedString, InputProcessor.SecondWordToEnd, Rooms);
                        break;

                    case ("use"):
                        Player.Use(InputProcessor.PlayerInput, InputProcessor.SecondWordToEnd, InputProcessor.PlayerInputSplittedString, InputProcessor.FirstItemString, InputProcessor.SecondItemString);
                        
                        break;

                    case ("take"):
                        Player.Take(InputProcessor.SecondWordToEnd, InputProcessor.PlayerInputSplittedString);
                        

                        break;

                    case ("drop"):
                        Player.Drop(InputProcessor.SecondWordToEnd, InputProcessor.PlayerInputSplittedString);

                        break;

                    case ("open"):
                        Player.Open(InputProcessor.SecondWordToEnd, InputProcessor.PlayerInputSplittedString);
                        
                        break;

                    case ("help"):
                        
                        Help();
                        break;

                    default:
                        Console.WriteLine($"I don't know the word \"{InputProcessor.PlayerInputSplittedString[0]}\".");
                        break;


                }
                moveCounter++;
            }

        }
        public void Intro()
        {
            string playerInput;
            string title = "▀█████████▄   ▄██████▄     ▄████████    ▄█   ▄█▄ \r\n  ███    ███ ███    ███   ███    ███   ███ ▄███▀ \r\n  ███    ███ ███    ███   ███    ███   ███▐██▀   \r\n ▄███▄▄▄██▀  ███    ███  ▄███▄▄▄▄██▀  ▄█████▀    \r\n▀▀███▀▀▀██▄  ███    ███ ▀▀███▀▀▀▀▀   ▀▀█████▄    \r\n  ███    ██▄ ███    ███ ▀███████████   ███▐██▄   \r\n  ███    ███ ███    ███   ███    ███   ███ ▀███▄ \r\n▄█████████▀   ▀██████▀    ███    ███   ███   ▀█▀ \r\n                          ███    ███   ▀         ";


            Console.WriteLine($"{title}");
            while (true)
            {
              
                Console.WriteLine("\nWelcome to Bork! Type 'start' to start the game. If you need help type 'help'. Have fun!");


                playerInput = Console.ReadLine();

                switch (playerInput)
                {
                    case ("help"):
                        Help();
                        break;
                    case ("start"):
                        GameRunning = true;
                        return;
                        
                }
            
            }
            
        }
        public void Help()
        {
            Console.WriteLine("\nWrite commands in the console to advance in the game.\n" +
                "If you don't know what to do, type 'inspect' or 'look' to get a clearer picture of what's going on around you.\n" +
                "Type 'inventory' to see what you are currently carrying.\n" +
                "You can use items with other items with the syntax: use item1 on item2.\n" +
                "Good luck!");

        }
    }
}
