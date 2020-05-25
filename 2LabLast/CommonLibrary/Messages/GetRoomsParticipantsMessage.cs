using System;
using System.Collections.Generic;
using System.Net;

namespace CommonLibrary
{
    [Serializable]
    public class RoomParticipantsMessage : Messages
    {
        public List<string> Participants { get; set; }
        public int RoomId { get; }

        public RoomParticipantsMessage(DateTime dateTime, IPAddress senderIp, int senderPort, List<string> participants, int roomId) : base(dateTime, senderIp, senderPort)
        {
            Participants = participants;
            RoomId = roomId;
        }
    }
}
