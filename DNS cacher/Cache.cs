namespace Server;

public class Cache
{
    public string Ip { get; set; }
    public string Domen { get; set; }
    public DateTime Ttl { get; set; }

    public Cache()
    {
        // пустой базовый конструктор для десериализации
    }

    public Cache(string Ip, string Domen, int Ttl)
    {
        this.Ip = Ip;
        this.Domen = Domen;
        this.Ttl = DateTime.Now.AddSeconds(Ttl);
    }
}
