using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    [Serializable]
    public class SendIdMessages : Messages
    {
        public int Id { get; }

        public SendIdMessages(DateTime dateTime, IPAddress senderIp, int senderPort, int id) : base(dateTime, senderIp, senderPort)
        {
            Id = id;
        }
    }
}
