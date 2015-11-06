namespace dh_server
{
    partial class ServerMain
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
            this.components = new System.ComponentModel.Container();
            this.logBox = new System.Windows.Forms.ListBox();
            this.clientsBox = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdLine = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmdSend = new System.Windows.Forms.Button();
            this.clientMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sendMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.help = new System.Windows.Forms.Button();
            this.shutdown = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.clientMenuStrip.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // logBox
            // 
            this.logBox.FormattingEnabled = true;
            this.logBox.ItemHeight = 16;
            this.logBox.Location = new System.Drawing.Point(9, 18);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(480, 212);
            this.logBox.TabIndex = 1;
            // 
            // clientsBox
            // 
            this.clientsBox.FormattingEnabled = true;
            this.clientsBox.ItemHeight = 16;
            this.clientsBox.Location = new System.Drawing.Point(6, 21);
            this.clientsBox.Name = "clientsBox";
            this.clientsBox.Size = new System.Drawing.Size(159, 212);
            this.clientsBox.TabIndex = 3;
            this.clientsBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clientsBox_MouseDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.logBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(495, 237);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.clientsBox);
            this.groupBox2.Location = new System.Drawing.Point(513, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(171, 237);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Clients";
            // 
            // cmdLine
            // 
            this.cmdLine.Location = new System.Drawing.Point(9, 20);
            this.cmdLine.Name = "cmdLine";
            this.cmdLine.Size = new System.Drawing.Size(388, 22);
            this.cmdLine.TabIndex = 7;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmdSend);
            this.groupBox3.Controls.Add(this.cmdLine);
            this.groupBox3.Location = new System.Drawing.Point(12, 256);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(495, 54);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Send a global message..";
            // 
            // cmdSend
            // 
            this.cmdSend.Location = new System.Drawing.Point(403, 17);
            this.cmdSend.Name = "cmdSend";
            this.cmdSend.Size = new System.Drawing.Size(86, 28);
            this.cmdSend.TabIndex = 8;
            this.cmdSend.Text = "Send";
            this.cmdSend.UseVisualStyleBackColor = true;
            this.cmdSend.Click += new System.EventHandler(this.cmdSend_Click);
            // 
            // clientMenuStrip
            // 
            this.clientMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendMessageToolStripMenuItem,
            this.kickToolStripMenuItem});
            this.clientMenuStrip.Name = "clientMenuStrip";
            this.clientMenuStrip.Size = new System.Drawing.Size(174, 52);
            this.clientMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.clientMenuStrip_ItemClicked);
            // 
            // sendMessageToolStripMenuItem
            // 
            this.sendMessageToolStripMenuItem.Name = "sendMessageToolStripMenuItem";
            this.sendMessageToolStripMenuItem.Size = new System.Drawing.Size(173, 24);
            this.sendMessageToolStripMenuItem.Text = "Send Message";
            // 
            // kickToolStripMenuItem
            // 
            this.kickToolStripMenuItem.Name = "kickToolStripMenuItem";
            this.kickToolStripMenuItem.Size = new System.Drawing.Size(173, 24);
            this.kickToolStripMenuItem.Text = "Kick";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.shutdown);
            this.groupBox4.Controls.Add(this.help);
            this.groupBox4.Location = new System.Drawing.Point(513, 256);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(171, 54);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Server";
            // 
            // help
            // 
            this.help.Location = new System.Drawing.Point(101, 17);
            this.help.Name = "help";
            this.help.Size = new System.Drawing.Size(64, 28);
            this.help.TabIndex = 8;
            this.help.Text = "Help";
            this.help.UseVisualStyleBackColor = true;
            this.help.Click += new System.EventHandler(this.help_Click);
            // 
            // shutdown
            // 
            this.shutdown.Location = new System.Drawing.Point(6, 17);
            this.shutdown.Name = "shutdown";
            this.shutdown.Size = new System.Drawing.Size(89, 28);
            this.shutdown.TabIndex = 9;
            this.shutdown.Text = "Shutdown";
            this.shutdown.UseVisualStyleBackColor = true;
            this.shutdown.Click += new System.EventHandler(this.shutdown_Click);
            // 
            // ServerMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 322);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "ServerMain";
            this.Text = "Chat Server";
            this.Load += new System.EventHandler(this.ServerMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.clientMenuStrip.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox logBox;
        private System.Windows.Forms.ListBox clientsBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox cmdLine;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button cmdSend;
        private System.Windows.Forms.ContextMenuStrip clientMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem sendMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kickToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button shutdown;
        private System.Windows.Forms.Button help;
    }
}

