using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobDocsLibrary
{
    public class StringProcessing
    {
        public static int MatchStrings(string s1, string s2)
        {
            return HelperLibrary.FuzzyMatching.LevenshteinDistance(s1, s2);
        }
    }
}
