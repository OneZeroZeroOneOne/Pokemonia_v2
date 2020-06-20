using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using Pokemonia.Dal.LogicModels;
using Pokemonia.WebServer.Handlers;

namespace Pokemonia.WebServer
{
    public class SocketServer
    {
        public SocketServer(Dictionary<int, MapDataHolder> collections)
        {
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);

            //создаю хендлери
            AuthorizeHandler authorizeHandler = new AuthorizeHandler();

            // Создаем сокет Tcp/Ip
            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Назначаем сокет локальной конечной точке и слушаем входящие сокеты
            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(100);

                // Начинаем слушать соединения
                while (true)
                {
                    Console.WriteLine("wait connect {0}", ipEndPoint);

                    // Программа приостанавливается, ожидая входящее соединение
                    Socket handler = sListener.Accept();
                    //Thread userSocketConn = new Thread();
                    Dispatcher dispatcher = new Dispatcher(new UserConnection(handler));
                    //запускаю обработку в отдельном потоке 
                    Thread th = new Thread(dispatcher.Run);
                    th.Start();
                
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
