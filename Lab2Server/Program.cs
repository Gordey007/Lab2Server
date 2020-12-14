using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Lab2Server;

namespace SocketServer2
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketServer socketServer = new SocketServer();
            socketServer.start(11000);
        }
    }
}