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

        public CreateRoomForm(List<NewChatParticipant> chatParticipants)
        {
            InitializeComponent();
            ChatParticipants = chatParticipants;
            if (ChatParticipants != null && ChatParticipants.Count != 0)
            {
                foreach (var chatParticipant in ChatParticipants)
                {
                    RoomParticipantsCheckedlistBox.Items.Add(chatParticipant.Name);
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
            var indices = RoomParticipantsCheckedlistBox.CheckedIndices;
            foreach (int index in indices)
            {
                RoomParticipantsIndecies.Add(index + 1);
            }
            Close();
        }
    }
}
