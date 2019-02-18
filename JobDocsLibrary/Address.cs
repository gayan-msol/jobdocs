using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobDocsLibrary
{
    public class Address
    {
        public string Title { get; set; }
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        public string NameSuffix { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }
        public string AddLine1 { get; set; }
        public string AddLine2 { get; set; }
        public string AddLine3 { get; set; }
        public string AddLine4 { get; set; }
        public string AddLine5 { get; set; }
        public string Locality { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }

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
            dtFieldList.Add("Title");
            dtFieldList.Add("First Name");
            dtFieldList.Add("Last Name");
            dtFieldList.Add("Name Suffix");
            dtFieldList.Add("Position");
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
    }
}
