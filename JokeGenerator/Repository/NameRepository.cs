using System.Net.Http;
using Newtonsoft.Json;

namespace JokeGenerator.Repository
{
    public class NameRepository
    {
        private readonly HttpClient httpClient;

        public NameRepository(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public string GetRandomName()
        {
            dynamic response = httpClient.GetAsync("https://www.names.privserv.com/api/").Result;
            return ParseNameFromResponse(response);
        }

        private string ParseNameFromResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                string responseString = responseContent.ReadAsStringAsync().Result;

                string name = JsonConvert.DeserializeObject<dynamic>(responseString).name;
                string surname = JsonConvert.DeserializeObject<dynamic>(responseString).surname;

                return $"{name} {surname}";
            }
            else
            {
                return null;
            }
        }
    }
}
