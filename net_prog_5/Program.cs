using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class UDPClient
{
    private const int serverPort = 11000;
    private const string serverIP = "127.0.0.1"; 

    public static void Main()
    {
        UdpClient udpClient = new UdpClient();

        try
        {
            
            udpClient.Connect(serverIP, serverPort);
            Console.WriteLine("Connected to server {0} on port {1}.", serverIP, serverPort);

            while (true)
            {
                
                Console.Write("Enter products (comma separated): ");
                string productsInput = Console.ReadLine();
                if (string.IsNullOrEmpty(productsInput))
                    continue;

             
                byte[] sendBytes = Encoding.UTF8.GetBytes(productsInput);
                udpClient.Send(sendBytes, sendBytes.Length);

               
                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] receivedBytes = udpClient.Receive(ref serverEndPoint);
                string receivedData = Encoding.UTF8.GetString(receivedBytes);
                Console.WriteLine("Received from server: ");
                Console.WriteLine(receivedData);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        finally
        {
            udpClient.Close();
        }
    }
}
