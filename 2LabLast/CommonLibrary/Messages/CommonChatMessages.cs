using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    [Serializable]
    public class CommonChatMessages : Messages
    {
        public string Content { get; }

        public int SenderId { get; }

        public CommonChatMessages(DateTime dateTime, IPAddress senderIp, int senderPort, string content, int senderId) : base(dateTime, senderIp, senderPort)
        {
            Content = content;
            SenderId = senderId;
        }
    }
}
