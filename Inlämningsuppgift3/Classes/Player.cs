using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
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

        public void Look(string playerInput, string secondWordToEnd, string[] splittedString)
        {
            if ((splittedString.Length == 1 && (splittedString[0] == "look")) || (playerInput == "look around"))
            {
                Console.WriteLine(Location.RoomDescription());
                Console.WriteLine(Location.RoomContainsDescription());
                Console.WriteLine(Location.RoomExitsDescription());
            }
            else
            {
                foreach (Item item in Location.Items)
                {
                    if ((item.Name.ToLower() == secondWordToEnd.ToLower()) && (item.IsVisible = true))
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

                foreach (RoomExit roomExit in Location.RoomExits)
                {
                    if (roomExit.Name.ToLower() == secondWordToEnd.ToLower())
                    {
                        Console.WriteLine($"{roomExit.Description}.");
                    }
                }

                foreach (RoomObjectOfInterest objectOfInterest in Location.RoomObjectOfInterest)
                {
                    if (objectOfInterest.Name.ToLower() == secondWordToEnd.ToLower())
                    {
                        Console.WriteLine(objectOfInterest.Description);
                        objectOfInterest.HasBeenInspected = true;

                        foreach (Item item in Location.Items)
                        {
                            item.IsVisible = true;
                        }
                    }
                }
            }
        }

        public bool Move(string[] playerinputSplittedString, string direction, List<Room> roomList)
        {
            bool hasMoved = false;
            string newLocation = "";

            if (playerinputSplittedString.Length == 1)
            {
                Console.WriteLine("Where do you want to 'move'?");
                return false;
            }

            foreach (RoomExit roomexit in Location.RoomExits)
            {
                if (direction.ToLower() == roomexit.Direction.ToLower())
                {
                    if (roomexit.IsClosed == false)
                    {
                        newLocation = roomexit.Connection;
                    }
                    else
                    {
                        Console.WriteLine("There is a door blocking your path.");
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

        public void Use(string playerInput, string secondWordToEnd, string[] splittedString, string firstItem, string secondItem)
        {
            if (splittedString.Length > 1)
            {
                if (secondWordToEnd.Contains(" on "))
                {
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
                    else if (foundObject1 != null && foundObject2 != null)
                    {
                        foundObject1.Combination(foundObject2, Inventory, Location.Items);
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

        public void Open(string secondWordToEnd, string[] splittedString)
        {
            if (secondWordToEnd == "")
            {
                Console.WriteLine("What do you want to 'open'?");
            }

            int amountOfClosedExits = 0;
            foreach (RoomExit roomExit in Location.RoomExits)
            {
                if (roomExit.IsClosed == true)
                {
                    amountOfClosedExits += 1;
                }
            }

            switch (amountOfClosedExits)
            {
                case 0:
                    Console.WriteLine("There is nothing to open here");
                    break;

                case 1:
                    foreach (RoomExit roomExit in Location.RoomExits)
                    {
                        if (secondWordToEnd == roomExit.Name.ToLower() || secondWordToEnd == "door")
                        {
                            if (!roomExit.IsLocked)
                            {
                                if (roomExit.IsClosed)
                                {
                                    roomExit.IsClosed = false;
                                    Console.WriteLine($"You open the {roomExit.Name.ToLower()}.");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("The door is already open.");
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine($"The {roomExit.Name.ToLower()} is locked.");
                                break;
                            }
                        }
                    }
                    break;

                default:
                    if (secondWordToEnd == "door")
                    {
                        Console.WriteLine("Which door do you want to open?");
                        break;
                    }
                    else
                    {
                        foreach (RoomExit roomExit in Location.RoomExits)
                        {
                            if (secondWordToEnd == roomExit.Name.ToLower())
                            {
                                if (!roomExit.IsLocked)
                                {
                                    if (roomExit.IsClosed)
                                    {
                                        roomExit.IsClosed = false;
                                        Console.WriteLine($"You open the {roomExit.Name.ToLower()}.");
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("The door is already open.");
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"The {roomExit.Name.ToLower()} is locked.");
                                    break;
                                }
                            }
                        }
                    }
                    break;
            }
        }

        public void Take(string takenObject, string[] splittedString)
        {
            if (splittedString.Length > 1)
            {
                foreach (Item item in Location.Items)
                {
                    if ((item.Name.ToLower() == takenObject.ToLower()) && item.IsVisible)
                    {
                        if (item.CanBeTaken)
                        {
                            Location.Items.Remove(item);
                            Inventory.Add(item);
                            Console.WriteLine($"You pick up the {item.Name.ToLower()}.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine(item.CantBeTakenDescription);
                            break;
                        }
                    }
                }
                Console.WriteLine($"What '{takenObject}'?");
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
