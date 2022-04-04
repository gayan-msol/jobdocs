using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JobDocs
{
    public partial class FormCount : Form
    {
        public decimal Count { get; set; }

        public FormCount()
        {
            InitializeComponent();
        }

        private void btnColseCount_Click(object sender, EventArgs e)
        {
            this.Count = numericUpDownCount.Value;
            this.DialogResult = DialogResult.OK;
        }
    }
}
