using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Programowanko.Services;
using Programowanko.Models;

namespace Programowanko
{
    public partial class Form1 : Form
    {
        List<Measurement> content;
        DataTable table;
        Calculations calculations;
        float AVG;
        double Variance;
        float Period;
        static TaskCompletionSource<float> task = new TaskCompletionSource<float>();
        public Form1()
        {
            
            InitializeComponent();
            table = new DataTable();
            calculations = new Calculations();
        }

        private async Task Calculating()
        {
            float avg = calculations.AVG(content);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            if(Programowanko.Properties.Settings.Default.DarkMode == 1)
            {
                this.BackColor = Color.DarkSlateBlue;
                panel1.BackColor = Color.DarkSlateBlue;
                loadDataButton.BackColor = Color.MidnightBlue;
                loadDataButton.ForeColor = Color.White;
                graphButton.BackColor = Color.MidnightBlue;
                graphButton.ForeColor = Color.White;
                saveButton.BackColor = Color.MidnightBlue;
                saveButton.ForeColor = Color.White;
            }

            table.Columns.Add("Numer pomiaru", typeof(int));
            table.Columns.Add("Wartość pomaru", typeof(float));
            dataGridView.DataSource = table;
            dataGridView.Enabled = false;
            avgBox.Text = calculations.AVG(content).ToString();
            graphButton.Enabled = false;
            varianceBox.Text = calculations.Variance(content).ToString();
            saveButton.Enabled = false;
        }

        private void loadDataButton_Click(object sender, EventArgs e)
        {
            dataGridView.Enabled = true;

            if (dataGridView.Rows.Count > 1)
            {
                dataGridView.DataSource = null;
                dataGridView.Refresh();
            }

            FileReader reader = new FileReader();
            string [] results= reader.GetContent();

            if (results.Length > 1)
            {
                MessageBox.Show("Załadowano dane");
            }
            else
            {
                MessageBox.Show("Nie zaladowano danych. Spróbuj ponownie");
                dataGridView.DataSource = null;
                dataGridView.Refresh();
            }
            ContentParser contentParser = new ContentParser(results);
            content = contentParser.Parse();

            foreach (Measurement measurement in content)
            {
                table.Rows.Add(measurement.GetNumber(), measurement.GetValue());
            }
            dataGridView.DataSource = table;

            AVG = calculations.AVG(content);
            Variance = calculations.Variance(content);
            Period = ((1.0f / content.Count) * 10);

            avgBox.Text = AVG.ToString();
            varianceBox.Text = Variance.ToString();
            periodBox.Text = $"{Period.ToString()}";

            if (content.Count > 1)
            {
                graphButton.Enabled = true;
                saveButton.Enabled = true;
            }
        }

        private void graphButton_Click(object sender, EventArgs e)
        {
            Graph graph = new Graph(content);
            graph.Visible = true;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string[] lines = new string[] { $"Wartość średnia: {AVG} \n .", $"Wariancja: {Variance} \n .", $"Okres: {Period} \n" };
            Console.WriteLine(Programowanko.Properties.Settings.Default.Path);
            FileAdder fileAdder = new FileAdder(@""+Programowanko.Properties.Settings.Default.Path, "wyniki.txt", lines);
            if (fileAdder.FileCreate() == true)
                MessageBox.Show(@"Wyniki zapisano");
            else
                MessageBox.Show("Nie utworzono pliku");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
            this.Hide();
        }
    }
}
