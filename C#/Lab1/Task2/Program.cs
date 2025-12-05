using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleInterface;

namespace Lab1
{
    class Converter
    {
        //ER - exchange rate relative to uah
        private double usdER;
        private double eurER;
        private double plnER;

        public Converter(double usd, double eur, double pln)
        {
            if (usd <= 0d || eur <= 0d || pln <= 0d)
                throw new ArgumentException();
            this.usdER = usd;
            this.eurER = eur;
            this.plnER = pln;
        }

        public double UAHtoUSD(double uah)
        {
            return Math.Round(uah / usdER, 2);
        }
        public double UAHtoEUR(double uah)
        {
            return Math.Round(uah / eurER, 2);
        }
        public double UAHtoPLN(double uah)
        {
            return Math.Round(uah / plnER, 2);
        }
        public double USDtoUAH(double usd)
        {
            return Math.Round(usd * usdER, 2);
        }
        public double EURtoUAH(double eur)
        {
            return Math.Round(eur * eurER, 2);
        }
        public double PLNtoUAH(double pln)
        {
            return Math.Round(pln * plnER, 2);
        }

        static void Main(string[] args)
        {
            Converter converter = new Converter(41.7, 48.5, 11.6);
            ConsoleInterface<double> consoleInterface = new ConsoleInterface<double>();
            consoleInterface.AddOption("Конвертувати гривні в долари", (value) => {
                double usd = converter.UAHtoUSD(value);
                Console.WriteLine(value + " гривень коштують " + usd + " доларів.");
            });
            consoleInterface.AddOption("Конвертувати гривні в євро", (value) => {
                double eur = converter.UAHtoEUR(value);
                Console.WriteLine(value + " гривень коштують " + eur + " євро.");
            });
            consoleInterface.AddOption("Конвертувати гривні в злоті", (value) => {
                double pln = converter.UAHtoPLN(value);
                Console.WriteLine(value + " гривень коштують " + pln + " злотих.");
            });

            consoleInterface.AddOption("Конвертувати долари в гривні", (value) => {
                double usd = converter.USDtoUAH(value);
                Console.WriteLine(value + " доларів коштують " + usd + "гривень.");
            });
            consoleInterface.AddOption("Конвертувати євро в гривні", (value) => {
                double eur = converter.EURtoUAH(value);
                Console.WriteLine(value + " євро коштують " + eur + " гривень.");
            });
            consoleInterface.AddOption("Конвертувати злоті в гривні", (value) => {
                double pln = converter.PLNtoUAH(value);
                Console.WriteLine(value + " злотих коштують " + pln + "гривень.");
            });

            consoleInterface.FinalizeOptions();

            uint selection = 0;
            double money = 0;
            do
            {
                consoleInterface.PrintOptions();
                selection = consoleInterface.ReadUint(0, consoleInterface.GetLastIndex());
                Console.Clear();
                consoleInterface.PrintOption(selection);
                Console.WriteLine("Введіть кількість коштів: ");
                money = consoleInterface.ReadDouble();
                consoleInterface.Select(selection, money);
                Console.ReadKey();
                Console.Clear();

            } while (true);

        }
    }
}
