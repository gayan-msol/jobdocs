using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.IO;

namespace ElmsLibrary
{
    public static class DataAccess
    {
        //public static string connStr = @"Data Source=GAYAN-PROBOOK\SQLEXPRESS;Initial Catalog=RMMS;integrated security=true";
        public static string connStr = @"Data Source=MSOL-P1\SQLEXPRESS;Initial Catalog=eLMS;Integrated Security=False;user Id=sa;Password=P@ssw0rd123SQL;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public static void insertRecords(string query)
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(connStr))
            {
                conn.Query(query);
            }
        }



        public static int saveElmsLogin(ElmsUser user)
        {
            string query = "IF NOT EXISTS (SELECT * FROM eLMSLogins WHERE User=@WindowsUser) INSERT INTO eLMSLogins ([eLMSUserName],[Password],[WindowsUser])  VALUES (@eLMSUserName,@Password,@WindowsUser) " +
                "ELSE UPDATE eLMSLogins SET eLMSUserName=@eLMSUserName, Password=@Password WHERE WindowsUser=@WindowsUser  ";
            int count = 0;

            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(connStr))
            {
                count = conn.Execute(query, user);
            }
            return count;
        }


        public static string GetIntZone(string country)
        {
            string query = $@"SELECT Zone from IntZones WHERE Country='{country}'";

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connStr))
            {
                var output = connection.Query<string>(query, new { country }).ToList();

                if (output.Count > 0)
                {
                    return output[0];
                }
                else
                {
                    return null;
                }
            }
        }

        public static ElmsUser GetElmsUser(string windowsUser)
        {
            string query = "SELECT * FROM eLMSLogins WHERE [WindowsUser] =@windowsUser";
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connStr))
            {
                var output = connection.Query<ElmsUser>(query, new { windowsUser}).ToList();

                if(output.Count >0 )
                {
                    return output[0];
                }
                else
                {
                    return null;
                }                            
            }

        }

        public static List<SortCategory> GetSortCategories(string sortType)
        {
            string query = "SELECT * FROM SortCategories WHERE [CategoryCode] =@sortType Order By CatOrder";
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connStr))
            {
                var output = connection.Query<SortCategory>(query, new { sortType }).ToList();

                return output;

            }

        }


        public static Article GetArticleInfo(string SortType, string size, string serviceType)
        {
            string query = "SELECT ProductGroup, ArticleType FROM ArticleSelections INNER JOIN GroupSelections ON ArticleSelections.ParentID=GroupSelections.ID WHERE [Name] =@SortType AND  [Size] =@size AND  [ServiceType] =@serviceType";
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connStr))
            {
                var output = connection.Query<Article>(query, new { SortType, size, serviceType }).ToList()[0];

                return output;

            }

        }

        public static List<string> GetSizes(string SortType)
        {
            string query = "SELECT DISTINCT(Size) FROM ArticleSelections WHERE [Name] =@SortType";
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connStr))
            {
                var output = connection.Query<string>(query, new { SortType }).ToList();

                return output;
            }
        }

        public static List<string> GetWeightCategories(string SortType, string ServiceType, string Size)
        {
            string query = "SELECT WeightCategory FROM LodgementOptions WHERE SortType=@SortType AND ServiceType=@ServiceType AND Size=@Size";
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connStr))
            {
                var output = connection.Query<string>(query, new { SortType, ServiceType, Size }).ToList();

                return output;
            }
        }

        /*
        public static void UpdateGeneratedStatus(Letter letter)
        {
            string queryLetters = "UPDATE Letters SET RTSGenerated= 'TRUE',RTSDate = @RTSDate WHERE LetterID  = @LetterID ";
            string queryScans = "UPDATE Scans SET RTS_Generated= 'TRUE',RTS_Date = @RTSDate WHERE LetterID  = @LetterID ";

            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(connStr))
            {
                conn.Execute(queryLetters, letter);
            }

            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(connStr))
            {
                conn.Execute(queryScans, letter);
            }
        }

        public static List<PioneerRTS> getPioneerRTS(DateTime fromDate, DateTime toDate, bool filterByJobNo, bool filterByDate, bool newOnly, string jobNo)
        {
            string query = "";

            List<DateTime> dates = new List<DateTime>();

            for (DateTime date = fromDate; date <= toDate; date = date.AddDays(1))
            {
                dates.Add(date);
            }


            if (filterByJobNo && filterByDate)
            {
                query = "SELECT L.*,S.letterid  FROM Letters L RIGHT JOIN Scans S on L.LetterID = S.LetterID WHERE S.LetterID in (SELECT LetterID FROM Scans WHERE  CONVERT(DATE,SessionID)  in @dates AND SUBSTRING([LetterID],0,7) = @jobNo AND RTS_Generated <> @newOnly)";
            }
            else if (filterByJobNo && !filterByDate)
            {
                query = "SELECT L.*,S.letterid  FROM Letters L RIGHT JOIN Scans S on L.LetterID = S.LetterID WHERE S.LetterID in (SELECT LetterID FROM Scans WHERE SUBSTRING([LetterID],0,7) = @jobNo AND RTS_Generated <> @newOnly)";
            }
            else if (!filterByJobNo && filterByDate)
            {
                query = "SELECT L.*,S.letterid  FROM Letters L RIGHT JOIN Scans S on L.LetterID = S.LetterID WHERE S.LetterID in (SELECT LetterID FROM Scans WHERE CONVERT(DATE,SessionID)  in @dates AND RTS_Generated <> @newOnly)";
            }
            else
            {
                query = "SELECT L.*,S.letterid  FROM Letters L RIGHT JOIN Scans S on L.LetterID = S.LetterID WHERE S.LetterID in (SELECT LetterID FROM Scans WHERE RTS_Generated <> @newOnly)";
            }

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connStr))
            {
                return connection.Query<PioneerRTS>(query, new { dates, newOnly, jobNo }).ToList();
            }
        }

        public static List<Letter> getRTS(DateTime fromDate, DateTime toDate, bool filterByJobNo, bool filterByDate, bool newOnly, string jobNo)
        {
            string query = "";

            List<DateTime> dates = new List<DateTime>();

            for (DateTime date = fromDate; date <= toDate; date = date.AddDays(1))
            {
                dates.Add(date);
            }


            if (filterByJobNo && filterByDate)
            {
                query = "SELECT * FROM Letters WHERE LetterID in ( SELECT LetterID FROM Scans WHERE  CONVERT(DATE,SessionID)  in @dates AND SUBSTRING([LetterID],0,7) = @jobNo AND RTS_Generated <> @newOnly)";
            }
            else if (filterByJobNo && !filterByDate)
            {
                query = "SELECT * FROM Letters WHERE LetterID in  (SELECT LetterID FROM Scans WHERE   SUBSTRING([LetterID],0,7) = @jobNo AND RTS_Generated <> @newOnly)";
            }
            else if (!filterByJobNo && filterByDate)
            {
                query = "SELECT * FROM Letters WHERE LetterID in (SELECT LetterID  FROM Scans WHERE   CONVERT(DATE,SessionID)  in @dates AND RTS_Generated <> @newOnly)";
            }
            else
            {
                query = "SELECT * FROM Letters WHERE LetterID in (SELECT LetterID FROM Scans WHERE RTS_Generated <> @newOnly)";
            }

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connStr))
            {
                return connection.Query<Letter>(query, new { dates, newOnly, jobNo }).ToList();
            }
        }

        public static List<ScanSession> getSessions(DateTime fromDate, DateTime toDate, bool filterByJobNo, bool filterByDate, bool newOnly, string jobNo)
        {
            string query = "";

            List<DateTime> dates = new List<DateTime>();

            for (DateTime date = fromDate; date <= toDate; date = date.AddDays(1))
            {
                dates.Add(date);
            }


            if (filterByJobNo && filterByDate)
            {
                query = "SELECT * FROM Scans WHERE  CONVERT(DATE,SessionID)  in @dates AND SUBSTRING([LetterID],0,7) = @jobNo AND RTS_Generated <> @newOnly";
            }
            else if (filterByJobNo && !filterByDate)
            {
                query = "SELECT * FROM Scans WHERE   SUBSTRING([LetterID],0,7) = @jobNo AND RTS_Generated <> @newOnly";
            }
            else if (!filterByJobNo && filterByDate)
            {
                //   query = $"SELECT *  FROM Scans WHERE   CONVERT(DATE,SessionID)  in @dates AND RTS_Generated <> '{newOnly}'";
                query = $"SELECT *  FROM Scans WHERE   CONVERT(DATE,SessionID)  in @dates AND RTS_Generated <> @newOnly";
            }
            else
            {
                query = $"SELECT * FROM Scans WHERE RTS_Generated <> @newOnly";
            }

            List<ScanSession> sessions = new List<ScanSession>();
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connStr))
            {
                var output = connection.Query<ScanSession>(query, new { dates, newOnly, jobNo }).ToList();
                if (output.Count > 0)
                { return output; }
                else
                { return null; }
            }
        }

 

        public static void archiveReturnedLetters(string LetterID)
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(connStr))
            {
                conn.Execute("db_owner.spArchiveProcessedRecords", new { LetterID }, commandType: CommandType.StoredProcedure);
            }

        }
        */


    }
}
