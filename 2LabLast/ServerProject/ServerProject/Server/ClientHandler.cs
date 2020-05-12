using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerProject
{
    public class ClientHandler
    {
        public string name;
        public Socket tcpSocket;
        public int id;
        private Thread listenTcpThread;
        private IMessagesSerializer messageSerializer;
        public delegate void ReceiveMessageDelegate(Messages message);
        public event ReceiveMessageDelegate ReceiveMessageEvent;
        public delegate void ClientDisconnected(ClientHandler clientHandler);
        public event ClientDisconnected ClientDisconnectedEvent;

        public ClientHandler(string name, Socket socket, int id, IMessagesSerializer messageSerializer)
        {
            this.messageSerializer = messageSerializer;
            this.name = name;
            this.id = id;
            tcpSocket = socket;
            listenTcpThread = new Thread(ListenTcp);
        }

        public void StartListenTcp()
        {
            listenTcpThread.Start();
        }

        private void ListenTcp()
        {
            int ReceiveDataBytesCount;
            byte[] ReceiveDataBuffer;
            while (true)
            {
                try
                {
                    ReceiveDataBuffer = new byte[1024];
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        do
                        {
                            ReceiveDataBytesCount = tcpSocket.Receive(ReceiveDataBuffer, ReceiveDataBuffer.Length, SocketFlags.None);
                            memoryStream.Write(ReceiveDataBuffer, 0, ReceiveDataBytesCount);
                        }
                        while (tcpSocket.Available > 0);
                        if (ReceiveDataBytesCount > 0)
                            ReceiveMessageEvent(messageSerializer.Deserialize(memoryStream.ToArray()));
                    }
                }
                catch (SocketException)
                {
                    ClientDisconnectedEvent(this);
                    FunctionsCommon.CloseAndNullSocket(ref tcpSocket);
                    FunctionsCommon.CloseAndNullThread(ref listenTcpThread);            
                }
            }
        }
    }
}
