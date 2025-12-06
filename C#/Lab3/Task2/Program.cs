using System;
using System.Collections.Generic;

namespace NetworkSimulation
{
    public interface IConnectable
    {
        void Connect(Computer target);
        void Disconnect(Computer target);
        void SendData(Computer target, string data);
        void ReceiveData(Computer source, string data);
    }
    public abstract class Computer : IConnectable
    {
        public string IP { get; set; }
        public int Power { get; set; }
        public string OS { get; set; }

        protected List<Computer> connections = new List<Computer>();

        public Computer(string ip, int power, string os)
        {
            IP = ip;
            Power = power;
            OS = os;
        }

        public virtual void Connect(Computer target)
        {
            if (!connections.Contains(target))
            {
                connections.Add(target);
                Console.WriteLine($"{IP} під'єднано до {target.IP}");
            }
        }

        public virtual void Disconnect(Computer target)
        {
            if (connections.Contains(target))
            {
                connections.Remove(target);
                Console.WriteLine($"{IP} відключено від {target.IP}");
            }
        }

        public virtual void SendData(Computer target, string data)
        {
            if (connections.Contains(target))
            {
                Console.WriteLine($"{IP} → {target.IP}: передано дані: '{data}'");
                target.ReceiveData(this, data);
            }
            else
            {
                Console.WriteLine($"{IP} не має з'єднання з {target.IP}, дані не передано");
            }
        }

        public virtual void ReceiveData(Computer source, string data)
        {
            Console.WriteLine($"{IP} отримав дані від {source.IP}: '{data}'");
        }
    }
    public class Server : Computer
    {
        public int MaxClients { get; set; }

        public Server(string ip, int power, string os, int maxClients)
            : base(ip, power, os)
        {
            MaxClients = maxClients;
        }
    }

    public class Workstation : Computer
    {
        public string UserName { get; set; }

        public Workstation(string ip, int power, string os, string user)
            : base(ip, power, os)
        {
            UserName = user;
        }
    }
    public class Router : Computer
    {
        public int PortCount { get; set; }

        public Router(string ip, int power, string os, int ports)
            : base(ip, power, os)
        {
            PortCount = ports;
        }

        public override void SendData(Computer target, string data)
        {
            Console.WriteLine($"Маршрутизатор {IP} перенаправляє дані");
            base.SendData(target, data);
        }
    }
    public class Network
    {
        public List<Computer> Devices { get; set; } = new List<Computer>();

        public void AddDevice(Computer comp)
        {
            Devices.Add(comp);
            Console.WriteLine($"Пристрій {comp.IP} додано до мережі");
        }

        public void ConnectDevices(Computer a, Computer b)
        {
            a.Connect(b);
            b.Connect(a);
        }

        public void DisconnectDevices(Computer a, Computer b)
        {
            a.Disconnect(b);
            b.Disconnect(a);
        }

        public void TransferData(Computer a, Computer b, string data)
        {
            a.SendData(b, data);
        }
    }
    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Network net = new Network();

            var server = new Server("192.168.0.1", 900, "Linux", 100);
            var ws = new Workstation("192.168.0.10", 300, "Windows", "User1");
            var router = new Router("192.168.0.254", 200, "RouterOS", 4);

            net.AddDevice(server);
            net.AddDevice(ws);
            net.AddDevice(router);

            net.ConnectDevices(ws, router);
            net.ConnectDevices(router, server);

            net.TransferData(ws, server, "Hello server!");
            net.TransferData(server, ws, "Response received.");

            net.DisconnectDevices(ws, router);
        }
    }
}