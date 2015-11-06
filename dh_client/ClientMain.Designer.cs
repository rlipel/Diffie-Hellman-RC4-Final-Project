namespace dh_client
{
    partial class ClientMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.userSend = new System.Windows.Forms.Button();
            this.chatBox = new System.Windows.Forms.ListBox();
            this.userInput = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.clientsList = new System.Windows.Forms.ListBox();
            this.serverPortLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.serverIpLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.nameEdit = new System.Windows.Forms.Button();
            this.nameLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.userSend);
            this.groupBox1.Controls.Add(this.chatBox);
            this.groupBox1.Controls.Add(this.userInput);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(417, 351);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chat";
            // 
            // userSend
            // 
            this.userSend.Location = new System.Drawing.Point(301, 319);
            this.userSend.Name = "userSend";
            this.userSend.Size = new System.Drawing.Size(96, 23);
            this.userSend.TabIndex = 2;
            this.userSend.Text = "Send";
            this.userSend.UseVisualStyleBackColor = true;
            this.userSend.Click += new System.EventHandler(this.userSend_Click);
            // 
            // chatBox
            // 
            this.chatBox.FormattingEnabled = true;
            this.chatBox.ItemHeight = 16;
            this.chatBox.Location = new System.Drawing.Point(7, 21);
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(390, 292);
            this.chatBox.TabIndex = 1;
            // 
            // userInput
            // 
            this.userInput.Location = new System.Drawing.Point(7, 320);
            this.userInput.Name = "userInput";
            this.userInput.Size = new System.Drawing.Size(289, 22);
            this.userInput.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.disconnectButton);
            this.groupBox2.Controls.Add(this.connectButton);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.clientsList);
            this.groupBox2.Controls.Add(this.serverPortLabel);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.serverIpLabel);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(435, 71);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(196, 292);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Server Info";
            // 
            // disconnectButton
            // 
            this.disconnectButton.Location = new System.Drawing.Point(6, 256);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(184, 27);
            this.disconnectButton.TabIndex = 10;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(6, 219);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(184, 31);
            this.connectButton.TabIndex = 9;
            this.connectButton.Text = "Connect to a server";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 17);
            this.label7.TabIndex = 8;
            this.label7.Text = "Clients online:";
            // 
            // clientsList
            // 
            this.clientsList.FormattingEnabled = true;
            this.clientsList.ItemHeight = 16;
            this.clientsList.Location = new System.Drawing.Point(9, 81);
            this.clientsList.Name = "clientsList";
            this.clientsList.Size = new System.Drawing.Size(181, 132);
            this.clientsList.TabIndex = 7;
            // 
            // serverPortLabel
            // 
            this.serverPortLabel.AutoSize = true;
            this.serverPortLabel.Location = new System.Drawing.Point(76, 44);
            this.serverPortLabel.Name = "serverPortLabel";
            this.serverPortLabel.Size = new System.Drawing.Size(13, 17);
            this.serverPortLabel.TabIndex = 6;
            this.serverPortLabel.Text = "-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "Port:";
            // 
            // serverIpLabel
            // 
            this.serverIpLabel.AutoSize = true;
            this.serverIpLabel.Location = new System.Drawing.Point(76, 27);
            this.serverIpLabel.Name = "serverIpLabel";
            this.serverIpLabel.Size = new System.Drawing.Size(13, 17);
            this.serverIpLabel.TabIndex = 4;
            this.serverIpLabel.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Address:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.nameEdit);
            this.groupBox3.Controls.Add(this.nameLabel);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(435, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(196, 53);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Your Info";
            // 
            // nameEdit
            // 
            this.nameEdit.Location = new System.Drawing.Point(6, 21);
            this.nameEdit.Name = "nameEdit";
            this.nameEdit.Size = new System.Drawing.Size(15, 17);
            this.nameEdit.TabIndex = 2;
            this.nameEdit.UseVisualStyleBackColor = true;
            this.nameEdit.Click += new System.EventHandler(this.nameEdit_Click);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(82, 21);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(40, 17);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "X-PC";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // ClientMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 371);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ClientMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Chat Client";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button userSend;
        private System.Windows.Forms.ListBox chatBox;
        private System.Windows.Forms.TextBox userInput;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox clientsList;
        private System.Windows.Forms.Label serverPortLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label serverIpLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button nameEdit;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label label1;
    }
}

