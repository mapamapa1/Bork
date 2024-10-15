namespace Inlämningsuppgift3.Classes
{
    public class RoomExit : GameObject
    {
        public string Direction { get; set; }

        public string Connection { get; set; }
        public bool IsLocked { get; set; }

        public bool IsClosed { get; set; }

        public RoomExit()
        {
                
        }
    }
}
