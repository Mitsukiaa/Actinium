using System.Net;
using System.Net.Sockets;

namespace Actinium.Sockets
{
    public class TCP_socket : IDisposable
    {
        public IPAddress Ip { get; }
        public ushort Port { get; }
        private TcpListener? tcpListener;
        private TcpClient? tcpClient;

        public TCP_socket(IPAddress ip_addr, ushort port)
        {
            Ip = ip_addr;
            Port = port;
            InitializeSocket();
        }

        private void InitializeSocket()
        {
            try
            {
                tcpListener = new TcpListener(Ip, Port);
                tcpListener.Start();
                tcpClient = new TcpClient();
            }
            catch (Exception ex)
            {
                throw new SocketInitializationException("Error initializing TCP socket.", ex);
            }
        }

        public TcpClient? GetSocket()
        {
            return tcpClient;
        }

        public void Dispose()
        {
            tcpListener?.Stop();
            tcpClient?.Close();
        }
    }
}
