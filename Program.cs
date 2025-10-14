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