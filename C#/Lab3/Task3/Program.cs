using System;
using System.Collections.Generic;

namespace CityTrafficSimulation
{
    public interface IDriveable
    {
        void Move(Road road);
        void Stop();
    }
    public class Road
    {
        public string Name { get; set; }
        public double Length { get; set; }
        public double Width { get; set; } 
        public int Lanes { get; set; }
        public double TrafficLevel { get; set; }

        public Road(string name, double length, double width, int lanes)
        {
            Name = name;
            Length = length;
            Width = width;
            Lanes = lanes;
            TrafficLevel = 0.0;
        }

        public void UpdateTraffic(double value)
        {
            TrafficLevel = Math.Clamp(value, 0, 1);
        }
    }
    public abstract class Vehicle : IDriveable
    {
        public string Type { get; set; }
        public double Speed { get; set; }
        public double Size { get; set; }
        public bool IsMoving { get; private set; }

        protected Vehicle(string type, double speed, double size)
        {
            Type = type;
            Speed = speed;
            Size = size;
        }

        public virtual void Move(Road road)
        {
            double effectiveSpeed = Speed * (1 - road.TrafficLevel);
            IsMoving = true;
            Console.WriteLine($"{Type} рухається по дорозі {road.Name} зі швидкістю {effectiveSpeed:F1} км/год");
        }

        public virtual void Stop()
        {
            IsMoving = false;
            Console.WriteLine($"{Type} зупинився");
        }
    }
    public class Car : Vehicle
    {
        public Car(double speed) : base("Автомобіль", speed, 1.0) { }
    }

    public class Truck : Vehicle
    {
        public double CargoWeight { get; set; }
        public Truck(double speed, double cargo) : base("Вантажівка", speed, 2.5)
        {
            CargoWeight = cargo;
        }
    }

    public class Bus : Vehicle
    {
        public int Passengers { get; set; }
        public Bus(double speed, int passengers) : base("Автобус", speed, 3.0)
        {
            Passengers = passengers;
        }
    }
    public class TrafficSimulator
    {
        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public List<Road> Roads { get; set; } = new List<Road>();
        public void AddVehicle(Vehicle v) => Vehicles.Add(v);
        public void AddRoad(Road r) => Roads.Add(r);
        public void SimulateStep()
        {
            Console.WriteLine("----- НОВИЙ КРОК СИМУЛЯЦІЇ -----");

            Random rnd = new Random();

            foreach (var road in Roads)
            {
                road.UpdateTraffic(rnd.NextDouble());
            }

            foreach (var v in Vehicles)
            {
                var road = Roads[rnd.Next(Roads.Count)];
                v.Move(road);
            }
        }
    }
    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            var sim = new TrafficSimulator();

            var road1 = new Road("Центральна", 3.5, 12, 3);
            var road2 = new Road("Окружна", 12.0, 14, 4);

            sim.AddRoad(road1);
            sim.AddRoad(road2);

            sim.AddVehicle(new Car(70));
            sim.AddVehicle(new Truck(50, 5.0));
            sim.AddVehicle(new Bus(40, 30));

            for (int i = 0; i < 25; i++)
            {
                sim.SimulateStep();
            }
        }
    }
}
