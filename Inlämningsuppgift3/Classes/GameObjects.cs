using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public class GameObject
    {
        public string Name { get; set; }
        public string Description { get; set; }       


        public void Inspect()
        {
            Console.WriteLine(Description);


        }

    }


}
