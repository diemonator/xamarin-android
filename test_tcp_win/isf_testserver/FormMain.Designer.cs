namespace isf_testserver
{
    partial class FormMain
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtbxServerRun = new System.Windows.Forms.TextBox();
            this.button_ServerStart = new System.Windows.Forms.Button();
            this.button_ClearLog = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txbox_ServerLog = new System.Windows.Forms.TextBox();
            this.checkListBox_ConnectedClients = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_DickonnectAllClients = new System.Windows.Forms.Button();
            this.button_DisconnectSelectedClient = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtbxServerRun);
            this.splitContainer1.Panel1.Controls.Add(this.button_ServerStart);
            this.splitContainer1.Panel1.Controls.Add(this.button_ClearLog);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.txbox_ServerLog);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.checkListBox_ConnectedClients);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(632, 445);
            this.splitContainer1.SplitterDistance = 446;
            this.splitContainer1.TabIndex = 0;
            // 
            // txtbxServerRun
            // 
            this.txtbxServerRun.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbxServerRun.Location = new System.Drawing.Point(12, 413);
            this.txtbxServerRun.Name = "txtbxServerRun";
            this.txtbxServerRun.ReadOnly = true;
            this.txtbxServerRun.Size = new System.Drawing.Size(215, 20);
            this.txtbxServerRun.TabIndex = 22;
            // 
            // button_ServerStart
            // 
            this.button_ServerStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ServerStart.Location = new System.Drawing.Point(233, 408);
            this.button_ServerStart.Name = "button_ServerStart";
            this.button_ServerStart.Size = new System.Drawing.Size(120, 29);
            this.button_ServerStart.TabIndex = 21;
            this.button_ServerStart.Text = "Server Start\r\n";
            this.button_ServerStart.UseVisualStyleBackColor = true;
            this.button_ServerStart.Click += new System.EventHandler(this.button_ServerStart_Click);
            // 
            // button_ClearLog
            // 
            this.button_ClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ClearLog.Location = new System.Drawing.Point(359, 409);
            this.button_ClearLog.Name = "button_ClearLog";
            this.button_ClearLog.Size = new System.Drawing.Size(75, 27);
            this.button_ClearLog.TabIndex = 20;
            this.button_ClearLog.Text = "Clear log";
            this.button_ClearLog.UseVisualStyleBackColor = true;
            this.button_ClearLog.Click += new System.EventHandler(this.button_ClearLog_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Server log";
            // 
            // txbox_ServerLog
            // 
            this.txbox_ServerLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbox_ServerLog.BackColor = System.Drawing.SystemColors.Info;
            this.txbox_ServerLog.Location = new System.Drawing.Point(3, 38);
            this.txbox_ServerLog.Multiline = true;
            this.txbox_ServerLog.Name = "txbox_ServerLog";
            this.txbox_ServerLog.ReadOnly = true;
            this.txbox_ServerLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbox_ServerLog.Size = new System.Drawing.Size(440, 363);
            this.txbox_ServerLog.TabIndex = 18;
            // 
            // checkListBox_ConnectedClients
            // 
            this.checkListBox_ConnectedClients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkListBox_ConnectedClients.FormattingEnabled = true;
            this.checkListBox_ConnectedClients.Location = new System.Drawing.Point(11, 40);
            this.checkListBox_ConnectedClients.Name = "checkListBox_ConnectedClients";
            this.checkListBox_ConnectedClients.Size = new System.Drawing.Size(159, 342);
            this.checkListBox_ConnectedClients.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button_DickonnectAllClients);
            this.groupBox1.Controls.Add(this.button_DisconnectSelectedClient);
            this.groupBox1.Location = new System.Drawing.Point(3, 392);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(176, 53);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Disconnect";
            // 
            // button_DickonnectAllClients
            // 
            this.button_DickonnectAllClients.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_DickonnectAllClients.Location = new System.Drawing.Point(99, 18);
            this.button_DickonnectAllClients.Name = "button_DickonnectAllClients";
            this.button_DickonnectAllClients.Size = new System.Drawing.Size(68, 25);
            this.button_DickonnectAllClients.TabIndex = 5;
            this.button_DickonnectAllClients.Text = "All Clients";
            this.button_DickonnectAllClients.UseVisualStyleBackColor = true;
            this.button_DickonnectAllClients.Click += new System.EventHandler(this.button_DickonnectAllClients_Click);
            // 
            // button_DisconnectSelectedClient
            // 
            this.button_DisconnectSelectedClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_DisconnectSelectedClient.Location = new System.Drawing.Point(4, 18);
            this.button_DisconnectSelectedClient.Name = "button_DisconnectSelectedClient";
            this.button_DisconnectSelectedClient.Size = new System.Drawing.Size(89, 25);
            this.button_DisconnectSelectedClient.TabIndex = 4;
            this.button_DisconnectSelectedClient.Text = "Selected Client\r\n";
            this.button_DisconnectSelectedClient.UseVisualStyleBackColor = true;
            this.button_DisconnectSelectedClient.Click += new System.EventHandler(this.button_DisconnectSelectedClient_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Connected clients";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.hideToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 48);
            this.contextMenuStrip1.Text = "Show";
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.showToolStripMenuItem.Text = "Show";
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(632, 445);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtbxServerRun;
        private System.Windows.Forms.Button button_ServerStart;
        private System.Windows.Forms.Button button_ClearLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbox_ServerLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_DickonnectAllClients;
        private System.Windows.Forms.Button button_DisconnectSelectedClient;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.ListBox checkListBox_ConnectedClients;
    }
}

