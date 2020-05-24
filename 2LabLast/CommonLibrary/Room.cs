using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class Room
    {
        public string RoomName { get; private set; }
        public List<int> RoomParticipants { get; private set; }

        public Room(string roomName, List<int> roomParticipants)
        {
            RoomName = roomName;
            RoomParticipants = roomParticipants;
        }
    }
}
