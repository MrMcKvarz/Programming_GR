using ConsoleInterface;
using System;

class User
{
    public string Login { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public DateTime RegistrationDate { get; }
    public User(string login, string firstName, string lastName, int age)
    {
        Login = login;
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        RegistrationDate = DateTime.Now;
    }
    public void PrintInfo()
    {
        Console.WriteLine("\n----- Інформація про користувача -----");
        Console.WriteLine($"Логін: {Login}");
        Console.WriteLine($"Ім'я: {FirstName}");
        Console.WriteLine($"Прізвище: {LastName}");
        Console.WriteLine($"Вік: {Age}");
        Console.WriteLine($"Дата заповнення анкети: {RegistrationDate}");
    }
}

class Program
{
    static void Main()
    {
        ConsoleInterface<double> consoleInterface = new ConsoleInterface<double>();
        Console.Write("Введіть логін: ");
        string login = Console.ReadLine();

        Console.Write("Введіть ім'я: ");
        string firstName = Console.ReadLine();

        Console.Write("Введіть прізвище: ");
        string lastName = Console.ReadLine();

        Console.Write("Введіть вік: ");
        uint age = consoleInterface.ReadUint();

        User user = new User(login, firstName, lastName, age);

        user.PrintInfo();
    }
}