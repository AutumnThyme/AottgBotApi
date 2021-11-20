using System.Collections.Generic;

namespace AottgBotApi.Models
{
    public class AottgRoomInfo
    {
        public string Name { get; set; }
        public string Map { get; set; }
        public string Difficulty { get; set; }
        public int Time { get; set; }
        public string Daylight { get; set; }
        public string EncryptedPassword { get; set; }
        public int RandomNumber { get; set; }
    }
}
