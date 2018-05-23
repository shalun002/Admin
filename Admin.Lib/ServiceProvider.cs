using Admin.Lib.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace Admin.Lib
{
    public class ServiceProvider
    {
        List<Provider> providers = new List<Provider>();
        List<int> providersPrefixs = new List<int>();

        public void AddProvider()
        {
            Provider provider = new Provider();
            Console.WriteLine("Введите название компании: ");
            provider.NameCompany = Console.ReadLine();

            Console.WriteLine("Введите логотип компании: ");
            provider.LogoUrl = Console.ReadLine();

            Console.WriteLine("Введите процент компании: ");
            provider.Percent = Double.Parse(Console.ReadLine());

            Console.WriteLine("Введите префикс компании: " + "Для выхода нажмите два раза Enter: ");

            bool exit = true;
            int pre = 0;
            do
            {
                exit = Int32.TryParse(Console.ReadLine(), out pre);

                if (exit && isExistsPrefix(pre))
                    provider.Prefix.Add(pre);

            } while (exit);

            if (isExistsProvider(provider))
            {
                providers.Add(provider);
                providersPrefixs.AddRange(provider.Prefix);
            }
        }

        private bool isExistsProvider(Provider pro)
        {
            if (providers.Where(w => w.NameCompany == pro.NameCompany).Count() > 0)
            {
                Console.WriteLine("Такой провайдер уже существует");
                return false;
            }
            return true;
        }

        private bool isExistsPrefix(int pref)
        {
            if (providersPrefixs.Where(item => item == pref).Count() > 0)
            {
                Console.WriteLine("Такой префикс уже существует");
                return false;
            }
            return true;
        }

        private void addProviderToXml(Provider pro)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement elem = doc.CreateElement("Провайдер");

            XmlElement LogoUrl = doc.CreateElement("LogoUrl");
            LogoUrl.InnerText = pro.LogoUrl;

            XmlElement NameCompany = doc.CreateElement("NameCompany");
            NameCompany.InnerText = pro.NameCompany;

            XmlElement Percent = doc.CreateElement("Percent");
            Percent.InnerText = pro.Percent.ToString();

            XmlElement Prefixs = doc.CreateElement("Prefixs");
            foreach(int item in pro.Prefix)
            {
                XmlElement Prefix = doc.CreateElement("Prefix");
                Prefix.InnerText = item.ToString();
                Prefixs.AppendChild(Prefix);
            }
            elem.AppendChild(LogoUrl);
            elem.AppendChild(NameCompany);
            elem.AppendChild(Percent);
            elem.AppendChild(Prefixs);
            doc.AppendChild(elem);
            doc.Save("Providers.xml");
        }
    }
}