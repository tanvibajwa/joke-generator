using System.IO;
using System.Collections.Generic;
using System.Net.Http;

using NUnit.Framework;

namespace JokeGeneratorTests.APIs.Framework
{
    /// <summary>
    /// This class implements basic functionality to talk to an API (+endpoint)
    /// and provides helper methods to convert responses to string.
    /// </summary>
    public class APITestBase
    {
        protected string baseUrl { get; set; }
        protected string endpoint { get; set; }
        private HttpClient httpClient { get; set; }

        [SetUp]
        public void BaseSetup()
        {
            baseUrl = string.Empty;
            endpoint = string.Empty;
            httpClient = new HttpClient();
        }

        /// <summary>
        /// Reads from the API using baseUrl and Endpoint and returns the result.
        /// </summary>
        protected HttpResponseMessage GetFromAPI(Dictionary<string, string> parameters = null, bool successCheck = true)
        {
            var complete_path = Path.Join(baseUrl, endpoint);

            if (string.IsNullOrEmpty(complete_path) || string.IsNullOrWhiteSpace(complete_path))
                Assert.Fail($"Invalid uri address created ('{complete_path}')");

            if (parameters != null)
            {
                complete_path += "?";

                foreach (var p in parameters)
                    complete_path += $"{p.Key}={p.Value}&";

                complete_path = complete_path.TrimEnd(new char[] { '&', '?' });
            }

            var result = httpClient.GetAsync(complete_path).Result;

            if (!result.IsSuccessStatusCode && successCheck == true)
                Assert.Fail("response returned Not Ok Status Code");

            return result;
        }


        /// <summary>
        /// Reads the response into a string
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        protected string ReadResponseAsString(HttpResponseMessage response)
        {
            var responseContent = response.Content;
            return responseContent.ReadAsStringAsync().Result;
        }
    }
}
