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

            consoleInterface.FinalizeOptions();

            uint selection = 0;
            double money = 0;
            do
            {
                consoleInterface.PrintOptions();
                selection = consoleInterface.ReadUint();
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
