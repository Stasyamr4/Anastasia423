using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

public enum Genre
{
    Фантастика,
    Детектив,
    Роман,
    Научная,
    Историческая
}

class Program
{
    static List<Books> books = new List<Books>();
    class Books
    {
        int id = 0;
        static int nextID = 1;
        string title { get; set; } = "";
        string author { get; set; } = "";
        string genre { get; set; } = "";
        int year { get; set; }
        int price { get; set; }


        public Books()
        {
            id = nextID++;
        }

        public override string ToString() //override - чтобы вызывать было легче
        {
            return $"ID: {id} | {title} | Автор: {author} | Жанр: {genre} | Год: {year} | Цена: {price} руб.";
        }
    }
        public class Library
        {
            public void AddTestData()
            {
            var book = new Books();
                books.AddRange(new[]
                {
            new Books { book.title = "1984", author = "Джордж Оруэлл", genre = "Фантастика", year = 1949, price = 500 },
            new Books { title = "Преступление и наказание", author = "Фёдор Достоевский", genre = "Роман", year = 1866, price = 450 },
            new Books { title = "Мастер и Маргарита", author = "Михаил Булгаков", genre = "Фантастика", year = 1967, price = 600 },
            new Books { title = "Шерлок Холмс", author = "Артур Конан Дойл", genre = "Детектив", year = 1887, price = 550 },
            new Books { title = "Война и мир", author = "Лев Толстой",genre = "Историческая", year = 1869, price = 700 }
        });

                Console.WriteLine("Добавлено 5 тестовых книг");
            }
        }


    static void Main()
    {
        var library = new Library();
        library.AddTestData();

        // Выводим все книги
        Console.WriteLine("\n=== ВСЕ КНИГИ В БИБЛИОТЕКЕ ===");
        foreach (var book in books)
        {
            Console.WriteLine(book);
        }
    }
}