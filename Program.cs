using System;
class Program
{
    static void Main()
    {

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
}

