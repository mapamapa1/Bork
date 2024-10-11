using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes.item_subclasses
{
    public class Bubblegum : Item
    {
        public int PiecesOfGumAmount;

        public Bubblegum()
        {
            PiecesOfGumAmount = 5;
        }


        public void Use()
        {
            if (PiecesOfGumAmount > 0)
            {



            }
            else Console.WriteLine("This pack of bubblegum is empty.");
        }
    }


}



