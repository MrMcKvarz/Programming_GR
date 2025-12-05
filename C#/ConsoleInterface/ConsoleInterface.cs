using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleInterface
{
    public class ConsoleInterface<T>
    {
        struct Data
        {
            public string description;
            public Action<T> func;
        }

        private Dictionary<uint, Data> Options;

        public ConsoleInterface()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Options = new Dictionary<uint, Data>();
        }

        public void AddOption(string description, Action<T> callback)
        {
            Data data;
            data.description = description;
            data.func = callback;
            uint index = GetLastIndex() + 1;
            Options.Add(index, data);
        }

        public void PrintOptions()
        {
            foreach (var option in Options)
            {
                Console.Write(option.Key);
                Console.Write(". ");
                Console.Write(option.Value.description);
                Console.WriteLine(".");
            }
        }

        public void PrintOption(uint index)
        {
            Data data;
            bool result = Options.TryGetValue(index, out data);
            if (result)
            {
                Console.WriteLine(data.description);
            }
        }

        public bool Select(uint index, T payload)
        {
            Data data;
            bool result = Options.TryGetValue(index, out data);
            if (result)
            {
                data.func.Invoke(payload);
            }

            return result;
        }

        public void FinalizeOptions()
        {
            uint index = GetLastIndex() + 1;
            Data data;
            data.description = "Вихід";
            data.func = (index) => Environment.Exit(0);
            Options.Add(index, data);
        }

        public uint GetLastIndex()
        {
            if (Options.Keys.Count == 0) return 0;
            return Options.Keys.Max();
        }
        public uint ReadUint(uint min = 0, uint max = Int32.MaxValue)
        {
            uint result = 0;
            bool exit = true;
            while (true)
            {
                try
                {
                    exit = true;
                    string? console_line = Console.ReadLine();
                    result = UInt32.Parse(console_line);
                }
                catch (Exception fe)
                {
                    Console.WriteLine("Введіть ціле число");
                    exit = false;
                }
                if(result < min || result > max)
                {
                    Console.WriteLine("Введіть ціле число між " + min + " і " + max);
                    continue;
                }
                if (exit) break;
            }
            return result;
        }
        public double ReadDouble()
        {
            double result = 0;
            bool exit = true;
            while (true)
            {
                try
                {
                    exit = true;
                    string? console_line = Console.ReadLine();
                    result = Double.Parse(console_line);
                }
                catch (Exception fe)
                {
                    Console.WriteLine("Введіть число");
                    exit = false;
                }
                if (exit) break;
            }
            return result;
        }

    }
}
