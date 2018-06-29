using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

using System.Net;
using System.Net.Sockets;
using System.Threading;

using System.Timers;

namespace isflib
{
    public delegate ClientInServer GetISFclient();


    public class TcpIpServer_2
    {
        const int MONITOR_CLIENTS_INTERVAL_MS = 1200;

        // private data members
        protected TcpListener tcpListener;
        protected Thread listenThread;

        Object logLock = new Object();

        protected bool _serverRun = false;
        protected bool serverRun
        {
            set 
            {
                lock (logLock)
                {
                    _serverRun = value;
                }
            }

            get 
            {
                bool b = _serverRun;

                lock (logLock)
                {
                    b = _serverRun;
                }

                return b;  
            }
        }

        //ClientProtocolsSupported protocol = ClientProtocolsSupported.eNOT_SUPPORTED;
        GetISFclient CreateClient = null;

        OnLogString logHdr = null;

        OnLogString logHdrClients = null;

        OnClientReceived recHdrClients = null;

        // TODO: introduce limitation for maximum number of sockets
        int cur_sock_id = 0;

//
//   Client collections
//
        public ConcurrentDictionary<int, ClientInServer> clients = new ConcurrentDictionary<int, ClientInServer>();

        OnClientSocketConnect connectHandler = null;
        OnClientSocketDisconnect disconnectHandler = null;

        System.Timers.Timer timer2monitorClients = null;

        //
        //   Public methods
        //

        //public TcpIpServer_2(ClientProtocolsSupported prtcl, GetISFclient create_client = null)
        public TcpIpServer_2(GetISFclient create_client = null)
        {
            CreateClient = create_client;

            // TODO: Check for fatal error(!): (prtcl == ClientProtocolsSupported.eNOT_SUPPORTED && create_client == null)

            timer2monitorClients = new System.Timers.Timer();
            timer2monitorClients.Elapsed += new ElapsedEventHandler(OnMonitorClients_TimerEvent);
            timer2monitorClients.Interval = MONITOR_CLIENTS_INTERVAL_MS;
            timer2monitorClients.Enabled = true;

            LOG_LINE("SERVER >> TcpIpServer_2()");
        }

        private void OnMonitorClients_TimerEvent(object source, ElapsedEventArgs e)
        {
            if (serverRun)
            { 
                foreach(var client in clients)
                {
                    if (!client.Value.connected)
                    {
                        ClientInServer cl;
                        int k = client.Key;
                        if (clients.TryRemove(k, out cl))
                        {
                            if(disconnectHandler != null)
                                disconnectHandler(k);
                            LOG_LINE("SERVER >> Client {0} disconnected and removed.", k);
                        }
                        else
                        {
                            LOG_LINE("SERVER >> ERROR: Failed to remove disconnected client {0}.", k);                        
                        }
                    }
                }
            }
        }

        public bool Start(int port)
        {
            try
            {
                this.tcpListener = new TcpListener(IPAddress.Any, port);
                this.listenThread = new Thread(new ThreadStart(ListenForClients));
                serverRun = true;
                this.listenThread.Start();
            }
            catch(Exception e)
            {
                LOG_LINE("SERVER >> Start()-> ERROR:" + e.ToString());
                serverRun = false;
                return false;
            }
            LOG_LINE("SERVER >> Start()-> Started at port {0}", port);


            return true;
        }
        
        //
        //  Stop the server, timeout to wait for the listener thread to be terminated
        //
        public bool Stop(int timeout)
        {
            try
            {
                serverRun = false;
                this.tcpListener.Stop();
                listenThread.Join(timeout);

                foreach (KeyValuePair<int, ClientInServer> client in clients)
                {
                    if (client.Value.Disconnect())
                    {
                        LOG_LINE("SERVER >> Client {0} disconnected.", client.Key);
                    }
                    else
                    {
                        LOG_LINE("SERVER >> ERROR: Client {0} cannot be disconnected.", client.Key);                    
                    }

                }
                clients.Clear();
            }
            catch (Exception e)
            {
                LOG_LINE("SERVER >> Stop():" + e.ToString());
                return false;
            }
            LOG_LINE("SERVER >> Stop()-> Stopped.");

            return true;
        }


//
// find client of client_id
//
        public ClientInServer Id_to_Client(int client_id)
        {
            try
            {
                foreach (KeyValuePair<int, ClientInServer> client in clients)
                {
                    if (client.Value.id == client_id)
                    {
                        return client.Value;
                    }
                }
            }
            catch 
            {
            }
            return null;
        }


        public bool DisconnectClient(int client_id)
        {
            try
            {
                ClientInServer client = Id_to_Client(client_id);
                if (client.Disconnect())
                {
                    LOG_LINE("SERVER >> Client {0} disconnected.", client_id);
                }
                else
                {
                    LOG_LINE("SERVER >> ERROR: Client {0} cannot be disconnected.", client_id);
                }

                // TO DO: Clear deletes all the clients - we must delete only the disconnected client !!!!
//              clients.Clear(); 
//                clients.TryRemove(client);
            }
            catch (Exception e)
            {
                LOG_LINE("SERVER >> DisconnectClient():" + e.ToString());
                return false;
            }

            return true;
        }

//
//   Private methods
//

        //  Listening thread
        private void ListenForClients()
        {
            this.tcpListener.Start();

            Socket socket = null;

            while (serverRun)
            {
                LOG_LINE("SERVER >> ListenForClients()-> Listening for clients...");
                
                // Blocks until a client has connected to the server
                try
                {
                    while ((serverRun) && (!this.tcpListener.Pending()))
                    {
                        Thread.Sleep(10);
                    }
                    if (serverRun)
                    {
                        socket = this.tcpListener.AcceptSocket();
                        LOG_LINE("SERVER >> Client socket accepted.");
                    }
                }
                catch (Exception e)
                {
                    serverRun = false;
                    LOG_LINE("SERVER >> ListenForClients()-> ERROR:" + e.ToString());
                }
                if (!serverRun)
                {
                    LOG_LINE("SERVER >> ListenForClients()-> Server was terminated. "); 
                    break;      // server was terminated
                }

                ClientInServer client = CreateClient();

                // Add the socket to the global client sockets list
                clients[cur_sock_id] = client; 
                // Add also GUI window for the socket

                // pass the clients log handler delagate(e.g. in the Clients Log form) to this client

                client.SubscribeLogHandler(logHdrClients);
                client.SubscribeRecHandler(recHdrClients);

                // start handling the client - communication thread will be start internally 
                client.StartHandlingClient(cur_sock_id, socket, logHdrClients); 

                LOG_LINE("SERVER >> Client # {0} from IP:{1} at port {2} connected and communication started.",
                                    cur_sock_id,
                                    ((IPEndPoint)(socket.RemoteEndPoint)).Address.ToString(),
                                    ((IPEndPoint)(socket.LocalEndPoint)).Port);

                if (connectHandler != null)
                    connectHandler(cur_sock_id);

                cur_sock_id++;

                // go listen for the next client
            }

            LOG_LINE("SERVER >> ListenForClients()-> Listenning thread finished. "); 
        }


        public void SubscribeConnectHandler(OnClientSocketConnect connect_hdr)
        {
            connectHandler = connect_hdr;
        }

        public void SubscribeDisconnectHandler(OnClientSocketDisconnect disconnect_hdr)
        {
            disconnectHandler = disconnect_hdr;
        }

        public void SubscribeLogHandler(OnLogString log_hdr)
        {
            logHdr = log_hdr;
        }

        void LOG_LINE(string format, params Object[] args)
        {
            // Write to GUI
            if (logHdr != null)
                logHdr(format, args);

            // Write to console
            Console.WriteLine(format, args);

        }

        public void SubscribeLogHandler_Clients(OnLogString log_hdr_clients)
        {
            logHdrClients = log_hdr_clients;
        }

        public void SubscribeRecHandler_Clients(OnClientReceived rec_hdr_clients)
        {
            recHdrClients = rec_hdr_clients;
        }

    }   // TcpIpServer

}   //  namespace SimpleClientServer


// EOF
