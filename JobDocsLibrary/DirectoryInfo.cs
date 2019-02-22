using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JobDocsLibrary
{
    public class DirectoryHelper
    {
        private static string miscBranch = "AAA MISCELLANEOUS";
        public static string databaseBranch = @"S:\DATABASES";
        public static string artworkBranch = @"S:\ART WORK TEMPLATES";


        public static List<string> GetOutPutFiles(string jobDirectory)
        {
            if(jobDirectory != null)
            {
                return Directory.GetFiles(jobDirectory).Select(x => Path.GetFileName(x)).ToList();
            }
            else
            {
                return null;
            }
        }

        public static string getJobDir(string jobNo, string customer, string branch)
        {
            string path = $@"{branch}\{customer}";
            string miscPath = $@"{branch}\{miscBranch}\{customer}";
            string t1 = findJobDir(path, jobNo);
            string t2= findJobDir(miscPath, jobNo);
            string jobDirectory = findJobDir(path, jobNo) ?? findJobDir(miscPath, jobNo);
            return jobDirectory;
        }


        private static string findJobDir(string path, string jobNo)
        {
            if (Directory.Exists(path))
            {
                string[] jobDirectoryList = Directory.GetDirectories(path);
                return jobDirectoryList.Where(x => x.Contains(jobNo)).FirstOrDefault();
            }
            return null;
        }
    }
}
