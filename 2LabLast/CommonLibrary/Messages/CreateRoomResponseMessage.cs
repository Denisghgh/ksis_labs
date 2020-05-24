using System;
using System.Net;

namespace CommonLibrary
{
    [Serializable]
    public class CreateRoomResponseMessage : Messages
    {
        public RoomInfo RoomInfo { get; }

        public CreateRoomResponseMessage(DateTime dateTime, IPAddress senderIp, int senderPort, RoomInfo roomInfo) : base(dateTime, senderIp, senderPort)
        {
            RoomInfo = roomInfo;
        }
    }
}
