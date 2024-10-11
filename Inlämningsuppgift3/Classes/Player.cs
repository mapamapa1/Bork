﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public class Player
    {
        public string Name { get; set; }
        public List<Item> Inventory { get; set; } = new List<Item>();

        public Room Location { get; set; }

        public Player()
        {
               //loadStartingItems()
        }
        public void DropItem(Item item)
        {
            Location.Items.Add(item);
            Inventory.Remove(item);
            
        }

        public void ShowInventory()
        {
            if (Inventory.Count == 0)
            {
                Console.WriteLine("You have no items in your inventory.");
            }
            else
            {

                Console.WriteLine($"Inventory:");

                foreach (Item item in Inventory)
                {
                    Console.WriteLine(item.Name);
                }
            }
        }

        public bool Move(string direction, List <Room> roomList)
        {

            bool hasMoved = false;

            foreach(RoomExit roomexit in Location.RoomExits)
            {
                string newLocation = "";

                if (direction.ToLower() == roomexit.Direction.ToLower())
                {
                    newLocation = roomexit.Connection;

                }

                foreach(Room room in roomList)
                {
                    if (newLocation == room.Name)
                    {
                        Location = room;
                        hasMoved = true;

                    }
                }

            }

            if (hasMoved)
            {
                return true;

            }
            else
            {
                Console.WriteLine("You can't move in that direction.");
                return false;
            }

        }

        public void Use()
        {
            
        }

        public void Take(string takenObject, string[] splittedString)
        {
            if (splittedString.Length > 1)
            {
                foreach (Item item in Location.Items)
                {
                    if (item.Name.ToLower() == takenObject.ToLower())
                    {
                        Location.Items.Remove(item);
                        Inventory.Add(item);
                        Console.WriteLine($"You pick up the {item.Name.ToLower()}.");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("What do you want to take?");
            }
        }

        public void Drop(string droppedObject, string[] splittedString)
        {
            if (splittedString.Length > 1)
            {

                foreach (Item item in Inventory)
                {
                    if (item.Name.ToLower() == droppedObject.ToLower())
                    {
                        Location.Items.Add(item);
                        Inventory.Remove(item);
                        Console.WriteLine($"You drop the {item.Name.ToLower()}.");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("What do you want to drop?");
            }
        }
    }
}
