using System.Net;
using System.Net.Sockets;
using System.Text;
using DNS_cacher;
using Server;

void SendData( Socket client, string domen, string ip )
{
    client.Send(Encoding.UTF8.GetBytes($"{domen}: {ip}"));
}

FileCache.Read();

var ipPoint = new IPEndPoint(IPAddress.Any, 53); // штука для запуска сервера, которая принимает любые адреса на порту 53
using var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);// IPv4, создаём поток
server.Bind(ipPoint); // передаём параметры из ipPoint сокету
Console.WriteLine("Server start");

while (true)
{
    server.Listen(); // блокирует поток
    using var client = server.Accept();//принимаем клиента
    var data = new byte[1024];
    var size = client.Receive(data);
    var dataString = Encoding.UTF8.GetString(data, 0, size);
    Cache? cache;
    if (( FileCache.cacheByDomen.TryGetValue(dataString, out cache)
         || FileCache.cacheByIp.TryGetValue(dataString, out cache) )
        && cache.Ttl >= DateTime.Now)
    {
        SendData(client, cache.Domen, cache.Ip);
        Console.WriteLine($"Send data from cache: {cache.Domen} {cache.Ip}");
        continue;
    }

    IPHostEntry dnsData; // данные, которые возвращает dns
    try
    {
        dnsData = Dns.GetHostEntry(dataString); // пытаемся получить данные
    }
    catch (Exception e)
    {
        Console.WriteLine($"Fail: {e.Message}");
        continue;
    }

    var name = dnsData.HostName; // домен
    var ips = dnsData.AddressList; // список адресов
    const int TTL = 600; //TTL
    FileCache.cacheByDomen[name] = new Cache(ips[0].ToString(), name, TTL);
    foreach (var ip in ips)
        FileCache.cacheByIp[ip.ToString()] = new Cache(ip.ToString(), name, TTL);
    SendData(client, name, ips[0].ToString());
    Console.WriteLine($"Send data from dns: {name} {ips[0]}");
    client.Close();
    FileCache.Save();
}

