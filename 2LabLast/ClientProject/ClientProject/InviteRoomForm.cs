using CommonLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientProject
{
    public partial class InviteRoomForm : Form
    {
        public List<NewChatParticipant> ChatParticipants { get; set; }
        public List<int> RoomParticipantsIndecies { get; private set; }
        public string RoomName { get; private set; }
        public int ClientId { get; }
        public List<string> CurrentRoomParticipants { get; }

        private List<NewChatParticipant> tempChatParticipants { get; set; }

        public InviteRoomForm(List<NewChatParticipant> chatParticipants, List<string> currentRoomParticipants, int clientId)
        {
            InitializeComponent();
            CurrentRoomParticipants = currentRoomParticipants;
            tempChatParticipants = new List<NewChatParticipant>();
            ClientId = clientId;
            ChatParticipants = chatParticipants;
            if (ChatParticipants != null && ChatParticipants.Count != 0)
            {
                foreach (var chatParticipant in ChatParticipants)
                {
                    if (chatParticipant.Id != ClientId && !IsCurrentRoomParticipantCheck(chatParticipant.Name))
                    {
                        RoomParticipantsCheckedlistBox.Items.Add(chatParticipant.Name);
                        tempChatParticipants.Add(chatParticipant);
                    }
                }
            }
            else
            {
                MessageBox.Show("Некого пригласить!");
            }

        }

        private bool IsCurrentRoomParticipantCheck(string name)
        {
            foreach (var currentParticipant in CurrentRoomParticipants)
            {
                if (name == currentParticipant)
                {
                    return true;
                }
            }
            return false;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            RoomParticipantsIndecies = new List<int>();
            var indices = RoomParticipantsCheckedlistBox.CheckedIndices;
            foreach (int index in indices)
            {
                RoomParticipantsIndecies.Add(tempChatParticipants[index].Id);
            }
            Close();
        }
    }
}
