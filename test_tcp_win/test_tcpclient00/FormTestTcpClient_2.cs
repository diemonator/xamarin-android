using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using isflib;

namespace test_tcpclient00
{
    public partial class FormTestTcpClient : Form
    {
        /// <summary>
        /// Set GUI controls, depending on the connection state of the client
        /// </summary>
        /// <param name="bConnected"></param>
        void SetGUIstate(bool bConnected)
        {
            if (bConnected)
            {
                button_ConnectDisconnect.Text = "DISCONNECT";
                buttonSend.Enabled = true;
                textBox_ServerIP.Enabled = false;
                numericUpDown_Port.Enabled = false;
            }
            else
            {
                button_ConnectDisconnect.Text = "CONNECT";
                buttonSend.Enabled = false;
                textBox_ServerIP.Enabled = true;
                numericUpDown_Port.Enabled = true;
            }
        
        }

        /// <summary>
        /// Get the current EOL configured to be added to the send frames
        /// </summary>
        string GetEOL()
        {
            string s = "";

            if (radioButton_NoEOL.Checked)
            {
                // do nothing
            }
            else if (radioButton_CRLF.Checked)
            {
                s = "\r\n";
            }
            else if (radioButton_CR.Checked)
            {
                s = "\r";
            }
            else if (radioButton_LF.Checked)
            {
                s = "\n";
            }

            return s;
        }

    } // class FormTestTcpClient

}  // namespace test_tcpclient00


// EOF


