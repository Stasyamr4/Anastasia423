using Microsoft.VisualBasic;
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

            Сountbukvi(text, out int count1, out int count2);
            Console.WriteLine($"кол-во гласных - {count1}, кол-во согласных - {count2}");

            int Long = CountLongWord(text);
            Console.WriteLine($"самое длинное слово: {Long}");
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

            static void Сountbukvi(string text, out int count1, out int count2)
            {
            count1 = 0;
            count2 = 0;
            string glasnie = "аеёиоуыэюя";
            string soglasnie = "бвгджзйклмнпрстфхцчшщ";


            for (int i = 0; i< text.Length;i++)
            {
                char current = char.ToLower(text[i]);//нижний регистр

                if (glasnie.IndexOf(current) >= 0)
                {
                    count1++;
                }
                
                else if (soglasnie.IndexOf(current) >= 0)
                {
                    count2++;
                }
                
            }
            Console.WriteLine ($"гласные - {count1}, согласные - {count2}");    
            }
        }
    static int CountLongWord(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return 0;
        }
        string[] Long = text.Split(new[] { ' ', '\t', '\n', '\r' },
            StringSplitOptions.RemoveEmptyEntries);
        string longword = Long[0];
        for (int i = 1; i < Long.Length; i++)
        {
            if (Long[i].Length > longword.Length)
            {
                longword = Long[i];
            }
        }
        Console.WriteLine($"самое длинное слово - {longword} (длина - {longword.Length})");
        return longword.Length;




    }
}

