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
            return uah * usdER;
        }
        public double UAHtoEUR(double uah)
        {
            return uah * eurER;
        }
        public double UAHtoPLN(double uah)
        {
            return uah * plnER;
        }

        static void Main(string[] args)
        {
            Converter converter = new Converter(41.7, 48.5, 11.6);
            ConsoleInterface<double> consoleInterface = new ConsoleInterface<double>();
            consoleInterface.AddOption("Конвертувати гривні в долари", (value) => converter.UAHtoUSD(value));
            consoleInterface.AddOption("Конвертувати гривні в євро", (value) => converter.UAHtoUSD(value));
            consoleInterface.AddOption("Конвертувати гривні в злоті", (value) => converter.UAHtoUSD(value));

            consoleInterface.FinalizeOptions();
            consoleInterface.PrintOptions();

            uint selection = 0;
            double money = 0;
            do
            {
                selection = (uint)Console.Read();
                consoleInterface.PrintOption(selection);
                Console.WriteLine("Введіть кількість коштів: ");
                money = Convert.ToDouble(Console.Read());
                consoleInterface.Select(selection, money);

            } while (true);

        }
    }
}
