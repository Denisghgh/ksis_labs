using ClientProject.Properties;
using CommonLibrary;
using FileSharingLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using Messages = CommonLibrary.Messages;
using System.Drawing;



namespace ClientProject
{
    public partial class ChatForm : Form
    {
        public const int DefaultSelectedDialog = 0;
        private const string CommonChatName = "Common";
        private const int CommonChatId = 0;
        private const string FileSharingServerUrl = "http://localhost:8888/";
        public static String avatarName = "Default";

        public ClientClass client;
        public FileSharingClient fileSharingClient;

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
                AvatarPictureBox.Image = new Bitmap(GetImageBinaryFromDb());
                CheckAvatarPictureBoxInDB(false);
            }
        }

        public ChatForm()
        {
            selectedDialog = DefaultSelectedDialog;
            InitializeComponent();

            client = new ClientClass(new BinaryMessagesSerializer());
            fileSharingClient = new FileSharingClient();
            client.ReceiveMessageEvent += ShowReceivedMessage;
            client.UnreadMessageEvent += UnreadMessageHandler;
            client.ReadMessageEvent += ReadMessageHandler;
            client.DeleteRoomEvent += HandleDeletedRoomUi;
            fileSharingClient.UpdateFilesToLoadListEvent += UpdateFilesToLoadList;
            client.SearchServers();
            VisibleSettings(false);          
        }

        private void UpdateFilesToLoadList(Dictionary<int, string> files)
        {
            FilesToLoadListBox.Items.Clear();
            foreach (var file in files)
            {
                FilesToLoadListBox.Items.Add(file.Value);
            }
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
                    ChatListBox.Items.Add(chatContent);
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
                ChatListBox.Items.Clear();
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
                    ChatListBox.Items.Add(chatContent);
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
                    ChatListBox.Items.Add(chatContent);
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
                    ChatListBox.Items.Add(chatContent);
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
            ChatListBox.Visible = isVisible;
            messageTextBox.Visible = isVisible;
            sendMessageButton.Visible = isVisible;
            AddFileButton.Visible = isVisible;
            DeleteFileButton.Visible = isVisible;
            FilesToLoadListBox.Visible = isVisible;
            label8.Visible = true;
            AvaibleFilesListBox.Visible = true;
            DownloadFileButton.Visible = true;
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
            ActiveForm.Width = 1040;
            Connect();         
        }
        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            if (roomsListBox.SelectedIndex == -1)
            {
                if (FilesToLoadListBox.Items.Count == 0)
                {
                    client.SendMessage(messageTextBox.Text, selectedDialog);
                }
                else
                {
                    client.SendFileMessage(messageTextBox.Text, selectedDialog, fileSharingClient.filesToSendDictionary);
                    fileSharingClient.totalFilesToLoadSize = 0;
                    fileSharingClient.filesToSendDictionary.Clear();
                    FilesToLoadListBox.Items.Clear();
                }
                messageTextBox.Clear();
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

        private void inviteRoomButton_Click(object sender, EventArgs e)
        {
            if (selectedRoom > -1)
            {
                var inviteRoomForm = new InviteRoomForm(client.participants.GetRange(1, client.participants.Count - 1), client.rooms[selectedRoom].Participants, client.id);
                inviteRoomForm.ShowDialog();
                var roomParticipantsIndecies = inviteRoomForm.RoomParticipantsIndecies;
                if (roomParticipantsIndecies != null && roomParticipantsIndecies.Count != 0)
                {
                    client.SendInviteRoomMessage(selectedRoom, roomParticipantsIndecies);
                }
            }
        }



        private async void AddFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var filePath = openFileDialog.FileName;
                    await fileSharingClient.SendFile(filePath, FileSharingServerUrl);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Исключение: " + ex.Message);
            }
        }

        private async void DeleteFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedFileIndex = FilesToLoadListBox.SelectedIndex;
                if (selectedFileIndex > -1 && selectedFileIndex < FilesToLoadListBox.Items.Count)
                {
                    var fileInfo = FilesToLoadListBox.Items[selectedFileIndex].ToString();
                    var fileId = fileSharingClient.GetFileIdByInfoInFilesToLoadList(fileInfo);
                    if (fileId != -1)
                    {
                        await fileSharingClient.DeleteFile(fileId, FileSharingServerUrl);
                    }
                    else
                    {
                        MessageBox.Show("Id файла с таким названием не найдено!", "Error!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Исключение: " + ex.Message);
            }
        }

        private Dictionary<int, string> GetFilesByMessageIndex()
        {
            var messages = client.participants[selectedDialog].MessageHistory;
            var selectedIndex = ChatListBox.SelectedIndex;
            if (selectedIndex != -1)
            {
                var i = 0;
                foreach (var message in messages)
                {
                    if (i == selectedIndex)
                    {
                        if (message is FileCommonMessage)
                        {
                            return (((FileCommonMessage)message).Files);
                        }
                        if (message is FileIndividualMessage)
                        {
                            return (((FileIndividualMessage)message).Files);
                        }
                        break;
                    }
                    i++;
                }
            }
            return null;
        }

        private int GetFileIdByFileInfo(string fileInfo)
        {
            var files = GetFilesByMessageIndex();
            foreach (var file in files)
            {
                if (fileInfo == file.Value)
                {
                    return file.Key;
                }
            }
            return -1;
        }

        private async void DownloadFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedFileIndex = AvaibleFilesListBox.SelectedIndex;
                if (selectedFileIndex > -1 && selectedFileIndex < AvaibleFilesListBox.Items.Count)
                {
                    var fileInfo = AvaibleFilesListBox.Items[selectedFileIndex].ToString();
                    var fileId = GetFileIdByFileInfo(fileInfo);
                    if (fileId != -1)
                    {
                        var downloadFile = await fileSharingClient.DownloadFile(fileId, FileSharingServerUrl);
                        if (downloadFile != null)
                        {
                            var saveFileDialog = new SaveFileDialog();
                            saveFileDialog.FileName = downloadFile.FileName;
                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                var filePath = saveFileDialog.FileName;
                                using (var fileStream = new FileStream(filePath, FileMode.Create))
                                {
                                    fileStream.Write(downloadFile.FileBytes, 0, downloadFile.FileBytes.Length);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Id файла с таким названием не найдено!", "Error!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Исключение: " + ex.Message);
            }
        }

        private void ShowFiles(Dictionary<int, string> files)
        {
            AvaibleFilesListBox.Items.Clear();
            foreach (var file in files)
            {
                AvaibleFilesListBox.Items.Add(file.Value);
            }
        }

        private void ChatListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var files = GetFilesByMessageIndex();
            if (files != null)
            {
                ShowFiles(files);
            }
        }

        private void roomsParticipantsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void AddAvatarPhoto()
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Image Files(*.JPG;*.PNG;*.GIF|*.JPG;*.PNG;*.GIF|All files (*.*)|*.*)";

            if (opf.ShowDialog() == DialogResult.OK)
            {

                PutImageBinaryInDb(opf.FileName);
                Image PassAvatarPhoto = GetImageBinaryFromDb();
                AvatarPictureBox.Image = new Bitmap(opf.FileName);
            }
        }
        private void PutImageBinaryInDb(string iFile)
        {
            string iAvatarName = avatarName;
            byte[] imageData = null;
            System.IO.FileInfo fInfo = new System.IO.FileInfo(iFile);
            long numBytes = fInfo.Length;
            FileStream fStream = new FileStream(iFile, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            imageData = br.ReadBytes((int)numBytes);

            string iImageExtension = (Path.GetExtension(iFile)).Replace(".", "").ToLower();
            
            using (SqlConnection sqlConnection = new SqlConnection(Settings.Default.DBMAINN))
            {
                string commandText = "INSERT INTO report (avatar_name, screen, screen_format) VALUES(@avatar_name, @screen, @screen_format)";
                SqlCommand command = new SqlCommand(commandText, sqlConnection);
                command.Parameters.AddWithValue("@avatar_name", iAvatarName);
                command.Parameters.AddWithValue("@screen", (object)imageData);
                command.Parameters.AddWithValue("@screen_format", iImageExtension);
                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        private Image GetImageBinaryFromDb()
        {
            List<byte[]> iScreen = new List<byte[]>();
            List<string> iScreen_format = new List<string>();
            List<string> iAvatar_Name = new List<string>();

            using (SqlConnection sqlConnection = new SqlConnection(Settings.Default.DBMAINN))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = @"SELECT [avatar_name], [screen], [screen_format] FROM [report] WHERE [avatar_name] = '" + avatarName + "'";
                SqlDataReader sqlReader = sqlCommand.ExecuteReader();
                byte[] iTrimByte = null;
                string iTrimText = null;
                string iTrimName = null;
                while (sqlReader.Read())
                {
                    iTrimByte = (byte[])sqlReader["screen"];
                    iScreen.Add(iTrimByte);
                    iTrimText = sqlReader["screen_format"].ToString().TrimStart().TrimEnd();
                    iScreen_format.Add(iTrimText);
                    iTrimName = sqlReader["avatar_name"].ToString();
                    iAvatar_Name.Add(iTrimName);
                }
                sqlConnection.Close();
            }
            byte[] imageData = iScreen[0];
            MemoryStream ms = new MemoryStream(imageData);
            Image newImage = Image.FromStream(ms);
            return newImage;
        }

        public void CheckAvatarPictureBoxInDB(bool NeedChangepic)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Settings.Default.DBMAINN))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                avatarName = clientUsername;
                sqlCommand.CommandText = @"SELECT count(*) from [report] where [avatar_name] = '" + avatarName + "'";
                int temp = Convert.ToInt16(sqlCommand.ExecuteScalar());
                if (temp >= 1)
                {
                    Image PassAvatarPhoto = GetImageBinaryFromDb();
                    AvatarPictureBox.Image = new Bitmap(PassAvatarPhoto);
                }
                else if (temp == 0 && NeedChangepic)
                {
                    AddAvatarPhoto();
                }
                sqlConnection.Close();
            }
        }
        public void DeleteRow1(string AvatarName)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Settings.Default.DBMAINN))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "DELETE FROM report WHERE avatar_name = @AvatarName";
                sqlCommand.Parameters.AddWithValue("@AvatarName", AvatarName);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }

        }
        private void AvatarPictureBox_Click(object sender, EventArgs e)
        {
            CheckAvatarPictureBoxInDB(true);
        }

        private void label10_Click(object sender, EventArgs e)
        {
            DeleteRow1(clientUsername);
            AddAvatarPhoto();
        }
    }
}
