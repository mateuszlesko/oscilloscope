using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programowanko
{
    public partial class Panel : UserControl
    {
        System.Windows.Forms.Form form;

        public Panel(System.Windows.Forms.Form form)
        {
            this.form = form;
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            form.Close();
        }

        private void minButton_Click(object sender, EventArgs e)
        {
            form.WindowState = FormWindowState.Minimized;
        }

    }
}
