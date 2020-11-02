using System.Collections.Generic;
using JokeGenerator.Interactions;
using JokeGenerator.Repository;
using JokeGenerator.Service;

namespace JokeGenerator.Runner
{
    public class ProgramRunner
    {
        private readonly UserInteractions userInteractions;
        private readonly JokeRepository jokeRepository;
        private readonly NameRepository nameRepository;
        private readonly JokeService jokeService;

        public ProgramRunner(
            UserInteractions userInteractions,
            JokeRepository jokeRepository,
            NameRepository nameRepository,
            JokeService jokeService)
        {
            this.userInteractions = userInteractions;
            this.jokeRepository = jokeRepository;
            this.nameRepository = nameRepository;
            this.jokeService = jokeService;
        }

        public void Run()
        {
            userInteractions.GreetUser();

            if (userInteractions.UserChoosesToProceedWithProgram())
            {
                BeginProgram();
            }

            userInteractions.SayGoodbyToUser();

            return;
        }

        private void BeginProgram()
        {
            while(true)
            {
                char response = userInteractions.DetermineFeatureToExecute();

                if(UserResponse.IsViewCategories(response))
                {
                    DisplayCategories();
                }
                else if(UserResponse.IsGetRandomJokes(response))
                {
                    GetRandomJokes();
                }
                else if(UserResponse.IsExitProgram(response))
                {
                    return;
                }
            }
        }

        private void DisplayCategories()
        {
            List<string> categories = jokeRepository.GetCategories();

            if (IsNonNull(categories))
            {
                userInteractions.ShowUserAvailableCategories(categories);
            }
            else
            {
                userInteractions.CategoriesCurrentlyAvailable();
            }
        }

        private void GetRandomJokes()
        {
            string randomName = null;
            string category = null;

            if(userInteractions.UserWantsToUseRandomName())
            {
                randomName = DetermineRandomName();
            }

            if (userInteractions.UsersWantsToChooseACategory())
            {
                category = DetermineCategory();
            }

            int numberOfJokes = userInteractions.GetNumberOfJokes();

            HashSet<string> jokes = jokeRepository.GetJokes(category, numberOfJokes);

            DisplayJokes(jokes, randomName, numberOfJokes);
        }

        private void DisplayJokes(HashSet<string> jokes, string randomName, int numberOfJokes)
        {
            if (IsNonNull(jokes))
            {
                HashSet<string> updatedJokes = jokeService.ReplaceChuckNorrisOccurences(jokes, randomName);

                userInteractions.DisplayJokes(updatedJokes, numberOfJokes);
            }
            else
            {
                userInteractions.DisplayJokesError();
            }
        }

        private string DetermineRandomName()
        {
            string randomName = nameRepository.GetRandomName();

            if (IsNonNull(randomName))
            {
                userInteractions.DisplayRandomName(randomName);
            }
            else
            {
                userInteractions.DisplayRandomNameError();
            }

            return randomName;
        }

        private string DetermineCategory()
        {
            string category = null;
            List<string> categories = jokeRepository.GetCategories();

            if (IsNonNull(categories))
            {
                category = userInteractions.ChooseCategory(categories);
                userInteractions.DisplayChosenCategory(category);
            }
            else
            {
                userInteractions.CategoriesCurrentlyAvailable();
            }

            return category;
        }

        private bool IsNonNull(dynamic value)
        {
            return value != null;
        }
    }
}
