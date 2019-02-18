using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobDocsLibrary
{
    public class Job : IJob
    {
        public string JobNo { get; set; }
        public string JobName { get; set; }
        public string Customer { get; set; }

    }
}
