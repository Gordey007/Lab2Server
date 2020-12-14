using Json.Net;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Lab2Server
{
    class SocketServer
    {
        public WorkFile workFile = new WorkFile();
        public void start(int port)
        {          
            // Устанавливаем для сокета локальную конечную точку
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);
           
            // Создаем сокет Tcp/Ip
            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Назначаем сокет локальной конечной точке и слушаем входящие сокеты
            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);
                Console.WriteLine("Ожидаем соединение через порт {0}", ipEndPoint);
                // Программа приостанавливается, ожидая входящее соединение
                while (true)
                {
                    Socket handler = sListener.Accept();
                    Thread threadSocket = new Thread(client);
                    Console.WriteLine();
                   threadSocket.Start(handler);
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


        void client(object obj)
        {
            Socket handler = (Socket)obj;
            // Начинаем слушать соединения
            while (true)
            {
                string data = null;

                // Мы дождались клиента, пытающегося с нами соединиться
                byte[] bytes = new byte[1024];
                int bytesRec = handler.Receive(bytes);

                data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                if (data == "1")
                {
                    string text = workFile.read();
                    byte[] msg = Encoding.UTF8.GetBytes(text);
                    handler.Send(msg);
                    if (data.IndexOf("<TheEnd>") > -1)
                    {
                        //   Console.WriteLine("Сервер завершил соединение с клиентом.");
                        break;
                    }
                }
                else if (data[0] == '2')
                {
                    string text = "";
                    for (int i = 1; i < data.Length; i++)
                    {
                        text += data[i];
                    }
                    byte[] msg = Encoding.UTF8.GetBytes(workFile.find(text));
                    handler.Send(msg);
                    if (data.IndexOf("<TheEnd>") > -1)
                    {
                        break;
                    }
                }
                else if (data[0] == '3')
                {
                    string text = "";
                    for (int i = 1; i < data.Length; i++)
                    {
                        text += data[i];
                    }
                    byte[] msg = Encoding.UTF8.GetBytes(workFile.del(3));
                    handler.Send(msg);
                    if (data.IndexOf("<TheEnd>") > -1)
                    {
                        break;
                    }
                }
                else if (data[0] == '4')
                {
                    string text = "";
                    for (int i = 2; i < data.Length; i++)
                    {
                        text += data[i];
                    }
                    byte[] msg = Encoding.UTF8.GetBytes(workFile.edit((int)Char.GetNumericValue(data[1]), text));
                    handler.Send(msg);
                    if (data.IndexOf("<TheEnd>") > -1)
                    {
                        break;
                    }
                }
                else
                {
                    workFile.write(data);

                    // Показываем данные на консоли
                    Console.Write("Полученный текст: " + data + "\n\n");
                }
                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close();
            }
        }
    }
}
