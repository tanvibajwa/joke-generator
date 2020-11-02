using System;

namespace JokeGenerator.Interactions.IO
{
    public class ConsoleWriter : IWriter
    {
        public ConsoleWriter()
        {
        }

        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}
