using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;

namespace JobDocs
{
    public class PrintSpecSheet_
    {
        public string JobNo { get; set; }
        public string JobName { get; set; }
        public string JobDirectory { get; set; }
        public string FileName { get; set; }
        public string PrintMachine { get; set; }
        public string PrintSize { get; set; }
        public string FinishedSize { get; set; }
        public string Stock { get; set; }
        public string Layout { get; set; }
        public List<string> StreamList { get; set; }
        public string Notes { get; set; }




    }
}
