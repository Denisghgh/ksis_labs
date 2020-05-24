using System;
using System.Collections.Generic;
using System.Net;

namespace CommonLibrary
{
    [Serializable]
    public class CreateRoomRequestMessage : Messages
    {
        public List<int> RoomParticipantsIndecies { get; }
        public string RoomName { get; }

        public CreateRoomRequestMessage(DateTime dateTime, IPAddress senderIp, int senderPort, List<int> roomParticipantsIndecies, string roomName) : base(dateTime, senderIp, senderPort)
        {
            RoomParticipantsIndecies = roomParticipantsIndecies;
            RoomName = roomName;
        }
    }
}
