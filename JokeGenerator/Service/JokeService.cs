using System;
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
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name)) //BugFix 1a            
                return jokes;

            if (jokes == null) //BugFix 1b
                throw new ArgumentNullException(nameof(jokes));


            HashSet<string> updatedJokes = new HashSet<string>();
            Regex regex = new Regex(@"\b(chuck norris)\b", RegexOptions.IgnoreCase | RegexOptions.Multiline);  //BugFix 1c

            foreach (string joke in jokes) //BugFix 1b
            {
                if (!string.IsNullOrEmpty(joke)) //BugFix 1b
                    updatedJokes.Add(regex.Replace(joke, name));
            }

            return updatedJokes;
        }
    } 
}
