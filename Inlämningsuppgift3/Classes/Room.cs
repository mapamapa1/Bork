using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public class Room : GameObject
    {
        public List<Items> Items { get; set; } = new List<Items>();
        public List<RoomExits> RoomExits { get; set; }

        public List<RoomObjectOfInterest> RoomObjectOfInterest { get; set; } = new List<RoomObjectOfInterest>();

        public Room()
        {
            



        }



        public string RoomContainsDescription()
        {
            string returnString = "";

           foreach(Items item in Items)
           {
                returnString += ($"There is a {item.Name.ToLower()} on the floor.\n");

           }

           

            return returnString;

        }





    }
}
