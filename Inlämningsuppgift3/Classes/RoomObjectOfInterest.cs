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
