using System.Runtime.Serialization.Json;
using System.Text;

namespace RickAndMortyLib
{
    public class RickAndMortyClient
    {
        public const string PATHCHARACTER = "https://rickandmortyapi.com/api/character/";
        public const string PATHEPISODE = "https://rickandmortyapi.com/api/episode";

        public static T Deserialize<T>(string json)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(json);
            var serializer = new DataContractJsonSerializer(typeof(T));
            using (var stream = new MemoryStream(buffer))
            {
                return (T)serializer.ReadObject(stream);
            }
        }
    }
    public class HttpClientCaller
    {
        public async Task<string> GetAsync(string url)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var client = new HttpClient();
            var response = await client.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                // read response as bytes
                var content = await response.Content.ReadAsByteArrayAsync();
                // convert bytes to string
                var text = Encoding.UTF8.GetString(content);
                return text;
            }
            throw new Exception("Not found");
        }
    }
}