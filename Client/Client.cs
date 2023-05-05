using System.Net.Sockets;
using System.Text;

while(true)
{
    var text = Console.ReadLine();
    var tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    tcpClient.Connect("127.0.0.1", 53);
    tcpClient.Send(Encoding.UTF8.GetBytes(text!)); // отправляем серверу
    var data = new byte[1024];
    if (tcpClient.Receive(data) == 0)
    {
        Console.WriteLine("Couldn't find");
        continue;
    }    
    Console.WriteLine(Encoding.UTF8.GetString(data));
}