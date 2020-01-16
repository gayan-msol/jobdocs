using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElmsLibrary;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Article  article = DataAccess.GetArticleInfo("PreSort", "Small", "Regular");
            Console.WriteLine($"{article.ArticleType} - {article.ProductGroup}");
            Console.ReadKey();
        }
    }
}
