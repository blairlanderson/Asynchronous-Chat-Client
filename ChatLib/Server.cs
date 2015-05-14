using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

namespace ChatLib
{
    public class Server
    {
        private TcpClient client;
        private TcpListener serv;
        private NetworkStream netstream;

        public Server(string server, int port) { }

        public void start() { }
        public void close() { }

        public void send() { }
        public string receive() { return ""; }



    }
}
