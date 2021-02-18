using System;
using System.Linq;

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
        static void Main(string[] args)
        {
            //Получить перечень файлов в директории-источнике
            var files = FileProvider.GetFiles();

            //Если файлы в директории отсутствуют
            if (files.Length == 0)
            {
                Console.WriteLine($"Source directory is empty");
                return;
            }

            //Вывести на консоль список файлов для выбора пользователем
            Console.WriteLine("Select the file to convert (enter the file number):");

            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {files[i]}");
            }
            
            //Проверка пользовательского ввода
            var result = int.TryParse(Console.ReadLine(), out int fileNumber);
            var fileIndex = fileNumber - 1;

            if (result == false || fileIndex < 0 || fileIndex >= files.Length)
            {
                Console.WriteLine("Incorrect input value");
                return;
            }

            //Запоминаем выбранный файл
            var selectedFile = files[fileIndex];
            var fileName = FileProvider.GetFileName(selectedFile);

            //Преобразовать содержимое выбранного файла в массив слов
            var words = Converter.StringToWords(selectedFile)
                .Select(word => word.ToLower())
                .ToArray();

            //Преобразовать массив слов в словарь, где ключ - слово, значение - количество вхождений слова в исходный массив
            var dictionary = Converter.ToDictionary(words);

            //Отсортировать словарь по убыванию значений
            dictionary = dictionary.OrderByDescending(x => x.Value)
                .ThenBy(x => x.Key)
                .ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);

            //Записать результат преобразований в файл с заданным именем
            FileProvider.WriteToFile(dictionary, fileName);
        }
    }
}
