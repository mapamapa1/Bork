using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public class Item : GameObject
    {

        public bool IsContainer { get; set; }
        public List<Item> Container { get; set; } = new List<Item>();

        public string[] Synonyms { get; set; }

        public string ItemInEnvironmentDescription { get; set; }

        //public int Amount { get; set; }

        public Item()
        {
       

        }

        public void Use(string[] splittedString, List<Item> inventory)
        {
            switch (Name)
            {

                case ("Pack of bubblegum"):
                    
                    Console.WriteLine("You take a piece of gum and chew it for a while. You put the now chewed gum in your inventory.");                
                    Item chewedGum = new Item();
                    chewedGum.Name = "Chewed gum";
                    chewedGum.Description = "A piece of chewed gum, the taste is long gone.";
                    inventory.Add(chewedGum);

                    break;

                case ("Chewed gum"):

                    Console.WriteLine("You can't use that here");
                    break;


                case ("Stick"):
                    Console.WriteLine("You hit yourself in the head with the stick.");
                    break;

                case ("Rusty key"):
                    Console.WriteLine("By itself it doesn't do much.");
                    break;

            }

        }

        //items on items
        public void Combination(Item item2)
        {
        
        }

        //keys on doors
        public void Combination(RoomExit roomExit)
        {
            if ((Name.ToLower() == "rusty key") && (roomExit.Name.ToLower() == "dungeon door"))
            {
                roomExit.IsLocked = false;
                Console.WriteLine("You unlock the door and open it.");

            }
        }

    }


}
