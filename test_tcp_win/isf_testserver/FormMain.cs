using System;
using System.Windows.Forms;

using isflib;

namespace isf_testserver
{
    public partial class FormMain : Form
    {
        const string strApp = "ISF Test Server v1.0.0 26092016_1637";

        const UInt16 PORT_DEFAULT = 12345;

        UInt16 port;

        bool bInitError = false;

        TcpIpServer_2 server = null;

        bool bServerRun = false;

        uint secTimer = 0;

        public FormMain()
        {
            InitializeComponent();

            this.Text = strApp + "/" + isflib.isflib.GetVersion();

            port = PORT_DEFAULT;

            server = new TcpIpServer_2(GetISFclient);

            this.Text = this.Text + "; ClientInServer_ToTest";
            Console.WriteLine(this.Text);
        }

        // On Form closing
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bServerRun)
            {
                // if the GUI log handler is not unsuscribed here, exception may occur
                server.SubscribeLogHandler(null); 

                server.Stop(2000);
            }
        }

        // On Form shown
        private void Form1_Shown(object sender, EventArgs e)
        {
            if (bInitError)
            {
                this.Close();
                return;
            }

            timer1.Start();

            server.SubscribeConnectHandler(OnClientConnected);
            server.SubscribeDisconnectHandler(OnClientDisconnected);
            server.SubscribeLogHandler( OnLogServer );
            server.SubscribeLogHandler_Clients(OnLogServer);
        }

        // Disconnect connected client specified by its ID 
        private void DisconnectClient(int aClientId)
        {
            Console.WriteLine("Client to disconnect {0}", aClientId);
            server.DisconnectClient(aClientId);
            checkListBox_ConnectedClients.Items.Remove(ClientID_to_String(aClientId));
        }


        // Disconnect selected client
        private void button_DisconnectSelectedClient_Click(object sender, EventArgs e)
        {
            int n = checkListBox_ConnectedClients.SelectedIndex;

            if (n == -1)
            {
                MessageBox.Show("No client is selected.");
            }
            else
            {
                DisconnectClient(String_to_ClientID(checkListBox_ConnectedClients.Items[n].ToString()));
            }

        }

        // Disconnect all clients
        private void button_DickonnectAllClients_Click(object sender, EventArgs e)
        {
            for (int i = checkListBox_ConnectedClients.Items.Count - 1; i >= 0; i--)
            {
                DisconnectClient(String_to_ClientID(checkListBox_ConnectedClients.Items[i].ToString()));
            }
        }

        string ClientID_to_String(int client_id)
        {
            return String.Format("Client #{0:D4}", client_id);
        }

        int String_to_ClientID(string s)
        {
            int client_id = -1;
            try
            {
                client_id = int.Parse(s.Replace("Client #", ""));
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return client_id;
        }
 
        // Log delegate for the server
        delegate void OnLogServer_Delegate(string format, params Object[] args);
        public void OnLogServer(string format, params Object[] args)
        {
            string s = string.Format(format, args);

            if (txbox_ServerLog.InvokeRequired)
            {
              Invoke(new OnLogServer_Delegate(OnLogServer), new object[] { format, args }); 
            }
            else
            {
                txbox_ServerLog.Text += s + "\r\n";  // Every logstate as line, add LF CR
                txbox_ServerLog.SelectionStart = txbox_ServerLog.TextLength;
                txbox_ServerLog.ScrollToCaret();

            }
        }

        // Server start /stop
        private void button_ServerStart_Click(object sender, EventArgs e)
        {
            if (!bServerRun)
            {
                // Server will be started

                if (server.Start(port))
                {
                    button_ServerStart.Text = "Stop Server";
                    bServerRun = true;
                }
                else
                {
                    MessageBox.Show("Cannot start server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Server will be stopped

                if (server.Stop(2000))
                {
                    button_ServerStart.Text = "Start Server";
                    bServerRun = false;
                }
                else
                {
                    MessageBox.Show("Cannot stop server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Timer 500 ms
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bServerRun)
            {
                txtbxServerRun.Text = (secTimer%2==0)? ("Server running at port " + port) : "";
            }
            else
            {
                txtbxServerRun.Text = "";
            }

            secTimer++;
        }

        // Clear server log
        private void button_ClearLog_Click(object sender, EventArgs e)
        {
            txbox_ServerLog.Text = "";
        }


//
//    Connect / Disconnect client handlers
//
        delegate bool OnClientConnected_Delegate(int id);
        bool OnClientConnected(int id)
        {
            if (checkListBox_ConnectedClients.InvokeRequired)
            {
                Invoke(new OnClientConnected_Delegate(OnClientConnected), new object[] { id });
            }
            else
            {
                checkListBox_ConnectedClients.Items.Add(ClientID_to_String(id));
                
            }

            return true;
        }

        delegate bool OnClientDisconnected_Delegate(int id);
        public bool OnClientDisconnected(int id)
        {
            if (checkListBox_ConnectedClients.InvokeRequired)
            {
                Invoke(new OnClientDisconnected_Delegate(OnClientDisconnected), new object[] { id });
            }
            else
            {
                checkListBox_ConnectedClients.Items.Remove(ClientID_to_String(id));
            }

            return true;
        }

        public ClientInServer GetISFclient()
        {
            return new ClientInServer_ToTest();
        }

    }   // class

}       // namespace

// EOF


