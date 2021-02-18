using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UniqueWordsCount
{
    public static class FileProvider
    {
        private const string sourceDirectoryPath = @"..\..\..\data\source\";
        private const string destinationDirectoryPath = @"..\..\..\data\destination\";

        public static string[] GetFiles()
        {
            if (Directory.Exists(sourceDirectoryPath))
            {
                return Directory.GetFiles(sourceDirectoryPath);
            }
            else
            {
                Directory.CreateDirectory(sourceDirectoryPath);
                return new string[0];
            }
        }

        public static string ReadFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (var sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }
            else throw new FileNotFoundException();
        }

        public static void WriteToFile(Dictionary<string, int> dictionary, string outputFileName)
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

        public static string GetFileName(string filePath)
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
    }
}
