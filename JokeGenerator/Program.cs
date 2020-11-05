using System;
using System.Net.Http;
using JokeGenerator.Interactions;
using JokeGenerator.Interactions.IO;
using JokeGenerator.Repository;
using JokeGenerator.Runner;
using JokeGenerator.Service;

namespace JokeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleWriter writer = null;

            try                                                   //BugFix #2b
            {
                HttpClient httpClient = new HttpClient();

                JokeRepository jokeRepository = new JokeRepository(httpClient);
                NameRepository nameRepository = new NameRepository(httpClient);

                writer = new ConsoleWriter();
                IReader reader = new ConsoleReader(writer);

                UserQuestions userQuestions = new UserQuestions(reader, writer);
                UserInteractions userInteractions = new UserInteractions(writer, userQuestions);

                JokeService jokeService = new JokeService();

                ProgramRunner programRunner = new ProgramRunner(userInteractions, jokeRepository, nameRepository, jokeService);

                programRunner.Run();
            }
            catch(Exception)
            {
                if (writer != null)
                {
                    writer.WriteLine("Something went terribly wrong.");
                    writer.WriteLine("Are you sure you're connected to the internet?");
                }
            }
        }
    }
}
