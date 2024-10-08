using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public class RoomExits
    {
        public Dictionary<string, string> ConnectingRooms { get; set; }

        public bool IsDoor { get; set; }

        public bool IsOpen { get; set; }
        public bool IsLocked { get; set; }


        public RoomExits()
        {
                
        }



    }
}
