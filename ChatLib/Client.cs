using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

using ChatLogger;
using Interfaces;

namespace ChatLib
{
    public class Client
    {
        private TcpClient client;
        private NetworkStream netstream;

        //private Logger logger = new Logger();
        private ILoggingService logger;
        public event GetMessageHandler GetMsg;

        private bool flag { get; set; }
        public bool Flag
        {
            get { return flag; }
            set { this.flag = value; }
        }

        /// <summary>
        /// Client Class Constructor
        /// </summary>
        /// <param name="server">The IP Address for the server to connect to</param>
        /// <param name="port">The port number to connect to</param>
        public Client(string server, int port, ILoggingService logger) 
        {
            try
            {
                this.logger = logger;
                this.client = new TcpClient(server, port);
            }
            catch(System.Exception)
            { }
            
        }//end constructor

        
        /// <summary>
        /// Start up new client's NetworkStream
        /// </summary>
        public void Start() 
        {
            flag = true;
            netstream = client.GetStream();
            logger.Log("New Conversation Started:");
            
        }

        /// <summary>
        /// Close the TcpClient, and NetworkStream instances
        /// </summary>
        public void Close() 
        {
            flag = false;
            netstream.Close();
            client.Close();
            logger.Log("END TRANSMISSION");
        }

        /// <summary>
        /// Send message to server using tcp. Converts string input to 
        /// ascii bytes for transmission over NetworkStream
        /// </summary>
        /// <param name="msg">Message input by Client to be sent to Server</param>
        public void Send(string msg) 
        {
            //package and send player message data to server
            byte[] data = System.Text.Encoding.ASCII.GetBytes("Player: " + msg + "\n");
            netstream.Write(data, 0, data.Length);

            //send to logger as well
            logger.Log("Player: " + msg);
        }


        /// <summary>
        /// Loop to receive messages from Server
        /// </summary>
        public void Receive() 
        {
            string msgIn = "";
            while(flag)
            {
                //TODO handle NULL Stream exception
                while (netstream.DataAvailable)
                {
                    // Buffer to store the response bytes.
                    byte[] data = new byte[1024];

                    // Read the first batch of the TcpServer response bytes.
                    int bytes = netstream.Read(data, 0, data.Length);
                    msgIn += System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                }

                if (msgIn.Length > 0)
                {
                    GetMsg(new GetMessageEventArgs("Computer: " + msgIn));
                    logger.Log("Computer: " + msgIn);
                    msgIn = "";
                }
            }
        }//end receive

    }
}
