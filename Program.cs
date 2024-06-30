using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TextRead
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"D:\MasterAndMargarita.txt";
            string text;

            // Чтение файла
            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
            {
                text = reader.ReadToEnd();
            }

            // Удаление знаков пунктуации с помощью регулярных выражений
            string cleanedText = Regex.Replace(text, @"[^\p{L}\s]", "");

            // Разбиение текста на слова
            string[] words = cleanedText.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Подсчет частоты слов
            Dictionary<string, int> wordFrequency = new Dictionary<string, int>();

            foreach (string word in words)
            {
                if (word.Length >= 3 && word.Length <= 20)
                {
                    if (wordFrequency.ContainsKey(word))
                    {
                        wordFrequency[word]++;
                    }
                    else
                    {
                        wordFrequency.Add(word, 1);
                    }
                }
            }

            // Сортировка по частоте упоминания
            var sortedList = new List<KeyValuePair<string, int>>(wordFrequency);
            sortedList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            var top50Words = sortedList.Take(50);

            // Вывод результата
            Console.WriteLine("+----+-------------------+-----------------+");
            Console.WriteLine("| №  | слово             | встречается раз |");
            Console.WriteLine("+----+-------------------+-----------------+");

            int rank = 1;
            foreach (var pair in top50Words)
            {
                Console.WriteLine($"| {rank,2} | {pair.Key,-17} | {pair.Value,15} |");
                rank++;
            }

            Console.WriteLine("+----+-------------------+-----------------+");
        }
    }
}

