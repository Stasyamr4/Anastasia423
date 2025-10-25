using System;
using System.Collections.Generic;
using System.Diagnostics;
class Person
{
    public String FIO;
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
    private string Telephone;
    public List<Cource> cources;
    private string City;

    public Student(string fio, DateOnly birthday, string gender,
                   int studentNumberId, string telephone, string city)
                   : base(fio, birthday, gender)
    {
        id = NextID++;
        StudentNumberID = studentNumberId;
        Telephone = telephone;
        City = city;
        cources = new List<Cource>();
    }
    public void AddCource(Cource cource)
    {
        cources.Add(cource);
        Console.WriteLine($"Студент {FIO} успешно записан на курс '{cource.Name}'");
    }
    public override void Print()
    {
        Console.WriteLine("Студент");
        base.Print();
        Console.WriteLine($"Студент ID: {StudentNumberID}\nТелефон: {Telephone}\nГород: {City}");

        if (cources.Count > 0)
        {
            Console.WriteLine("Курсы:");
            foreach (var cource in cources)
            {
                Console.WriteLine($"  - {cource.Name}");
            }
        }
    }
}
class Teacher : Person
{
    public int id = 0;
    static public int NextID = 1;
    private string Subject;
    private string Telephone;
    private string City;
    public List<Cource> cources;


    public Teacher(string fio, DateOnly birthday, string gender, string subject, string telephone, string city)
        : base(fio, birthday, gender)
    {
        id = NextID++ ;
        Subject = subject;
        Telephone = telephone;
        City = city;
        cources = new List<Cource>();
    }
    public void AddCource(Cource cource)
    {
        cources.Add(cource);
        Console.WriteLine($"Учитель {FIO} успешно записан на курс '{cource.Name}'");
    }

    public override void Print()
    {
        Console.WriteLine("Учитель");
        base.Print();
        Console.WriteLine($"Предмет: {Subject}\nТелефон: {Telephone}\nГород: {City}");

        if (cources.Count > 0)
        {
            Console.WriteLine("Преподаваемые курсы:");
            foreach (var cource in cources)
            {
                Console.WriteLine($"  - {cource.Name}");
            }
        }
    }
}
class Cource
{
    public int id = 0;
    static public int NextID = 1;
    public string Name;
    public int Hours;

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

        students.Add(new Student("Иванов Иван Иванович", new DateOnly(2000, 5, 15), "М", 244878, "+79991112233", "Москва"));
        students.Add(new Student("Петрова Анна Сергеевна", new DateOnly(2001, 8, 22), "Ж", 237856, "+79992223344", "Санкт-Петербург"));
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
        cources.Add(new Cource("Русский язык", 20));
        cources.Add(new Cource("Английский язык", 20));
        cources.Add(new Cource("Испанский язык", 20));
        cources.Add(new Cource("Литература и писатели", 20));
        cources.Add(new Cource("Изо", 20));
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
    static void ShowTeacher()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("Учителей нет");
            return;
        }

        foreach (var teacher in teachers)
        {
            teacher.Print();
            Console.WriteLine("");
        }
    }
    static void ShowCource()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("Курсов нет");
            return;
        }

        foreach (var cource in cources)
        {
            cource.Print();
            Console.WriteLine("");
        }
    }
    static void RegisterStudent()
    {
        Console.Clear();
        Console.WriteLine("ЗАПИСЬ СТУДЕНТА НА КУРС");

        if (students.Count == 0)
        {
            Console.WriteLine("Ошибка: нет студентов!");
            return;
        }

        if (cources.Count == 0)
        {
            Console.WriteLine("Ошибка: нет курсов!");
            return;
        }

        // Показываем всех студентов
        Console.WriteLine("\nСписок студентов:");
        foreach (var student in students)
        {
            Console.WriteLine($"{student.id}. {student.FIO}");
        }

        // Выбираем студента
        Console.Write("\nВведите ID студента: ");
        if (!int.TryParse(Console.ReadLine(), out int studentId))
        {
            Console.WriteLine("Ошибка: нужно ввести число!");
            return;
        }

        // Ищем студента через LINQ
        Student serchSt = students.FirstOrDefault(s => s.id == studentId);
        if (serchSt == null)
        {
            Console.WriteLine("Ошибка: студент не найден!");
            return;
        }

        // Показываем курсы этого студента
        Console.WriteLine($"\nТекущие курсы студента {serchSt.FIO}:");
        if (serchSt.cources.Count == 0)
        {
            Console.WriteLine("Нет записанных курсов");
        }
        else
        {
            foreach (var cource in serchSt.cources)
            {
                cource.Print();
                Console.WriteLine("");
            }
        }

        // Показываем все доступные курсы
        Console.WriteLine("\nВсе доступные курсы:");
        foreach (var cource in cources)
        {
            // Показываем только те курсы, на которые студент еще не записан
            if (!serchSt.cources.Any(c => c.id == cource.id))
            {
                Console.WriteLine($"{cource.id}. {cource.Name}");
            }
        }

        // Выбираем курс для записи
        Console.Write("\nВведите ID курса для записи: ");
        if (!int.TryParse(Console.ReadLine(), out int courceId))
        {
            Console.WriteLine("Ошибка: нужно ввести число!");
            return;
        }

        // Ищем курс через LINQ
        Cource selectedCource = cources.FirstOrDefault(c => c.id == courceId);
        if (selectedCource == null)
        {
            Console.WriteLine("Ошибка: курс не найден!");
            return;
        }

        // Проверяем, не записан ли уже студент на этот курс
        if (serchSt.cources.Any(c => c.id == courceId))
        {
            Console.WriteLine($"ОШИБКА: Студент уже записан на курс '{selectedCource.Name}'!");
            return;
        }

        // Записываем студента на курс
        serchSt.AddCource(selectedCource);
    }

    static void RegisterTeacher()
    {
        Console.Clear();
        Console.WriteLine("ЗАПИСЬ УЧИТЕЛЯ НА КУРС");

        if (teachers.Count == 0)
        {
            Console.WriteLine("Ошибка: нет учителей!");
            return;
        }

        if (cources.Count == 0)
        {
            Console.WriteLine("Ошибка: нет курсов!");
            return;
        }
        Console.WriteLine("\nСписок учителей:");
        foreach (var teacher in teachers)
        {
            Console.WriteLine($"{teacher.id}. {teacher.FIO}");
        }
        // Выбираем учителя
        Console.Write("\nВведите ID учителя: ");
        if (!int.TryParse(Console.ReadLine(), out int teacherId))
        {
            Console.WriteLine("Ошибка: нужно ввести число!");
            return;
        }

        // Ищем учителя
        Teacher searchTeacher = teachers.FirstOrDefault(t => t.id == teacherId);
        if (searchTeacher == null)
        {
            Console.WriteLine("Ошибка: учитель не найден!");
            return;
        }
        Console.WriteLine($"\nТекущие курсы учителя {searchTeacher.FIO}:");
        if (searchTeacher.cources.Count == 0)
        {
            Console.WriteLine("Нет записанных курсов");
        }
        else
        {
            foreach (var cource in searchTeacher.cources)
            {
                Console.WriteLine($"  - {cource.Name}");
            }
        }
        Console.WriteLine("\nВсе доступные курсы:");
        foreach (var cource in cources)
        {
            // Показываем только те курсы, на которые учитель еще не записан
            if (!searchTeacher.cources.Any(c => c.id == cource.id))
            {
                Console.WriteLine($"{cource.id}. {cource.Name}");
            }
        }

        // Выбираем курс для записи
        Console.Write("\nВведите ID курса для записи: ");
        if (!int.TryParse(Console.ReadLine(), out int courceId))
        {
            Console.WriteLine("Ошибка: нужно ввести число!");
            return;
        }
        Cource selectedCource = cources.FirstOrDefault(c => c.id == courceId);
        if (selectedCource == null)
        {
            Console.WriteLine("Ошибка: курс не найден!");
            return;
        }

        // Проверяем, не записан ли уже учитель на этот курс
        if (searchTeacher.cources.Any(c => c.id == courceId))
        {
            Console.WriteLine($"ОШИБКА: Учитель уже записан на курс '{selectedCource.Name}'!");
            return;
        }

        // Записываем учителя на курс
        searchTeacher.AddCource(selectedCource);
    }
    static void ShowCourceStudent()
    {
        Console.Clear();
        Console.WriteLine("КУРСЫ СТУДЕНТА");

        if (students.Count == 0)
        {
            Console.WriteLine("Студентов нет");
            return;
        }

        // Показываем всех студентов
        Console.WriteLine("\nСписок студентов:");
        foreach (var student in students)
        {
            Console.WriteLine($"{student.id}. {student.FIO}");
        }

        // Выбираем студента
        Console.Write("\nВведите ID студента: ");
        if (!int.TryParse(Console.ReadLine(), out int studentId))
        {
            Console.WriteLine("Ошибка: нужно ввести число!");
            return;
        }

        // Ищем студента
        Student searchStudent = students.FirstOrDefault(s => s.id == studentId);
        if (searchStudent == null)
        {
            Console.WriteLine("Ошибка: студент не найден!");
            return;
        }

        // Показываем курсы выбранного студента
        Console.WriteLine($"\nКурсы студента {searchStudent.FIO}:");
        if (searchStudent.cources.Count == 0)
        {
            Console.WriteLine("Нет записанных курсов");
        }
        else
        {
            foreach (var cource in searchStudent.cources)
            {
                Console.WriteLine($"  - {cource.Name} ({cource.Hours} часов)");
            }
        }
    }
    static void ShowCourceTeacher()
    {
        Console.Clear();
        Console.WriteLine("КУРСЫ УЧИТЕЛЯ");

        if (teachers.Count == 0)
        {
            Console.WriteLine("Учителей нет");
            return;
        }

        // Показываем всех учителей
        Console.WriteLine("\nСписок учителей:");
        foreach (var teacher in teachers)
        {
            Console.WriteLine($"{teacher.id}. {teacher.FIO}");
        }

        // Выбираем учителя
        Console.Write("\nВведите ID учителя: ");
        if (!int.TryParse(Console.ReadLine(), out int teacherId))
        {
            Console.WriteLine("Ошибка: нужно ввести число!");
            return;
        }

        // Ищем учителя
        Teacher searchTeacher = teachers.FirstOrDefault(t => t.id == teacherId);
        if (searchTeacher == null)
        {
            Console.WriteLine("Ошибка: учитель не найден!");
            return;
        }

        // Показываем курсы выбранного учителя
        Console.WriteLine($"\nКурсы учителя {searchTeacher.FIO}:");
        if (searchTeacher.cources.Count == 0)
        {
            Console.WriteLine("Нет записанных курсов");
        }
        else
        {
            foreach (var cource in searchTeacher.cources)
            {
                Console.WriteLine($"  - {cource.Name} ({cource.Hours} часов)");
            }
        }

    }
    }
