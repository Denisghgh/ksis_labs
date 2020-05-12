using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    [Serializable]
    public class ParticipantsListMessages : Messages
    {
        public List<NewChatParticipant> participants;

        public ParticipantsListMessages(DateTime dateTime, IPAddress senderIp, int senderPort, List<NewChatParticipant> participants) : base(dateTime, senderIp, senderPort)
        {
            this.participants = participants;
        }
    }
}
