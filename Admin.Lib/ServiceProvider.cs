﻿using Admin.Lib.Module;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace Admin.Lib
{
    public class ServiceProvider
    {
        public ServiceProvider() : this("") { }

        public ServiceProvider(string path)
        {
            if (string.IsNullOrEmpty(path))
                this.path = Path.Combine(@"\\dc\Студенты\ПКО\SEB-171.2\C#", "Operators.xml");
            else
                this.path = path;
        }

        List<Provider> providers = new List<Provider>();
        List<int> providersPrefixs = new List<int>();

        private string path { get; set; }

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
                addProviderToXml(provider);
            }
        }

        public void editProvider()
        {
            // 1. Search provider
            Console.WriteLine("Введите название провайдера: ");
            string s = Console.ReadLine();

            XmlNode xn = SearchProviderByName(s);
            if (xn != null)
            {
                Console.WriteLine(xn.SelectSingleNode("NameCompany").InnerText);
            }
            else
            {
                Console.WriteLine("Провайдер не найден!");
            }
        }

        public void deleteProvider()
        {

        }

        public XmlNode SearchProviderByName(string name)
        {
            XmlDocument xd = getDocument();
            XmlElement root = xd.DocumentElement;

            foreach (XmlElement item in root)
            {
                foreach (XmlNode i in item.ChildNodes)
                {
                    if (i.Name == "NameCompany" && i.InnerText == name)
                        return i;
                }
            }
            return null;
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
            XmlDocument doc = getDocument();
            XmlElement elem = doc.CreateElement("Provider");

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

            XmlElement root = doc.DocumentElement;
            root.AppendChild(elem);
            doc.Save(path);
        }

        private XmlDocument getDocument()
        {
            XmlDocument xd = new XmlDocument();
            //\\dc\Студенты\ПКО\SEB-171.2\C#

            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                xd.Load(path);
            }
            else
            {
                //1
                //FileStream fs = fi.Create();
                //fs.Close();

                //2
                XmlElement xl = xd.CreateElement("Providers");
                xd.AppendChild(xl);
                xd.Save(path);
            }
            return xd;
        }
    }
}