using System;

namespace JokeGenerator.Interactions.IO
{
    public class ConsoleReader : IReader
    {
        private readonly ConsoleWriter consoleWriter;

        public ConsoleReader(ConsoleWriter consoleWriter)
        {
            this.consoleWriter = consoleWriter;
        }

        public Char ReadInCharacter()
        {
            char value;
            while (!Char.TryParse(Console.ReadLine(), out value))
            {
                consoleWriter.WriteLine("Oops, looks like you didn't enter a letter.");
                consoleWriter.WriteLine("Please enter a valid letter.");
            }

            return value;
        }

        public int ReadInInteger()
        {
            int value;
            while (!Int32.TryParse(Console.ReadLine(), out value))
            {
                consoleWriter.WriteLine("\nOops, looks like you didn't enter a valid number.");
                consoleWriter.WriteLine("Please enter a valid number.");
            }

            return value;
        }

        private bool ValidInteger(int value)
        {
            return value > 0 && value < 10;
        }
    }
}
