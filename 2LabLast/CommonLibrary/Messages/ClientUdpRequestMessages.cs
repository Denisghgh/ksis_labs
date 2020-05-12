using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    [Serializable]
    public class ClientUdpRequestMessages : Messages
    {
        public ClientUdpRequestMessages(DateTime dateTime, IPAddress senderIp, int senderPort) : base(dateTime, senderIp, senderPort) { }
    }
}
