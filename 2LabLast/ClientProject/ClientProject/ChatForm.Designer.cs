namespace ClientProject
{
    partial class ChatForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.serversListBox = new System.Windows.Forms.ListBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.currentChatLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.participantsListBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.sendMessageButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.roomsListBox = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.roomsParticipantsListBox = new System.Windows.Forms.ListBox();
            this.inviteRoomButton = new System.Windows.Forms.Button();
            this.exitRoomButton = new System.Windows.Forms.Button();
            this.AvaibleFilesListBox = new System.Windows.Forms.ListBox();
            this.DownloadFileButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.AddFileButton = new System.Windows.Forms.Button();
            this.FilesToLoadListBox = new System.Windows.Forms.ListBox();
            this.DeleteFileButton = new System.Windows.Forms.Button();
            this.ChatListBox = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.AvatarPictureBox = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AvatarPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // serversListBox
            // 
            this.serversListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serversListBox.FormattingEnabled = true;
            this.serversListBox.Location = new System.Drawing.Point(3, 237);
            this.serversListBox.Name = "serversListBox";
            this.serversListBox.Size = new System.Drawing.Size(144, 69);
            this.serversListBox.TabIndex = 0;
            // 
            // connectButton
            // 
            this.connectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectButton.Location = new System.Drawing.Point(3, 312);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(145, 81);
            this.connectButton.TabIndex = 2;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(352, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Current chat is";
            // 
            // currentChatLabel
            // 
            this.currentChatLabel.AutoSize = true;
            this.currentChatLabel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.currentChatLabel.Location = new System.Drawing.Point(505, 10);
            this.currentChatLabel.Name = "currentChatLabel";
            this.currentChatLabel.Size = new System.Drawing.Size(88, 24);
            this.currentChatLabel.TabIndex = 4;
            this.currentChatLabel.Text = "common";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(149, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 22);
            this.label2.TabIndex = 6;
            this.label2.Text = "Participants List:";
            // 
            // participantsListBox
            // 
            this.participantsListBox.FormattingEnabled = true;
            this.participantsListBox.Location = new System.Drawing.Point(151, 37);
            this.participantsListBox.Name = "participantsListBox";
            this.participantsListBox.Size = new System.Drawing.Size(144, 134);
            this.participantsListBox.TabIndex = 7;
            this.participantsListBox.SelectedIndexChanged += new System.EventHandler(this.participantsListBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(5, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 96);
            this.label3.TabIndex = 8;
            this.label3.Text = "choose server";
            // 
            // messageTextBox
            // 
            this.messageTextBox.Location = new System.Drawing.Point(310, 213);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(380, 20);
            this.messageTextBox.TabIndex = 9;
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendMessageButton.Location = new System.Drawing.Point(310, 241);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.Size = new System.Drawing.Size(380, 39);
            this.sendMessageButton.TabIndex = 10;
            this.sendMessageButton.Text = "Send";
            this.sendMessageButton.UseVisualStyleBackColor = true;
            this.sendMessageButton.Click += new System.EventHandler(this.sendMessageButton_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(7, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 81);
            this.label4.TabIndex = 13;
            this.label4.Text = "Hello, ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(7, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 18);
            this.label5.TabIndex = 14;
            this.label5.Text = "Please,";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(696, 216);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 65);
            this.button1.TabIndex = 15;
            this.button1.Text = "Create room";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.CreateRoomButton_Click);
            // 
            // roomsListBox
            // 
            this.roomsListBox.FormattingEnabled = true;
            this.roomsListBox.Location = new System.Drawing.Point(870, 37);
            this.roomsListBox.Name = "roomsListBox";
            this.roomsListBox.Size = new System.Drawing.Size(131, 82);
            this.roomsListBox.TabIndex = 16;
            this.roomsListBox.SelectedIndexChanged += new System.EventHandler(this.roomsListBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(880, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 22);
            this.label6.TabIndex = 17;
            this.label6.Text = "Rooms List:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 12F);
            this.label7.Location = new System.Drawing.Point(867, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(149, 18);
            this.label7.TabIndex = 19;
            this.label7.Text = "Rooms Participants:";
            // 
            // roomsParticipantsListBox
            // 
            this.roomsParticipantsListBox.FormattingEnabled = true;
            this.roomsParticipantsListBox.Location = new System.Drawing.Point(870, 140);
            this.roomsParticipantsListBox.Name = "roomsParticipantsListBox";
            this.roomsParticipantsListBox.Size = new System.Drawing.Size(131, 82);
            this.roomsParticipantsListBox.TabIndex = 18;
            this.roomsParticipantsListBox.SelectedIndexChanged += new System.EventHandler(this.roomsParticipantsListBox_SelectedIndexChanged);
            // 
            // inviteRoomButton
            // 
            this.inviteRoomButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inviteRoomButton.Location = new System.Drawing.Point(870, 228);
            this.inviteRoomButton.Name = "inviteRoomButton";
            this.inviteRoomButton.Size = new System.Drawing.Size(132, 24);
            this.inviteRoomButton.TabIndex = 20;
            this.inviteRoomButton.Text = "Invite";
            this.inviteRoomButton.UseVisualStyleBackColor = true;
            this.inviteRoomButton.Click += new System.EventHandler(this.inviteRoomButton_Click);
            // 
            // exitRoomButton
            // 
            this.exitRoomButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exitRoomButton.Location = new System.Drawing.Point(869, 256);
            this.exitRoomButton.Name = "exitRoomButton";
            this.exitRoomButton.Size = new System.Drawing.Size(132, 25);
            this.exitRoomButton.TabIndex = 21;
            this.exitRoomButton.Text = "Exit";
            this.exitRoomButton.UseVisualStyleBackColor = true;
            this.exitRoomButton.Click += new System.EventHandler(this.exitRoomButton_Click);
            // 
            // AvaibleFilesListBox
            // 
            this.AvaibleFilesListBox.FormattingEnabled = true;
            this.AvaibleFilesListBox.Location = new System.Drawing.Point(696, 37);
            this.AvaibleFilesListBox.Name = "AvaibleFilesListBox";
            this.AvaibleFilesListBox.Size = new System.Drawing.Size(153, 134);
            this.AvaibleFilesListBox.TabIndex = 27;
            // 
            // DownloadFileButton
            // 
            this.DownloadFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownloadFileButton.Location = new System.Drawing.Point(696, 176);
            this.DownloadFileButton.Name = "DownloadFileButton";
            this.DownloadFileButton.Size = new System.Drawing.Size(153, 24);
            this.DownloadFileButton.TabIndex = 26;
            this.DownloadFileButton.Text = "Download file";
            this.DownloadFileButton.UseVisualStyleBackColor = true;
            this.DownloadFileButton.Click += new System.EventHandler(this.DownloadFileButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(706, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 24);
            this.label8.TabIndex = 25;
            this.label8.Text = "Send Files:";
            // 
            // AddFileButton
            // 
            this.AddFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddFileButton.Location = new System.Drawing.Point(151, 228);
            this.AddFileButton.Name = "AddFileButton";
            this.AddFileButton.Size = new System.Drawing.Size(145, 24);
            this.AddFileButton.TabIndex = 24;
            this.AddFileButton.Text = "Add file";
            this.AddFileButton.UseVisualStyleBackColor = true;
            this.AddFileButton.Click += new System.EventHandler(this.AddFileButton_Click);
            // 
            // FilesToLoadListBox
            // 
            this.FilesToLoadListBox.AllowDrop = true;
            this.FilesToLoadListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FilesToLoadListBox.FormattingEnabled = true;
            this.FilesToLoadListBox.ItemHeight = 15;
            this.FilesToLoadListBox.Location = new System.Drawing.Point(151, 203);
            this.FilesToLoadListBox.Name = "FilesToLoadListBox";
            this.FilesToLoadListBox.ScrollAlwaysVisible = true;
            this.FilesToLoadListBox.Size = new System.Drawing.Size(145, 19);
            this.FilesToLoadListBox.TabIndex = 23;
            // 
            // DeleteFileButton
            // 
            this.DeleteFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteFileButton.Location = new System.Drawing.Point(151, 256);
            this.DeleteFileButton.Name = "DeleteFileButton";
            this.DeleteFileButton.Size = new System.Drawing.Size(145, 24);
            this.DeleteFileButton.TabIndex = 22;
            this.DeleteFileButton.Text = "Delete file";
            this.DeleteFileButton.UseVisualStyleBackColor = true;
            this.DeleteFileButton.Click += new System.EventHandler(this.DeleteFileButton_Click);
            // 
            // ChatListBox
            // 
            this.ChatListBox.FormattingEnabled = true;
            this.ChatListBox.Location = new System.Drawing.Point(310, 37);
            this.ChatListBox.Name = "ChatListBox";
            this.ChatListBox.Size = new System.Drawing.Size(380, 173);
            this.ChatListBox.TabIndex = 28;
            this.ChatListBox.SelectedIndexChanged += new System.EventHandler(this.ChatListBox_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 12F);
            this.label9.Location = new System.Drawing.Point(157, 182);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(135, 18);
            this.label9.TabIndex = 29;
            this.label9.Text = "Select file to send:";
            // 
            // AvatarPictureBox
            // 
            this.AvatarPictureBox.Location = new System.Drawing.Point(3, 10);
            this.AvatarPictureBox.Name = "AvatarPictureBox";
            this.AvatarPictureBox.Size = new System.Drawing.Size(142, 97);
            this.AvatarPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.AvatarPictureBox.TabIndex = 30;
            this.AvatarPictureBox.TabStop = false;
            this.AvatarPictureBox.Click += new System.EventHandler(this.AvatarPictureBox_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(34, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 13);
            this.label10.TabIndex = 31;
            this.label10.Text = "Upload avatar";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(151, 405);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.AvatarPictureBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ChatListBox);
            this.Controls.Add(this.AvaibleFilesListBox);
            this.Controls.Add(this.DownloadFileButton);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.AddFileButton);
            this.Controls.Add(this.FilesToLoadListBox);
            this.Controls.Add(this.DeleteFileButton);
            this.Controls.Add(this.exitRoomButton);
            this.Controls.Add(this.inviteRoomButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.roomsParticipantsListBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.roomsListBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.sendMessageButton);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.participantsListBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.currentChatLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.serversListBox);
            this.Controls.Add(this.label3);
            this.Name = "ChatForm";
            this.Text = "Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AvatarPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label currentChatLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox participantsListBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Button sendMessageButton;
        public System.Windows.Forms.ListBox serversListBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox roomsListBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox roomsParticipantsListBox;
        private System.Windows.Forms.Button inviteRoomButton;
        private System.Windows.Forms.Button exitRoomButton;
        private System.Windows.Forms.ListBox AvaibleFilesListBox;
        private System.Windows.Forms.Button DownloadFileButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button AddFileButton;
        private System.Windows.Forms.ListBox FilesToLoadListBox;
        private System.Windows.Forms.Button DeleteFileButton;
        private System.Windows.Forms.ListBox ChatListBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox AvatarPictureBox;
        private System.Windows.Forms.Label label10;
    }
}

