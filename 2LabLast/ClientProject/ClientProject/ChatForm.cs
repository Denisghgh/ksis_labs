using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Windows.Forms;
using Messages = CommonLibrary.Messages;

namespace ClientProject
{
    public partial class ChatForm : Form
    {
        public const int DefaultSelectedDialog = 0;
        private const string CommonChatName = "Common";
        private const int CommonChatId = 0;
        public ClientClass client;
        private int selectedDialog = -1;
        private int selectedRoom = -1;
        private string clientUsername;
        public string ClientUsername
        {
            get { return clientUsername; }
            set
            { 
                clientUsername = value;
                label4.Text = label4.Text + " " + clientUsername;
            }
        }

        public ChatForm()
        {
            selectedDialog = DefaultSelectedDialog;
            InitializeComponent();

            client = new ClientClass(new BinaryMessagesSerializer());
            client.ReceiveMessageEvent += ShowReceivedMessage;
            client.UnreadMessageEvent += UnreadMessageHandler;
            client.ReadMessageEvent += ReadMessageHandler;
            client.DeleteRoomEvent += HandleDeletedRoomUi;
            client.SearchServers();
            VisibleSettings(false);          
        }

        private void ReadMessageHandler(Messages message)
        {
            Action action = delegate
            {
                if (message is IndividualChatMessages)
                {
                    IndividualChatMessages individualChatMessage = (IndividualChatMessages)message;
                    if (client.id == individualChatMessage.ReceiverId)
                    {
                        participantsListBox.Items[individualChatMessage.SenderId] = client.participants[individualChatMessage.SenderId].Name;
                    }
                }
                else if (message is CommonChatMessages)
                {
                    participantsListBox.Items[CommonChatId] = CommonChatName;
                }
            };
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void UnreadMessageHandler(string unreadMessageString, Messages message)
        {
            Action action = delegate
            {
                if (message is IndividualChatMessages)
                {
                    IndividualChatMessages individualChatMessage = (IndividualChatMessages)message;
                    if ((client.id == individualChatMessage.ReceiverId)&&(selectedDialog != individualChatMessage.SenderId))
                    {
                        participantsListBox.Items[individualChatMessage.SenderId] = unreadMessageString;
                    }
                }
                else if ((message is CommonChatMessages)&&(selectedDialog != CommonChatId))
                {
                    participantsListBox.Items[CommonChatId] = unreadMessageString;
                }
            };
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }

        public void AddServerInfoToServersListBox(ServerUdpAnswerMessages serverUdpAnswerMessage)
        {
            Action action = delegate
            {
                string serverInfo = serverUdpAnswerMessage.ServerName + " port: " + serverUdpAnswerMessage.SenderPort + " ip: " + serverUdpAnswerMessage.SenderIp;
                serversListBox.Items.Add(serverInfo);
                
            };
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void AddNewCommonChatMessage(CommonChatMessages commonChatMessage)
        {
            Action action = delegate
            {
                if (selectedDialog == 0)
                {
                    string chatContent = "[" + commonChatMessage.DateTime.ToString() + " " + commonChatMessage.SenderIp.ToString() + ":" + commonChatMessage.SenderPort + "]: \"" + client.participants[commonChatMessage.SenderId].Name + "\": " + commonChatMessage.Content + "\r\n";
                    chatTextBox.Text += chatContent;
                }                        
            };
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void RefreshParticipantsListBox(ParticipantsListMessages participantsListMessage)
        {
            Action action = delegate
            {
                participantsListBox.Items.Clear();
                foreach (NewChatParticipant participant in participantsListMessage.participants)
                {
                    participantsListBox.Items.Add(participant.Name);
                }
            };
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }       
        }

        private void HandleChatMessage(CommonChatMessages commonChatMessage)
        {
            if (commonChatMessage is IndividualChatMessages)
            {
                IndividualChatMessages individualChatMessage = (IndividualChatMessages)commonChatMessage;
                if (client.id == individualChatMessage.ReceiverId)
                {
                    ReceiverHandleIndividualChatMessage(individualChatMessage);
                }
                else if (client.id == individualChatMessage.SenderId)
                {
                    SenderHandleIndividualChatMessage(individualChatMessage);
                }
            }
            else if (commonChatMessage is CommonChatMessages)
            {
                AddNewCommonChatMessage(commonChatMessage);
            }
        }

        private void RefreshChatTextBox(List<Messages> messageHistory)
        {
            Action action = delegate
            {
                chatTextBox.Clear();
                if ((messageHistory != null)&&(messageHistory.Count != 0))
                    foreach (Messages message in messageHistory)
                    {
                        ShowReceivedMessage(message);
                    }
            };
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }  
        }

        private void SenderHandleIndividualChatMessage(IndividualChatMessages individualChatMessage)
        {
            Action action = delegate
            {
                if (individualChatMessage.ReceiverId == selectedDialog)
                {
                    string chatContent = "[" + individualChatMessage.DateTime.ToString() + " " + individualChatMessage.SenderIp.ToString() + ":" + individualChatMessage.SenderPort + "]: \"" + client.participants[individualChatMessage.SenderId].Name + "\": " + individualChatMessage.Content + "\r\n";
                    chatTextBox.Text += chatContent;
                }
            };
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void ReceiverHandleIndividualChatMessage(IndividualChatMessages individualChatMessage)
        {
            Action action = delegate
            {
                if (individualChatMessage.SenderId == selectedDialog)
                {
                    string chatContent = "[" + individualChatMessage.DateTime.ToString() + " " + individualChatMessage.SenderIp.ToString() + ":" + individualChatMessage.SenderPort + "]: \"" + client.participants[individualChatMessage.SenderId].Name + "\": " + individualChatMessage.Content + "\r\n";
                    chatTextBox.Text += chatContent;
                }
            };
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }  
        }

        private void AddRoomToRoomsListBox(CreateRoomResponseMessage createRoomResponseMessage)
        {
            Action action = delegate
            {
                roomsListBox.Items.Add(createRoomResponseMessage.RoomInfo.RoomName);
            };
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void HandleRoomMessage(RoomMessage roomMessage)
        {
            Action action = delegate
            {
                if (selectedRoom == roomMessage.RoomId)
                {
                    string chatContent = "[" + roomMessage.DateTime.ToString() + " " + roomMessage.SenderIp.ToString() + ":" + roomMessage.SenderPort + "]: \"" + client.participants[roomMessage.SenderId].Name + "\": " + roomMessage.Content + "\r\n";
                    chatTextBox.Text += chatContent;
                }
            };
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void RefreshRoomParticipantsListBox()
        {
            Action action = delegate
            {
                roomsParticipantsListBox.Items.Clear();
                var room = client.rooms[selectedRoom];
                foreach (var participant in room.Participants)
                {
                    roomsParticipantsListBox.Items.Add(participant);
                }
            };
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void HandleRoomParticipantsMessage(RoomParticipantsMessage message)
        {
            if (message.RoomId == selectedRoom)
            {
                RefreshRoomParticipantsListBox();
            }
        }

        public void ShowReceivedMessage(Messages message)
        {
            if (message is RoomParticipantsMessage)
            {
                HandleRoomParticipantsMessage((RoomParticipantsMessage)message);
            }
            if (message is CreateRoomResponseMessage)
            {
                AddRoomToRoomsListBox((CreateRoomResponseMessage)message);
            }
            if (message is ServerUdpAnswerMessages)
            {
                AddServerInfoToServersListBox((ServerUdpAnswerMessages)message);
            }
            else if (message is ParticipantsListMessages)
            {
                RefreshParticipantsListBox((ParticipantsListMessages)message);
            }
            else if (message is RoomMessage)
            {
                HandleRoomMessage((RoomMessage)message);
            }
            else if (message is CommonChatMessages)
            {
                CommonChatMessages commonChatMessage = (CommonChatMessages)message;
                HandleChatMessage(commonChatMessage);
            } 
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("ВЫ УВЕРЕНЫ ЧТО ХОТИТЕ ЗАКРЫТЬ?", "ОСТАНОВОЧКА, МИЛЕЙШИЙ )", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                client.Close();
                this.Owner.Show();
            }
            else
            {
                e.Cancel = true;
            }
        }
        private void VisibleSettings(bool isVisible)
        {
            participantsListBox.Visible = isVisible;
            label2.Visible = isVisible;
            label1.Visible = isVisible;
            currentChatLabel.Visible = isVisible;
            chatTextBox.Visible = isVisible;
            messageTextBox.Visible = isVisible;
            sendMessageButton.Visible = isVisible;
        }
        
        private void Connect()
        {           
            if (serversListBox.SelectedIndex == -1)
                serversListBox.SelectedIndex = 0;
            int serverIndex = serversListBox.SelectedIndex;
            client.ConnectToServer(serverIndex, clientUsername);
            connectButton.Visible = false;
            serversListBox.Visible = false;
            label5.Visible = false;
            label3.Text = "Your server is " + serversListBox.SelectedItem;
            VisibleSettings(true);
        }
        private void connectButton_Click(object sender, EventArgs e)
        {
            Connect();         
        }
        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            if (roomsListBox.SelectedIndex == -1)
            {
                client.SendMessage(messageTextBox.Text, selectedDialog);
            }
            else
            {
                client.SendRoomMessage(messageTextBox.Text, roomsListBox.SelectedIndex);
            }
            messageTextBox.Clear();
        }

        private bool UnreadMessageCheck(NewChatParticipant chatParticipant)
        {
            if (chatParticipant.GetNotReadedMessageCount() != 0)
                return true;
            return false;
        }

        private void ChangeDialog()
        {
            NewChatParticipant chatParticipant = client.participants[selectedDialog];
            if (UnreadMessageCheck(chatParticipant))
            {
                chatParticipant.SetNotReadedMessageCountZero();
            }
            currentChatLabel.Text = client.participants[selectedDialog].Name;
            List<Messages> newMessages = client.participants[selectedDialog].MessageHistory;
            RefreshChatTextBox(newMessages);
        }

        private void ChangeDialog(int selectedRoom)
        {
            var room = client.rooms[selectedRoom];
            currentChatLabel.Text = room.RoomName;
            List<Messages> newMessages = room.MessageHistory;
            RefreshChatTextBox(newMessages);
            RefreshRoomParticipantsListBox();
        }

        private void participantsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            roomsListBox.SelectedIndex = -1;
            selectedRoom = -1;
            if ((participantsListBox.SelectedIndex != selectedDialog)&&(participantsListBox.SelectedIndex >= 0))
            {
                selectedDialog = participantsListBox.SelectedIndex;
                ChangeDialog();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Owner.Hide();
        }

        private void chatTextBox_TextChanged(object sender, EventArgs e)
        {
            chatTextBox.SelectionStart = chatTextBox.Text.Length;
            chatTextBox.ScrollToCaret();
            chatTextBox.Refresh();
        }

        private void CreateRoomButton_Click(object sender, EventArgs e)
        {
            var createRoomForm = new CreateRoomForm(client.participants.GetRange(1, client.participants.Count - 1), client.id);
            createRoomForm.ShowDialog();
            var roomParticipantsIndecies = createRoomForm.RoomParticipantsIndecies;
            var roomName = createRoomForm.RoomName;
            if (roomParticipantsIndecies != null && roomParticipantsIndecies.Count != 0)
            {
                client.SendCreateRoomRequestMessage(roomName, roomParticipantsIndecies);
            }
        }

        private void roomsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            participantsListBox.SelectedIndex = -1;
            selectedDialog = -1;
            selectedRoom = roomsListBox.SelectedIndex;
            if (selectedRoom >= 0)
            {
                ChangeDialog(roomsListBox.SelectedIndex);
            }
        }

        private void HandleDeletedRoomUi(int roomId)
        {
            selectedRoom = -1;
            roomsListBox.SelectedIndex = -1;
            roomsListBox.Items.RemoveAt(roomId);
            roomsParticipantsListBox.Items.Clear();
        }

        private void exitRoomButton_Click(object sender, EventArgs e)
        {
            client.SendExitRoomMessage(selectedRoom);
        }
    }
}
