using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
