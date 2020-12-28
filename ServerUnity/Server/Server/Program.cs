using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Server
    {
        const int port = 1337;
        static void Main(string[] args)
        {
            string road = string.Empty;
            TcpListener server = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);
                server.Start();

                while(true)
                {
                    Console.WriteLine("Ожидание подключений... ");

                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Подключен клиент. Выполнение запроса...");

                    NetworkStream stream = client.GetStream();

                    byte[] data = new byte[256];
                    StringBuilder response = new StringBuilder();

                    do
                    {
                        int bytes = stream.Read(data, 0, data.Length);
                        response.Append(Encoding.UTF8.GetString(data, 0, bytes));

                    } while (stream.DataAvailable);

                    switch(response.ToString())
                    {
                        case "Дорога 1":
                            road = "0";
                            break;
                        case "Дорога 2":
                            road = "1";
                            break;
                    }
                    data = Encoding.UTF8.GetBytes(road);
                    stream.Write(data, 0, data.Length);
                    stream.Close();
                    client.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (server != null)
                    server.Stop();
            }
        }
    }
}
