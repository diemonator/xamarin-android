using System;
using System.Text;

using System.Net.Sockets;
using System.Threading;

namespace isflib
{
    public delegate bool OnClientSocketConnect(int id);
    public delegate bool OnClientSocketDisconnect(int id);

    public delegate bool OnClientReceived(int id, byte[] recBuf);

    public class ClientInServer
    {
        const int POLL_DISCONNECTED_PERIOD_MS = 2500;

        bool bInit = false;

        public int id = 0;

        protected Socket clientSocket = null;
        protected OnLogString logHdr = null;

        protected OnClientReceived onRecHdr = null;

        int checkConnectedTime_ms = 0;

        protected Thread clientThread;

        Object cl_lock = new Object();

        protected bool _connected = false;
        public bool connected
        {
            set
            {
                lock (cl_lock)
                {
                    _connected = value;
                }
            }

            get
            {
                bool b = _connected;

                lock (cl_lock)
                {
                    b = _connected;
                }

                return b;
            }
        }


        public bool StartHandlingClient(int id_client, 
                                        Socket client_socket,
                                        OnLogString log_hdr)
        {
            id = id_client;

            clientSocket = client_socket;
            logHdr = log_hdr;

            bInit = true;

            try
            {
                connected = true;

                clientThread = new Thread(new ThreadStart(CommThread));
                clientThread.Start();
            }
            catch (Exception e)
            {
                LOG_LINE( "CLIENT >>> StartHandlingClient()-> ERROR:" + e.ToString() );
                connected = false;
                return false;
            }

            return true;
        }

        //  Client communication thread with the server
        private void CommThread()
        {
            LOG_LINE("CLIENT >>> Client {0} in server started . . .", id);

            checkConnectedTime_ms = Environment.TickCount + POLL_DISCONNECTED_PERIOD_MS;
            while (connected)
            {
                try
                {
                    Thread.Sleep(1);

                    bool disconnect;
                    DoCommunicationProtocol(out disconnect);
                    
                    if (disconnect)
                    {
                        clientSocket.Disconnect(false);
                        connected = false;
                        LOG_LINE("CLIENT >>> Protocol request to disconnected client {0} from server.", id);
                        break;
                    }

                    if (!IsConnected())
                    {
                        connected = false;
                        LOG_LINE("CLIENT >>> Client {0} has disconnected from server.", id);  
                        break;
                    }
                }
                catch (Exception e)
                {
                    connected = false;
                    LOG_LINE("CLIENT >>> ClientServer CommThread()-> ERROR:" + e.ToString());
                }                
            }
            OnDisconnect();


        }

        public bool Disconnect()
        {
            if (!bInit) return false;
            connected = false;

            clientThread.Join(2500);

            // TODO: Decide if the next line must be actually here - what if client is already disconnected?
            clientSocket.Disconnect(false); 

            return true;
        }

            
        virtual protected void DoCommunicationProtocol(out bool bDisconnect)
        {
            // IMPORTANT: 
            // In the derived class implement the communication protocol.
            // Use the Send and Receive methods implemented here to exchange data
            // according to the protocol logic. Implement specific decode and encode
            // methods in the client protocol derived class 

            bDisconnect = true;
            
        }

        virtual protected void OnDisconnect()
        {

        }

        protected bool IsConnected()
        {
            if (Environment.TickCount > checkConnectedTime_ms)
            {
                checkConnectedTime_ms = Environment.TickCount + POLL_DISCONNECTED_PERIOD_MS;


                //bool part1 = clientSocket.Poll(1000, SelectMode.SelectRead);
                //bool part2 = (clientSocket.Available == 0);
                //if (part1 & part2)
                //    return false;
                //else
                //    return true;
                try
                {
                    if (clientSocket.Poll(0, SelectMode.SelectRead))
                    {
                        byte[] buff = new byte[1];
                        int ret = clientSocket.Receive(buff, SocketFlags.Peek);
                        if (ret == 0)
                        {
                            // disconnected
                            return false;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Socket may be disconnected . . . {0}", e.Message);
                    return false;
                }
            }
            // still connected
            return true;
        }

        public bool Send(byte[] buffer)
        {
            if (!connected) return false;

            try
            {
                if( clientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None) != buffer.Length)
                    return false;
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.WouldBlock ||
                    ex.SocketErrorCode == SocketError.IOPending ||
                    ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                {
                    // socket buffer is probably full, wait and try again
                }
                return false;
            }
            
            return true;
        }

        protected bool Send(string s)
        {
            if (!connected) return false;

            byte[] buffer = Encoding.ASCII.GetBytes(s);

            try
            {
                if (clientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None) != buffer.Length)
                    return false;
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.WouldBlock ||
                    ex.SocketErrorCode == SocketError.IOPending ||
                    ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                {
                    // socket buffer is probably full, wait and try again
                }
                return false;
            }

            return true;
        }

        protected int ReceiveAvailable(out byte[] buff, int bytes2receive)
        {
            int ret = -1;

            if (clientSocket.Available != 0)
            {
                int bytes2read = clientSocket.Available;
                buff = new byte[bytes2read];
                int n = clientSocket.Receive(buff, bytes2read, SocketFlags.None);

                if (n != bytes2read)
                {
                    LOG_LINE("CLIENT >>> ReceiveAvailable() -> ERROR: Expected {0} bytes, received {1} bytes!",
                        bytes2read, n);
                }
                else
                    ret = n;
            }
            else
                buff = null;
            return ret;
        }

        protected bool ReceiveAvailable(out byte[] buff, out bool err)
        {
            err = false; // no error
            buff = null;

            if (clientSocket.Available != 0)
            {
                int bytes2read = clientSocket.Available;
                buff = new byte[bytes2read];
                int n = clientSocket.Receive(buff, bytes2read, SocketFlags.None);

                if (n != bytes2read)
                {
                    LOG_LINE("CLIENT >>> ReceiveAvailable() -> ERROR: Expected {0} bytes, received {1} bytes!",
                        bytes2read, n);
                    err = true;    // error
                }
                else
                {
                    return true;    // bytes received
                }
            }

            return false;
        }

        protected int ReceiveDataOnTimeout(ref byte[] buff, int bytes2receive, int timeoutMs)
        {
            int startTickCount = Environment.TickCount;
            int received = 0;  // how many bytes is already received
            int offset = 0;

            do
            {
                if (Environment.TickCount > startTickCount + timeoutMs)
                {
                    // timeout
                    return received;
                }
                try
                {
                    Thread.Sleep(1);
                    received += clientSocket.Receive(buff, offset + received, bytes2receive - received, SocketFlags.None);
                    if (!clientSocket.Connected)
                    {
                        // client might have disconnected
                        return -1;
                    }
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.WouldBlock ||
                        ex.SocketErrorCode == SocketError.IOPending ||
                        ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                    {
                        // socket buffer is probably empty, wait and try again
                        Thread.Sleep(30);
                    }
                    else
                    {
                        LOG_LINE("CLIENT >>> ReceiveDataOnTimeout()-> ERROR:" + ex.ToString());
                        return -1;  // any serious error occurr
                    }
                }
                catch (Exception e)
                {
                    LOG_LINE("CLIENT >>> ReceiveDataOnTimeout()-> ERROR:" + e.ToString());
                    return -1;
                }
            }
            while (received < bytes2receive);

            return received;
        }

        protected int ReceiveDataOnTimeout(ref byte[] buff, int timeoutMs)
        {
            int startTickCount = Environment.TickCount;
            int received = 0;  // how many bytes is already received

            while (true)
            {
                if (Environment.TickCount > startTickCount + timeoutMs)
                {
                    // timeout
                    return received;
                }
                try
                {
                    Thread.Sleep(1);
                    // TODO: try use, ReceiveAvailable(out byte[] buff, int bytes2receive),  but first fix second input param: bytes2receive!
                    if (!clientSocket.Connected)
                    {
                        // client might have disconnected
                        return -1;
                    }

                    int bytes2read = clientSocket.Available;
                    if (bytes2read > 0)
                    {
                        return clientSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
                    }
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.WouldBlock ||
                        ex.SocketErrorCode == SocketError.IOPending ||
                        ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                    {
                        // socket buffer is probably empty, wait and try again
                        Thread.Sleep(30);
                    }
                    else
                    {
                        LOG_LINE("CLIENT >>> ReceiveDataOnTimeout()-> ERROR:" + ex.ToString());
                        return -1;  // any serious error occurr
                    }
                }
                catch (Exception e)
                {
                    LOG_LINE("CLIENT >>> ReceiveDataOnTimeout()-> ERROR:" + e.ToString());
                    return -1;
                }
            }
        }

        protected void LOG_LINE(string format, params Object[] args)
        {
            if (logHdr != null)
                logHdr(format, args);

            Console.WriteLine(format, args);

        }

        public void SubscribeLogHandler(OnLogString log_hdr)
        {
            logHdr = log_hdr;
        }

        public void SubscribeRecHandler(OnClientReceived rec_hdr)
        {
            onRecHdr = rec_hdr;
        }

        protected void LogPacket(string packName, byte[] data)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("[{0}, ", packName);
            sb.AppendFormat(" {0} bytes : ]", data.Length);

            foreach (byte bt in data)
                sb.AppendFormat(" {0:X2}", bt);

            LOG_LINE(sb.ToString());
        }

    }   // ClientInServer

}   //  namespace isflib_001


// EOF
