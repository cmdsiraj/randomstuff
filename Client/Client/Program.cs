using System.Net.Sockets;
using System.Text;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                using (var client = new TcpClient("127.0.0.1", 11000))
                using (var stream = client.GetStream())
                {

                    Console.Write("You: ");
                    var message = Console.ReadLine();
                    if (message == null || string.Compare(message, "end", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        break;
                    }

                    var messageBytes = Encoding.ASCII.GetBytes(message);
                    stream.Write(messageBytes, 0, messageBytes.Length);

                    var buffer = new byte[1024];
                    var byteCount = stream.Read(buffer, 0, buffer.Length);
                    var response = Encoding.ASCII.GetString(buffer, 0, byteCount);
                    Console.WriteLine($"Reponse: {response}");
                
            }
        }
            Console.WriteLine("Connection Closed. Bye!");
        }
    }
}
