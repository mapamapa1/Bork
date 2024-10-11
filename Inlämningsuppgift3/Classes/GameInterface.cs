﻿using System;
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

                switch (InputProcessor.PlayerInputSplittedString[0])
                {
                    case (null):
                    case (""):

                        Console.WriteLine("Write a command.");
                        break;

                    case ("look"):

                        if (InputProcessor.PlayerInputSplittedString.Length == 1)
                        {

                            Console.WriteLine(Player.Location.Description);
                            Console.WriteLine(Player.Location.RoomContainsDescription());
                            break;
                        }
                        else
                        {

                            foreach (Item item in Player.Location.Items)
                            {
                                if (item.Name.ToLower() == InputProcessor.SecondWordToEnd.ToLower())
                                {
                                    Console.WriteLine(item.Description);

                                }
                            }
                            foreach (Item item in Player.Inventory)
                            {
                                if (item.Name.ToLower() == InputProcessor.SecondWordToEnd.ToLower())
                                {
                                    Console.WriteLine($"{item.Description} [in inventory]");

                                }
                            }
                        }
                        break;



                    case ("inventory"):
                    case ("open inventory"):
                        Player.ShowInventory();
                        break;

                    case ("go"):

                        newLocation = Player.Move(InputProcessor.PlayerInputSplittedString[1], Rooms);
                        break;

                    case ("use"):
                        Player.Use(InputProcessor.SecondWordToEnd, InputProcessor.PlayerInputSplittedString);

                        break;

                    case ("take"):
                        Player.Take(InputProcessor.SecondWordToEnd, InputProcessor.PlayerInputSplittedString);

                        break;

                    case ("drop"):
                        Player.Drop(InputProcessor.SecondWordToEnd, InputProcessor.PlayerInputSplittedString);

                        break;

                    default:
                        Console.WriteLine($"I don't know the word \"{InputProcessor.PlayerInputSplittedString[0]}\".");
                        break;


                }

            }

        }

    }
}
