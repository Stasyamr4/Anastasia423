using System;
using System.Runtime.CompilerServices;

class Program
{
    // Список для хранения статистики всех текстов
    static List<string> text = new List<string>();
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Введите текст");
            string text = Console.ReadLine();

            if (!String.IsNullOrEmpty(text))
            {

                if (text.Length < 100)
                {
                    Console.WriteLine("\nВведите текст не менее 100 символов\n");
                }

            }

            else
            {
                Console.WriteLine("Значение пусто, введите текст");
            }

            int word = Words(text);
            Console.WriteLine($"количество слов: {word}");

            int sentenceCount = CountSentences(text);
            Console.WriteLine($"Количество предложений: {sentenceCount}");
        }

        static int Words(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            string[] words = text.Split(new[] { ' ', '\t', '\n', '\r' },
                                      StringSplitOptions.RemoveEmptyEntries);

            string shortestWord = words[0];
            for (int i = 1; i < words.Length; i++)
            {
                if (words[i].Length < shortestWord.Length)
                {
                    shortestWord = words[i];
                }
            }
            Console.WriteLine($"Самое короткое слово: '{shortestWord}' (длина: {shortestWord.Length})");
            return words.Length;
        }

            static int CountSentences(string text)
            {
                int count = 0;
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == '.' || text[i] == '!' || text[i] == '?')
                        count++;
                }
                return count;
            }

        }
    
}

