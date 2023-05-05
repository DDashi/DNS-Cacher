using Server;
using System.Text.Json;

namespace DNS_cacher
{
    public static class FileCache
    {
        private static string _IPFileName = "cacheByIp.json";
        private static string _domenFileName = "cacheByDomen.json";

        public static Dictionary<string, Cache> cacheByIp = new Dictionary<string, Cache>();
        public static Dictionary<string, Cache> cacheByDomen = new Dictionary<string, Cache>();

        private async static Task<Dictionary<string, Cache>> ReadData(string fileName)
        {
            if (!File.Exists(fileName))
                SaveData(fileName, new Dictionary<string, Cache>());

            using (StreamReader reader = new StreamReader(fileName))
            {
                string json = await reader.ReadToEndAsync();
                var date = JsonSerializer.Deserialize<Dictionary<string, Cache>>(json);

                foreach (var key in date.Keys)
                {
                    if (date[key].Ttl < DateTime.Now)
                        date.Remove(key);
                }
                return date;
            }
        }

        public async static void Read()
        {
            cacheByIp = await ReadData(_IPFileName);
            cacheByDomen = await ReadData(_domenFileName);
        }

        public static void Save()
        {
            SaveData(_IPFileName, cacheByIp);
            SaveData(_domenFileName, cacheByDomen);
        }

        private async static void SaveData(string fileName, Dictionary<string, Cache> data)
        {
            string file = JsonSerializer.Serialize(data);

            using (StreamWriter writer = new StreamWriter(fileName, false))
            {
                await writer.WriteLineAsync(file);
            }
        }
    }
}
