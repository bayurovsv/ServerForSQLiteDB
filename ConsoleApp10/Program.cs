using ServerDB.Aplication;

namespace ServerDB
{
    class Program
    {
        static int port = 8005;
        /// <summary>Путь сохранения</summary>
        static string ip = "192.168.1.2";
        static void Main(string[] args)
        {
            ServerControlService serverControl = new ServerControlService(ip, port);
            serverControl.Active();
        }
    }
}
