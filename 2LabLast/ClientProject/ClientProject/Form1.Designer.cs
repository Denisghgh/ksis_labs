namespace ClientProject
{
    partial class Form1
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
            this.chatTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.participantsListBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.sendMessageButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // serversListBox
            // 
            this.serversListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serversListBox.FormattingEnabled = true;
            this.serversListBox.Location = new System.Drawing.Point(9, 129);
            this.serversListBox.Name = "serversListBox";
            this.serversListBox.Size = new System.Drawing.Size(131, 69);
            this.serversListBox.TabIndex = 0;
            // 
            // connectButton
            // 
            this.connectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectButton.Location = new System.Drawing.Point(9, 204);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(131, 82);
            this.connectButton.TabIndex = 2;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(187, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Current chat is";
            // 
            // currentChatLabel
            // 
            this.currentChatLabel.AutoSize = true;
            this.currentChatLabel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.currentChatLabel.Location = new System.Drawing.Point(340, 16);
            this.currentChatLabel.Name = "currentChatLabel";
            this.currentChatLabel.Size = new System.Drawing.Size(88, 24);
            this.currentChatLabel.TabIndex = 4;
            this.currentChatLabel.Text = "common";
            // 
            // chatTextBox
            // 
            this.chatTextBox.Location = new System.Drawing.Point(146, 48);
            this.chatTextBox.Multiline = true;
            this.chatTextBox.Name = "chatTextBox";
            this.chatTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.chatTextBox.Size = new System.Drawing.Size(380, 160);
            this.chatTextBox.TabIndex = 5;
            this.chatTextBox.TextChanged += new System.EventHandler(this.chatTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(528, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 22);
            this.label2.TabIndex = 6;
            this.label2.Text = "Participants List:";
            // 
            // participantsListBox
            // 
            this.participantsListBox.FormattingEnabled = true;
            this.participantsListBox.Location = new System.Drawing.Point(532, 48);
            this.participantsListBox.Name = "participantsListBox";
            this.participantsListBox.Size = new System.Drawing.Size(131, 238);
            this.participantsListBox.TabIndex = 7;
            this.participantsListBox.SelectedIndexChanged += new System.EventHandler(this.participantsListBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(10, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 96);
            this.label3.TabIndex = 8;
            this.label3.Text = "choose server";
            // 
            // messageTextBox
            // 
            this.messageTextBox.Location = new System.Drawing.Point(145, 219);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(380, 20);
            this.messageTextBox.TabIndex = 9;
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendMessageButton.Location = new System.Drawing.Point(145, 247);
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
            this.label4.Location = new System.Drawing.Point(12, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 81);
            this.label4.TabIndex = 13;
            this.label4.Text = "Hello, ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(10, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 18);
            this.label5.TabIndex = 14;
            this.label5.Text = "Please,";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 312);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.sendMessageButton);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.participantsListBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chatTextBox);
            this.Controls.Add(this.currentChatLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.serversListBox);
            this.Controls.Add(this.label3);
            this.Name = "Form1";
            this.Text = "Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label currentChatLabel;
        private System.Windows.Forms.TextBox chatTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox participantsListBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Button sendMessageButton;
        public System.Windows.Forms.ListBox serversListBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

