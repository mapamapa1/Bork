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
        public string InEnvironmentDescription { get; set; }
        public bool IsVisible { get; set; }
        public bool CanBeTaken { get; set; }
        public string CantBeTakenDescription { get; set; }

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

                    Console.WriteLine("You can't use that here.");
                    break;


                case ("Stick"):
                    Console.WriteLine("You hit yourself in the head with the stick.");
                    break;

                case ("Rusty key"):
                    Console.WriteLine("By itself it doesn't do much.");
                    break;

                case ("Golden key"):
                    Console.WriteLine("By itself it doesn't do much.");
                    break;

            }
        }

        //items on items
        public void Combination(Item item2, List<Item> inventory, List<Item> locationItems)
        {
            if (((Name.ToLower() == "chewed gum") && (item2.Name.ToLower() == "stick"))|| ((Name.ToLower() == "stick") && (item2.Name.ToLower() == "chewed gum")))
            {
                inventory.Remove(this);
                inventory.Remove(item2);
                locationItems.Remove(this);
                locationItems.Remove(item2);

                Item newItem = new Item();
                Console.WriteLine("You put the chewed gum at the end of the stick. You have created an abomination.");
                newItem.Name = "Chewing gum stick";
                newItem.Description = "A stick with a piece of chewed gum attached to the end of it.";
                inventory.Add(newItem);
                return;

            }
            if (((Name.ToLower() == "chewing gum stick") && (item2.Name.ToLower() == "golden key")) || ((Name.ToLower() == "golden key") && (item2.Name.ToLower() == "chewed gum on a stick")))
            {
                inventory.Remove(this);
                inventory.Remove(item2);
                locationItems.Remove(this);
                locationItems.Remove(item2);

                Item newItem = new Item();
                Console.WriteLine("You reach down the drain with your new abomination and press it against the key.\nThe key attaches itself to the gum and you pull it out.\n" +
                    "You put the key in your inventory.");
                newItem.Name = "Golden key";
                newItem.Description = "A shining golden key that probably fits an equally shining door.";
                inventory.Add(newItem);
                return;
            }
            if (((Name.ToLower() == "golden key") && (item2.Name.ToLower() == "stick")) || ((Name.ToLower() == "stick") && (item2.Name.ToLower() == "golden key")))
            {
                Console.WriteLine("You can reach the key with the stick,\nbut if you tried to touch it you would propably just push it down further.");
                return;
            }

            Console.WriteLine("That doesn't work.");
        }

        //keys on doors
        public void Combination(RoomExit roomExit)
        {

            if ((Name.ToLower() == "rusty key") && (roomExit.Name.ToLower() == "iron door"))
            {
                roomExit.IsLocked = false;
                Console.WriteLine($"You unlock the {roomExit.Name.ToLower()}.");

                return;
            }
            
            if ((Name.ToLower() == "golden key") && (roomExit.Name.ToLower() == "golden door"))
            {
                roomExit.IsLocked = false;
                Console.WriteLine($"You unlock the {roomExit.Name.ToLower()}.");

                return;

            }
            
            Console.WriteLine("That doesn't work.");           
        }
    }
}
