using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerProject
{
    internal class ServerClass
    {
        private const int MainServerPort = 8088;
        private const string CommonChatName = "Common";
        private const int ClientsLimits = 15;
        private const int CommonChatId = 0;
        private string name;
        private List<ClientHandler> clients;
        private List<RoomCreateInfo> rooms;
        private List<Messages> messageHistory;
        private Socket tcpSocket;
        private Socket udpSocket;
        private Thread listenUdpThread;
        private Thread listenTcpThread;
        private IMessagesSerializer messageSerializer;

        public ServerClass(IMessagesSerializer messageSerializer)
        {
            this.messageSerializer = messageSerializer;
            clients = new List<ClientHandler>();
            messageHistory = new List<Messages>();
            listenUdpThread = new Thread(ListenUdp);
            listenTcpThread = new Thread(ListenTcp);
            rooms = new List<RoomCreateInfo>();
        }

        public void Close()
        {
            FunctionsCommon.CloseAndNullSocket(ref tcpSocket);
            FunctionsCommon.CloseAndNullSocket(ref udpSocket);
            FunctionsCommon.CloseAndNullThread(ref listenTcpThread);
            FunctionsCommon.CloseAndNullThread(ref listenUdpThread);
        }

        public void ListenTcp()
        {
            int receivedDataBytesCount;
            byte[] receivedDataBuffer;
            tcpSocket.Listen(ClientsLimits);
            while (true)
            {
                try
                {
                    Socket connectedSocket = tcpSocket.Accept();
                    receivedDataBuffer = new byte[1024];
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        do
                        {
                            receivedDataBytesCount = connectedSocket.Receive(receivedDataBuffer, receivedDataBuffer.Length, SocketFlags.None);
                            memoryStream.Write(receivedDataBuffer, 0, receivedDataBytesCount);
                        }
                        while (udpSocket.Available > 0);
                        if (receivedDataBytesCount > 0)
                            HandleReceivedMessage(messageSerializer.Deserialize(memoryStream.ToArray()), connectedSocket);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        public void ListenUdp()
        {
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 0);
            EndPoint endPoint = ipEndPoint;
            int receivedDataBytesCount;
            byte[] receivedDataBuffer;
            while (true)
            {
                try
                {
                    receivedDataBuffer = new byte[1024];
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        do
                        {
                            receivedDataBytesCount = udpSocket.ReceiveFrom(receivedDataBuffer, receivedDataBuffer.Length, SocketFlags.None, ref endPoint);
                            memoryStream.Write(receivedDataBuffer, 0, receivedDataBytesCount);
                        }
                        while (udpSocket.Available > 0);
                        if (receivedDataBytesCount > 0)
                            HandleReceivedMessage(messageSerializer.Deserialize(memoryStream.ToArray()));
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        private bool SetupUdpAndTcpLocalIp()
        {
            udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpSocket.EnableBroadcast = true ;
            tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint localUdpIp = new IPEndPoint(IPAddress.Any, MainServerPort);
            IPEndPoint localTcpIp = new IPEndPoint(FunctionsCommon.GetCurrrentHostIp(), MainServerPort);
            try
            {
                udpSocket.Bind(localUdpIp);
                tcpSocket.Bind(localTcpIp);
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }

        }

        public void Start()
        {
            Console.Write("Server name: ");
            name = Console.ReadLine();
            if (SetupUdpAndTcpLocalIp())
            {
                WriteLine("Server is ready!");
                listenUdpThread.Start();
                listenTcpThread.Start();
            }
            else
            {
                WriteLine("Error, server cant be satrted");
            }
        }

        public void RemoveConnection(ClientHandler disconnectedClient)
        {
            if (clients.Remove(disconnectedClient))
                WriteLine("\"" + disconnectedClient.name + "\"" + " left from the chat, Dead end:)");
            SendMessageToAllClients(GetParticipantsListMessage());
        }

        public void SendMessageToAllClients(Messages message)
        {
            foreach (ClientHandler clientHandler in clients)
            {
                SendMessageToClient(message, clientHandler);
            }
        }

        public void SendMessageToClient(Messages message, ClientHandler clientHandler)
        {
            clientHandler.tcpSocket.Send(messageSerializer.Serialize(message));
        }

        private void HandleClientUdpRequestMessage(ClientUdpRequestMessages clientUdpRequestMessage)
        {
            ServerUdpAnswerMessages serverUdpAnswerMessage = GetServerUdpAnswerMessage();
            IPEndPoint clientEndPoint = new IPEndPoint(clientUdpRequestMessage.SenderIp, clientUdpRequestMessage.SenderPort);
            Socket serverUdpAnswerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            serverUdpAnswerSocket.SendTo(messageSerializer.Serialize(serverUdpAnswerMessage), clientEndPoint);
        }

        private ClientHandler GetClientHandler(RegistrationMessages registrationMessage, Socket connectedSocket)
        {
            ClientHandler clientHandler = new ClientHandler(registrationMessage.ClientName, connectedSocket, GetClientUniqueId(), messageSerializer);
            clientHandler.ReceiveMessageEvent += HandleReceivedMessage;
            clientHandler.ClientDisconnectedEvent += RemoveConnection;
            clients.Add(clientHandler);
            clientHandler.StartListenTcp();
            return clientHandler;
        }

        private void HandleRegistrationMessage(RegistrationMessages registrationMessage, Socket connectedSocket)
        {
            ClientHandler clientHandler = GetClientHandler(registrationMessage, connectedSocket);
            SendMessageToClient(GetSendIdMessage(clientHandler), clientHandler);
            SendMessageToAllClients(GetParticipantsListMessage());
            SendMessageToAllClients(GetMessagesHistoryMessage());
        }

        private void HandleCommonChatMessage(CommonChatMessages commonChatMessage)
        {
            messageHistory.Add(commonChatMessage);
            SendMessageToAllClients(commonChatMessage);
            SendMessageToAllClients(GetMessagesHistoryMessage());
        }

        private void HandleIndividualChatMessage(IndividualChatMessages individualChatMessage)
        {
            foreach (ClientHandler clientHandler in clients)
            {
                if (clientHandler.id == individualChatMessage.ReceiverId)
                {
                    SendMessageToClient(individualChatMessage, clientHandler);
                    break;
                }
            }
        }

        private int AddNewRoom(CreateRoomRequestMessage createRoomRequestMessage)
        {
            var roomName = createRoomRequestMessage.RoomName;
            var roomParticipantsIndecies = createRoomRequestMessage.RoomParticipantsIndecies;
            var room = new RoomCreateInfo(roomName, roomParticipantsIndecies);
            rooms.Add(room);
            return rooms.IndexOf(room);
        }

        private bool IsRoomParticipantClient(int roomId, int clientId)
        {
            var room = rooms[roomId];
            var roomPerticipantIndecies = room.RoomParticipants;
            foreach (var participantIndex in roomPerticipantIndecies)
            {
                if (clientId == participantIndex)
                {
                    return true;
                }
            }
            return false;
        }

        private void HandleCreateRommRequestMessage(CreateRoomRequestMessage createRoomRequestMessage)
        {
            var roomId = AddNewRoom(createRoomRequestMessage);
            var roomName = createRoomRequestMessage.RoomName;
            var createRoomResponseMessage = GetCreateRoomResponseMessage(roomId, roomName);
            foreach (var clientHandler in clients)
            {
                if (IsRoomParticipantClient(roomId, clientHandler.id))
                {
                    SendMessageToClient(createRoomResponseMessage, clientHandler);
                }
            }
        }

        private void HandleRoomMessage(RoomMessage roomMessage)
        {
            foreach (var clientHandler in clients)
            {
                if (IsRoomParticipantClient(roomMessage.RoomId, clientHandler.id))
                {
                    SendMessageToClient(roomMessage, clientHandler);
                }
            }
        }

        public void HandleReceivedMessage(Messages message)
        {
            if (message is ClientUdpRequestMessages)
            {
                ClientUdpRequestMessages clientUdpRequestMessage = (ClientUdpRequestMessages)message;               
                HandleClientUdpRequestMessage(clientUdpRequestMessage);              
            }
            if (message is CreateRoomRequestMessage)
            {
                CreateRoomRequestMessage createRoomRequestMessage = (CreateRoomRequestMessage)message;
                HandleCreateRommRequestMessage(createRoomRequestMessage);
            }
            if (message is RoomMessage)
            {
                RoomMessage roomMessage = (RoomMessage)message;
                HandleRoomMessage(roomMessage);
            } 
            else if (message is IndividualChatMessages)
            {
                IndividualChatMessages individualChatMessage = (IndividualChatMessages)message;
                HandleIndividualChatMessage(individualChatMessage);
            }
            else if (message is CommonChatMessages)
            {
                CommonChatMessages commonChatMessage = (CommonChatMessages)message;
                WriteLine("\"" + GetName(commonChatMessage.SenderId) + "\": " + commonChatMessage.Content);  
                HandleCommonChatMessage(commonChatMessage);            
            }
        }

        public void HandleReceivedMessage(Messages message, Socket connectedSocket)
        {
            if (message is RegistrationMessages)
            {
                RegistrationMessages registrationMessage = (RegistrationMessages)message;
                WriteLine("\"" + registrationMessage.ClientName + "\" has join the server");
                HandleRegistrationMessage(registrationMessage, connectedSocket);               
            }
        }

        private string GetName(int id)
        {
            foreach (ClientHandler clientHandler in clients)
            {
                if (id == clientHandler.id)
                {
                    return clientHandler.name;
                }
            }
            return null;
        }

        private void WriteLine(string content)
        {
            Console.WriteLine("[" + DateTime.Now.ToString() + "]: " + content + ";");
        }

        private CreateRoomResponseMessage GetCreateRoomResponseMessage(int roomId, string roomName)
        {
            IPEndPoint serverIp = (IPEndPoint)(tcpSocket.LocalEndPoint);
            return new CreateRoomResponseMessage(DateTime.Now, serverIp.Address, serverIp.Port, new RoomInfo(roomName, roomId, new List<Messages>()));
        }

        private ServerUdpAnswerMessages GetServerUdpAnswerMessage() 
        {
            return new ServerUdpAnswerMessages(DateTime.Now, FunctionsCommon.GetCurrrentHostIp(), MainServerPort, name);
        }

        private SendIdMessages GetSendIdMessage(ClientHandler clientHandler)
        {
            IPEndPoint serverIp = (IPEndPoint)tcpSocket.LocalEndPoint;
            return new SendIdMessages(DateTime.Now, serverIp.Address, serverIp.Port, clientHandler.id);
        }

        private MessagesHistoryMessage GetMessagesHistoryMessage()
        {
            IPEndPoint serverIp = (IPEndPoint)(tcpSocket.LocalEndPoint);
            return new MessagesHistoryMessage(DateTime.Now, serverIp.Address, serverIp.Port, messageHistory);
        }

        private ParticipantsListMessages GetParticipantsListMessage()
        {
            List<NewChatParticipant> participantsList = new List<NewChatParticipant>();
            participantsList.Add(new NewChatParticipant(CommonChatName, CommonChatId, new List<Messages>()));
            foreach (ClientHandler clientHandler in clients)
            {
                participantsList.Add(new NewChatParticipant(clientHandler.name, clientHandler.id, new List<Messages>()));
            }
            IPEndPoint serverIp = (IPEndPoint)(tcpSocket.LocalEndPoint);
            ParticipantsListMessages participantsListMessage = new ParticipantsListMessages(DateTime.Now, serverIp.Address, serverIp.Port, participantsList);
            return participantsListMessage;
        }

        private int GetClientUniqueId()
        {
            int clientsCount = 0;
            foreach (ClientHandler clientHandler in clients)
            {
                clientsCount++;
            }
            return clientsCount + 1;
        }

        ~ServerClass()
        {
            Close();
        }
    }
}
