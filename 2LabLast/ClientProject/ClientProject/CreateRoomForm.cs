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
    public partial class CreateRoomForm : Form
    {
        public List<NewChatParticipant> ChatParticipants { get; set; }
        public List<int> RoomParticipantsIndecies { get; private set; }
        public string RoomName { get; private set; }
        public int ClientId { get; }

        private List<NewChatParticipant> tempChatParticipants { get; set; }

        public CreateRoomForm(List<NewChatParticipant> chatParticipants, int clientId)
        {
            InitializeComponent();
            tempChatParticipants = new List<NewChatParticipant>();
            ClientId = clientId;
            ChatParticipants = chatParticipants;
            if (ChatParticipants != null && ChatParticipants.Count != 0)
            {
                foreach (var chatParticipant in ChatParticipants)
                {
                    if (chatParticipant.Id != ClientId)
                    {
                        RoomParticipantsCheckedlistBox.Items.Add(chatParticipant.Name);
                        tempChatParticipants.Add(chatParticipant);
                    }
                }
            }
            else
            {
                MessageBox.Show("Кроме вас в чате никого нет!");
            }
            
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            RoomName = RoomNameTextBox.Text;
            RoomParticipantsIndecies = new List<int>();
            RoomParticipantsIndecies.Add(ClientId);
            var indices = RoomParticipantsCheckedlistBox.CheckedIndices;
            foreach (int index in indices)
            {
                RoomParticipantsIndecies.Add(tempChatParticipants[index].Id);
            }
            Close();
        }
    }
}
