using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
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
                    break;
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

            (int countsogl, int countglasn) = Сountbukvi(text);
            Console.WriteLine($"кол-во гласных - {countglasn}, кол-во согласных - {countsogl}");

            int Long = CountLongWord(text);
            Console.WriteLine($"самое длинное слово: {Long}");

            Dictionary<char, int> frequency = CountLetterFrequency(text);
            DisplayFrequency(frequency);
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

            static (int countgl, int countsogl) Сountbukvi(string text)
            {
            int count1 = 0;
            int count2 = 0;
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
            return (count1, count2);
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
    static Dictionary<char, int> CountLetterFrequency(string text)
    {
        Dictionary<char, int> frequency = new Dictionary<char, int>();
        for (int i = 0; i < text.Length; i++)
        {
            char currentChar = char.ToLower(text[i]); 
            if ((currentChar >= 'а' && currentChar <= 'я') || currentChar == 'ё')
            {
                if (frequency.ContainsKey(currentChar))
                {
                    frequency[currentChar]++;
                }
                else
                {
                    frequency[currentChar] = 1;
                }
            }
        }

        return frequency;
    }

    static void DisplayFrequency(Dictionary<char, int> frequency)
    {
        Console.WriteLine("Статистика по частоте букв:");
        string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        for (int i = 0; i < alphabet.Length; i++)
        {
            char letter = alphabet[i];
            if (frequency.ContainsKey(letter))
            {
                Console.WriteLine($"Буква '{letter}': {frequency[letter]} раз");
            }
        }
    }

}

