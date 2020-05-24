using System;
using System.Net;

namespace CommonLibrary
{
    [Serializable]
    public class RoomMessage : CommonChatMessages
    {
        public int RoomId { get; }

        public RoomMessage(DateTime dateTime, IPAddress senderIp, int senderPort, string content, int senderId, int roomId) : base(dateTime, senderIp, senderPort, content, senderId)
        {
            RoomId = roomId;
        }
    }
}
