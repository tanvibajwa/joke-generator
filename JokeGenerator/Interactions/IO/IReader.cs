using System;

namespace JokeGenerator.Interactions.IO
{
    public interface IReader
    {
        Char ReadInCharacter();
        int ReadInInteger();
    }
}
