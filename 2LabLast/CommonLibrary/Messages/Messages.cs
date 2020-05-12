using System;
using System.Net;

namespace CommonLibrary
{
    [Serializable]
    public abstract class Messages
    {
        public DateTime DateTime { get; }

        public IPAddress SenderIp { get; }

        public int SenderPort { get; }

        public Messages(DateTime dateTime, IPAddress senderIp, int senderPort)
        {
            DateTime = dateTime;
            SenderIp = senderIp;
            SenderPort = senderPort;
        }
    }
}
