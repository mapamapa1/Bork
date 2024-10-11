using System;
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
        public List<Items> Inventory { get; set; } = new List<Items>();

        public Room Location { get; set; }

        public Player()
        {
               //loadStartingItems()
        }
        public void DropItem(Items item)
        {
            Location.Items.Add(item);
            Inventory.Remove(item);
            
        }


        public void ShowInventory()
        {


            if (Inventory.Count == 0)
            {
                Console.WriteLine("You have no items in your inventory");
            }
            else
            {

                Console.WriteLine($"Inventory:");

                foreach (Items item in Inventory)
                {
                    Console.WriteLine(item.Name);
                }

            }

        }

        public bool Move(string direction, List <Room> roomList)
        {

            bool hasMoved = false;

            foreach(KeyValuePair<string, string> pair in Location.RoomExits)
            {
                string newLocation = "";

                if (direction.ToLower() == pair.Key.ToLower())
                {
                    newLocation = pair.Value;

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
    }
}
