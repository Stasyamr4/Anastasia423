using System;
using System.Collections.Generic;
using System.Linq;

public enum Genre
{
    Фантастика = 1,
    Детектив,
    Роман,
    Научная,
    Историческая
}

public class Book
{
    private static int nextId = 1;

    public int Id { get; private set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public Genre Genre { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }

    public Book()
    {
        Id = nextId++;
    }

    public override string ToString()
    {
        return $"ID: {Id} | Название: {Title} | Автор: {Author} | Жанр: {Genre} | Год: {Year} | Цена: {Price:F2} руб.";
    }
}
public class Library
{
    private List<Book> books = new List<Book>();

    public void AddBook(string title, string author, Genre genre, int year, decimal price)
    {
        var book = new Book
        {
            Title = title,
            Author = author,
            Genre = genre,
            Year = year,
            Price = price
        };
        books.Add(book);
        Console.WriteLine($"\n Книга успешно добавлена! ID: {book.Id}");
    }

    public bool RemoveBook(int id)
    {
        var book = books.FirstOrDefault(b => b.Id == id);
        if (book != null)
        {
            books.Remove(book);
            Console.WriteLine($"\n Книга с ID {id} успешно удалена.");
            return true;
        }
        Console.WriteLine($"\n Книга с ID {id} не найдена.");
        return false;
    }

    public void FindBooksByTitle(string title)
    {
        var foundBooks = books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
        DisplayBooks("Найденные книги по названию:", foundBooks);
    }

    public void FindBooksByAuthor(string author)
    {
        var foundBooks = books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
        DisplayBooks("Найденные книги по автору:", foundBooks);
    }

    public void FindBooksByGenre(Genre genre)
    {
        var foundBooks = books.Where(b => b.Genre == genre).ToList();
        DisplayBooks($"Найденные книги в жанре {genre}:", foundBooks);
    }

    public void SortByTitle()
    {
        var sortedBooks = books.OrderBy(b => b.Title).ToList();
        DisplayBooks("Книги отсортированные по названию:", sortedBooks);
    }

    public void SortByYear()
    {
        var sortedBooks = books.OrderBy(b => b.Year).ToList();
        DisplayBooks("Книги отсортированные по году издания:", sortedBooks);
    }

    public void ShowMostExpensiveAndCheapest()
    {
        if (!books.Any())
        {
            Console.WriteLine("В библиотеке нет книг.");
            return;
        }

        var mostExpensive = books.OrderByDescending(b => b.Price).First();
        var cheapest = books.OrderBy(b => b.Price).First();

        Console.WriteLine("=== САМАЯ ДОРОГАЯ КНИГА ===");
        Console.WriteLine(mostExpensive);
        Console.WriteLine("\n=== САМАЯ ДЕШЕВАЯ КНИГА ===");
        Console.WriteLine(cheapest);
    }

    public void GroupByAuthor()
    {
        var groupedBooks = books.GroupBy(b => b.Author)
                               .OrderByDescending(g => g.Count())
                               .ToList();

        Console.WriteLine("=== КОЛИЧЕСТВО КНИГ ПО АВТОРАМ ===");
        foreach (var group in groupedBooks)
        {
            Console.WriteLine($"Автор: {group.Key} | Количество книг: {group.Count()}");
        }
    }

    public void DisplayAllBooks()
    {
        DisplayBooks("=== ВСЕ КНИГИ В БИБЛИОТЕКЕ ===", books);
    }

    private void DisplayBooks(string message, List<Book> booksToDisplay)
    {
        Console.WriteLine($"\n{message}");
        if (!booksToDisplay.Any())
        {
            Console.WriteLine("Книги не найдены.");
            return;
        }

        foreach (var book in booksToDisplay)
        {
            Console.WriteLine(book);
        }
    }

    public void AddTestData()
    {
        AddBook("Война и мир", "Лев Толстой", Genre.Роман, 1869, 1200.50m);
        AddBook("Преступление и наказание", "Федор Достоевский", Genre.Роман, 1866, 950.00m);
        AddBook("Мастер и Маргарита", "Михаил Булгаков", Genre.Фантастика, 1967, 1100.00m);
        AddBook("1984", "Джордж Оруэлл", Genre.Фантастика, 1949, 850.75m);
        AddBook("Шерлок Холмс", "Артур Конан Дойл", Genre.Детектив, 1887, 700.25m);
    }

    public bool BookExists(int id) => books.Any(b => b.Id == id);
    public bool HasBooks() => books.Any();
}
class Program
{
    static Library library = new Library();

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Добавляем тестовые данные
        library.AddTestData();

        ShowMainMenu();
    }

    static void ShowMainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== БИБЛИОТЕКА КНИГ ===");
            Console.WriteLine("1. Показать все книги");
            Console.WriteLine("2. Добавить книгу");
            Console.WriteLine("3. Удалить книгу");
            Console.WriteLine("4. Найти книгу");
            Console.WriteLine("5. Сортировать книги");
            Console.WriteLine("6. Самая дорогая/дешевая книга");
            Console.WriteLine("7. Статистика по авторам");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1": ShowAllBooks(); break;
                case "2": AddBookMenu(); break;
                case "3": RemoveBookMenu(); break;
                case "4": FindBookMenu(); break;
                case "5": SortBooksMenu(); break;
                case "6": ShowPriceExtremes(); break;
                case "7": ShowAuthorStats(); break;
                case "0": return;
                default: ShowError("Неверный выбор!"); break;
            }
        }
    }
    static void ShowAllBooks()
    {
        Console.Clear();
        library.DisplayAllBooks();
        WaitForUser();
    }

    static void AddBookMenu()
    {
        Console.Clear();
        Console.WriteLine("=== ДОБАВЛЕНИЕ НОВОЙ КНИГИ ===");

        string title = GetValidatedInput("Введите название книги: ",
            input => !string.IsNullOrWhiteSpace(input), "Название не может быть пустым!");

        string author = GetValidatedInput("Введите автора книги: ",
            input => !string.IsNullOrWhiteSpace(input), "Автор не может быть пустым!");

        Genre genre = GetGenreFromUser();
        int year = GetValidatedYear();
        decimal price = GetValidatedPrice();

        library.AddBook(title, author, genre, year, price);
        WaitForUser();
    }

    static void RemoveBookMenu()
    {
        Console.Clear();
        if (!library.HasBooks())
        {
            ShowError("В библиотеке нет книг для удаления!");
            return;
        }

        library.DisplayAllBooks();
        Console.Write("\nВведите ID книги для удаления: ");

        if (int.TryParse(Console.ReadLine(), out int id))
        {
            library.RemoveBook(id);
        }
        else
        {
            ShowError("Неверный формат ID!");
        }
        WaitForUser();
    }
    static void FindBookMenu()
    {
        Console.Clear();
        Console.WriteLine("=== ПОИСК КНИГ ===");
        Console.WriteLine("1. По названию");
        Console.WriteLine("2. По автору");
        Console.WriteLine("3. По жанру");
        Console.Write("Выберите тип поиска: ");

        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Console.Write("Введите название для поиска: ");
                library.FindBooksByTitle(Console.ReadLine());
                break;
            case "2":
                Console.Write("Введите автора для поиска: ");
                library.FindBooksByAuthor(Console.ReadLine());
                break;
            case "3":
                Genre genre = GetGenreFromUser();
                library.FindBooksByGenre(genre);
                break;
            default:
                ShowError("Неверный выбор!");
                break;
        }
        WaitForUser();
    }