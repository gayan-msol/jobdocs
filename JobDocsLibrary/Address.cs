using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobDocsLibrary
{
    public class Address
    {
        //public string Title { get; set; }
        //public string FisrtName { get; set; }
        //public string LastName { get; set; }
        //public string NameSuffix { get; set; }
        //public string Position { get; set; }
        //public string Company { get; set; }
        //public string AddLine1 { get; set; }
        //public string AddLine2 { get; set; }
        //public string AddLine3 { get; set; }
        //public string AddLine4 { get; set; }
        //public string AddLine5 { get; set; }
        //public string Locality { get; set; }
        //public string State { get; set; }
        //public string Postcode { get; set; }
        //public string Country { get; set; }

        public string Title = "Title";
        public string FisrtName = "First Name";
        public string LastName = "Last Name";
        public string NameSuffix = "Name Suffix";
        public string Position = "Position";
        public string Company = "Company Name";
        public string AddLine1 = "Address Line 1";
        public string AddLine2 = "Address Line 2";
        public string AddLine3 = "Address Line 3";
        public string AddLine4 = "Address Line 4";
        public string AddLine5 = "Address Line 5";
        public string Locality = "Locality";
        public string State = "State";
        public string Postcode = "Postcode";
        public string Country = "Country";

        public static List<string> CreateAddressBlock(Address address)
        {
            var addBlockList = new List<string>();
            addBlockList.Add($"{address.Title} {address.FisrtName} {address.LastName} {address.NameSuffix}".Replace("  ", " ").Trim());
            addBlockList.Add(address.Position);
            addBlockList.Add(address.Company);
            addBlockList.Add(address.AddLine1);
            addBlockList.Add(address.AddLine2);
            addBlockList.Add(address.AddLine3);
            addBlockList.Add(address.AddLine4);
            addBlockList.Add(address.AddLine5);
            addBlockList.Add($"{address.Locality} {address.State} {address.Postcode}".Replace("  ", " ").Trim());
            addBlockList.Add(address.Country);

            foreach (var item in addBlockList)
            {
                if(string.IsNullOrWhiteSpace(item))
                {
                    addBlockList.Remove(item);
                }
            }

            return addBlockList;
        }

      //  public static List<string> dtFieldList;

        public static List<string> getDtFieldList()
        {
            List<string> dtFieldList = new List<string>();
            dtFieldList.Add("<Select or Enter DT Field>");
            dtFieldList.Add("Stream");
            dtFieldList.Add("Sort Order");
            dtFieldList.Add("Title");
            dtFieldList.Add("First Name");
            dtFieldList.Add("Last Name");
            dtFieldList.Add("Name Suffix");
            dtFieldList.Add("Position");
            dtFieldList.Add("Company Name");
            dtFieldList.Add("Address Line 1");
            dtFieldList.Add("Address Line 2");
            dtFieldList.Add("Address Line 3");
            dtFieldList.Add("Address Line 4");
            dtFieldList.Add("Address Line 5");
            dtFieldList.Add("Locality");
            dtFieldList.Add("State");
            dtFieldList.Add("Postcode");
            dtFieldList.Add("Country");

            return dtFieldList;
        }

        public static List<string> GetAddressBlock(List<string> colList)
        {
            List<string> addressColList = new List<string>();
            StringBuilder nameLine = new StringBuilder();
            StringBuilder locStPcLine = new StringBuilder();

            nameLine.Append(colList.Contains("Title") ? "<Title> " : "");
            nameLine.Append(colList.Contains("First Name") ? "<First Name> " : "");
            nameLine.Append(colList.Contains("Last Name") ? "<Last Name> " : "");
            nameLine.Append(colList.Contains("Name Suffix") ? "<Name Suffix>" : "");
            if (nameLine.ToString() != "")
            {
                addressColList.Add(nameLine.ToString());
            }
            if (colList.Contains("Position"))
            {
                addressColList.Add("<Position>");
            }
            if (colList.Contains("Company Name"))
            {
                addressColList.Add("<Company Name>");
            }
            if (colList.Contains("Address Line 1"))
            {
                addressColList.Add("<Address Line 1>");
            }
            if (colList.Contains("Address Line 2"))
            {
                addressColList.Add("<Address Line 2>");
            }
            if (colList.Contains("Address Line 3"))
            {
                addressColList.Add("<Address Line 3>");
            }
            if (colList.Contains("Address Line 4"))
            {
                addressColList.Add("<Address Line 4>");
            }
            if (colList.Contains("Address Line 5"))
            {
                addressColList.Add("<Address Line 5>");
            }

            locStPcLine.Append(colList.Contains("Locality") ? "<Locality> " : "");
            locStPcLine.Append(colList.Contains("State") ? "<State> " : "");
            locStPcLine.Append(colList.Contains("Postcode") ? "<Postcode>" : "");
            if (locStPcLine.ToString() != "")
            {
                addressColList.Add(locStPcLine.ToString());
            }
            if (colList.Contains("Country"))
            {
                addressColList.Add("<Country>");
            }
            return addressColList;
        }

        public static List<string> GetAddressBlock_2(List<string> colList)
        {
            List<string> addressColList = new List<string>();
            StringBuilder nameLine = new StringBuilder();
            StringBuilder locStPcLine = new StringBuilder();

            nameLine.Append(colList.Contains("Title") ? "<Title> " : "");
            nameLine.Append(colList.Contains("First Name") ? "<First Name> " : "");
            nameLine.Append(colList.Contains("Last Name") ? "<Last Name> " : "");
            nameLine.Append(colList.Contains("Name Suffix") ? "<Name Suffix>" : "");
            if (nameLine.ToString() != "")
            {
                addressColList.Add(nameLine.ToString());
            }
            if (colList.Contains("Position"))
            {
                addressColList.Add("<Position>");
            }
            if (colList.Contains("Company Name"))
            {
                addressColList.Add("<Company Name>");
            }
            if (colList.Contains("Address Line 1"))
            {
                addressColList.Add("<Address Line 1>");
            }
            if (colList.Contains("Address Line 2"))
            {
                addressColList.Add("<Address Line 2>");
            }
            if (colList.Contains("Address Line 3"))
            {
                addressColList.Add("<Address Line 3>");
            }
            if (colList.Contains("Address Line 4"))
            {
                addressColList.Add("<Address Line 4>");
            }
            if (colList.Contains("Address Line 5"))
            {
                addressColList.Add("<Address Line 5>");
            }

            locStPcLine.Append(colList.Contains("Locality") ? "<Locality> " : "");
            locStPcLine.Append(colList.Contains("State") ? "<State> " : "");
            locStPcLine.Append(colList.Contains("Postcode") ? "<Postcode>" : "");
            if (locStPcLine.ToString() != "")
            {
                addressColList.Add(locStPcLine.ToString());
            }
            if (colList.Contains("Country"))
            {
                addressColList.Add("<Country>");
            }
            return addressColList;
        }
    }
}
