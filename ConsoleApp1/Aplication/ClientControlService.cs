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

namespace ConsoleApp1.Aplication
{

    public class ClientControlService
    {
        /// <summary>
        /// ip сервера
        /// </summary>
        private string IPAddres { get; }
        /// <summary>
        /// Порт
        /// </summary>
        private int Port { get; }
        public ClientControlService(string ip, int port)
        {
            IPAddres = ip ?? throw new Exception(nameof(ip));
            if (port < 0) throw new Exception(nameof(port));
            Port = port;
        }

        public bool Update(SQLiteCommand command)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(IPAddres), Port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipPoint);
            CommandSQL commandSend = new CommandSQL();
            commandSend.CommandText = command.CommandText;
            foreach (SQLiteParameter par in command.Parameters)
            {
                commandSend.Parameters += "$" + par.ParameterName + "$" + par.Value;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = int.MaxValue;
            string json = serializer.Serialize(commandSend);
            byte[] data = Encoding.Unicode.GetBytes(json);
            socket.Send(data);
            data = new byte[256]; // буфер для ответа
            StringBuilder builder = new StringBuilder();
            int bytes = 0; // количество полученных байт
            do
            {
                bytes = socket.Receive(data, data.Length, 0);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (socket.Available > 0);
            string test = builder.ToString();
            if (test == "True")
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                return true;
            }
            else
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                return false;
            }
        }
        public bool Insert(SQLiteCommand command)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(IPAddres), Port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipPoint);
            CommandSQL commandSend = new CommandSQL();
            commandSend.CommandText = command.CommandText;
            foreach (SQLiteParameter par in command.Parameters)
            {
                commandSend.Parameters += "$" + par.ParameterName + "$" + par.Value;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = int.MaxValue;
            string json = serializer.Serialize(commandSend);
            byte[] data = Encoding.Unicode.GetBytes(json);
            socket.Send(data);
            data = new byte[256]; // буфер для ответа
            StringBuilder builder = new StringBuilder();
            int bytes = 0; // количество полученных байт
            do
            {
                bytes = socket.Receive(data, data.Length, 0);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (socket.Available > 0);
            string test = builder.ToString();
            if (test == "True")
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                return true;
            }
            else
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                return false;
            }
        }

        public DataRow[] Select(SQLiteCommand command)
        {
            string[] parsingSQL = command.CommandText.Split(' ');
            string table = parsingSQL[3];
            DataRow[] dataRows = null;
            switch (table)
            {
                #region Applications
                case "Applications":
                    dataRows = DtApplications(command);
                    break;
                #endregion

                #region ApplicationInfo
                case "ApplicationInfo":
                    dataRows = DtApplicationInfo(command);
                    break;
                #endregion

                #region FilesContains
                case "FilesContains":
                    dataRows = DtFilesContains(command);
                    break;
                #endregion

                #region FilesVersions
                case "FilesVersions":
                    dataRows = DtFilesVersions(command);
                    break;
                    #endregion
            }
            return dataRows;
        }
        #region DataRows
        private DataRow[] DtApplications(SQLiteCommand command)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(IPAddres), Port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipPoint);
            //добавление в модель данных 
            CommandSQL commandSend = new CommandSQL();
            commandSend.CommandText = command.CommandText;
            foreach (SQLiteParameter par in command.Parameters)
            {
                commandSend.Parameters += "$" + par.ParameterName + "$" + par.Value;
            }
            //сериализация для отправки команды
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = int.MaxValue;
            string json = serializer.Serialize(commandSend);
            //отправка
            byte[] data = Encoding.Unicode.GetBytes(json);
            socket.Send(data);
            // получаем ответ
            data = new byte[256]; // буфер для ответа
            StringBuilder builder = new StringBuilder();
            int bytes = 0; // количество полученных байт
            do
            {
                bytes = socket.Receive(data, data.Length, 0);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (socket.Available > 0);
            string test = builder.ToString();
            string[] mystring = test.Split('$');
            DataRow[] dataRows = null;
            for (int i = 1; i < mystring.Length; i++)
            {
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                serializer1.MaxJsonLength = int.MaxValue;
                Applications meta = serializer1.Deserialize<Applications>(mystring[i]);
                dataRows[i].ItemArray = new Object[] { meta.ApplicationName };
            }
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            return dataRows;
        }
        private DataRow[] DtApplicationInfo(SQLiteCommand command)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(IPAddres), Port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipPoint);
            //добавление в модель данных 
            CommandSQL commandSend = new CommandSQL();
            commandSend.CommandText = command.CommandText;
            foreach (SQLiteParameter par in command.Parameters)
            {
                commandSend.Parameters += "$" + par.ParameterName + "$" + par.Value;
            }
            //сериализация для отправки команды
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = int.MaxValue;
            string json = serializer.Serialize(commandSend);
            //отправка
            byte[] data = Encoding.Unicode.GetBytes(json);
            socket.Send(data);
            // получаем ответ
            data = new byte[256]; // буфер для ответа
            StringBuilder builder = new StringBuilder();
            int bytes = 0; // количество полученных байт
            do
            {
                bytes = socket.Receive(data, data.Length, 0);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (socket.Available > 0);
            string test = builder.ToString();
            string[] mystring = test.Split('$');
            DataRow[] dataRows = null;
            for (int i = 1; i < mystring.Length; i++)
            {
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                serializer1.MaxJsonLength = int.MaxValue;
                ApplicationInfo meta = serializer1.Deserialize<ApplicationInfo>(mystring[i]);
                dataRows[i].ItemArray = new Object[] { meta.Application, meta.AppVersion, meta.AppDiscription, meta.Flag };
            }
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            return dataRows;
        }
        private DataRow[] DtFilesVersions(SQLiteCommand command)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(IPAddres), Port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipPoint);
            //добавление в модель данных 
            CommandSQL commandSend = new CommandSQL();
            commandSend.CommandText = command.CommandText;
            foreach (SQLiteParameter par in command.Parameters)
            {
                commandSend.Parameters += "$" + par.ParameterName + "$" + par.Value;
            }
            //сериализация для отправки команды
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = int.MaxValue;
            string json = serializer.Serialize(commandSend);
            //отправка
            byte[] data = Encoding.Unicode.GetBytes(json);
            socket.Send(data);
            // получаем ответ
            data = new byte[256]; // буфер для ответа
            StringBuilder builder = new StringBuilder();
            int bytes = 0; // количество полученных байт
            do
            {
                bytes = socket.Receive(data, data.Length, 0);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (socket.Available > 0);
            string test = builder.ToString();
            string[] mystring = test.Split('$');
            DataTable table = new DataTable();
            table.Columns.Add("AppVersion");
            table.Columns.Add("FileHash");
            table.Columns.Add("CreationDate");
            table.Columns.Add("FileSize");
            table.Columns.Add("FilePath");
            table.Columns.Add("DateCreateDB");
            table.Columns.Add("Applications");
            //dataRows[0].Table.Columns.Add("AppVersions",Type. );
            for (int i = 1; i < mystring.Length; i++)
            {
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                serializer1.MaxJsonLength = int.MaxValue;
                FilesVersions meta = serializer1.Deserialize<FilesVersions>(mystring[i]);
               table.Rows.Add( new object[] { meta.AppVersions, meta.FileHash, meta.CreationDate, meta.FileSize, meta.FilePath, meta.DateCreateDB, meta.Applications });
            }
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            DataRow[] dataRows = table.Rows.Cast<DataRow>().ToArray();
            return dataRows;
        }
        private DataRow[] DtFilesContains(SQLiteCommand command)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(IPAddres), Port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipPoint);
            //добавление в модель данных 
            CommandSQL commandSend = new CommandSQL();
            commandSend.CommandText = command.CommandText;
            foreach (SQLiteParameter par in command.Parameters)
            {
                commandSend.Parameters += "$" + par.ParameterName + "$" + par.Value;
            }
            //сериализация для отправки команды
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = int.MaxValue;
            string json = serializer.Serialize(commandSend);
            //отправка
            byte[] data = Encoding.Unicode.GetBytes(json);
            socket.Send(data);
            // получаем ответ
            data = new byte[256]; // буфер для ответа
            StringBuilder builder = new StringBuilder();
            int bytes = 0; // количество полученных байт
            do
            {
                bytes = socket.Receive(data, data.Length, 0);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (socket.Available > 0);
            string test = builder.ToString();
            string[] mystring = test.Split('$');
            DataRow[] dataRows = null;
            for (int i = 1; i < mystring.Length; i++)
            {
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                serializer1.MaxJsonLength = int.MaxValue;
                FilesContains meta = serializer1.Deserialize<FilesContains>(mystring[i]);
                dataRows[i].ItemArray = new Object[] { meta.Applications, meta.AppVersions, meta.FilePath, meta.DateCreateDB, meta.Contains };
            }
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            return dataRows;
        }

        #endregion
    }
}
