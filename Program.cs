using System;
class Program
{
    static void Main()
    {
        List<Student> students = new List<Student>();
        List<Teacher> list = new List<Teacher>();  
        List<Cource> cources = new List<Cource>();
    }
    static void ShowMenu()
    {
        while (true)
        {
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
        }
    }
}
class Person
{
    private int id;
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
    private int StudentNumberID;
    private int CourseNumber;
    private string Telephone;
    private string City;

    public Student(string fio, DateOnly birthday, string gender,
                   int studentNumberId, int courseNumber, string telephone, string city)
                   : base(fio, birthday, gender)
    {
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
    private string Subject;
    private string Telephone;
    private string City;


    public Teacher(string fio, DateOnly birthday, string gender, string subject, string telephone, string city)
        : base(fio, birthday, gender)
    {
        Subject = subject;
        Telephone =telephone;
        City = city;
    }

    public override void Print()
    {
        Console.WriteLine("Учитель");
        base.Print();
        Console.WriteLine($"Предмет: {Subject}\nТелефон: {Telephone}\nГород: {City}");
    }
}
class Cource : Person
{
    private int Id;
    private string Name;
    private string Teacher;

    public Cource(string fio, DateOnly birthday, string gender, int id,  string name, string teacher)
        : base(fio, birthday, gender)
    {
        Id = id;
        Name = name; 
        Teacher = teacher;
    }
}

