using System;
using System.Collections;
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

        public void Look(string secondWordToEnd, string[] splittedString)
        {
            if (splittedString.Length == 1)
            {
                Console.WriteLine(Location.Description);
                Console.WriteLine(Location.RoomContainsDescription());
                Console.WriteLine(Location.RoomExitsDescription());
            }
            else
            {
                foreach (Item item in Location.Items)
                {
                    if (item.Name.ToLower() == secondWordToEnd.ToLower())
                    {
                        Console.WriteLine(item.Description);
                    }
                }

                foreach (Item item in Inventory)
                {
                    if (item.Name.ToLower() == secondWordToEnd.ToLower())
                    {
                        Console.WriteLine($"{item.Description} [in inventory]");
                    }
                }
            }
        }

        public bool Move(string direction, List<Room> roomList)
        {
            bool hasMoved = false;
            string newLocation = "";

            foreach (RoomExit roomexit in Location.RoomExits)
            {
                

                if (direction.ToLower() == roomexit.Direction.ToLower())
                {
                    if (roomexit.IsLocked == false)
                    {
                        newLocation = roomexit.Connection;
                    }
                    else
                    {
                        Console.WriteLine("There is a locked door blocking your path.");
                        return false;
                    }

                }

                foreach (Room room in roomList)
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

        public void Use(string playerInput, string secondWordToEnd, string[] splittedString)
        {
            if (splittedString.Length > 1)
            {
                if (secondWordToEnd.Contains("on"))
                {
                    
                    int indexBeforeOn = secondWordToEnd.IndexOf("on");
                    string firstItem = secondWordToEnd.Substring(0, indexBeforeOn-1);
                    string secondItem = secondWordToEnd.Substring(indexBeforeOn + "on".Length+1);
                    
                    

                    Item? foundObject1 = Location.Items.FirstOrDefault(i => i.Name.ToLower() == firstItem) ??
                                            Inventory.FirstOrDefault(i => i.Name.ToLower() == firstItem);

                    Item? foundObject2 = Location.Items.FirstOrDefault(i => i.Name.ToLower() == secondItem) ??
                                            Inventory.FirstOrDefault(i => i.Name.ToLower() == secondItem);

                    if (foundObject1 != null && foundObject2 == null)
                    {
                        RoomExit? foundRoomExit = Location.RoomExits.FirstOrDefault(i => i.Name.ToLower() == secondItem);

                        if (foundRoomExit != null)
                        {
                            foundObject1.Combination(foundRoomExit);
                        }
                        else
                        {
                            Console.WriteLine($"I don't know what '{secondItem}' is.");
                        }
                    }
                    else if(foundObject1 != null && foundObject2 != null)
                    {
                        foundObject1.Combination(foundObject2);
                    }
                    else
                    {
                        Console.WriteLine($"I don't know what '{firstItem}' is.");
                    }
                }
                else
                {
                    Item? foundObject = Location.Items.FirstOrDefault(i => i.Name.ToLower() == secondWordToEnd) ??
                                            Inventory.FirstOrDefault(i => i.Name.ToLower() == secondWordToEnd);

                    if (foundObject != null)
                    {
                        foundObject.Use(splittedString, Inventory);
                    }
                    else
                    {
                        Console.WriteLine($"I don't know what '{secondWordToEnd}' is.");
                    }
                }
            }
            else
            {
                Console.WriteLine("What do you want to 'use'?");
            }
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
                Console.WriteLine("What do you want to 'take'?");
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
                Console.WriteLine("What do you want to 'drop'?");
            }
        }
    }
}
