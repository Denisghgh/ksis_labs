using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public delegate void NotReadedMessageCountDelegate(string NotReadedMessageString, Messages message);
    public delegate void ReadMessageDelegate(Messages message);

    [Serializable]
    public class NewChatParticipant
    {
        public string Name { get; }
        public int Id { get; }
        public List<Messages> MessageHistory { get; set; }
        public Dictionary<int, string> Files { get; set; }
        public string NotReadedMessageString 
        {
            get
            {
                return NotReadedMessageCount + " new message(s)!";
            }
        }
        private int NotReadedMessageCount;
        public event NotReadedMessageCountDelegate NotReadedMessageEvent;
        public event ReadMessageDelegate ReadedMessageEvent;

        public NewChatParticipant(string name, int id, List<Messages> messageHistory, Dictionary<int, string> files)
        {
            Name = name;
            Id = id;
            Files = files;
            MessageHistory = messageHistory;
            NotReadedMessageCount = 0;
        }

        public int GetNotReadedMessageCount()
        {
            return NotReadedMessageCount;
        }

        public void SetNotReadedMessageCountZero()
        {
            NotReadedMessageCount = 0;
            ReadedMessageEvent(MessageHistory[MessageHistory.Count - 1]);
        }

        public void NotReadedMessageCountIncrement(CommonChatMessages commonChatMessage)
        {
            NotReadedMessageCount++;
            NotReadedMessageEvent(NotReadedMessageString, commonChatMessage);
        }
    }
}
