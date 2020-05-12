using CommonLibrary;

namespace ServerProject
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerClass server = new ServerClass(new BinaryMessagesSerializer());
            server.Start();
        }
    }
}
