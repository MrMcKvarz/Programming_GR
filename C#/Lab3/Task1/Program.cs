using ConsoleInterface;
using System;
using System.Collections.Generic;

namespace EcosystemSimulation
{
    public interface IReproducible
    {
        LivingOrganism Reproduce();
    }

    public interface IPredator
    {
        void Hunt(LivingOrganism target);
    }

    public abstract class LivingOrganism
    {
        public int Energy { get; set; }
        public int Age { get; set; }
        public double Size { get; set; }
        public bool IsAlive => Energy > 0;

        public LivingOrganism(int energy, int age, double size)
        {
            Energy = energy;
            Age = age;
            Size = size;
        }

        public virtual void Live()
        {
            Age++;
            Energy -= 1;
        }
    }

    public class Animal : LivingOrganism, IReproducible, IPredator
    {
        public bool IsCarnivore { get; private set; }

        public Animal(int energy, int age, double size, bool iscarnivore)
            : base(energy, age, size)
        {
            IsCarnivore = iscarnivore;
        }

        public void Hunt(LivingOrganism target)
        {
            bool consume = false;
            if (target.IsAlive && IsAlive)
            {
                consume = IsCarnivore && target is Animal;
                if(!consume)
                {
                    consume = !IsCarnivore && target is Plant;
                }
            }
            if(consume)
            {            
                Energy += target.Energy;
                target.Energy = 0;
                Size++;
            }
        }

        public LivingOrganism Reproduce()
        {
            return new Animal(10, 0, Size * 0.5, IsCarnivore);
        }
    }

    public class Plant : LivingOrganism, IReproducible
    {
        public int PhotosynthesisRate { get; private set; }

        public Plant(int energy, int age, double size, int rate)
            : base(energy, age, size)
        {
            PhotosynthesisRate = rate;
        }

        public override void Live()
        {
            base.Live();
            Energy += PhotosynthesisRate;
            Size += 0.2;
        }

        public LivingOrganism Reproduce()
        {
            return new Plant(5, 0, this.Size * 0.3, this.PhotosynthesisRate);
        }
    }

    public class Microorganism : LivingOrganism, IReproducible, IPredator
    {
        public double MutationChance { get; private set; }

        public Microorganism(int energy, int age, double size, double mutationChance)
            : base(energy, age, size)
        {
            MutationChance = mutationChance;
        }

        public override void Live()
        {
            base.Live();
            if (new Random().NextDouble() < MutationChance)
            {
                Size *= 1.05;
            }
        }

        public LivingOrganism Reproduce()
        {
            return new Microorganism(energy: 5, age: 0, this.Size * 0.3, this.MutationChance);
        }

        public void Hunt(LivingOrganism target)
        {
            if (target.IsAlive && IsAlive)
            {
                if (target is Microorganism)
                {
                    Energy += target.Energy;
                    target.Energy = 0;
                    Size++;
                }
            }
        }
    }

    public class Ecosystem
    {
        public List<LivingOrganism> Organisms { get; private set; }
        private Random rand = new Random();

        public Ecosystem()
        {
            Organisms = new List<LivingOrganism>();
        }

        public void AddOrganism(LivingOrganism org)
        {
            Organisms.Add(org);
        }

        public void Setup()
        {
            int animals = rand.Next(90);
            int plants = rand.Next(100);
            int microbs = rand.Next(200);

            for (int i = 0; i < animals; i++)
            {
                bool isCarnivore = rand.Next(2) == 0;
                AddOrganism(new Animal(rand.Next(5, 20), 0, rand.NextDouble() * 5 + 1, isCarnivore));
            }
            for (int i = 0; i < plants; i++)
            {
                AddOrganism(new Plant(rand.Next(3, 15), 0, rand.NextDouble() * 3 + 1, rand.Next(1, 5)));
            }
            for (int i = 0; i < microbs; i++)
            {
                AddOrganism(new Microorganism(rand.Next(1, 10), 0, rand.NextDouble() * 1 + 0.5, rand.NextDouble() * 0.3));
            }
        }
     
        public void Reset()
        {
            Organisms.Clear();
        }

        public void SimulateStep()
        {
            var newborns = new List<LivingOrganism>();

            foreach (var org in Organisms)
            {
                if (!org.IsAlive) continue;
                org.Live();

                if (org is IReproducible repro && rand.NextDouble() < 0.1)
                {
                    newborns.Add(repro.Reproduce());
                }

                if (org is IPredator predator)
                {
                    var prey = Organisms[rand.Next(Organisms.Count)];
                    if (prey != org && prey.IsAlive)
                    {
                        predator.Hunt(prey);
                    }
                }
            }

            Organisms.AddRange(newborns);

            Organisms.RemoveAll(o => !o.IsAlive);
        }

        public void PrintStatus()
        {
            Console.WriteLine($"Total Organisms: {Organisms.Count}");
            int animals = 0, plants = 0, microbs = 0;
            foreach (var org in Organisms)
            {
                if (org is Animal) animals++;
                else if (org is Plant) plants++;
                else if (org is Microorganism) microbs++;
            }
            Console.WriteLine($"Animals: {animals}, Plants: {plants}, Microorganisms: {microbs}");
        }
        public void PrintWinners()
        {
            Console.WriteLine("Top 5 Organisms:");
            var topOrganisms = new List<LivingOrganism>(Organisms);
            topOrganisms.Sort((a, b) => b.Size.CompareTo(a.Size));
            Console.WriteLine("Largest:");
            for (int i = 0; i < Math.Min(5, topOrganisms.Count); i++)
            {
                var org = topOrganisms[i];
                Console.WriteLine($"{i + 1}. Type: {org.GetType().Name}, Size: {String.Format("{0:0.00}", org.Size)}, Energy: {org.Energy}, Age: {org.Age}");
            }

            Console.WriteLine("Energetic:");
            topOrganisms.Sort((a, b) => b.Energy.CompareTo(a.Energy));
            for (int i = 0; i < Math.Min(5, topOrganisms.Count); i++)
            {
                var org = topOrganisms[i];
                Console.WriteLine($"{i + 1}. Type: {org.GetType().Name}, Size: {String.Format("{0:0.00}", org.Size)}, Energy: {org.Energy}, Age: {org.Age}");
            }

            Console.WriteLine("Oldest:");
            topOrganisms.Sort((a, b) => b.Age.CompareTo(a.Age));
            for (int i = 0; i < Math.Min(5, topOrganisms.Count); i++)
            {
                var org = topOrganisms[i];
                Console.WriteLine($"{i + 1}. Type: {org.GetType().Name}, Size: {String.Format("{0:0.00}", org.Size)}, Energy: {org.Energy}, Age: {org.Age}");
            }

        }
    }

    public class Program
    {
        public static void Main()
        {
            ConsoleInterface<uint> consoleInterface = new ConsoleInterface<uint>(); 
            Ecosystem eco = new Ecosystem();

            consoleInterface.AddOption("Почати симуляцію екосистеми", (uint _) => 
            {
                eco.Setup();
                Console.WriteLine("Введіть кількість кроків симуляції:");
                uint steps = consoleInterface.ReadUint(1);
                for (int i = 0; i < steps; i++)
                {
                    eco.SimulateStep();
                }
                eco.PrintStatus();
                eco.PrintWinners();
                eco.Reset();
            });
            consoleInterface.FinalizeOptions();

            do
            {                 
                consoleInterface.PrintOptions();
                uint choice = consoleInterface.ReadUint(1, consoleInterface.GetLastIndex());
                consoleInterface.Select(choice, 0);
                Console.ReadKey();
                Console.Clear();
            } while (true);

        }
    }
}
