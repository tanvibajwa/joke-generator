using System;

namespace JokeGenerator.Interactions.IO
{
    public class ConsoleWriter : IWriter
    {
        public ConsoleWriter()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode; //Bugfix # 2a
        }

        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}
