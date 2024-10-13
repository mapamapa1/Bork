using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{   

    public class RoomObjectOfInterest : GameObject
    {
        public bool HasBeenInspected { get; set; }

        public RoomObjectOfInterest()
        {
            HasBeenInspected = false;
        }
    }
}
