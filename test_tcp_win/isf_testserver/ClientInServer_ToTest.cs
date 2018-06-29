using isflib;

namespace isf_testserver
{
    public class ClientInServer_ToTest : ClientInServer
    {
        const int MAX_BYTES_REC = 512;

        bool bFirstMsg2client = true;

        override protected void DoCommunicationProtocol(out bool bDisconnect)
        {
            byte[] buff;

            if(bFirstMsg2client)
            {
                if ( !Send("[ Server accepted client with id #" + id + " ]\n") )
                    LOG_LINE("DoCommunicationProtocol() -> ERROR: Send() failed.\r\n");

                bFirstMsg2client = false;
            }
            
            int ret = ReceiveAvailable(out buff, MAX_BYTES_REC); 
            if (ret > 0)
            {
                LOG_LINE("CLIENT[{0}] SENT:{1}", id, System.Text.Encoding.UTF8.GetString(buff));

                if(buff == null)
                {
                    LOG_LINE("DoCommunicationProtocol() -> ERROR: ReceiveAvailable() failed.\r\n");
                }
                else 
                {
                    if (!Send(buff))
                        LOG_LINE("DoCommunicationProtocol() -> ERROR: Send() failed.\r\n");
                }
            }
            bDisconnect = false;

        }



    }   // ClientInServer_Echo

}   //  namespace isflib_001


// EOF
