using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace prjGipSOFO_2021.Helper
{
    public class TCPclient
    {

        public static void Send(String server, String message)
        {
            try
            {
                // Create TcpClient
                Int32 port = 8888;
                TcpClient client = new TcpClient(server, port);

                // Translate naar ASCII en sla op in Byte array
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                NetworkStream stream = client.GetStream();

                // Send message
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: " + message);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: " + e);
            }

        }

        public static void Listen(string ip, int port)
        {
            try
            {
                Console.Write("Waiting for a connection... ");

                // start TCP listener
                TcpListener listener = new TcpListener(IPAddress.Any, port);
                listener.Start();

                // Allocated buffer om data op te slaan
                Byte[] bytes = new Byte[256];

                TcpClient client;

                while (true)
                {
                    // Accept connection
                    client = listener.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    // stream object om te kunnen lezen
                    NetworkStream stream = client.GetStream();

                    // leest data van de stream
                    int bytesRead;

                    // in deze loop wordt er steeds nieuwe data gelezen zodat je continu nieuwe data kunt sturen
                    while ((bytesRead = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // zet byte data om naar string
                        string data = System.Text.Encoding.ASCII.GetString(bytes, 0, bytesRead);
                        Console.WriteLine("\n\n" + data.Split('\n')[0] + "\n");

                        Console.WriteLine("Refreshing");
                        
                        AdminPanel.EventTriggered();

                    }
                }

                // sluiten = client.Close()
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

        }

    }

}