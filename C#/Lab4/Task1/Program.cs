using ConsoleInterface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopSystem
{
    public interface ISearchable
    {
        List<Product> SearchByCategory(string category);
        List<Product> SearchByPrice(double minPrice, double maxPrice);
        List<Product> SearchByRating(double minRating);
    }
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double Rating { get; set; }

        public Product(string name, double price, string desc, string cat, double rating)
        {
            Name = name;
            Price = price;
            Description = desc;
            Category = cat;
            Rating = rating;
        }

        public override string ToString()
        {
            return $"{Name} | {Category} | {Price} грн | Рейтинг: {Rating}";
        }
    }
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public List<Order> PurchaseHistory { get; set; } = new List<Order>();

        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
    public class Order
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public uint Quantity { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; }

        public Order(List<Product> items, uint qty)
        {
            Products = items;
            Quantity = qty;
            TotalPrice = items.Sum(p => p.Price) * qty;
            Status = "Обробляється";
        }

        public override string ToString()
        {
            return $"Замовлення: {Products.Count} товарів, К-сть: {Quantity}, Сума: {TotalPrice} грн, Статус: {Status}";
        }
    }
    public class Shop : ISearchable
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public List<User> Users { get; set; } = new List<User>();
        public List<Order> Orders { get; set; } = new List<Order>();

        public Shop()
        {
            Products.Add(new Product("Ноутбук HP", 25000, "Потужний ноутбук", "Електроніка", 4.5));
            Products.Add(new Product("Кава Jacobs", 120, "Розчинна кава", "Продукти", 4.1));
            Products.Add(new Product("Смартфон Samsung", 18000, "Android пристрій", "Електроніка", 4.7));
            Products.Add(new Product("Шкарпетки", 50, "Чорні бавовняні", "Одяг", 3.9));
        }

        public void RegisterUser(string login, string password)
        {
            Users.Add(new User(login, password));
            Console.WriteLine($"Користувача {login} зареєстровано.");
        }

        public User LoginUser(string login, string password)
        {
            return Users.FirstOrDefault(u => u.Login == login && u.Password == password);
        }

        public void PlaceOrder(User user, List<Product> items, uint qty)
        {
            var order = new Order(items, qty);
            Orders.Add(order);
            user.PurchaseHistory.Add(order);
            Console.WriteLine("Створено замовлення: " + order);
        }
        public List<Product> SearchByCategory(string category)
            => Products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<Product> SearchByPrice(double min, double max)
            => Products.Where(p => p.Price >= min && p.Price <= max).ToList();

        public List<Product> SearchByRating(double minRating)
            => Products.Where(p => p.Rating >= minRating).ToList();

        public void PrintProducts()
        {
            foreach (var p in Products)
                Console.WriteLine(p);
        }
        public static void Main()
        {
            Shop shop = new Shop();

            shop.RegisterUser("admin", "1234");
            var user = shop.LoginUser("admin", "1234");

            ConsoleInterface<int> consoleInterface = new ConsoleInterface<int>();

            consoleInterface.AddOption("Показати всі товари", (_) => shop.PrintProducts());
            consoleInterface.AddOption("Пошук по категорії", (_) =>
            {
                Console.Write("Категорія: ");
                var cat = Console.ReadLine();
                shop.Show(shop.SearchByCategory(cat));
            });
            consoleInterface.AddOption("Пошук по ціні", (_) =>
            {
                Console.Write("Мін ціна: "); 
                double min = consoleInterface.ReadDouble(); 
                Console.Write("Макс ціна: "); 
                double max = consoleInterface.ReadDouble();
                shop.Show(shop.SearchByPrice(min, max));
            });
            consoleInterface.AddOption("Пошук за рейтингом", (_) =>
            {
                Console.Write("Мін рейтинг: "); 
                double r = consoleInterface.ReadDouble();
                shop.Show(shop.SearchByRating(r));
            });
            consoleInterface.AddOption("Створити замовлення", (_) =>
            {
                Console.WriteLine("Введіть назву товару: ");
                var name = Console.ReadLine();
                var product = shop.Products.FirstOrDefault(p => p.Name == name);
                if (product != null)
                {
                    Console.Write("Кількість: "); uint q = consoleInterface.ReadUint(1);
                    shop.PlaceOrder(user, new List<Product> { product }, q);
                }
                else Console.WriteLine("Товар не знайдено.");
            });

            consoleInterface.FinalizeOptions();

            while (true)
            {
                consoleInterface.PrintOptions();

                uint selection = consoleInterface.ReadUint(0, consoleInterface.GetLastIndex());
                consoleInterface.Select(selection, 0);
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void Show(List<Product> list)
        {
            if (list.Count == 0) Console.WriteLine("Нічого не знайдено.");
            else list.ForEach(p => Console.WriteLine(p));
        }
    }

    public class Program
    {

    }
}
