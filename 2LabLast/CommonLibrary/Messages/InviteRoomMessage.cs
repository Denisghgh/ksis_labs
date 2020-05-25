using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    [Serializable]
    public class InviteRoomMessage : Messages
    {
        public List<int> RoomParticipantsIndecies { get; }
        public int RoomId { get; }

        public InviteRoomMessage(DateTime dateTime, IPAddress senderIp, int senderPort, List<int> roomParticipantsIndecies, int roomId) : base(dateTime, senderIp, senderPort)
        {
            RoomParticipantsIndecies = roomParticipantsIndecies;
            RoomId= roomId;
        }
    }
}
