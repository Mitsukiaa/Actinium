using System.Net;
using System.Net.Sockets;

namespace Actinium.Sockets
{
    public class UDP_socket : IDisposable
    {
        public IPAddress Ip { get; }
        public ushort Port { get; }
        private UdpClient? udpClient;

        public UDP_socket(IPAddress ip_addr, ushort port)
        {
            Ip = ip_addr;
            Port = port;
            InitializeSocket();
        }

        private void InitializeSocket()
        {
            try
            {
                udpClient = new UdpClient();
                udpClient.Client.Bind(new IPEndPoint(Ip, Port));
            }
            catch (Exception ex)
            {
                throw new SocketInitializationException("Error initializing UDP socket.", ex);
            }
        }

        public UdpClient? GetSocket()
        {
            return udpClient;
        }

        public void Dispose()
        {
            udpClient?.Close();
        }
    }
}
