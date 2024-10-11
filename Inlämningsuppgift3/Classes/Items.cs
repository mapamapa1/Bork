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
     
    }


}
