using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NorthernNerds.Scraper.Boliga
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
        }

        public static void Test()
        {
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"http://www.boliga.dk/maegler/indeks/Sj%C3%A6lland/fritidshus");
            FindAllePostnumre(driver);
            driver.Quit();
        }

        public static void FindAllePostnumre(IWebDriver driver)
        {
            var dic = new Dictionary<string, string>();
            var elemenets = driver.FindElements(By.CssSelector("tbody:nth-child(1)>tr>td>a")).ToList();


            var postNrRegex = new Regex(@"\d{4}");
            var navnRegex = new Regex(@"\D*\s\D*");
            foreach (var webElement in elemenets)
            {
                var postNr = postNrRegex.Match(webElement.Text).Value;
                var navn = navnRegex.Match(webElement.Text).Value;

                if (!dic.ContainsKey(postNr))
                {
                    dic[postNr] = navn.Trim();
                }
            }
        }
    }
}
