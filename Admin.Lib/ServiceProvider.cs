using Admin.Lib.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Admin.Lib
{
    public class ServiceProvider
    {
        List<Provider> providers = new List<Provider>();
        public void AddProvider()
        {
            Provider provider = new Provider();
            Console.WriteLine("Введите название компании: ");
            provider.NameCompany = Console.ReadLine();

            Console.WriteLine("Введите логотип компании: ");
            provider.LogoUrl = Console.ReadLine();

            Console.WriteLine("Введите процент компании: ");
            provider.Percent = Double.Parse(Console.ReadLine());

            Console.WriteLine("Введите префикс компании: ");

            bool exit = true;
            int pre = 0;
            do
            {
                exit = Int32.TryParse(Console.ReadLine(), out pre);

                if (exit)
                    provider.Prefix.Add(pre);

            } while (exit);
        }
    }
}
