using System;
using System.Text;

using System.Net;
using System.Net.Sockets;

namespace isflib
{
    public class SimpleTcpClient 
    {

        const int POLL_DISCONNECTED_PERIOD_MS = 1000;

        TcpClient client = null;
        NetworkStream stream = null;

        int checkConnectedTime_ms = 0;

        #region Public Methods

        /// <summary>
        /// Connect to the specified server
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool Connect(string ip, int port, out string err)
        {
            bool bRet = false;
            
            err = "";

            if (IsConnected())
            {
                err = "Already connected";
                return bRet;
            }

            try
            {
                client = new TcpClient();
                client.Connect(IPAddress.Parse(ip), port);
                
                stream = client.GetStream();

                if (IsConnected())
                {
                    bRet = true;
                }
                else
                {
                    SetDisconnected();
                    err = "Failed to connect t the server";
                    return bRet;                
                }
            }
            catch(Exception e)
            {
                SetDisconnected();
                err = e.Message;
                bRet = false;
            }
            
            return bRet;
        }

        /// <summary>
        /// Disconnect from server
        /// </summary>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool Disconnect(out string err)
        {
            bool bRet = false;
            err = "";

            if (!IsConnected())
            {
                err = "Not connected";
                return bRet;
            }

            try
            {
                // Close everything.
                stream.Close(); // in older isf tcpClien used Close(5000);
                client.Close();
                bRet = true;
            }
            catch (Exception e)
            {
                err = e.Message;
                bRet = false;
            }
            SetDisconnected();

            return bRet;
        }

        /// <summary>
        /// Send string to server
        /// </summary>
        /// <param name="s"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool Send(string s, out string err)
        {
            bool bRet = false;
            err = "";

            if (!IsConnected())
            {
                err = "Not connected";
                return bRet;
            }

            try
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(s);
                stream.Write(data, 0, data.Length);

                bRet = true;
            }
            catch (Exception e)
            {
                err = e.Message;
                bRet = false;
            }

            return bRet;        
        }

        /// <summary>
        /// Receive string from server
        /// </summary>
        /// <param name="s"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool Receive(out string s, out string err)
        {
            bool bRet = false;

            err = "";
            s = "";

            if (!IsConnected())
            {
                err = "Not connected";
            }
            else
            {
                try
                {                   
                    if (stream.CanRead)
                    {
                        byte[] readBuffer = new byte[1024];
                        StringBuilder completeMessage = new StringBuilder();
                        int numberOfBytesRead = 0;

                        // Incoming message may be larger than the buffer size ! 
                        // TODO: Check the amount of data available!
                        while (stream.DataAvailable)
                        {
                            numberOfBytesRead = stream.Read(readBuffer, 0, readBuffer.Length);
                            completeMessage.AppendFormat("{0}", Encoding.ASCII.GetString(readBuffer, 0, numberOfBytesRead));
                        }
                        s = completeMessage.ToString();
                    }
                    bRet = true;
                }
                catch (Exception e)
                {
                    err = e.Message;
                }
            }

            return bRet;
        }

        /// <summary>
        /// Check if the underlying socket is still connected
        /// </summary>
        /// <returns></returns>
        public bool IsStillConnected()
        {
            if (!IsConnected())
            {
                // Has not been connected 
                return false;
            }

            if (Environment.TickCount > checkConnectedTime_ms)
            {
                checkConnectedTime_ms = Environment.TickCount + POLL_DISCONNECTED_PERIOD_MS;
                try
                {
                    if (client.Client.Poll(0, SelectMode.SelectRead))
                    {
                        byte[] buff = new byte[1];
                        int ret = client.Client.Receive(buff, SocketFlags.Peek);
                        if (ret == 0)
                        {
                            // disconnected
                            SetDisconnected();
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

        #endregion Public Methods


        #region Private Methods

        /// <summary>
        /// Check if the client socket is considered connected:
        /// both client and stream object are null
        /// </summary>
        /// <returns></returns>
        bool IsConnected()
        {
            if(client != null && stream != null)
                return true;

            SetDisconnected();
            return false;
        }

        /// <summary>
        /// Set both client and stream object are null
        /// to indicate the socket is disconnected
        /// </summary>
        void SetDisconnected()
        {
            client = null; 
            stream = null;
        }

        #endregion Private Methods


    }   // SimpleTcpClient

}   //  namespace isflib


// EOF
