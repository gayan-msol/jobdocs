using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmsLibrary
{
    public class eLMS
    {
        static string URL = "https://elms.auspost.com.au/elms/loginCheck.do";
        static string ChromeDriverPath = @"S:\SCRIPTS\DotNetProgrammes\Source Code\ChromeDriver";

        public static void Lodge(Lodgement lodgement, ElmsUser elmsUser)
        {

            ChromeDriver chromeDriver = new ChromeDriver(ChromeDriverPath);


            chromeDriver.Url = URL;
            chromeDriver.Navigate();


            IWebElement user = chromeDriver.FindElement(By.Name("username"));
            user.SendKeys(elmsUser.UserName);

            IWebElement pwd = chromeDriver.FindElement(By.Name("password"));
            pwd.SendKeys(elmsUser.Password);

            IWebElement btnLogin = chromeDriver.FindElement(By.ClassName("inputSubmit"));
            btnLogin.Click();


            WebDriverWait wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(
            By.LinkText("New Mailing Statement")));

            IWebElement link = chromeDriver.FindElement(By.LinkText("New Mailing Statement"));
            link.Click();

            new SelectElement(chromeDriver.FindElement(By.Name("accountCode"))).SelectByText(lodgement.AccNo);

            IWebElement jn = chromeDriver.FindElement(By.Name("jobNumber"));
            jn.SendKeys(lodgement.JobNo);

            IWebElement jobName = chromeDriver.FindElement(By.Name("jobName"));
            jobName.SendKeys(lodgement.JobName);

            IWebElement UID = chromeDriver.FindElement(By.Name("uniqueId"));
            UID.SendKeys(lodgement.JobName);

            IWebElement IRD = chromeDriver.FindElement(By.Name("invoiceRefDetails"));
            IRD.SendKeys(lodgement.JobNo);

            new SelectElement(chromeDriver.FindElement(By.Name("lodgementPointCode"))).SelectByText("Revenue Collection Group - PMC");


            IWebElement btnContinue = chromeDriver.FindElement(By.Id("submitButton"));
            btnContinue.Click();

            Article article = DataAccess.GetArticleInfo(lodgement.SortType, lodgement.Size, lodgement.ServiceType);

            new SelectElement(chromeDriver.FindElement(By.Id("productGroups"))).SelectByText(article.ProductGroup);


            new SelectElement(chromeDriver.FindElement(By.Id("articleTypes"))).SelectByText(article.ArticleType);

            new SelectElement(chromeDriver.FindElement(By.Name("CT001D"))).SelectByText("Promotional");

            foreach (var sort in lodgement.SortList)
            {
                if (sort.Value != "0")
                {
                    IWebElement webElement = chromeDriver.FindElement(By.Name(sort.Key));
                    webElement.SendKeys(sort.Value.ToString());
                }

            }








            chromeDriver.Close();
            chromeDriver.Dispose();

        }

    }
}
