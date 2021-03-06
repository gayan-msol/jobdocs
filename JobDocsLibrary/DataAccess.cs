using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.IO;

namespace _JobDocsLibrary
{
    public static class DataAccess
    {
        //public static string connStr = @"Data Source=GAYAN-PROBOOK\SQLEXPRESS;Initial Catalog=RMMS;integrated security=true";
        public static string connStr = @"Data Source=MS-WS-HP01\SQLEXPRESS;Initial Catalog=ELMS;Integrated Security=False;user Id=sa;Password=MsolSQL123$;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public static void insertRecords(string query)
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(connStr))
            {
                conn.Query(query);
            }
        }

        //public static int insertLetters(List<Letter> letters)
        //{
        //    string query = "IF NOT EXISTS (SELECT * FROM Letters WHERE LetterID=@LetterID) " +
        //        "INSERT INTO Letters ([LetterID],[AccNo],[ClientID],[SortOrder],[AddressLine1],[AddressLine2],[AddressLine3],[AddressLine4],[AddressLine5],[Locality],[State],[Postcode],[Country],[LetterCode],[DebtCode],[Title],[FirstName],[LastName],[CompanyName],[Source],[Custom1],[Custom2],[Custom3],[JobNumber],[Returned]) " +
        //        "VALUES (@LetterID,@AccNo,@ClientID,@SortOrder,@AddressLine1,@AddressLine2,@AddressLine3,@AddressLine4,@AddressLine5,@Locality,@State,@Postcode,@Country,@LetterCode,@DebtCode,@Title,@FirstName,@LastName,@CompanyName,@Source,@Custom1,@Custom2,@Custom3,@JobNumber,'False') " +
        //        "ELSE UPDATE Letters SET [AccNo]=@AccNo,[ClientID]=@ClientID,[SortOrder]=@SortOrder,[AddressLine1]=@AddressLine1,[AddressLine2]=@AddressLine2,[AddressLine3]=@AddressLine3,[AddressLine4]=@AddressLine4,[AddressLine5]=@AddressLine5,[Locality]=@Locality," +
        //        "[State]=@State,[Postcode]=@Postcode,[Country]=@Country,[LetterCode]=@LetterCode,[DebtCode]=@DebtCode,[Title]=@Title,[FirstName]=@FirstName,[LastName]=@LastName,[CompanyName]=@CompanyName,[Source]=@Source,[Custom1]=@Custom1,[Custom2]=@Custom2,[Custom3]=@Custom3,[JobNumber]=@JobNumber,[Returned]=@Returned " +
        //        "WHERE [LetterID]=@LetterID";

        //    using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(connStr))
        //    {
        //        return conn.Execute(query, letters);
        //    }
        //    //  return count;
        //}


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

        public static Letter getLettersByID(string letterID, bool pioneerDaily = false)
        {
            string query = "SELECT * FROM Letters WHERE [LetterID] =@letterID";
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connStr))
            {
                var output = connection.Query<Letter>(query, new { letterID }).ToList();
                if (output.Count > 0)
                { return output[0]; }
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
