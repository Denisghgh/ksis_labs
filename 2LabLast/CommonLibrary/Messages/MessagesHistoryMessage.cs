﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    [Serializable]
    public class MessagesHistoryMessage : Messages
    {
        public List<Messages> MessagesHistory { get; }

        public MessagesHistoryMessage(DateTime dateTime, IPAddress senderIp, int senderPort, List<Messages> messagesHistory) : base(dateTime, senderIp, senderPort)
        {
            MessagesHistory = messagesHistory;
        }
    }
}
