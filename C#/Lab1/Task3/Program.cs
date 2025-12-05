using ConsoleInterface;
using System;

class Employee
{
    public string FirstName { get; }
    public string LastName { get; }

    public string Position { get; set; }
    public double Experience { get; set; }

    private const double ExpRate = 0.05;
    public Employee(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public double CalculateSalary()
    {
        double baseSalary;

        switch (Position.ToLower())
        {
            case "менеджер":
                baseSalary = 15000;
                break;

            case "розробник":
            case "програміст":
                baseSalary = 25000;
                break;

            case "тестувальник":
                baseSalary = 18000;
                break;

            default:
                baseSalary = 12000;
                break;
        }

        double experienceBonus = Math.Min(Experience * ExpRate, 0.5);

        return baseSalary * (1 + experienceBonus);
    }
    public double CalculateTax()
    {
        return CalculateSalary() * 0.18d;
    }
    static void Main(string[] args)
    {
        ConsoleInterface<double> consoleInterface = new ConsoleInterface<double>();

        Console.Write("Введіть ім'я: ");
        string firstName = Console.ReadLine();

        Console.Write("Введіть прізвище: ");
        string lastName = Console.ReadLine();

        Console.Write("Введіть посаду: ");
        string position = Console.ReadLine();

        Console.Write("Введіть стаж (років): ");
        double experience = consoleInterface.ReadDouble();

        Employee emp = new Employee(firstName, lastName)
        {
            Position = position,
            Experience = experience
        };

        double salary = emp.CalculateSalary();
        double tax = emp.CalculateTax();

        Console.WriteLine("\n----- Інформація про співробітника -----");
        Console.WriteLine($"Ім'я: {emp.FirstName}");
        Console.WriteLine($"Прізвище: {emp.LastName}");
        Console.WriteLine($"Посада: {emp.Position}");
        Console.WriteLine($"Оклад: {salary:F2} грн");
        Console.WriteLine($"Податковий збір (18%): {tax:F2} грн");
    }
}

