using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElmsLibrary;
using JobDocsLibrary;
using Dolphin;


namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Article  article = DataAccess.GetArticleInfo("PreSort", "Small", "Regular");
            //Console.WriteLine($"{article.ArticleType} - {article.ProductGroup}");
            //Console.ReadKey();

         //   DolphinCo dolphin = new Dolphin();
            Job job = Job.GetJob("207989");

          List<  Dolphin.PostAccount> accList = Dolphin.PostAccount.GetAccountDetails(job.DocID);

            Console.ReadKey();
        }
    }
}
