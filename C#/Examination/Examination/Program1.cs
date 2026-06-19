using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        private static int _productCount = 0;

        public static int ProductCount => _productCount;
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
            _productCount++; 
        }
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Продукт: {Name}");
            Console.WriteLine($"Ціна: {Price:C}");
            Console.WriteLine($"Загальна кількість створених продуктів у системі: {ProductCount}");
        }
    }
    public class ElectronicProduct : Product
    {
        public int WarrantyPeriod { get; set; }
        public ElectronicProduct(string name, decimal price, int warrantyPeriod)
            : base(name, price)
        {
            WarrantyPeriod = warrantyPeriod;
        }
        public override void DisplayInfo()
        {
            Console.WriteLine("===== ЕЛЕКТРОНІКА =====");
            Console.WriteLine($"Продукт: {Name}");
            Console.WriteLine($"Ціна: {Price:C}");
            Console.WriteLine($"Гарантійний термін: {WarrantyPeriod} міс.");
            Console.WriteLine($"Загальна кількість створених продуктів у системі: {ProductCount}");
            Console.WriteLine(new string('-', 25));
        }
    }
    public class ClothingProduct : Product
    {
        public string Size { get; set; }

        public ClothingProduct(string name, decimal price, string size)
            : base(name, price)
        {
            Size = size;
        }
        public override void DisplayInfo()
        {
            Console.WriteLine("===== ОДЯГ =====");
            Console.WriteLine($"Продукт: {Name}");
            Console.WriteLine($"Ціна: {Price:C}");
            Console.WriteLine($"Розмір: {Size}");
            Console.WriteLine($"Загальна кількість створених продуктів у системі: {ProductCount}");
            Console.WriteLine(new string('-', 25));
        }
    }
    class Program
    {
        static void Main(string[] sender)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine($"Початкова кількість продуктів: {Product.ProductCount}\n");

            ElectronicProduct laptop = new ElectronicProduct("Ноутбук ASUS ROG", 42000.00m, 24);
            ClothingProduct jacket = new ClothingProduct("Зимова куртка", 3500.50m, "XL");
            ElectronicProduct smartphone = new ElectronicProduct("iPhone 15", 38000.00m, 12);

            laptop.DisplayInfo();
            jacket.DisplayInfo();
            smartphone.DisplayInfo();

            Console.WriteLine($"Фінальна кількість продуктів: {Product.ProductCount}");
        }
    }
}
