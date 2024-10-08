using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public class Items : GameObject
    {

        public bool IsContainer { get; set; }
        public List<Items> Container { get; set; } = new List<Items>();

        public string ItemInEnvironmentDescription { get; set; }

        //public int Amount { get; set; }

        public Items()
        {
       

        }
     
    }


}
