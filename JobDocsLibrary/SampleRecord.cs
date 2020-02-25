using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobDocsLibrary
{
    public class Record
    {
        public int Index { get; set; }
        public int EmptyColCount { get; set; }
        public List<int> EmptyColIndexes { get; set; }
        public int VariableColIndex { get; set; }
    }
}
