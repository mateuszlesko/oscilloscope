using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programowanko
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (Programowanko.Properties.Settings.Default.ClickedDM == 1)
            {
                checkBox1.CheckState = CheckState.Checked;
            }
            if (checkBox1.Checked == true && (Programowanko.Properties.Settings.Default.ClickedDM == 0) )
            {
                
                Programowanko.Properties.Settings.Default.DarkMode = 1;
                
                this.BackColor = Color.DarkSlateBlue;
                panel1.BackColor = Color.DarkSlateBlue;
                this.ForeColor = Color.White;
               
            }
            Programowanko.Properties.Settings.Default.Path = textBox1.Text.Equals(null) ? Programowanko.Properties.Settings.Default.Path : textBox1.Text;

            Form1 form1 = new Form1();
            form1.Show();
            this.Close();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            Programowanko.Properties.Settings.Default.Path = @"C:\wyniki";
            Programowanko.Properties.Settings.Default.DarkMode = 0;
            Programowanko.Properties.Settings.Default.ClickedDM = 0;
            this.Refresh();
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void Settings_Load(object sender, EventArgs e)
        {

            textBox1.Text = Programowanko.Properties.Settings.Default.Path;

            if (Programowanko.Properties.Settings.Default.DarkMode == 1)
            {
                this.BackColor = Color.DarkSlateBlue;
                panel1.BackColor = Color.DarkSlateBlue;
                resetButton.BackColor = Color.MidnightBlue;
                resetButton.ForeColor = Color.White;
                saveButton.BackColor = Color.MidnightBlue;
                saveButton.ForeColor = Color.White;
            }
        }
    }
}
