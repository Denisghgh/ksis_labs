using System;
using System.Collections.Generic;

namespace CommonLibrary
{
    [Serializable]
    public class RoomInfo
    {
        public string RoomName { get; private set; }
        public int RoomId { get; private set; }
        public List<string> Participants { get; set; }
        public List<Messages> MessageHistory { get; private set; }

        public RoomInfo(string roomName, int roomId, List<Messages> messagesHistory, List<string> participants)
        {
            RoomName = roomName;
            RoomId = roomId;
            MessageHistory = messagesHistory;
            Participants = participants;
        }
    }
}
