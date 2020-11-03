using NUnit.Framework;
using JokeGenerator;
using JokeGenerator.Service;
using System.Collections.Generic;
using System;

namespace JokeGeneratorTests.UnitTests.Service
{
    public class JokeServiceTests
    {
        private JokeService jokeService;
        private HashSet<string> testJoke;

        [SetUp]
        public void Setup()
        {
            jokeService = new JokeService();
            testJoke = new HashSet<string> { "this is a test joke with name Chuck Norris that should be replaced." };
        }

        //Valid input, Single joke
        [Test]
        public void should_return_joke_with_name_replaced_when_valid_name_replacement_is_sent()
        {
            //First name, Last name
            var result = jokeService.ReplaceChuckNorrisOccurences(testJoke, "Anna Bell");
            Assert.AreEqual(result, new HashSet<string> { "this is a test joke with name Anna Bell that should be replaced." });

            //First name
            result = jokeService.ReplaceChuckNorrisOccurences(testJoke, "Jasleen");
            Assert.AreEqual(result, new HashSet<string> { "this is a test joke with name Jasleen that should be replaced." });
        }

        //Valid input, Multiple jokes
        [Test]
        public void should_return_jokes_with_name_replaced_when_valid_name_replacement_is_sent()
        {
            testJoke = new HashSet<string> {
                "this is the 1st test joke with name Chuck Norris that should be replaced." ,
                "this is the 2nd test joke with name Chuck Norris that should be replaced."
            };

            var result = jokeService.ReplaceChuckNorrisOccurences(testJoke, "Bajwa");
            Assert.AreEqual(result, new HashSet<string> {
                "this is the 1st test joke with name Bajwa that should be replaced.",
                "this is the 2nd test joke with name Bajwa that should be replaced."
            });
        }

        //Invalid input of name
        [Test]
        public void should_return_original_joke_when_invaid_name_is_sent()
        {
            testJoke = new HashSet<string> {
                "this is the 1st test joke with name Chuck Norris that should NOT be replaced." ,
                "this is the 2nd test joke with name Chuck Norris that should NOT be replaced."
            };

            //when name is an empty string
            var result = jokeService.ReplaceChuckNorrisOccurences(testJoke, "");
            Assert.AreEqual(result, testJoke);

            //when name is whitespace(s)
            result = jokeService.ReplaceChuckNorrisOccurences(testJoke, "  ");
            Assert.AreEqual(result, testJoke);

            //when name is null
            result = jokeService.ReplaceChuckNorrisOccurences(testJoke, null);
            Assert.AreEqual(result, testJoke);

            //not checking for special characters
        }

        //Invalid input of joke(s)
        [Test]
        public void should_handle_invalid_joke_inputs()
        {
            // when one of the jokes is null
            testJoke = new HashSet<string> {
                "this is the 1st test joke with name Chuck Norris that should be replaced." ,
                null
            };

            var result = jokeService.ReplaceChuckNorrisOccurences(testJoke, "Alan");
            Assert.AreEqual(result, new HashSet<string> {
                "this is the 1st test joke with name Alan that should be replaced." });



            //when one/many of the jokes is/are an empty string
            testJoke = new HashSet<string> {
                "this is the 1st test joke with name Chuck Norris that should be replaced." ,
                "",
                ""
            };

            result = jokeService.ReplaceChuckNorrisOccurences(testJoke, "Alan");
            Assert.AreEqual(result, new HashSet<string> {
                "this is the 1st test joke with name Alan that should be replaced." });


            //when joke is null
            var exceptionThrown = false;
            try
            {
                result = jokeService.ReplaceChuckNorrisOccurences(null, "Alan");
            }
            catch (ArgumentNullException)
            {
                exceptionThrown = true;
            }

            Assert.True(exceptionThrown);
        }

        //
        //some tests related to edge cases
        //
        
        [Test]
        public void should_be_insensitive_to_case()
        {
            testJoke = new HashSet<string> { "this is a test joke with name chuck norris that should be replaced." };
            //First name, Last name
            var result = jokeService.ReplaceChuckNorrisOccurences(testJoke, "Anna Bell");
            Assert.AreEqual(result, new HashSet<string> { "this is a test joke with name Anna Bell that should be replaced." });
        }


        [Test]
        public void should_not_confuse_with_names_that_look_similar_to_chuck_norris()
        {
            testJoke = new HashSet<string>() { "a joke with DeChuck NorrisM sounding name" };
            var result = jokeService.ReplaceChuckNorrisOccurences(testJoke, "bajwa");
            Assert.AreEqual(testJoke, result);


            testJoke = new HashSet<string>() { "a joke with DeChuck Norris sounding name" };
            result = jokeService.ReplaceChuckNorrisOccurences(testJoke, "bajwa");
            Assert.AreEqual(testJoke, result);


            testJoke = new HashSet<string>() { "a joke with Chuck NorrisM sounding name" };
            result = jokeService.ReplaceChuckNorrisOccurences(testJoke, "bajwa");
            Assert.AreEqual(testJoke, result);


            testJoke = new HashSet<string>() { "Chuck NorrisM at starting" };
            result = jokeService.ReplaceChuckNorrisOccurences(testJoke, "bajwa");
            Assert.AreEqual(testJoke, result);


            testJoke = new HashSet<string>() { "joke ending with Chuck NorrisM" };
            result = jokeService.ReplaceChuckNorrisOccurences(testJoke, "bajwa");
            Assert.AreEqual(testJoke, result);
        }
    }
}