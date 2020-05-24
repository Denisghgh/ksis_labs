using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    [Serializable]
    public class RoomInfo
    {
        public string RoomName { get; private set; }
        public int RoomId { get; private set; }

        public RoomInfo(string roomName, int roomId)
        {
            RoomName = roomName;
            RoomId = roomId;
        }
    }
}
