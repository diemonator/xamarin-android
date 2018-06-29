namespace test_tcpclient00
{
    partial class FormTestTcpClient
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
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBox_Send = new System.Windows.Forms.TextBox();
            this.groupBox_Send = new System.Windows.Forms.GroupBox();
            this.radioButton_LF = new System.Windows.Forms.RadioButton();
            this.buttonClearSend = new System.Windows.Forms.Button();
            this.radioButton_CR = new System.Windows.Forms.RadioButton();
            this.radioButton_NoEOL = new System.Windows.Forms.RadioButton();
            this.radioButton_CRLF = new System.Windows.Forms.RadioButton();
            this.textBox_Receive = new System.Windows.Forms.TextBox();
            this.groupBox_ReceiveLog = new System.Windows.Forms.GroupBox();
            this.checkBox_Log2file = new System.Windows.Forms.CheckBox();
            this.checkBox_LogToWindow = new System.Windows.Forms.CheckBox();
            this.buttonClearReceive = new System.Windows.Forms.Button();
            this.textBox_ServerIP = new System.Windows.Forms.TextBox();
            this.label_ServerIP = new System.Windows.Forms.Label();
            this.numericUpDown_Port = new System.Windows.Forms.NumericUpDown();
            this.label_Port = new System.Windows.Forms.Label();
            this.groupBox_Server = new System.Windows.Forms.GroupBox();
            this.button_ConnectDisconnect = new System.Windows.Forms.Button();
            this.timer_10ms = new System.Windows.Forms.Timer(this.components);
            this.groupBox_Send.SuspendLayout();
            this.groupBox_ReceiveLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Port)).BeginInit();
            this.groupBox_Server.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(13, 38);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(85, 24);
            this.buttonSend.TabIndex = 0;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textBox_Send
            // 
            this.textBox_Send.Location = new System.Drawing.Point(13, 17);
            this.textBox_Send.Name = "textBox_Send";
            this.textBox_Send.Size = new System.Drawing.Size(640, 20);
            this.textBox_Send.TabIndex = 1;
            // 
            // groupBox_Send
            // 
            this.groupBox_Send.Controls.Add(this.radioButton_LF);
            this.groupBox_Send.Controls.Add(this.buttonClearSend);
            this.groupBox_Send.Controls.Add(this.radioButton_CR);
            this.groupBox_Send.Controls.Add(this.radioButton_NoEOL);
            this.groupBox_Send.Controls.Add(this.radioButton_CRLF);
            this.groupBox_Send.Location = new System.Drawing.Point(3, 2);
            this.groupBox_Send.Name = "groupBox_Send";
            this.groupBox_Send.Size = new System.Drawing.Size(658, 64);
            this.groupBox_Send.TabIndex = 2;
            this.groupBox_Send.TabStop = false;
            this.groupBox_Send.Text = "Send data to server";
            // 
            // radioButton_LF
            // 
            this.radioButton_LF.AutoSize = true;
            this.radioButton_LF.Location = new System.Drawing.Point(335, 40);
            this.radioButton_LF.Name = "radioButton_LF";
            this.radioButton_LF.Size = new System.Drawing.Size(37, 17);
            this.radioButton_LF.TabIndex = 14;
            this.radioButton_LF.TabStop = true;
            this.radioButton_LF.Text = "LF";
            this.radioButton_LF.UseVisualStyleBackColor = true;
            // 
            // buttonClearSend
            // 
            this.buttonClearSend.Location = new System.Drawing.Point(585, 36);
            this.buttonClearSend.Name = "buttonClearSend";
            this.buttonClearSend.Size = new System.Drawing.Size(66, 23);
            this.buttonClearSend.TabIndex = 0;
            this.buttonClearSend.Text = "Clear";
            this.buttonClearSend.UseVisualStyleBackColor = true;
            this.buttonClearSend.Click += new System.EventHandler(this.buttonClearSend_Click);
            // 
            // radioButton_CR
            // 
            this.radioButton_CR.AutoSize = true;
            this.radioButton_CR.Location = new System.Drawing.Point(289, 40);
            this.radioButton_CR.Name = "radioButton_CR";
            this.radioButton_CR.Size = new System.Drawing.Size(40, 17);
            this.radioButton_CR.TabIndex = 13;
            this.radioButton_CR.TabStop = true;
            this.radioButton_CR.Text = "CR";
            this.radioButton_CR.UseVisualStyleBackColor = true;
            // 
            // radioButton_NoEOL
            // 
            this.radioButton_NoEOL.AutoSize = true;
            this.radioButton_NoEOL.Location = new System.Drawing.Point(157, 40);
            this.radioButton_NoEOL.Name = "radioButton_NoEOL";
            this.radioButton_NoEOL.Size = new System.Drawing.Size(63, 17);
            this.radioButton_NoEOL.TabIndex = 11;
            this.radioButton_NoEOL.TabStop = true;
            this.radioButton_NoEOL.Text = "No EOL";
            this.radioButton_NoEOL.UseVisualStyleBackColor = true;
            // 
            // radioButton_CRLF
            // 
            this.radioButton_CRLF.AutoSize = true;
            this.radioButton_CRLF.Location = new System.Drawing.Point(226, 40);
            this.radioButton_CRLF.Name = "radioButton_CRLF";
            this.radioButton_CRLF.Size = new System.Drawing.Size(57, 17);
            this.radioButton_CRLF.TabIndex = 12;
            this.radioButton_CRLF.TabStop = true;
            this.radioButton_CRLF.Text = "CR/LF";
            this.radioButton_CRLF.UseVisualStyleBackColor = true;
            // 
            // textBox_Receive
            // 
            this.textBox_Receive.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox_Receive.Location = new System.Drawing.Point(160, 87);
            this.textBox_Receive.Multiline = true;
            this.textBox_Receive.Name = "textBox_Receive";
            this.textBox_Receive.ReadOnly = true;
            this.textBox_Receive.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_Receive.Size = new System.Drawing.Size(491, 263);
            this.textBox_Receive.TabIndex = 3;
            // 
            // groupBox_ReceiveLog
            // 
            this.groupBox_ReceiveLog.Controls.Add(this.checkBox_Log2file);
            this.groupBox_ReceiveLog.Controls.Add(this.checkBox_LogToWindow);
            this.groupBox_ReceiveLog.Controls.Add(this.buttonClearReceive);
            this.groupBox_ReceiveLog.Location = new System.Drawing.Point(150, 72);
            this.groupBox_ReceiveLog.Name = "groupBox_ReceiveLog";
            this.groupBox_ReceiveLog.Size = new System.Drawing.Size(511, 308);
            this.groupBox_ReceiveLog.TabIndex = 4;
            this.groupBox_ReceiveLog.TabStop = false;
            this.groupBox_ReceiveLog.Text = "Received from server";
            // 
            // checkBox_Log2file
            // 
            this.checkBox_Log2file.AutoSize = true;
            this.checkBox_Log2file.Enabled = false;
            this.checkBox_Log2file.Location = new System.Drawing.Point(121, 284);
            this.checkBox_Log2file.Name = "checkBox_Log2file";
            this.checkBox_Log2file.Size = new System.Drawing.Size(72, 17);
            this.checkBox_Log2file.TabIndex = 11;
            this.checkBox_Log2file.Text = "Log to file";
            this.checkBox_Log2file.UseVisualStyleBackColor = true;
            // 
            // checkBox_LogToWindow
            // 
            this.checkBox_LogToWindow.AutoSize = true;
            this.checkBox_LogToWindow.Location = new System.Drawing.Point(10, 284);
            this.checkBox_LogToWindow.Name = "checkBox_LogToWindow";
            this.checkBox_LogToWindow.Size = new System.Drawing.Size(95, 17);
            this.checkBox_LogToWindow.TabIndex = 6;
            this.checkBox_LogToWindow.Text = "Log to window";
            this.checkBox_LogToWindow.UseVisualStyleBackColor = true;
            this.checkBox_LogToWindow.CheckedChanged += new System.EventHandler(this.checkBox_LogToWindow_CheckedChanged);
            // 
            // buttonClearReceive
            // 
            this.buttonClearReceive.Location = new System.Drawing.Point(425, 279);
            this.buttonClearReceive.Name = "buttonClearReceive";
            this.buttonClearReceive.Size = new System.Drawing.Size(57, 23);
            this.buttonClearReceive.TabIndex = 5;
            this.buttonClearReceive.Text = "Clear";
            this.buttonClearReceive.UseVisualStyleBackColor = true;
            this.buttonClearReceive.Click += new System.EventHandler(this.buttonClearReceive_Click);
            // 
            // textBox_ServerIP
            // 
            this.textBox_ServerIP.Location = new System.Drawing.Point(27, 20);
            this.textBox_ServerIP.Name = "textBox_ServerIP";
            this.textBox_ServerIP.Size = new System.Drawing.Size(109, 20);
            this.textBox_ServerIP.TabIndex = 5;
            this.textBox_ServerIP.Text = "127.0.0.1";
            // 
            // label_ServerIP
            // 
            this.label_ServerIP.AutoSize = true;
            this.label_ServerIP.Location = new System.Drawing.Point(9, 23);
            this.label_ServerIP.Name = "label_ServerIP";
            this.label_ServerIP.Size = new System.Drawing.Size(17, 13);
            this.label_ServerIP.TabIndex = 6;
            this.label_ServerIP.Text = "IP";
            // 
            // numericUpDown_Port
            // 
            this.numericUpDown_Port.Location = new System.Drawing.Point(42, 51);
            this.numericUpDown_Port.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown_Port.Name = "numericUpDown_Port";
            this.numericUpDown_Port.Size = new System.Drawing.Size(94, 20);
            this.numericUpDown_Port.TabIndex = 7;
            // 
            // label_Port
            // 
            this.label_Port.AutoSize = true;
            this.label_Port.Location = new System.Drawing.Point(10, 55);
            this.label_Port.Name = "label_Port";
            this.label_Port.Size = new System.Drawing.Size(26, 13);
            this.label_Port.TabIndex = 8;
            this.label_Port.Text = "Port";
            // 
            // groupBox_Server
            // 
            this.groupBox_Server.Controls.Add(this.label_Port);
            this.groupBox_Server.Controls.Add(this.numericUpDown_Port);
            this.groupBox_Server.Controls.Add(this.textBox_ServerIP);
            this.groupBox_Server.Controls.Add(this.label_ServerIP);
            this.groupBox_Server.Location = new System.Drawing.Point(3, 72);
            this.groupBox_Server.Name = "groupBox_Server";
            this.groupBox_Server.Size = new System.Drawing.Size(141, 84);
            this.groupBox_Server.TabIndex = 9;
            this.groupBox_Server.TabStop = false;
            this.groupBox_Server.Text = "Server IP / port";
            // 
            // button_ConnectDisconnect
            // 
            this.button_ConnectDisconnect.Location = new System.Drawing.Point(12, 162);
            this.button_ConnectDisconnect.Name = "button_ConnectDisconnect";
            this.button_ConnectDisconnect.Size = new System.Drawing.Size(127, 42);
            this.button_ConnectDisconnect.TabIndex = 10;
            this.button_ConnectDisconnect.Text = "CONNECT";
            this.button_ConnectDisconnect.UseVisualStyleBackColor = true;
            this.button_ConnectDisconnect.Click += new System.EventHandler(this.button_ConnectDisconnect_Click);
            // 
            // timer_10ms
            // 
            this.timer_10ms.Interval = 10;
            this.timer_10ms.Tick += new System.EventHandler(this.timer_10ms_Tick);
            // 
            // FormTestTcpClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 382);
            this.Controls.Add(this.button_ConnectDisconnect);
            this.Controls.Add(this.textBox_Receive);
            this.Controls.Add(this.textBox_Send);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.groupBox_Send);
            this.Controls.Add(this.groupBox_ReceiveLog);
            this.Controls.Add(this.groupBox_Server);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormTestTcpClient";
            this.Text = "Test TCP/IP client";
            this.groupBox_Send.ResumeLayout(false);
            this.groupBox_Send.PerformLayout();
            this.groupBox_ReceiveLog.ResumeLayout(false);
            this.groupBox_ReceiveLog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Port)).EndInit();
            this.groupBox_Server.ResumeLayout(false);
            this.groupBox_Server.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox textBox_Send;
        private System.Windows.Forms.GroupBox groupBox_Send;
        private System.Windows.Forms.TextBox textBox_Receive;
        private System.Windows.Forms.GroupBox groupBox_ReceiveLog;
        private System.Windows.Forms.Button buttonClearSend;
        private System.Windows.Forms.Button buttonClearReceive;
        private System.Windows.Forms.TextBox textBox_ServerIP;
        private System.Windows.Forms.Label label_ServerIP;
        private System.Windows.Forms.NumericUpDown numericUpDown_Port;
        private System.Windows.Forms.Label label_Port;
        private System.Windows.Forms.GroupBox groupBox_Server;
        private System.Windows.Forms.Button button_ConnectDisconnect;
        private System.Windows.Forms.RadioButton radioButton_LF;
        private System.Windows.Forms.RadioButton radioButton_CR;
        private System.Windows.Forms.RadioButton radioButton_NoEOL;
        private System.Windows.Forms.RadioButton radioButton_CRLF;
        private System.Windows.Forms.CheckBox checkBox_Log2file;
        private System.Windows.Forms.CheckBox checkBox_LogToWindow;
        private System.Windows.Forms.Timer timer_10ms;
    }
}

