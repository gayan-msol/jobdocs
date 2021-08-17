using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
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
        static string EdgeDriverPath = @"S:\DOT NET PROGRAMMES\SOURCE CODE\Edge Driver";

        public static void Lodge(Lodgement lodgement, ElmsUser elmsUser)
        {
            EdgeDriver edgeDriver = new EdgeDriver(EdgeDriverPath);
            
                edgeDriver.Url = URL;
                edgeDriver.Navigate();


                IWebElement user = edgeDriver.FindElement(By.Name("username"));
                user.SendKeys(elmsUser.eLMSUserName);

                IWebElement pwd = edgeDriver.FindElement(By.Name("password"));
                pwd.SendKeys(elmsUser.Password);

                IWebElement btnLogin = edgeDriver.FindElement(By.ClassName("inputSubmit"));
                btnLogin.Click();


                WebDriverWait wait = new WebDriverWait(edgeDriver, TimeSpan.FromSeconds(30));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(
                By.LinkText("New Mailing Statement")));

                IWebElement link = edgeDriver.FindElement(By.LinkText("New Mailing Statement"));
                link.Click();

                new SelectElement(edgeDriver.FindElement(By.Name("accountCode"))).SelectByText(lodgement.AccNo);

                IWebElement jn = edgeDriver.FindElement(By.Name("jobNumber"));
                jn.SendKeys(lodgement.JobNo);

                IWebElement jobName = edgeDriver.FindElement(By.Name("jobName"));
                jobName.SendKeys(lodgement.JobName);

                IWebElement UID = edgeDriver.FindElement(By.Name("uniqueId"));
                UID.SendKeys(lodgement.JobName);

                IWebElement IRD = edgeDriver.FindElement(By.Name("invoiceRefDetails"));
                IRD.SendKeys(lodgement.JobNo);

                new SelectElement(edgeDriver.FindElement(By.Name("lodgementPointCode"))).SelectByText("Revenue Collection Group - PMC");


                IWebElement btnContinue = edgeDriver.FindElement(By.Id("submitButton"));
                btnContinue.Click();

                //  Article article = DataAccess.GetArticleInfo(lodgement.SortType, lodgement.Size, lodgement.ServiceType);

                new SelectElement(edgeDriver.FindElement(By.Id("productGroups"))).SelectByText(lodgement.ProductGroup);


                new SelectElement(edgeDriver.FindElement(By.Id("articleTypes"))).SelectByText(lodgement.ArticleType);



                if (lodgement.SortType=="PreSort")
                {
                    new SelectElement(edgeDriver.FindElement(By.Name("CT001D"))).SelectByText("Promotional");
                }

                foreach (var sort in lodgement.SortList)
                {
                    if (sort.Value != "0")
                    {

                        if (sort.Key.Substring(0, 2) == "WT")
                        {
                            new SelectElement(edgeDriver.FindElement(By.Name(sort.Key))).SelectByText(sort.Value.ToString());
                        }
                        else
                        {
                            IWebElement webElement = edgeDriver.FindElement(By.Name(sort.Key));
                            webElement.SendKeys(sort.Value.ToString());
                        }                                  
                    }

                }

                // select weight cat

                IWebElement btnAdd = edgeDriver.FindElement(By.ClassName("inputSubmit"));
                btnAdd.Click();
            
        


        


        }

    }
}
