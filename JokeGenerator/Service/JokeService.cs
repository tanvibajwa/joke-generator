using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace JokeGenerator.Service
{
    public class JokeService
    {
        public JokeService()
        {
        }

        public HashSet<string> ReplaceChuckNorrisOccurences(HashSet<string> jokes, string name)
        {
            if (name == null)
            {
                return jokes;
            }

            HashSet<string> updatedJokes = new HashSet<string>();

            Regex regex = new Regex("(?i)Chuck Norris(?-i)");
            foreach (string joke in jokes)
            {
                updatedJokes.Add(regex.Replace(joke, name));
            }

            return updatedJokes;
        }
    } 
}
