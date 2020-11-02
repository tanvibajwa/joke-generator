using System;
using System.Collections.Generic;
using System.Linq;
using JokeGenerator.Interactions.IO;

namespace JokeGenerator.Interactions
{
    public class UserInteractions
    {
        private readonly IWriter writer;
        private readonly UserQuestions userQuestions;

        public UserInteractions(IWriter writer, UserQuestions userQuestions)
        {
            this.writer = writer;
            this.userQuestions = userQuestions;
        }

        public void GreetUser()
        {
            writer.WriteLine("\nWelcome to the Joke Generator");
        }

        public void SayGoodbyToUser()
        {
            writer.WriteLine("\nThank you for using the Joke Generator");
        }

        public void CategoriesCurrentlyAvailable()
        {
            writer.WriteLine("\nThe categories are temporarily unavailable");
        }

        public void ShowUserAvailableCategories(List<string> categories)
        {
            writer.WriteLine("\nAvailable Categories:");
            DisplayList(categories);
        }

        public void DisplayRandomName(string name)
        {
            if(name != null)
            {
                writer.WriteLine($"Name Chosen: {name}");
            }
        }

        public void DisplayRandomNameError()
        {
            writer.WriteLine("No random name could be chosen");
            writer.WriteLine("Proceeding with default name");
        }

        public void DisplayChosenCategory(string category)
        {
            writer.WriteLine($"Category Chosen: {category}");
        }

        public void DisplayJokes(HashSet<string> jokes, int numberOfRequestedJokes)
        {
            if(jokes.Count < numberOfRequestedJokes)
            {
                writer.WriteLine("\nCould get some of the jokes requested:");
            }
            else
            {
                writer.WriteLine("\nJokes:");
            }
                
            DisplayList(jokes.ToList());
        }

        public void DisplayJokesError()
        {
            writer.WriteLine("\nUnfortunately an error occurred and no jokes were generated");
            writer.WriteLine("Please try again later");
        }

        public bool UserWantsToUseRandomName()
        {
            return UserResponse.IsYes(userQuestions.GetYesOrNoAnswer("\nShould a random name be used?"));
        }

        public bool UsersWantsToChooseACategory()
        {
            return UserResponse.IsYes(userQuestions.GetYesOrNoAnswer("\nDo you want to choose the joke category?"));
        }

        public int GetNumberOfJokes()
        {
            return userQuestions.GetNumber("\nHow many jokes do you want? (1-9)", 9);
        }

        public string ChooseCategory(List<string> categories)
        {
            ShowUserAvailableCategories(categories);

            string message = "\nEnter the number for the category you want to select:";
            int categoryIndex = userQuestions.GetNumber(message, categories.Count) - 1;

            return categories[categoryIndex];
        }

        public bool UserChoosesToProceedWithProgram()
        {
            List<string> options = new List<string>()
            {
                $"Press {UserResponse.SEE_INSTRUCTIONS} to get instructions",
                $"Press {UserResponse.EXIT_PROGRAM} to exit program"
            };

            List<char> validResponses = new List<char>() {
                UserResponse.SEE_INSTRUCTIONS,
                UserResponse.EXIT_PROGRAM,
            };

            char response = userQuestions.GetUsersNextAction(options, validResponses);

            if (UserResponse.IsSeeInstructions(response))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public char DetermineFeatureToExecute()
        {
            List<string> options = new List<string>()
            {
                $"Press {UserResponse.GET_RANDOM_JOKES} to view some random jokes",
                $"Press {UserResponse.VIEW_CATEGORIES} to see available joke categories",
                $"Press {UserResponse.EXIT_PROGRAM} to exit program"
            };

            List<char> validResponses = new List<char>() {
                UserResponse.GET_RANDOM_JOKES,
                UserResponse.EXIT_PROGRAM,
                UserResponse.VIEW_CATEGORIES
            };

            return userQuestions.GetUsersNextAction(options, validResponses);
        }

        private void DisplayList(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                writer.WriteLine($"{i + 1}. {list[i]}");
            }
        }

    }
}
