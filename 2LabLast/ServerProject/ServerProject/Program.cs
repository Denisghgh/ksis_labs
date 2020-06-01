using CommonLibrary;
using FileSharingLibrary;

namespace ServerProject
{
    class Program
    {
        private const string FileSharingServerUrl = "http://localhost:8888/";

        static void Main(string[] args)
        {
            ServerClass server = new ServerClass(new BinaryMessagesSerializer());
            FileSharingServer fileSharingServer = new FileSharingServer(FileSharingServerUrl);
            server.Start();
            fileSharingServer.Start();
        }
    }
}
