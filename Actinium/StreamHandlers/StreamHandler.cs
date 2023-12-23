using System.Net;
using System.Net.Sockets;
using Actinium.Sockets;

namespace Actinium.StreamHandlers
{
    public class StreamHandler : IDisposable
    {
        private TcpClient? tcpClient;
        private UdpClient? udpClient;
        public Int32 BufferSize { get; set; } = 1024;

        public StreamHandler(TCP_socket tcpSocket)
        {
            tcpClient = tcpSocket.GetSocket();
        }

        public StreamHandler(UDP_socket udpSocket)
        {
            udpClient = udpSocket.GetSocket();
        }

        public void SendData(byte[] data)
        {
            if (tcpClient != null)
            {
                NetworkStream stream = tcpClient.GetStream();
                stream.Write(data, 0, data.Length);
            }
            else if (udpClient != null)
            {
                udpClient.Send(data, data.Length);
            }
            else
            {
                throw new InvalidOperationException("No socket available to send data.");
            }
        }

        public byte[] ReceiveData()
        {
            if (tcpClient != null)
            {
                NetworkStream stream = tcpClient.GetStream();
                byte[] buffer = new byte[BufferSize];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                byte[] receivedData = new byte[bytesRead];
                Array.Copy(buffer, receivedData, bytesRead);
                return receivedData;
            }
            else if (udpClient != null)
            {
                IPEndPoint remoteEndPoint = null;
                byte[] receivedData = udpClient.Receive(ref remoteEndPoint);
                return receivedData;
            }
            else
            {
                throw new InvalidOperationException("No socket available to receive data.");
            }
        }

        public void Dispose()
        {
            tcpClient?.Close();
            udpClient?.Close();
        }
    }
}
