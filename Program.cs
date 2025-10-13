using System;
using System.Collections.Generic;

class Program
{
    // Список для хранения статистики всех текстов
    static List<string> allStatistics = new List<string>();
    static string currentText = "";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n=== ГЛАВНОЕ МЕНЮ ===");
            Console.WriteLine("1 - Ввести новый текст");
            Console.WriteLine("2 - Подсчёт количества слов");
            Console.WriteLine("3 - Поиск самого короткого слова");
            Console.WriteLine("4 - Подсчёт количества предложений");
            Console.WriteLine("5 - Подсчёт гласных и согласных букв");
            Console.WriteLine("6 - Поиск самого длинного слова");
            Console.WriteLine("7 - Статистика по частоте букв");
            Console.WriteLine("8 - Показать статистику по прошлым текстам");
            Console.WriteLine("0 - Выйти");
            Console.Write("Выберите опцию: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    VvodTexta();
                    break;
                case "2":
                    ShowWordCount();
                    break;
                case "3":
                    ShowShortestWord();
                    break;
                case "4":
                    ShowSentenceCount();
                    break;
                case "5":
                    ShowBukvi();
                    break;
                case "6":
                    ShowLongestWord();
                    break;
                case "7":
                    ShowLetterFrequency();
                    break;
                case "8":
                    ShowPastStatistics();
                    break;
                case "0":
                    Console.WriteLine("Выход из программы...");
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    static void VvodTexta()
    {
        Console.WriteLine("Введите текст (не менее 100 символов):");
        string textInput = Console.ReadLine();

        if (!String.IsNullOrEmpty(textInput))
        {
            if (textInput.Length < 100)
            {
                Console.WriteLine("\nВведите текст не менее 100 символов\n");
                return;
            }
        }
        else
        {
            Console.WriteLine("Значение пусто, введите текст");
            return;
        }

        currentText = textInput;

        // Сохраняем статистику
        string stats = CollectStatistics(textInput);
        allStatistics.Add(stats);
        Console.WriteLine("Текст успешно обработан и сохранен!");
    }

    static string CollectStatistics(string text)
    {
        string stats = "=== СТАТИСТИКА ТЕКСТА ===\n";

        int word = Words(text);
        stats += $"Количество слов: {word}\n";

        int sentenceCount = CountSentences(text);
        stats += $"Количество предложений: {sentenceCount}\n";

        (int countsogl, int countglasn) = Сountbukvi(text);
        stats += $"Гласные: {countglasn}, Согласные: {countsogl}\n";

        int Long = CountLongWord(text);
        stats += $"Длина самого длинного слова: {Long}\n";

        Dictionary<char, int> frequency = CountLetterFrequency(text);
        stats += "Статистика букв: ";
        foreach (var pair in frequency)
        {
            stats += $"{pair.Key}:{pair.Value} ";
        }
        stats += "\n" + new string('=', 50);

        return stats;
    }

    static void ShowWordCount()
    {
        if (string.IsNullOrEmpty(currentText))
        {
            Console.WriteLine("Сначала введите текст через пункт меню 1!");
            return;
        }
        int word = Words(currentText);
        Console.WriteLine($"количество слов: {word}");
    }

    static void ShowShortestWord()
    {
        if (string.IsNullOrEmpty(currentText))
        {
            Console.WriteLine("Сначала введите текст через пункт меню 1!");
            return;
        }
        Words(currentText);
    }

    static void ShowSentenceCount()
    {
        if (string.IsNullOrEmpty(currentText))
        {
            Console.WriteLine("Сначала введите текст через пункт меню 1!");
            return;
        }
        int sentenceCount = CountSentences(currentText);
        Console.WriteLine($"Количество предложений: {sentenceCount}");
    }

    static void ShowBukvi()
    {
        if (string.IsNullOrEmpty(currentText))
        {
            Console.WriteLine("Сначала введите текст через пункт меню 1!");
            return;
        }
        Сountbukvi(currentText);
    }

    static void ShowLongestWord()
    {
        if (string.IsNullOrEmpty(currentText))
        {
            Console.WriteLine("Сначала введите текст через пункт меню 1!");
            return;
        }
        CountLongWord(currentText);
    }

    static void ShowLetterFrequency()
    {
        if (string.IsNullOrEmpty(currentText))
        {
            Console.WriteLine("Сначала введите текст через пункт меню 1!");
            return;
        }
        Dictionary<char, int> frequency = CountLetterFrequency(currentText);
        DisplayFrequency(frequency);
    }

    static void ShowPastStatistics()
    {
        if (allStatistics.Count == 0)
        {
            Console.WriteLine("Статистика по прошлым текстам отсутствует.");
            return;
        }

        Console.WriteLine("\n=== СТАТИСТИКА ПО ПРОШЛЫМ ТЕКСТАМ ===");
        for (int i = 0; i < allStatistics.Count; i++)
        {
            Console.WriteLine($"\n--- Текст #{i + 1} ---");
            Console.WriteLine(allStatistics[i]);
        }
    }

    // Ваши оригинальные методы:

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

        for (int i = 0; i < text.Length; i++)
        {
            char current = char.ToLower(text[i]);

            if (glasnie.IndexOf(current) >= 0)
            {
                count1++;
            }
            else if (soglasnie.IndexOf(current) >= 0)
            {
                count2++;
            }
        }
        Console.WriteLine($"гласные - {count1}, согласные - {count2}");
        return (count1, count2);
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