using ConsoleInterface;
using System;

class Rectangle
{
    private double side1;
    private double side2;
    public Rectangle(double side1, double side2)
    {
        if (side1 <= 0 || side2 <= 0)
            throw new ArgumentException();
        this.side1 = side1;
        this.side2 = side2;
    }
    public double CalculateArea()
    {
        return side1 * side2;
    }
    public double CalculatePerimeter()
    {
        return 2 * (side1 + side2);
    }
    public double Area
    {
        get { return CalculateArea(); }
    }
    public double Perimeter
    {
        get { return CalculatePerimeter(); }
    }

    static void Main()
    {
        ConsoleInterface<double> consoleInterface = new ConsoleInterface<double>();
        Console.Write("Введіть довжину першої сторони: ");
        double s1 = consoleInterface.ReadDouble();

        Console.Write("Введіть довжину другої сторони: ");
        double s2 = consoleInterface.ReadDouble();

        Rectangle rect = new Rectangle(s1, s2);

        Console.WriteLine($"\nПлоща прямокутника: {rect.Area}");
        Console.WriteLine($"Периметр прямокутника: {rect.Perimeter}");
    }
}
