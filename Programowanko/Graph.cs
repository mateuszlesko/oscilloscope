using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using Programowanko.Services;
using Programowanko.Models;
using LiveCharts.Defaults;

namespace Programowanko
{
    public partial class Graph : Form
    {
        List<Measurement> measurements;
        public Graph(List<Measurement> measurements)
        {
            InitializeComponent();
            this.measurements = measurements;

            string[] values = new string[measurements.Count];
            
            string[] numbers = new string[measurements.Count];

            Calculations calc = new Calculations();

            float[] OY = new float[measurements.Count];

            for (int i = 0; i < measurements.Count; i++)
            {
                Measurement measurement = measurements[i];
                OY[i] = measurement.GetValue();
                //values[i] = measurement.GetValue().ToString();
            }

            OY = calc.SortValues(OY);

            for (int i = 0; i < OY.Length; i++)
            {
                values[i] = OY[i].ToString();
            }
            

            for (int i = 0; i < measurements.Count; i++)
            {
                Measurement measurement = measurements[i];
                numbers[i] = measurement.GetNumber().ToString();
            }

            //bindingSource1.DataSource = new List<Measurement>();
            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Wartość pomiaru"
                
            }
            );
            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Numer pomiaru"
                
            }
            );
            cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Bottom;

            MessageBox.Show($"Min: {OY[0]} , Max: {OY[OY.Length - 1]}");

        }

        private void Graph_Load(object sender, EventArgs e)
        {


            if (Programowanko.Properties.Settings.Default.DarkMode == 1)
            {
                this.BackColor = Color.DarkSlateBlue;
              
            }

            cartesianChart1.Series.Clear();
            float[] values = new float[measurements.Count];

            for (int i = 0; i < measurements.Count; i++)
                values[i] = measurements[i].GetValue();

            int j = 1;
            foreach (float value in values) {
                cartesianChart1.Series.Add(new LineSeries { Title =$"Pomiar {j} {value}", Values=new ChartValues<float>( values)});
                j++;
            }
            //cartesianChart1.Series.Add(new LineSeries { Title = $"Średnia: {avg}", Values = new ChartValues<float>( new List<float>() { avg} )});

            cartesianChart1.DataClick += CartesianChart1OnDataClick;
        }

        private void cartesianChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void CartesianChart1OnDataClick(object sender, ChartPoint chartPoint)
        {
            MessageBox.Show("Kliknięto na punkt: (" + chartPoint.X + "," + chartPoint.Y + ")");
        }

        private void avgBox_CheckedChanged(object sender, EventArgs e)
        {
            if (avgBox.Checked)
            {

                Calculations calculations = new Calculations();
                float avg = calculations.AVG(measurements);
                float[] avgs = new float[measurements.Count];

                for (int i = 0; i < measurements.Count; i++)
                    avgs[i] = avg;

                cartesianChart1.Series.Add(new LineSeries { Title = $"Średnia: {avg}", Values = new ChartValues<float>(avgs) });

            }
            else
            {
                cartesianChart1.Series.Clear();
                float[] values = new float[measurements.Count];

                for (int i = 0; i < measurements.Count; i++)
                    values[i] = measurements[i].GetValue();

                int j = 1;
                foreach (float value in values)
                {
                    cartesianChart1.Series.Add(new LineSeries { Title = $"Pomiar {j} {value}", Values = new ChartValues<float>(values) });
                    j++;
                }
            }
        }
    }
}
