using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ClientProject
{
    public delegate void ReceiveMessage(Messages message);
    public delegate void DeleteRoomDelegate(int roomId);

    public class ClientClass
    {
        private const int ServerPort = 8088;
        public int id;

        private List<ServerInfo> serversInfo;
        public List<NewChatParticipant> participants;
        public List<RoomInfo> rooms;

        private Socket tcpSocket;
        private Socket udpSocket;
        private Thread listenUdpThread;
        private Thread listenTcpThread;
        private IMessagesSerializer messageSerializer;

        public event ReceiveMessage ReceiveMessageEvent;
        public event NotReadedMessageCountDelegate UnreadMessageEvent;
        public event ReadMessageDelegate ReadMessageEvent;
        public event DeleteRoomDelegate DeleteRoomEvent;

        public ClientClass(IMessagesSerializer messageSerializer)
        {
            this.messageSerializer = messageSerializer;
            serversInfo = new List<ServerInfo>();
            participants = new List<NewChatParticipant>();
            tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpSocket.EnableBroadcast = true;
            listenUdpThread = new Thread(ListenUdp);
            listenTcpThread = new Thread(ListenTcp);
            rooms = new List<RoomInfo>();
        }

        public void ConnectToServer(int serverIndex, string clientName)
        {
            try
            {
                if ((serverIndex >= 0) && (serverIndex <= serversInfo.Count - 1))
                {
                    tcpSocket.Connect(GetServerIpEndPoint(serverIndex));
                    listenTcpThread.Start();
                    SendMessage(GetRegistrationMessage(clientName));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Close();
            }
        }

        public void DisconnectFromServer()
        {
            FunctionsCommon.CloseAndNullSocket(ref tcpSocket);
            FunctionsCommon.CloseAndNullThread(ref listenTcpThread);
        }

        private void AddNewServerInfo(ServerUdpAnswerMessages serverUdpAnswerMessage)
        {
            serversInfo.Add(new ServerInfo(serverUdpAnswerMessage.ServerName, serverUdpAnswerMessage.SenderIp, serverUdpAnswerMessage.SenderPort));
        }

        private void SetEventsForParticipants()
        {
            foreach (NewChatParticipant chatParticipant in participants)
            {
                chatParticipant.NotReadedMessageEvent += UnreadMessageEvent;
                chatParticipant.ReadedMessageEvent += ReadMessageEvent;
            }
        }

        private void HandleParticipantsListMessage(ParticipantsListMessages participantsListMessage)
        {
            participants = participantsListMessage.participants;
            SetEventsForParticipants();
        }

        private void HandleMessagesHistoryMessage(MessagesHistoryMessage messagesHistoryMessage)
        {
            if (participants.Count != 0)
                participants[0].MessageHistory = messagesHistoryMessage.MessagesHistory;
        }

        private void HandleChatMessage(CommonChatMessages commonChatMessage)
        {
            if (commonChatMessage is IndividualChatMessages)
            {
                IndividualChatMessages individualChatMessage = (IndividualChatMessages)commonChatMessage;
                foreach (NewChatParticipant chatParticipant in participants)
                {
                    if (chatParticipant.Id == individualChatMessage.SenderId)
                    {
                        chatParticipant.MessageHistory.Add(individualChatMessage);
                        chatParticipant.NotReadedMessageCountIncrement(individualChatMessage);
                        break;
                    }
                }
            }
            else
            {
                participants[0].NotReadedMessageCountIncrement(commonChatMessage);
            }
        }

        private void AddNewRoom(CreateRoomResponseMessage createRoomResponseMessage)
        {
            rooms.Add(createRoomResponseMessage.RoomInfo);
        }

        private void HandleRoomMessage(RoomMessage roomMessage)
        {
            var room = rooms[roomMessage.RoomId];
            room.MessageHistory.Add(roomMessage);
        }

        private void HandleRoomParticipantsMessage(RoomParticipantsMessage message)
        {
            var room = rooms[message.RoomId];
            room.Participants = message.Participants;
        }

        public void HandleReceivedMessage(Messages message)
        {
            if (message is RoomParticipantsMessage)
            {
                HandleRoomParticipantsMessage((RoomParticipantsMessage)message);
            }
            if (message is ServerUdpAnswerMessages)
            {
                AddNewServerInfo((ServerUdpAnswerMessages)message);
            }
            if (message is CreateRoomResponseMessage)
            {
                AddNewRoom((CreateRoomResponseMessage)message);
            }
            if (message is ParticipantsListMessages)
            {
                HandleParticipantsListMessage((ParticipantsListMessages)message);
            }
            if (message is MessagesHistoryMessage)
            {
                HandleMessagesHistoryMessage((MessagesHistoryMessage)message);
                return;
            }
            if (message is RoomMessage)
            {
                HandleRoomMessage((RoomMessage)message);
            }
            else if ((message is IndividualChatMessages) || (message is CommonChatMessages))
            {
                HandleChatMessage((CommonChatMessages)message);
            }
            if (message is SendIdMessages)
            {
                id = ((SendIdMessages)message).Id;
                return;
            }
            ReceiveMessageEvent(message);
        }

        public void ListenTcp()
        {
            int receivedDataBytesCount;
            byte[] receivedDataBuffer;
            try
            {
                while (true)
                {
                    receivedDataBuffer = new byte[1024];
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        do
                        {
                            receivedDataBytesCount = tcpSocket.Receive(receivedDataBuffer, receivedDataBuffer.Length, SocketFlags.None);
                            memoryStream.Write(receivedDataBuffer, 0, receivedDataBytesCount);
                        }
                        while (tcpSocket.Available > 0);
                        if (receivedDataBytesCount > 0)
                            HandleReceivedMessage(messageSerializer.Deserialize(memoryStream.ToArray()));
                    }
                }
            }
            catch (SocketException)
            {
                DisconnectFromServer();
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
                catch (SocketException)
                {
                    Close();
                }
            }
        }

        public void SearchServers()
        {
            IPEndPoint broadcastEndPoint = new IPEndPoint(IPAddress.Broadcast, ServerPort);
            IPEndPoint localIp = new IPEndPoint(IPAddress.Any, 0);
            udpSocket.Bind(localIp);
            Socket sendUdpRequest = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sendUdpRequest.EnableBroadcast = true;
            sendUdpRequest.SendTo(messageSerializer.Serialize(GetClientUdpRequestMessage()), broadcastEndPoint);
            listenUdpThread.Start();
        }

        public void SendMessage(string content, int selectedDialog)
        {
            if (participants[selectedDialog].Id == 0)
            {
                tcpSocket.Send(messageSerializer.Serialize(GetCommonChatMessage(content)));
            }
            else
            {
                IndividualChatMessages individualChatMessage = GetIndividualChatMessage(content, participants[selectedDialog].Id);
                if (individualChatMessage.SenderId != individualChatMessage.ReceiverId)
                {
                    tcpSocket.Send(messageSerializer.Serialize(individualChatMessage));
                }
                participants[selectedDialog].MessageHistory.Add(individualChatMessage);
                ReceiveMessageEvent(individualChatMessage);
            }
        }

        public void SendRoomMessage(string content, int selectedRoom)
        {
            var roomMessage = GetRoomMessage(content, selectedRoom);
            SendMessage(roomMessage);
        }

        public void SendMessage(Messages message)
        {
            tcpSocket.Send(messageSerializer.Serialize(message));
        }

        public void SendCreateRoomRequestMessage(string roomName, List<int> roomParticipantsIndecies)
        {
            var createRoomRequestMessage = GetCreateRoomRequestMessage(roomName, roomParticipantsIndecies);
            SendMessage(createRoomRequestMessage);
        }

        public void SendExitRoomMessage(int roomId)
        {
            var exitRoomMessage = GetExitRoomMessage(roomId);
            SendMessage(exitRoomMessage);
            rooms.RemoveAt(roomId);
            DeleteRoomEvent(roomId);
        }

        public void Close()
        {
            FunctionsCommon.CloseAndNullSocket(ref tcpSocket);
            FunctionsCommon.CloseAndNullSocket(ref udpSocket);
            FunctionsCommon.CloseAndNullThread(ref listenTcpThread);
            FunctionsCommon.CloseAndNullThread(ref listenUdpThread);
        }

        private ExitRoomMessage GetExitRoomMessage(int roomId)
        {
            IPEndPoint clientIp = (IPEndPoint)(tcpSocket.LocalEndPoint);
            return new ExitRoomMessage(DateTime.Now, clientIp.Address, clientIp.Port, roomId, id);
        }

        private RoomMessage GetRoomMessage(string content, int selectedRoom)
        {
            IPEndPoint clientIp = (IPEndPoint)(tcpSocket.LocalEndPoint);
            return new RoomMessage(DateTime.Now, clientIp.Address, clientIp.Port, content, id, selectedRoom);
        }

        private CreateRoomRequestMessage GetCreateRoomRequestMessage(string roomName, List<int> roomParticipantsIndecies)
        {
            IPEndPoint clientIp = (IPEndPoint)(tcpSocket.LocalEndPoint);
            return new CreateRoomRequestMessage(DateTime.Now, clientIp.Address, clientIp.Port, roomParticipantsIndecies, roomName);
        }

        private IndividualChatMessages GetIndividualChatMessage(string content, int receiverId)
        {
            IPEndPoint clientIp = (IPEndPoint)(tcpSocket.LocalEndPoint);
            return new IndividualChatMessages(DateTime.Now, clientIp.Address, clientIp.Port, content, id, receiverId);
        }

        private CommonChatMessages GetCommonChatMessage(string content)
        {
            IPEndPoint clientIp = (IPEndPoint)(tcpSocket.LocalEndPoint);
            return new CommonChatMessages(DateTime.Now, clientIp.Address, clientIp.Port, content, id);
        }

        private ClientUdpRequestMessages GetClientUdpRequestMessage()
        {
            IPEndPoint localIp = (IPEndPoint)udpSocket.LocalEndPoint;
            return new ClientUdpRequestMessages(DateTime.Now, FunctionsCommon.GetCurrrentHostIp(), localIp.Port);
        }

        private IPEndPoint GetServerIpEndPoint(int serverIndex)
        {
            ServerInfo serverInfo = GetServerInfo(serverIndex);
            return new IPEndPoint(serverInfo.ServerIp, serverInfo.ServerPort);
        }

        private RegistrationMessages GetRegistrationMessage(string clientName)
        {
            IPEndPoint clientIp = (IPEndPoint)(tcpSocket.LocalEndPoint);
            return new RegistrationMessages(DateTime.Now, clientIp.Address, clientIp.Port, clientName);
        }

        private ServerInfo GetServerInfo(int serverIndex)
        {
            if ((serverIndex >= 0) && (serverIndex <= serversInfo.Count - 1))
            {
                return serversInfo[serverIndex];
            }
            return null;
        }
    }
}
