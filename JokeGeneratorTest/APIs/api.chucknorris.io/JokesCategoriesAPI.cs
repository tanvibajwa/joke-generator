using System.Collections.Generic;

using JokeGeneratorTests.APIs.Framework;

using Newtonsoft.Json;
using NUnit.Framework;


namespace JokeGeneratorTests.APIs.api.chucknorris.io.jokes
{
    class categories : APITestBase
    {
        [Test]
        public void should_return_the_expected_list_of_categories()
        {
            baseUrl = "https://api.chucknorris.io/";
            endpoint = "jokes/categories";

            var response = GetFromAPI();
            string responseString = ReadResponseAsString(response);

            var categories = JsonConvert.DeserializeObject<string[]>(responseString);

            Assert.That(categories, Is.EqualTo(new string[] { 
                "animal", "career", "celebrity", "dev", "explicit", 
                "fashion", "food", "history", "money", "movie", "music", 
                "political", "religion", "science", "sport", "travel" 
            }));
        }
    }

    class random : APITestBase
    {
        [SetUp]
        public void Setup()
        {
            baseUrl = "https://api.chucknorris.io/";
            endpoint = "jokes/random";
        }

        [Test]
        public void should_return_a_random_joke_when_no_category_is_specified()
        {
            var response = GetFromAPI();
            string responseString = ReadResponseAsString(response);

            string joke = JsonConvert.DeserializeObject<dynamic>(responseString).value;

            Assert.That(!string.IsNullOrEmpty(joke));
            Assert.That(!string.IsNullOrWhiteSpace(joke));
        }

        [Test]
        public void should_return_the_joke_for_the_requested_category()
        {
            var parameters = new Dictionary<string, string>()
            {
                { "category", "animal" }
            };

            var response = GetFromAPI(parameters);
            string responseString = ReadResponseAsString(response);
            string[] category = JsonConvert.DeserializeObject<dynamic>(responseString).categories.ToObject<string[]>();
            string joke = JsonConvert.DeserializeObject<dynamic>(responseString).value;

            Assert.That(category, Is.EqualTo(new string[] { "animal" }));
            Assert.That(!string.IsNullOrEmpty(joke));
            Assert.That(!string.IsNullOrWhiteSpace(joke));
        }
    }

    class search : APITestBase
    {
        [SetUp]
        public void Setup()
        {
            baseUrl = "https://api.chucknorris.io/";
            endpoint = "jokes/search";
        }

        [Test]
        public void should_return_no_joke_if_gibberish_is_passed()
        {
            var parameters = new Dictionary<string, string>()
            {
                { "query", "jfdlkasfjadskljfdasl;kj" }
            };

            var response = GetFromAPI(parameters);
            string responseString = ReadResponseAsString(response);

            int joke_count = JsonConvert.DeserializeObject<dynamic>(responseString).total;
            string[] results = JsonConvert.DeserializeObject<dynamic>(responseString).result.ToObject<string[]>();

            Assert.That(joke_count == 0);
            Assert.That(results, Is.Empty);
        }

        [Test]
        public void should_return_a_joke_upon_match()
        {
            var parameters = new Dictionary<string, string>()
            {
                { "query", "vulnerable" }
            };

            var response = GetFromAPI(parameters);
            string responseString = ReadResponseAsString(response);

            int joke_count = JsonConvert.DeserializeObject<dynamic>(responseString).total;
            object[] results = JsonConvert.DeserializeObject<dynamic>(responseString).result.ToObject<object[]>();

            Assert.That(joke_count != 0);
            Assert.That(results.Length != 0);
        }
    }
}
