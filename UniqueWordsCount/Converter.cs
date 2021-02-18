using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UniqueWordsCount
{
    public static class Converter
    {
        public static Dictionary<string, int> ToDictionary(string[] words)
        {
            var dictionary = new Dictionary<string, int>();

            foreach (var word in words)
            {
                if (!dictionary.ContainsKey(word))
                {
                    dictionary[word] = 0;
                }

                dictionary[word]++;
            }

            return dictionary;
        }

        public static string[] StringToWords(string filePath)
        {
            return Regex.Split(FileProvider.ReadFile(filePath), @"\W+");
        }
    }
}
