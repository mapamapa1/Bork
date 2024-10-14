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
                            Console.WriteLine(item.InEnvironmentDescription);
                        }
                        return;
                    }
                }
                Console.WriteLine($"What is '{secondWordToEnd}'?");
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
                        Console.WriteLine($"There is a {roomexit.Name.ToLower()} blocking your path.");
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
            if (direction != "north" && direction != "south" && direction != "west" && direction != "east")
            {
                Console.WriteLine($"'{direction}' is not a valid direction. Directions: 'north', 'south', 'east', 'west'.");
                return false;
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
                        if (secondItem == "door")
                        {
                            if (Location.RoomExits.Count == 1)
                            {
                                foundObject1.Combination(Location.RoomExits[0]);
                                return;
                            }
                            else
                            {
                                Console.WriteLine("Which door?");
                                return;
                            }
                        }

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
            int amountOfRoomExits = Location.RoomExits.Count;

            if (secondWordToEnd == "")
            {
                Console.WriteLine("What do you want to 'open'?");
                return;
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

                    if(amountOfRoomExits == 1)
                    {

                        Console.WriteLine("The door is already open.");
                        return;
                    }
                    
                    Console.WriteLine("There is nothing to open here.");
                    return;

                case 1:


                    if (amountOfRoomExits > 1 && secondWordToEnd == "door")
                    {
                        Console.WriteLine("Which door do you want to open?");
                        return;
                    }


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
                                    return;
                                }
                                else
                                {
                                    Console.WriteLine("The door is already open.");
                                    return;
                                }
                            }
                            else
                            {
                                Console.WriteLine($"The {roomExit.Name.ToLower()} is locked.");
                                return;
                            }
                        }

                    }
                    Console.WriteLine($"What is '{secondWordToEnd}'?");
                    return;

                default:
                    if (secondWordToEnd == "door")
                    {
                        Console.WriteLine("Which door do you want to open?");
                        return;
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
                                        return;
                                    }
                                    else
                                    {
                                        Console.WriteLine("The door is already open.");
                                        return;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"The {roomExit.Name.ToLower()} is locked.");
                                    return;
                                }
                            }
                        }
                    }
                    Console.WriteLine($"What is '{secondWordToEnd}'?");
                    return;
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
                            item.InEnvironmentDescription = null;
                            Console.WriteLine($"You pick up the {item.Name.ToLower()}.");
                            return;
                        }
                        else
                        {
                            Console.WriteLine(item.CantBeTakenDescription);
                            return;
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
                        return; 
                    }

                }
                Console.WriteLine($"What '{droppedObject}'?");
            }
            else
            {
                Console.WriteLine("What do you want to 'drop'?");
            }
        }
    }
}
