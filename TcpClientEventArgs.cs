using System.Net.Sockets;

namespace Server_Shopping_cart.TCP_Server
{
    public class TcpClientEventArgs
    {
        private TcpClient client;

        public TcpClientEventArgs(TcpClient client)
        {
            this.client = client;
        }
    }
}