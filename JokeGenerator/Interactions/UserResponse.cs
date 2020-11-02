using System;
using System.Collections.Generic;

namespace JokeGenerator.Interactions
{
    public static class UserResponse
    {
        public static readonly char SEE_INSTRUCTIONS = '?';
        public static readonly char VIEW_CATEGORIES = 'c';
        public static readonly char GET_RANDOM_JOKES = 'r';
        public static readonly char EXIT_PROGRAM = 'x';
        public static readonly char YES = 'y';
        public static readonly char NO = 'n';

        public static bool IsSeeInstructions(char response)
        {
            return IsMatchingResponse(response, SEE_INSTRUCTIONS);
        }

        public static bool IsViewCategories(char response)
        {
            return IsMatchingResponse(response, VIEW_CATEGORIES);
        }

        public static bool IsGetRandomJokes(char response)
        {
            return IsMatchingResponse(response, GET_RANDOM_JOKES);
        }

        public static bool IsExitProgram(char response)
        {
            return IsMatchingResponse(response, EXIT_PROGRAM);
        }

        public static bool IsYes(char response)
        {
            return IsMatchingResponse(response, YES);
        }

        public static bool IsNo(char response)
        {
            return IsMatchingResponse(response, NO);
        }

        public static bool IsValidResponse(List<char> expectedResponses, char actualResonse)
        {
            return new HashSet<char>(expectedResponses).Contains(actualResonse);
        }

        private static bool IsMatchingResponse(char response, char expectedResponse)
        {
            return response == expectedResponse;
        }
    }
}
