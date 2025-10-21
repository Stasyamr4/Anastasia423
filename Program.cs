using System;
using System.Diagnostics;
class Person
{
    private String FIO;
    private DateOnly Birthday;
    private string Gender;
    public Person(string fio, DateOnly birthday, string gender)
    {
        FIO = fio;
        Birthday = birthday;
        Gender = gender;
    }
    public virtual void Print()
    {
        Console.WriteLine($"ФИО: {FIO}\nДень рождения: {Birthday}\nПол: {Gender} ");
    }
}
class Student : Person
{
    public int id = 0;
    static public int NextID = 1;
    private int StudentNumberID;
    private int CourseNumber;
    private string Telephone;
    private string City;

    public Student(string fio, DateOnly birthday, string gender,
                   int studentNumberId, int courseNumber, string telephone, string city)
                   : base(fio, birthday, gender)
    {
        id = NextID++;
        StudentNumberID = studentNumberId;
        CourseNumber = courseNumber;
        Telephone = telephone;
        City = city;
    }
    public override void Print()
    {
        Console.WriteLine("Студент");
        base.Print();
        Console.WriteLine($"Студент ID: {StudentNumberID}\nКурс: {CourseNumber}\nТелефон: {Telephone}\nГород: {City}");
    }
}
class Teacher : Person
{
    public int id = 0;
    static public int NextID = 1;
    private string Subject;
    private string Telephone;
    private string City;


    public Teacher(string fio, DateOnly birthday, string gender, string subject, string telephone, string city)
        : base(fio, birthday, gender)
    {
        id = NextID++ ;
        Subject = subject;
        Telephone = telephone;
        City = city;
    }

    public override void Print()
    {
        Console.WriteLine("Учитель");
        base.Print();
        Console.WriteLine($"Предмет: {Subject}\nТелефон: {Telephone}\nГород: {City}");
    }
}
class Cource
{
    public int id = 0;
    static public int NextID = 1;
    private string Name;
    private int Hours;

    public Cource(string name, int hours)
    {
        id = NextID++ ;
        Name = name;
        Hours = hours;
    }
    public void Print()
    {
        Console.WriteLine("Курсы");
        Console.WriteLine($"Предмет: {Name}\nЧасы курса: {Hours}");
    }
}


class Program
{
    static List<Student> students = new List<Student>();
    static List<Teacher> teachers = new List<Teacher>();
    static List<Cource> cources = new List<Cource>();

    static void Main()
    {
        ShowMenu();
    }
    static void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Добавить студента");
            Console.WriteLine("2. Добавить учителя");
            Console.WriteLine("3. Добавить курс");
            Console.WriteLine("4. Показать всех студентов");
            Console.WriteLine("5. Показать всех учителей");
            Console.WriteLine("6. Показать все курсы");
            Console.WriteLine("7. Записаться на курс (студент)");
            Console.WriteLine("8. Записаться на курс (учитель)");
            Console.WriteLine("9. Показать курсы студента");
            Console.WriteLine("10. Показать курсы учителя");
            Console.WriteLine("0. Выход");

            var choice = Console.ReadLine();
            switch (choice)
            {
                
                case "1": AddStudent(); break;
                case "2": AddTeacher(); break;
                case "3": AddCource(); break;
                case "4": ShowStudent(); break;
                case "5": ShowTeacher(); break;
                case "6": ShowCource(); break;
                case "7": RegisterStudent(); break;
                case "8": RegisterTeacher(); break;
                case "9": ShowCourceStudent(); break;
                case "10": ShowCourceTeacher(); break;
                case "0": return;
                default: Console.WriteLine("Неверный выбор!"); break;
            }   
                if (choice != "0")
                  {
                        Console.WriteLine("\nНажмите любую клавишу для продолжения");
                        Console.ReadKey();
                  }
            
        }
    }
    static void AddStudent()
    {

        students.Add(new Student("Иванов Иван Иванович", new DateOnly(2000, 5, 15), "М", 244878, 3, "+79991112233", "Москва"));
        students.Add(new Student("Петрова Анна Сергеевна", new DateOnly(2001, 8, 22), "Ж", 237856, 1, "+79992223344", "Санкт-Петербург"));
    }
    static void AddTeacher()
    {
        teachers.Add(new Teacher("Сидоров Алексей Владимирович", new DateOnly(1980, 3, 10), "М", "Физика", "+79776153755", "Москва"));
        teachers.Add(new Teacher("Козлова Елена Михайловна", new DateOnly(1975, 11, 5), "Ж", "Математика", "+79256754323", "Казань"));
    }
    static void AddCource()
    {
        cources.Add(new Cource("Высшая математика", 30));
        cources.Add(new Cource("Общая физика", 20));
    }
    static void ShowStudent()
    {
        Console.Clear();
        Console.WriteLine("СПИСОК СТУДЕНТОВ");

        if (students.Count == 0)
        {
            Console.WriteLine("Студентов нет");
            return;
        }

        foreach (var student in students)
        {
            student.Print();
            Console.WriteLine("");
        }
    }

    
}
