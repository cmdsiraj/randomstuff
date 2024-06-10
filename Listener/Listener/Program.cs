using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Listener
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrintIpAddress();
            var listener = new TcpListener(IPAddress.Any, 11000);
            listener.Start();
            Console.WriteLine("Waiting for a Connection...");
            while(true)
            {
                using (var client = listener.AcceptTcpClient())
                {
                    using(var stream = client.GetStream())
                    {
                            var buffer = new byte[1024];
                            var byteCount = stream.Read(buffer, 0, buffer.Length);
                            var request = Encoding.ASCII.GetString(buffer, 0, byteCount);
                            Console.WriteLine($"Received message: {request}");

                            Console.Write("You: ");
                            var message = Console.ReadLine();
                            if(message==null || string.Compare(message, "end", StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                break;
                            }
                            var response = Encoding.ASCII.GetBytes(message);
                            stream.Write(response, 0, response.Length);
                        
                    }
                }
            }
            Console.WriteLine("Closing Connection. Bye!");
        }


        static void PrintIpAddress()
        {
            var hostName = Dns.GetHostName();
            Console.WriteLine($"Host Name: {hostName}");

            IPAddress[] IPAddresses = Dns.GetHostAddresses(hostName);
            foreach(var ipAddress in IPAddresses)
            {
                if(ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    Console.WriteLine($"Ip Address: {ipAddress}");
                }
            }
        }
    }
}
