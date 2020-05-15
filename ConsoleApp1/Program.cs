using ConsoleApp1.Aplication;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ConsoleApp1
{
    public struct ApplicationInfo
    {
        public string Application { set; get; }
        public string AppVersion { set; get; }
        public string AppDiscription { set; get; }
        public string Flag { set; get; }
    }
    public struct Applications
    {
        public string ApplicationName { set; get; }
    }
    public struct FilesContains
    {
        public string Applications { set; get; }
        public string AppVersions { set; get; }
        public string FilePath { set; get; }
        public string DateCreateDB { set; get; }
        public byte[] Contains { set; get; }
    }
    public struct FilesVersions
    {
        public string AppVersions { set; get; }
        public string FileHash { set; get; }
        public string CreationDate { set; get; }
        public int FileSize { set; get; }
        public string FilePath { set; get; }
        public string DateCreateDB { set; get; }
        public string Applications { set; get; }
    }
    class Program
    {
        static int port = 8005; // порт сервера
        static string address = "192.168.1.2"; // адрес сервера
        static void Main(string[] args)
        {
            ClientControlService clientControl = new ClientControlService(address, port);
            SQLiteCommand command = new SQLiteCommand();
            command.CommandText = "INSERT INTO Applications (ApplicationName) VALUES(@ApplicationName)";
            command.Parameters.Add("@ApplicationName", DbType.String).Value = "Test21";
            var test1 = clientControl.Insert(command);
            SQLiteCommand command1 = new SQLiteCommand();
            command1.CommandText = "SELECT * FROM FilesVersions";
            var test2 = clientControl.Select(command1);
            SQLiteCommand command2 = new SQLiteCommand();
            command2.CommandText = "Update ApplicationInfo set AppDiscription=@AppDiscription,Flag=@Flag WHERE Application=@Application AND AppVersion=@AppVersion";
            command2.Parameters.Add("@AppDiscription", DbType.String).Value = "";
            command2.Parameters.Add("@Flag", DbType.String).Value = "False";
            command2.Parameters.Add("@Application", DbType.String).Value = "TestA";
            command2.Parameters.Add("@AppVersion", DbType.String).Value = "1_b0";
            var test3 = clientControl.Update(command2);
            Console.WriteLine();
        }
    }
}
