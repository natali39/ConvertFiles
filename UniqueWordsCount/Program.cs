using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UniqueWordsCount
{
    //Прочитать файл
    //Разбить файл на слова (возможно, использовать регулярные выражения), убрать пустые строки
    //получить массив всех слов текста (предположим, что регистр слов не имеет значения, тогда все слова привести к нижнему регистру)
    //посчитать количество вхождений каждого слова в текст
    //отсортировать полученную коллекцию по количеству вхождений слова (по убыванию)
    //Записать результаты выборки в файл в виде строк "слово - количество вхождений"

    class Program
    {
        private const string sourceDirectoryPath = @"..\..\..\data\source\";
        private const string destinationDirectoryPath = @"..\..\..\data\destination\";

        static void Main(string[] args)
        {
            var files = GetFiles(sourceDirectoryPath);

            if (files.Length == 0)
            {
                Console.WriteLine($"Directory {sourceDirectoryPath} is empty");
                return;
            }

            Console.WriteLine("Select the file to convert:");

            var number = 0;
            foreach (var file in files)
            {
                Console.WriteLine($"{number}. {file}");
                number++;
            }

            var userInput = Console.ReadLine();
            var result = int.TryParse(userInput, out int fileNumber);

            if (result == false || fileNumber < 0 || fileNumber >= files.Length)
            {
                Console.WriteLine("Incorrect input value");
                return;
            }

            var filePath = files[fileNumber];
            var fileName = GetFileName(filePath);

            var words = GetWords(filePath)
                .Select(x => x.ToLower())
                .ToArray();

            var dictionary = ConvertWordsArrayToDictionary(words);

            dictionary = SortByValueDescending(dictionary);

            WriteToFile(dictionary, fileName);
        }

        private static Dictionary<string, int> ConvertWordsArrayToDictionary(string[] words)
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

        private static string GetFileName(string filePath)
        {
            var fileName = string.Empty;

            for (int i = filePath.Length - 1; i >= 0; i--)
            {
                if (filePath[i] == '\\')
                {
                    fileName = filePath.Substring(i + 1);
                    break;
                }
            }

            return fileName;
        }

        private static string[] GetFiles(string sourcePath)
        {
            if (Directory.Exists(sourcePath))
            {
                return Directory.GetFiles(sourcePath);
            }
            else
            {
                Directory.CreateDirectory(sourcePath);
                return new string[0];
            }
        }

        private static string[] GetWords(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (var sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    return Regex.Split(sr.ReadToEnd(), @"\W+");
                }
            }
            else throw new FileNotFoundException();
        }

        private static void WriteToFile(Dictionary<string, int> dictionary, string outputFileName)
        {
            var filePath = destinationDirectoryPath + outputFileName;
            if (Directory.Exists(destinationDirectoryPath))
            {
                using (var sw = new StreamWriter(filePath))
                {
                    foreach (var item in dictionary)
                    {
                        sw.WriteLine($"{item.Key} - {item.Value}");
                    }
                }
            }
            else throw new DirectoryNotFoundException();
        }

        private static Dictionary<string, int> SortByValueDescending(Dictionary<string, int> dictionary)
        {
            return dictionary
                .OrderByDescending(x => x.Value)
                .ThenBy(x => x.Key)
                .ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
        }
    }
}
