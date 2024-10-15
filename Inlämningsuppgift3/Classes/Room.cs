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

        public string RoomDescription() {

            string description = $"\n----------------\n{Name}\n----------------\n{Description}\n";

            return description;
        }
           
        public string RoomContainsDescription()
        {
            string returnString = "";

            foreach (Item item in Items)
            {
                if (item.IsVisible == true)
                {

                    if (item.InEnvironmentDescription == null)
                    {
                        returnString += ($"There is a {item.Name.ToLower()} on the floor.\n");
                    }
                    else
                    {
                        returnString += (item.InEnvironmentDescription);

                    }
                }
            }
            return returnString;

        }

        public string RoomExitsDescription()
        {
            string returnString = "";

            foreach (RoomExit roomExit in RoomExits)
            {
                string openClose = (roomExit.IsClosed) ? "closed" : "open";

                returnString += ($"On the {roomExit.Direction.ToLower()} wall there is a {roomExit.Name.ToLower()}. It is {openClose}.\n");

            }
    
            return returnString;

        }
    }
}
