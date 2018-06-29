using System;
using System.Windows.Forms;

using isflib;

namespace test_tcpclient00
{
    public partial class FormTestTcpClient : Form
    {
        string sVer = "Test TCP/IP client v1.0.0 04022016_1653";

        SimpleTcpClient client = new SimpleTcpClient();

        bool bConnected = false;

        bool bLogToWindow = false;

        public FormTestTcpClient()
        {
            InitializeComponent();

            numericUpDown_Port.Value = 12345;

            radioButton_NoEOL.Checked = true;

            SetGUIstate(bConnected);

            this.Text = sVer;

            timer_10ms.Start();
        }

        /// <summary>
        /// On button Connect / Disconnect to the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ConnectDisconnect_Click(object sender, EventArgs e)
        {
            string err;

            bool bError = true;
            string s = "Unknown error";

            if(!bConnected)
            {
                // try to connect
                if (client.Connect(textBox_ServerIP.Text, (int)numericUpDown_Port.Value, out err))
                {
                    s = "Successfuly connected to the server";
                    bConnected = true;
                    bError = false;
                }
                else
                {
                    s = "Failed to connect to server: " + err;
                }
            }
            else
            {
                // always disconnect on error (the client object will be disconnected anyways after calling Disconnect()
                bConnected = false;
                // try to disconnect
                if (client.Disconnect(out err))
                {
                    s = "Successfuly disconnected from the server";
                    bError = false;
                }
                else
                {
                    s = "Failed to disconnect from server: " + err;
                }
            }

            if (bError)
            {
                MessageBox.Show(s, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            textBox_Receive.Text += "DEBUG > " + s + "\r\n";
            textBox_Receive.SelectionStart = textBox_Receive.TextLength;
            textBox_Receive.ScrollToCaret();

            button_ConnectDisconnect.Text = bConnected ? "DISCONNECT" : "CONNECT";

            SetGUIstate(bConnected);
        }

        /// <summary>
        /// On button Send to the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSend_Click(object sender, EventArgs e)
        {
            string err = "";

            if (bConnected)
            {
                if (!client.Send(textBox_Send.Text + GetEOL(), out err))
                {
                    MessageBox.Show("Error while sending to server: " + err, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Not connected to the server!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// On clear Send string
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClearSend_Click(object sender, EventArgs e)
        {
            textBox_Send.Text = "";
        }

        /// <summary>
        /// On clear receive log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClearReceive_Click(object sender, EventArgs e)
        {
            textBox_Receive.Text = "";
        }

        /// <summary>
        /// Show the received data from server into the log window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_LogToWindow_CheckedChanged(object sender, EventArgs e)
        {
            bLogToWindow = checkBox_LogToWindow.Checked;
        }

        

        /// <summary>
        /// 10 ms GUI timer tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        uint nTimer_10ms = 0;
        private void timer_10ms_Tick(object sender, EventArgs e)
        {
            string err = "";
            if (bConnected)
            { 
                string s;
                if (client.Receive(out s, out err))
                {
                    if (s != "")
                    {
                        textBox_Receive.Text += s;
                    }
                }
                else
                { 
                    // error handling, client socket seems to be disconnected
                    client.Disconnect(out err);
                    textBox_Receive.Text += err + "\r\n";
                }
                textBox_Receive.SelectionStart = textBox_Receive.TextLength;
                textBox_Receive.ScrollToCaret();
            }

            // Check every 1 second (1000 ms) if the socket is still connected
            if(nTimer_10ms % 100 == 0)  
            {
                if (bConnected)
                {
                    if (!client.IsStillConnected())
                    {
                        bConnected = false;
                        SetGUIstate(bConnected);
                        textBox_Receive.Text += "DEBUG > " + "Disconnected from server.\r\n";
                        textBox_Receive.SelectionStart = textBox_Receive.TextLength;
                        textBox_Receive.ScrollToCaret();
                    }
                }
            }
            nTimer_10ms++;
        }



    } // class FormTestTcpClient

}  // namespace test_tcpclient00


// EOF


