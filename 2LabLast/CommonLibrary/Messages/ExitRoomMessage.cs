using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    [Serializable]
    public class ExitRoomMessage : Messages
    {
        public int RoomId { get; }
        public int ClientId { get; }

        public ExitRoomMessage(DateTime dateTime, IPAddress senderIp, int senderPort, int roomId, int clientId) : base(dateTime, senderIp, senderPort)
        {
            RoomId = roomId;
            ClientId = clientId;
        }
    }
}
