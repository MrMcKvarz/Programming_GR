
internal class Program
{
    private static void Main(string[] args)
    {
        Shape[] shapes = new Shape[]
        {
        new Circle(5),
        new Triangle(4, 6),
        new Square(3)
        };
        foreach (var shape in shapes)
        {
            shape.Draw();
            Console.WriteLine($"Area: {shape.Area()}");
        }
    }
}

interface IDrawable
{
    void Draw();
}

class Shape : IDrawable
{
    public virtual double Area()
    {
        return 0;
    }
    virtual public void Draw()
    {
        Console.WriteLine("Drawing a shape");
    }
}

class Circle : Shape
{
    private double radius;
    public Circle(double radius)
    {
        this.radius = radius;
    }
    public override double Area()
    {
        return Math.PI * radius * radius;
    }

    public override void Draw()
    {
        Console.WriteLine("Drawing a circle");
    }

}

class Triangle : Shape
{
    private double @base;
    private double height;
    public Triangle(double @base, double height)
    {
        this.@base = @base;
        this.height = height;
    }
    public override double Area()
    {
        return 0.5 * @base * height;
    }

    public override void Draw()
    {
        Console.WriteLine("Drawing a triangle");
    }
}

class Square : Shape
{
    private double side;
    public Square(double side)
    {
        this.side = side;
    }
    public override double Area()
    {
        return side * side;
    }
    public override void Draw()
    {
        Console.WriteLine("Drawing a square");
    }
}

