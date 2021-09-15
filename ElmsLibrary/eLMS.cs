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

        public static void Lodge(List<Lodgement> lodgements, ElmsUser elmsUser)
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

                new SelectElement(edgeDriver.FindElement(By.Name("accountCode"))).SelectByText(lodgements[0].AccNo);

                IWebElement jn = edgeDriver.FindElement(By.Name("jobNumber"));
                jn.SendKeys(lodgements[0].JobNo);

                IWebElement jobName = edgeDriver.FindElement(By.Name("jobName"));
                jobName.SendKeys(lodgements[0].JobName);

                IWebElement UID = edgeDriver.FindElement(By.Name("uniqueId"));
                UID.SendKeys(lodgements[0].JobName);

                IWebElement IRD = edgeDriver.FindElement(By.Name("invoiceRefDetails"));
                IRD.SendKeys(lodgements[0].JobNo);

                new SelectElement(edgeDriver.FindElement(By.Name("lodgementPointCode"))).SelectByText("Revenue Collection Group - PMC");


                IWebElement btnContinue = edgeDriver.FindElement(By.Id("submitButton"));
                btnContinue.Click();

                //  Article article = DataAccess.GetArticleInfo(lodgement.SortType, lodgement.Size, lodgement.ServiceType);

                new SelectElement(edgeDriver.FindElement(By.Id("productGroups"))).SelectByText(lodgements[0].ProductGroup);


                new SelectElement(edgeDriver.FindElement(By.Id("articleTypes"))).SelectByText(lodgements[0].ArticleType);



                if (lodgements[0].SortType=="PreSort" || lodgements[0].SortType == "Charity Mail")
                {
                    new SelectElement(edgeDriver.FindElement(By.Name("CT001D"))).SelectByText("Promotional");
                }

                foreach (var sort in lodgements[0].SortList)
                {
                    if (sort.Value != "0")
                    {

                        if (sort.Key.Substring(0, 2) == "WT" && lodgements[0].SortType != "INT Full Rate")
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


                IWebElement btnAdd = edgeDriver.FindElement(By.ClassName("inputSubmit"));
                btnAdd.Click();

            if(lodgements.Count > 1)
            {
                new SelectElement(edgeDriver.FindElement(By.Id("productGroups"))).SelectByText(lodgements[1].ProductGroup);


                new SelectElement(edgeDriver.FindElement(By.Id("articleTypes"))).SelectByText(lodgements[1].ArticleType);



                //if (lodgements[0].SortType == "PreSort")
                //{
                //    new SelectElement(edgeDriver.FindElement(By.Name("CT001D"))).SelectByText("Promotional");
                //}

                foreach (var sort in lodgements[1].SortList)
                {
                    if (sort.Value != "0")
                    {

                        //if (sort.Key.Substring(0, 2) == "WT" && lodge)
                        //{
                        //    new SelectElement(edgeDriver.FindElement(By.Name(sort.Key))).SelectByText(sort.Value.ToString());
                        //}
                        //else
                        {
                            IWebElement webElement = edgeDriver.FindElement(By.Name(sort.Key));
                            webElement.SendKeys(sort.Value.ToString());
                        }
                    }

                }


                IWebElement btnAdd2 = edgeDriver.FindElement(By.ClassName("inputSubmit"));
                btnAdd2.Click();

            }







        }

    }
}
