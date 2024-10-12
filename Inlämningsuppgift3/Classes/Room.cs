using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public class Room : GameObject
    {
        public List<Item> Items { get; set; }
        public List<RoomExit> RoomExits { get; set; }

        public List<RoomObjectOfInterest> RoomObjectOfInterest { get; set; }

        public Room()
        {
            Items = new List<Item>();

            RoomExits = new List<RoomExit>();

            RoomObjectOfInterest = new List<RoomObjectOfInterest>();

        }



        public string RoomContainsDescription()
        {
            string returnString = "";

           foreach(Item item in Items)
           {
                returnString += ($"There is a {item.Name.ToLower()} on the floor.\n");

           }

           

            return returnString;

        }

        public string RoomExitsDescription()
        {
            string returnString = "";

            foreach (RoomExit roomExit in RoomExits)
            {
                string openClose = (roomExit.IsLocked) ? "closed" : "open";

                returnString += ($"On the {roomExit.Direction.ToLower()} wall there is a {roomExit.Name.ToLower()}. It is {openClose}.\n");

            }



            return returnString;

        }





    }
}
