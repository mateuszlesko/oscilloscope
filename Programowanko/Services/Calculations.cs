using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programowanko.Models;

namespace Programowanko.Services
{
    class Calculations
    {
        //TODO w konstruktorze stworzyc wartosci aby od razu przy tworzeniu obiektu wyliczalo funkcje
        public float AVG(List<Measurement> measurements)
        {
            if (measurements == null)
                return 0.0f;

            return CalculateAVG(measurements);
        }
        static float CalculateAVG(List<Measurement> measurments)
        {
            float result=0.0f;
            foreach (Measurement measurement in measurments)
                result += measurement.GetValue();
            return result / measurments.Count();

        }
        public double Variance(List<Measurement> measurements)
        {
            return CalculateVariance(measurements);
        }
        static double CalculateVariance(List<Measurement> measurements)
        {
            if (measurements == null)
                return 0;
            
            double result = 0.0f;
            float avg = CalculateAVG(measurements);
            foreach(Measurement measurement in measurements)
            {
                result += Math.Pow(Convert.ToDouble(measurement.GetValue()-avg), 2);
            }
            return result / measurements.Count();
        }

        public float[] SortValues(float[] values)
        {
            bool sorted = true;
            for (int i = 0; i < values.Length && sorted; i++)
            {
                sorted = false;
                for(int j=1; j < values.Length; j++)
                {
                    if (values[j - 1] > values[j])
                    {
                        float tmp = values[j];
                        values[j] = values[j - 1];
                        values[j - 1] = tmp;
                        sorted = true;
                    }
                }
            }

            return values;
        }
    }
}
