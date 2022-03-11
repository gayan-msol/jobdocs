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
        static List<string> validExtensions = new List<string> { ".txt",".pdf",".ps",".zip",".csv" };

        public static List<string> GetOutPutFiles(string jobDirectory)
        {
          
            if(!string.IsNullOrWhiteSpace(jobDirectory))
            {
                return Directory.GetFiles(jobDirectory).Select(x => Path.GetFileName(x)).Where( file => validExtensions.Contains(  Path.GetExtension(file).ToLower()) ).ToList();
            }
            else
            {
                return null;
            }
        }

        public static string GetManifestFile(string jobDirectory, string jobNo)
        {
            List<string> files = new List<string>();
            if(!string.IsNullOrWhiteSpace(jobDirectory) && Directory.Exists(jobDirectory))
            {
                string jobDocDir = $@"{jobDirectory}\{jobNo} Job Docs";
                files = Directory.GetFiles(jobDirectory).Select(x => Path.GetFileName(x)).Where( file => file.Contains(  "Manifest Summary") ).ToList();

                if(files.Count > 0)
                {
                    return $@"{jobDirectory}\{files[0]}";
                }
                else if (Directory.Exists(jobDocDir))
                {
                    files = Directory.GetFiles(jobDocDir).Select(x => Path.GetFileName(x)).Where(file => file.Contains("Manifest Summary")).ToList();
                    if (files.Count > 0)
                    {
                        return $@"{jobDocDir}\{files[0]}";
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
              //  
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

        public static string getDelimiter(string fileName)
        {
            string header = File.ReadLines(fileName).First();
            if(header.Split(',').Length > header.Split('\t').Length)
            {
                return ",";
            }
            else
            {
                return "\t";
            }
        }
    }
}
