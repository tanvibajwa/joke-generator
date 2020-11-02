using System;
using JokeGenerator.Interactions.IO;
using System.Collections.Generic;

namespace JokeGenerator.Interactions
{
    public class UserQuestions
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        public UserQuestions(IReader reader, IWriter writer)
        {

            this.reader = reader;
            this.writer = writer;
        }

        public char GetUsersNextAction(List<String> options, List<char> validResponses)
        {
            while (true)
            {
                WriteOptions(options);

                char response = reader.ReadInCharacter();

                if (!UserResponse.IsValidResponse(validResponses, response))
                {
                    WriteInvalidResponse();
                }
                else
                {
                    return response;
                }
            }
        }

        public char GetYesOrNoAnswer(string question)
        {
            List<char> validResponses = new List<char>() { UserResponse.YES, UserResponse.NO };

            while (true)
            {
                writer.WriteLine($"{question} ({UserResponse.YES}/{UserResponse.NO})");

                char response = reader.ReadInCharacter();

                if (!UserResponse.IsValidResponse(validResponses, response))
                {
                    WriteInvalidResponse();
                }
                else
                {
                    return response;
                }
            }
        }

        private void WriteOptions(List<string> options)
        {
            writer.WriteLine("\nOptions:");
            foreach (string option in options)
            {
                writer.WriteLine($"  - {option}");
            }
        }

        public int GetNumber(string message, int maxNumber)
        {
            while (true)
            {
                writer.WriteLine(message);

                int response = reader.ReadInInteger();

                if (response > 0 && response <= maxNumber)
                {
                    return response;
                }
                else
                {
                    WriteInvalidResponse();
                }
            }
        }

        private void WriteInvalidResponse()
        {
            writer.WriteLine("\nOops, that is not a valid value");
            writer.WriteLine("Please try again");
        }
    }
}
