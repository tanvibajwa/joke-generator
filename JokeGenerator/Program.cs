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
            HttpClient httpClient = new HttpClient();

            JokeRepository jokeRepository = new JokeRepository(httpClient);
            NameRepository nameRepository = new NameRepository(httpClient);

            ConsoleWriter writer = new ConsoleWriter();
            IReader reader = new ConsoleReader(writer);

            UserQuestions userQuestions = new UserQuestions(reader, writer);
            UserInteractions userInteractions = new UserInteractions(writer, userQuestions);

            JokeService jokeService = new JokeService();

            ProgramRunner programRunner = new ProgramRunner(userInteractions, jokeRepository, nameRepository, jokeService);

            programRunner.Run();
        }
    }
}
