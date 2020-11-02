using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace JokeGenerator.Repository
{
    public class JokeRepository
    {
        private readonly string baseUrl = "https://api.chucknorris.io/jokes/";
        private readonly HttpClient httpClient;

        public JokeRepository(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public List<string> GetCategories()
        {
            dynamic response = httpClient.GetAsync($"{baseUrl}/categories").Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;

                string responseString = responseContent.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<string>>(responseString);
            }
            else
            {
                return null;
            }
        }

        public HashSet<string> GetJokes(string category, int numberOfJokes)
        {
            return GetJokeAsync(category, numberOfJokes).Result;
        }

        private async Task<HashSet<string>> GetJokeAsync(string category, int numberOfJokes)
        {
            string url = CreateJokeUrl(category);

            List<Task<HttpResponseMessage>> httpTasks = new List<Task<HttpResponseMessage>>();
            for(int i = 0; i < numberOfJokes; i++)
            {
                httpTasks.Add(httpClient.GetAsync(url));
            }

            await Task.WhenAll(httpTasks);

            HashSet<string> jokes = new HashSet<string>();

            for (int i = 0; i < numberOfJokes; i++)
            {
                string result = ParseJokeFromResponse(httpTasks[i].Result);

                if(result != null)
                {
                    jokes.Add(result);
                }
            }

            return jokes;
        }

        private string CreateJokeUrl(string category)
        {
            string url = $"{baseUrl}/random";

            if(category != null)
            {
                url = $"{url}?category={category}";
            }

            return url;
        }

        private string ParseJokeFromResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                string responseString = responseContent.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<dynamic>(responseString).value;
            }
            else
            {
                return null;
            }
        }
    }
}
