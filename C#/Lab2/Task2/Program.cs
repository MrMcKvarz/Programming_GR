using System;

class Point
{
    private double x;
    private double y;
    private string name;

    public double X { get { return x; } }
    public double Y { get { return y; } }
    public string Name { get { return name; } }
    public Point(double x, double y, string name)
    {
        this.x = x;
        this.y = y;
        this.name = name;
    }
}

class Figure
{
    private Point[] points;
    private double perimeter;
    public double P { get { return perimeter; } }
    public Figure(Point a, Point b, Point c)
    {
        points = new Point[] { a, b, c };
    }

    public Figure(Point a, Point b, Point c, Point d)
    {
        points = new Point[] { a, b, c, d };
    }

    public Figure(Point a, Point b, Point c, Point d, Point e)
    {
        points = new Point[] { a, b, c, d, e };
    }
    public double GetSideLength(Point A, Point B)
    {
        return Math.Sqrt((B.X - A.X) * (B.X - A.X) + (B.Y - A.Y) * (B.Y - A.Y));
    }
    public void CalculatePerimeter()
    {
        for (int i = 0; i < points.Length - 1; i++)
        {
            Point current = points[i];
            Point next = points[i + 1];
            perimeter += GetSideLength(current, next);
        }
    }
    public string Name
    {
        get
        {
            string result = "";
            foreach (var p in points)
                result += p.Name;

            return result;
        }
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;

        Point A = new Point(0, 0, "A");
        Point B = new Point(4, 0, "B");
        Point C = new Point(4, 3, "C");
        Point D = new Point(0, 3, "D");
        Point E = new Point(10, 5, "E");

        Figure triangle = new Figure(A, B, C);
        Figure rectangle = new Figure(A, B, C, D);
        Figure polygon = new Figure(A, E, C, D, B);

        triangle.CalculatePerimeter();
        rectangle.CalculatePerimeter();
        polygon.CalculatePerimeter();

        Console.WriteLine($"Фігура: {triangle.Name}");
        Console.WriteLine($"Периметр: {triangle.P:F2}");

        Console.WriteLine($"Фігура: {rectangle.Name}");
        Console.WriteLine($"Периметр: {rectangle.P:F2}");

        Console.WriteLine($"Фігура: {polygon.Name}");
        Console.WriteLine($"Периметр: {polygon.P:F2}");
    }
}