using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net;

using JokeGeneratorTests.APIs.Framework;

using NUnit.Framework;
using Newtonsoft.Json;


namespace JokeGeneratorTests.APIs.names.privserv.com
{
    public class NamesAPI: APITestBase
    {
        [SetUp]
        public void Setup()
        {
            baseUrl = "https://www.names.privserv.com/";
            endpoint = "api";
        }

        [Test]
        public void should_return_the_name_for_requested_gender()
        {
            var parameters = new Dictionary<string, string>()
            {
                { "gender", "female" }
            };

            var response = GetFromAPI(parameters);
            string responseString = ReadResponseAsString(response);
            string result = JsonConvert.DeserializeObject<dynamic>(responseString).gender;

            Assert.That(result == "female");
        }

        [Test]
        public void should_return_an_error_if_region_not_found()
        {
            var parameters = new Dictionary<string, string>()
            {
                { "region", "abc" }
            };

            var response = GetFromAPI(parameters, false);
            string responseString = ReadResponseAsString(response);
            string result = JsonConvert.DeserializeObject<dynamic>(responseString).error;

            Assert.That(result == "Region or language not found");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public void should_return_the_name_of_specified_length()
        {
            var parameters = new Dictionary<string, string>()
            {
                { "minlen", "12" }
            };

            var response = GetFromAPI(parameters);
            string responseString = ReadResponseAsString(response);

            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);
            var nameLength = (result["name"] + " " + result["surname"]).Length;

            Assert.That(nameLength > 11);
        }

        [Test]
        public void should_return_the_number_of_names_requested_for_which_are_also_unique()
        {
            var parameters = new Dictionary<string, string>()
            {
                { "amount", "10" }
            };

            var response = GetFromAPI(parameters);

            string responseString = ReadResponseAsString(response);
            var personsData = JsonConvert.DeserializeObject<List<Person>>(responseString);
            Assert.That(personsData.Count == 10);

            for (int i = 0; i < personsData.Count - 1; i++)
            {
                for (int j = i + 1; j < personsData.Count; j++)
                {
                    if (personsData[i].name == personsData[j].name)
                        Assert.Fail("Name repeated within 10 names");
                }
            }
        }

        [Test]
        public void should_return_name_from_the_requested_region()
        {
            var parameters = new Dictionary<string, string>()
            {
                { "region", "china" }
            };

            var response = GetFromAPI(parameters);

            string responseString = ReadResponseAsString(response);
            var personsData = JsonConvert.DeserializeObject<Person>(responseString);

            Assert.True(IsChinese(personsData.name));
            Assert.True(IsChinese(personsData.surname));
        }

        private static bool IsChinese(string input)
        {
            Regex cjkCharRegex = new Regex(@"\p{IsCJKUnifiedIdeographs}");  // Is 'China' 'Japan' 'Korea' Unified Ideograph
            return cjkCharRegex.IsMatch(input);
        }
    }
}
