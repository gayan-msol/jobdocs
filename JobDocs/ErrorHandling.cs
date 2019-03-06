using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JobDocs
{
    public class ErrorHandling
    {
        public static void ShowMessage(Exception ex, string message="Error :")
        {
            string messageString = $"{message} {ex?.Message}";

            MessageBox.Show(messageString, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
